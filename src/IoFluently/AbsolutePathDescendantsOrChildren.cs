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
using UtilityDisposables;

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

        public int Count => this.AsEnumerable().Count();
        
        public void Dispose()
        {
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

            return ToLiveLinqWithFsWatch(_path, includeFileContentChanges, observationMethod);
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFsWatch(AbsolutePath root, bool includeFileContentChanges, PathObservationMethod observationMethod)
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

            var initialState = this
                .ToImmutableDictionary(x => x, x => x.GetPathType());

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
                                            LiveLinq.Utility.DictionaryRemove(MoreCollections.Utility.KeyValuePair(item, state.State[item])),
                                            LiveLinq.Utility.DictionaryAdd(MoreCollections.Utility.KeyValuePair(item, item.GetPathType()))
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
                                        LiveLinq.Utility.DictionaryRemove(MoreCollections.Utility.KeyValuePair(item, state.State[item])),
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
                                    LiveLinq.Utility.DictionaryAdd(MoreCollections.Utility.KeyValuePair(item, item.GetPathType())),
                                }
                            };
                        }
                    })
                .SelectMany(state => state.LastEvents);
            
            resultObservable = Observable.Return(LiveLinq.Utility.DictionaryAdd(initialState))
                .Concat(resultObservable);
            
            var result = resultObservable.ToLiveLinq().KeysAsSet();
            
            if (!string.IsNullOrWhiteSpace(_pattern))
            {
                var regex = IoService.FileNamePatternToRegex(_pattern);
                result = result.Where(path => regex.IsMatch(path.Name));
            }
            
            return result;
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFileSystemWatcher(AbsolutePath root, bool includeFileContentChanges)
        {
            var observable = Observable.Create<ISetChange<AbsolutePath>>(observer =>
            {
                var watcher = new FileSystemWatcher
                {
                    Path = root.ToString(),
                    IncludeSubdirectories = IncludeSubdirectories,
                    Filter = _pattern,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.FileName
                };

                var creationsSubscription = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                        handler =>
                            watcher.Created += handler,
                        handler =>
                            watcher.Created -= handler)
                    .Select(x => x.EventArgs)
                    .Subscribe(args =>
                    {
                        var path = root.IoService.TryParseAbsolutePath(args.FullPath).Value;
                        observer.OnNext(LiveLinq.Utility.SetChange(CollectionChangeType.Add, path));
                    });

                var deletionsSubscription = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                        handler =>
                            watcher.Deleted += handler,
                        handler =>
                            watcher.Deleted -= handler)
                    .Select(x => x.EventArgs)
                    .Subscribe(args =>
                    {
                        var path = root.IoService.TryParseAbsolutePath(args.FullPath).Value;
                        observer.OnNext(LiveLinq.Utility.SetChange(CollectionChangeType.Remove, path));
                    });

                var changesSubscription = !includeFileContentChanges ? EmptyDisposable.Default : Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                        handler =>
                            watcher.Changed += handler,
                        handler =>
                            watcher.Changed -= handler)
                    .Select(x => x.EventArgs)
                    .Where(x => x.ChangeType == WatcherChangeTypes.Changed)
                    .GroupBy(x => x.FullPath)
                    .Select(x => x.Throttle(TimeSpan.FromSeconds(.2)))
                    .Merge()
                    .Subscribe(args =>
                    {
                        var path = root.IoService.TryParseAbsolutePath(args.FullPath).Value;
                        observer.OnNext(LiveLinq.Utility.SetChange(CollectionChangeType.Remove, path));
                        observer.OnNext(LiveLinq.Utility.SetChange(CollectionChangeType.Add, path));
                    });

                var renamesSubscription = Observable.FromEventPattern<RenamedEventHandler, RenamedEventArgs>(
                        handler =>
                            watcher.Renamed += handler,
                        handler =>
                            watcher.Renamed -= handler)
                    .Select(x => x.EventArgs)
                    .Subscribe(args =>
                    {
                        var oldPath = root.IoService.TryParseAbsolutePath(args.OldFullPath).Value;
                        var path = root.IoService.TryParseAbsolutePath(args.FullPath).Value;
                        observer.OnNext(LiveLinq.Utility.SetChange(CollectionChangeType.Remove, oldPath));
                        observer.OnNext(LiveLinq.Utility.SetChange(CollectionChangeType.Add, path));
                    });

                var initialSetChange = LiveLinq.Utility.SetChange(CollectionChangeType.Add, this.Select(path => path));

                observer.OnNext(initialSetChange);

                watcher.EnableRaisingEvents = true;

                return new DisposableCollector(renamesSubscription, changesSubscription, deletionsSubscription, creationsSubscription);
            });


            return observable.ToLiveLinq();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<AbsolutePath> GetEnumerator();

        protected IEnumerable<AbsolutePath> GetChildren(AbsolutePath parent)
        {
            var parentString = parent.ToString();
            if (Directory.Exists(parentString))
            {
                return Directory.GetFileSystemEntries(parentString).Select(x => IoService.ParseAbsolutePath(x, _path.Flags));
            }
            
            return Enumerable.Empty<AbsolutePath>();
        }
    }
}
