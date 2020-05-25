using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LiveLinq;
using LiveLinq.Core;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using ReactiveProcesses;

namespace IoFluently
{
    public abstract class AbsolutePathDescendantsOrChildren : IReadOnlyObservableSet<AbsolutePath>
    {
        protected readonly AbsolutePath _path;
        protected readonly string _pattern;

        public AbsolutePathDescendantsOrChildren(AbsolutePath path, string pattern, bool includeSubdirectories, IIoService ioService)
        {
            _path = path;
            _pattern = pattern;
            IncludeSubdirectories = includeSubdirectories;
            IoService = ioService;
        }

        public IIoService IoService { get; }

        protected bool IncludeSubdirectories { get; }

        public ISetChanges<AbsolutePath> ToLiveLinq()
        {
            return ToLiveLinq(true, PathObservationMethod.Default);
        }

        public ISetChanges<AbsolutePath> ToLiveLinq(bool includeFileContentChanges, PathObservationMethod observationMethod = PathObservationMethod.Default)
        {
            if (observationMethod == PathObservationMethod.Default)
            {
                observationMethod = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? PathObservationMethod.FileSystemWatcher : PathObservationMethod.FsWatchDefault;
            }
            
            if (observationMethod == PathObservationMethod.FileSystemWatcher)
            {
                return ToLiveLinqWithFileSystemWatcher(_path, includeFileContentChanges)
                    .ToDictionaryLiveLinq(x => x, x => x.GetPathType()).KeysAsSet();
            }

            return ToLiveLinqWithFsWatch(_path, includeFileContentChanges, observationMethod).KeysAsSet();
        }

        private IDictionaryChangesStrict<AbsolutePath, PathType> ToLiveLinqWithFsWatch(AbsolutePath root, bool includeFileContentChanges, PathObservationMethod observationMethod)
        {
            // TODO - add support for FSWatch events on Windows and Linux as well. Although I think I already support all the ones on Linux
            // and the FileSystemWatcher class on Windows should be sufficient, it would be nice to have this support for
            // completeness' sake.

            ReactiveProcess proc;

            var recursiveArg = IncludeSubdirectories ? "--recursive" : string.Empty;
            
            if (observationMethod == PathObservationMethod.FsWatchDefault)
            {
                proc = IoService.ReactiveProcessFactory.Start("fswatch", $"-0 {recursiveArg} \"{root}\"");
            }
            else if (observationMethod == PathObservationMethod.FsWatchPollMonitor)
            {
                proc = IoService.ReactiveProcessFactory.Start("fswatch", $"--monitor=poll_monitor -0 {recursiveArg} \"{root}\"");
            }
            else if (observationMethod == PathObservationMethod.FsWatchFsEventsMonitor)
            {
                proc = IoService.ReactiveProcessFactory.Start("fswatch", $"--monitor=fsevents_monitor -0 {recursiveArg} \"{root}\"");
            }
            else if (observationMethod == PathObservationMethod.FsWatchKQueueMonitor)
            {
                proc = IoService.ReactiveProcessFactory.Start("fswatch", $"--monitor=kqueue_monitor -0 {recursiveArg} \"{root}\"");
            }
            else
            {
                throw new ArgumentException($"Unknown path observation method: {observationMethod}");
            }

            var initialState = GetChildren(root).ToImmutableDictionary(x => x, x => x.GetPathType());

            var resultObservable = proc.StandardOutput
                .Scan(new {StringBuilder = new StringBuilder(), BuiltString = (string) null},
                    (state, ch) =>
                    {
                        if (ch == 0)
                        {
                            return new
                            {
                                StringBuilder = new StringBuilder(), BuiltString = state.StringBuilder.ToString()
                            };
                        }

                        state.StringBuilder.Append(ch);
                        return new {state.StringBuilder, BuiltString = (string) null};
                    }).Where(state => state.BuiltString != null).Select(state => state.BuiltString)
                .Scan(new {State = initialState, LastEvents = (IDictionaryChangeStrict<AbsolutePath, PathType>[]) null},
                    (state, itemString) =>
                    {
                        var item = IoService.TryParseAbsolutePath(itemString).Value;
                        if (state.State.ContainsKey(item))
                        {
                            if (File.Exists(itemString) || Directory.Exists(itemString))
                            {
                                if (includeFileContentChanges)
                                {
                                    return new
                                    {
                                        state.State,
                                        LastEvents = new []
                                        {
                                            Utility.DictionaryRemove(MoreCollections.Utility.KeyValuePair(item, state.State[item])),
                                            Utility.DictionaryAdd(MoreCollections.Utility.KeyValuePair(item, item.GetPathType()))
                                        }
                                    };
                                }
                                else
                                {
                                    return new
                                    {
                                        state.State,
                                        LastEvents = new IDictionaryChangeStrict<AbsolutePath, PathType>[0]
                                    };
                                }
                            }
                            else
                            {
                                // TODO - fix bug where when a directory is deleted, subdirectories and subfolders are not removed from the state.
                                
                                return new
                                {
                                    State = state.State.Remove(item),
                                    LastEvents = new IDictionaryChangeStrict<AbsolutePath, PathType>[]
                                    {
                                        Utility.DictionaryRemove(MoreCollections.Utility.KeyValuePair(item, state.State[item])),
                                    }
                                };
                            }
                        }
                        else
                        {
                            return new
                            {
                                State = state.State.Add(item, item.GetPathType()),
                                LastEvents = new IDictionaryChangeStrict<AbsolutePath, PathType>[]
                                {
                                    Utility.DictionaryAdd(MoreCollections.Utility.KeyValuePair(item, item.GetPathType())),
                                }
                            };
                        }
                    })
                .SelectMany(state => state.LastEvents);
            resultObservable = Observable.Return(Utility.DictionaryAdd(initialState))
                .Concat(resultObservable);
                
            // TODO - add pattern support for fswatch
            
            return resultObservable.ToLiveLinq();
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFileSystemWatcher(AbsolutePath root, bool includeFileContentChanges)
        {
            var watcher = new FileSystemWatcher(root.ToString())
            {
                IncludeSubdirectories = true,
                Filter = _pattern,
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite
            };

            var creations = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    handler => watcher.Created += handler,
                    handler => watcher.Created -= handler)
                .Select(args =>
                {
                    var path = root.IoService.TryParseAbsolutePath(args.EventArgs.FullPath).Value;
                    return Utility.SetChange(CollectionChangeType.Add, path);
                });

            var deletions = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    handler => watcher.Deleted += handler,
                    handler => watcher.Deleted -= handler)
                .Select(args =>
                {
                    var path = root.IoService.TryParseAbsolutePath(args.EventArgs.FullPath).Value;
                    return Utility.SetChange(CollectionChangeType.Remove, path);
                });

            var changes = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    handler => watcher.Changed += handler,
                    handler => watcher.Changed -= handler)
                .Where(x => x.EventArgs.ChangeType == WatcherChangeTypes.Changed)
                .GroupBy(x => x.EventArgs.FullPath)
                .Select(x => x.Throttle(TimeSpan.FromSeconds(.2)))
                .Merge()
                .SelectMany(args =>
                {
                    var path = root.IoService.TryParseAbsolutePath(args.EventArgs.FullPath).Value;
                    return new[]
                    {
                        Utility.SetChange(CollectionChangeType.Remove, path),
                        Utility.SetChange(CollectionChangeType.Add, path),
                    }.ToObservable();
                });

            var renames = Observable.FromEventPattern<RenamedEventHandler, RenamedEventArgs>(
                    handler => watcher.Renamed += handler,
                    handler => watcher.Renamed -= handler)
                .SelectMany(args =>
                {
                    var oldPath = root.IoService.TryParseAbsolutePath(args.EventArgs.OldFullPath).Value;
                    var path = root.IoService.TryParseAbsolutePath(args.EventArgs.FullPath).Value;
                    return new[]
                    {
                        Utility.SetChange(CollectionChangeType.Remove, oldPath),
                        Utility.SetChange(CollectionChangeType.Add, path),
                    }.ToObservable();
                });

            var initialState = GetChildren(root)
                .Select(path => Utility.SetChange(CollectionChangeType.Add, path))
                .ToObservable();

            var unified = initialState.Concat(creations.Merge(deletions).Merge(renames).Merge(changes));

            return unified.ToLiveLinq();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<AbsolutePath> GetEnumerator();

        protected IEnumerable<AbsolutePath> GetChildren(AbsolutePath parent)
        {
            return Directory.GetFileSystemEntries(_path.ToString()).Select(x => IoService.ParseAbsolutePath(x, _path.Flags));
        }
    }
}