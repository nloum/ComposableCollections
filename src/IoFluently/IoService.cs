using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LiveLinq;
using LiveLinq.Core;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using ReactiveProcesses;
using SimpleMonads;
using UtilityDisposables;
using static SimpleMonads.Utility;

namespace IoFluently
{
    public class IoService : IoServiceBase
    {
        public IoService(IReactiveProcessFactory reactiveProcessFactory) : base(reactiveProcessFactory, Environment.NewLine)
        {
            PathObservationMethod = GetDefaultPathObservationMethod();
        }

        public PathObservationMethod GetDefaultPathObservationMethod()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? PathObservationMethod.FileSystemWatcher : PathObservationMethod.FsWatchDefault;
        }
        
        public PathObservationMethod PathObservationMethod { get; set; }
        
        public override ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            if (PathObservationMethod == PathObservationMethod.FileSystemWatcher)
            {
                return ToLiveLinqWithFileSystemWatcher(path, includeFileContentChanges, includeSubFolders, pattern)
                    .ToDictionaryLiveLinq(x => x, x => x.GetPathType()).KeysAsSet();
            }

            return ToLiveLinqWithFsWatch(path, includeFileContentChanges, includeSubFolders, pattern);
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFsWatch(AbsolutePath root, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            // TODO - add support for FSWatch events on Windows and Linux as well. Although I think I already support all the ones on Linux
            // and the FileSystemWatcher class on Windows should be sufficient, it would be nice to have this support for
            // completeness' sake.

            ReactiveProcess proc;

            var recursiveArg = includeSubFolders ? "--recursive" : string.Empty;
            
            if (PathObservationMethod == PathObservationMethod.FsWatchDefault)
            {
                proc = ReactiveProcessFactory.Start("fswatch", $"-0 {recursiveArg} \"{root}\"");
            }
            else if (PathObservationMethod == PathObservationMethod.FsWatchPollMonitor)
            {
                proc = ReactiveProcessFactory.Start("fswatch", $"--monitor=poll_monitor -0 {recursiveArg} \"{root}\"");
            }
            else if (PathObservationMethod == PathObservationMethod.FsWatchFsEventsMonitor)
            {
                proc = ReactiveProcessFactory.Start("fswatch", $"--monitor=fsevents_monitor -0 {recursiveArg} \"{root}\"");
            }
            else if (PathObservationMethod == PathObservationMethod.FsWatchKQueueMonitor)
            {
                proc = ReactiveProcessFactory.Start("fswatch", $"--monitor=kqueue_monitor -0 {recursiveArg} \"{root}\"");
            }
            else
            {
                throw new ArgumentException($"Unknown path observation method: {PathObservationMethod}");
            }

            var initialState = includeSubFolders ? root.Descendants()
                .ToImmutableDictionary(x => x, x => x.GetPathType())
                : root.Children().ToImmutableDictionary(x => x, x => x.GetPathType());

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
                        var item = TryParseAbsolutePath(itemString).Value;
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
            
            if (!string.IsNullOrWhiteSpace(pattern))
            {
                var regex = FileNamePatternToRegex(pattern);
                result = result.Where(path => regex.IsMatch(path.Name));
            }
            
            return result;
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFileSystemWatcher(AbsolutePath root, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            var observable = Observable.Create<ISetChange<AbsolutePath>>(observer =>
            {
                var watcher = new FileSystemWatcher
                {
                    Path = root.ToString(),
                    IncludeSubdirectories = includeSubFolders,
                    Filter = pattern,
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

                var initialSetChange = LiveLinq.Utility.SetChange(CollectionChangeType.Add, includeSubFolders ? root.Descendants()
                    : root.Children().AsEnumerable());

                observer.OnNext(initialSetChange);

                watcher.EnableRaisingEvents = true;

                return new DisposableCollector(renamesSubscription, changesSubscription, deletionsSubscription, creationsSubscription);
            });


            return observable.ToLiveLinq();
        }

        public override IMaybe<StreamWriter> TryOpenWriter(AbsolutePath absolutePath)
        {
            try
            {
                return Something(AsFileInfo(absolutePath).CreateText());
            }
            catch (Exception ex)
            {
                return Nothing<StreamWriter>(() => throw ex);
            }
        }

        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.TryParent().IfHasValue(parent => parent.Create(PathType.Folder));
                return Something<Stream>(AsFileInfo(path).Open(fileMode, fileAccess, fileShare));
            }
            catch (Exception ex)
            {
                return Nothing<Stream>(() => throw ex);
            }
        }

        public override IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes)
        {
            try
            {
                return Something(AsFileInfo(attributes).Attributes);
            }
            catch (Exception ex)
            {
                return Nothing<FileAttributes>(() => throw ex);
            }
        }

        public override IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath attributes)
        {
            try
            {
                return Something<DateTimeOffset>(AsFileInfo(attributes).CreationTime);
            }
            catch (Exception ex)
            {
                return Nothing<DateTimeOffset>(() => throw ex);
            }
        }

        public override IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath attributes)
        {
            try
            {
                return Something<DateTimeOffset>(AsFileInfo(attributes).LastAccessTime);
            }
            catch (Exception ex)
            {
                return Nothing<DateTimeOffset>(() => throw ex);
            }
        }

        public override IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath attributes)
        {
            try
            {
                return Something<DateTimeOffset>(AsFileInfo(attributes).LastWriteTime);
            }
            catch (Exception ex)
            {
                return Nothing<DateTimeOffset>(() => throw ex);
            }
        }

        public override IMaybe<string> TryFullName(AbsolutePath attributes)
        {
            try
            {
                return Something(AsFileInfo(attributes).FullName);
            }
            catch (Exception ex)
            {
                return Nothing<string>(() => throw ex);
            }
        }
        
        public override IMaybe<long> TryLength(AbsolutePath path)
        {
            try
            {
                return Something<long>(AsFileInfo(path).Length);
            }
            catch (Exception ex)
            {
                return Nothing<long>(() => throw ex);
            }
        }

        public override IMaybe<bool> TryIsReadOnly(AbsolutePath path)
        {
            try
            {
                return Something<bool>(AsFileInfo(path).IsReadOnly);
            }
            catch (Exception ex)
            {
                return Nothing<bool>(() => throw ex);
            }
        }

        private FileInfo AsFileInfo(AbsolutePath path)
        {
            return new FileInfo(path.ToString());
        }

        private DirectoryInfo AsDirectoryInfo(AbsolutePath path)
        {
            return new DirectoryInfo(path.ToString());
        }

        public override AbsolutePath DeleteFile(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.None)
                return path;
            try
            {
                AsFileInfo(path).Delete();
            }
            catch (IOException)
            {
                AsFileInfo(path).Delete();
            }
            catch (UnauthorizedAccessException)
            {
                AsFileInfo(path).Delete();
            }

            return path;
        }

        public override AbsolutePath Decrypt(AbsolutePath path)
        {
            AsFileInfo(path).Decrypt();
            return path;
        }

        public override AbsolutePath Encrypt(AbsolutePath path)
        {
            AsFileInfo(path).Encrypt();
            return path;
        }

        public override IReadOnlyObservableSet<AbsolutePath> Storage => _storage;

        private readonly ObservableSet<AbsolutePath> _storage = new ObservableSet<AbsolutePath>();

        public override IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, bool includeFolders = true, bool includeFiles = true)
        {
            if (!path.IsFolder()) return ImmutableArray<AbsolutePath>.Empty;

            var fullName = AsDirectoryInfo(path).FullName;

            if (includeFiles && includeFolders)
                return Directory.GetFileSystemEntries(fullName).Select(x => ParseAbsolutePath(x));

            if (includeFiles) return Directory.GetFiles(fullName).Select(x => ParseAbsolutePath(x));

            if (includeFolders)
                return Directory.GetDirectories(fullName).Select(x => ParseAbsolutePath(x));

            return ImmutableArray<AbsolutePath>.Empty;
        }

        public override void UpdateStorage()
        {
            var currentStorage = Directory.GetLogicalDrives();
            foreach (var drive in currentStorage)
            {
                var drivePath = TryParseAbsolutePath(drive).Value;
                if (!_storage.Contains(drivePath))
                    _storage.Add(drivePath);
            }

            var drivesThatWereRemoved = new List<AbsolutePath>();

            foreach (var drive in _storage)
                if (!currentStorage.Contains(drive + "\\"))
                    drivesThatWereRemoved.Add(drive);

            foreach (var driveThatWasRemoved in drivesThatWereRemoved) _storage.Remove(driveThatWasRemoved);
        }

        public override IObservable<AbsolutePath> Renamings(AbsolutePath path)
        {
            var parent = path.TryParent();
            if (!parent.HasValue) return Observable.Return(path);

            return Observable.Create<AbsolutePath>(
                async (observer, token) =>
                {
                    var currentPath = path;
                    while (!token.IsCancellationRequested)
                    {
                        var watcher = new FileSystemWatcher(currentPath.Parent().ToString())
                        {
                            IncludeSubdirectories = false,
                            Filter = currentPath.Name
                        };

                        var tcs = new TaskCompletionSource<AbsolutePath>();

                        RenamedEventHandler handler = (_, args) =>
                        {
                            tcs.SetResult(new AbsolutePath(path.Flags, path.DirectorySeparator, this, new[]{args.FullPath}));
                        };

                        watcher.EnableRaisingEvents = true;

                        watcher.Renamed += handler;

                        currentPath = await tcs.Task;

                        observer.OnNext(currentPath);

                        watcher.Renamed -= handler;
                        watcher.Dispose();
                    }
                });
        }

        public override PathFlags GetDefaultFlagsForThisEnvironment()
        {
            lock (_lock)
            {
                if (defaultFlagsForThisEnvironment == null)
                {
                    var file = Path.GetTempFileName();
                    var caseSensitive = File.Exists(file.ToLower()) && File.Exists(file.ToUpper());
                    File.Delete(file);
                    if (caseSensitive)
                        defaultFlagsForThisEnvironment = PathFlags.CaseSensitive;
                    else
                        defaultFlagsForThisEnvironment = PathFlags.None;
                }

                return defaultFlagsForThisEnvironment.Value;
            }
        }
        
        public override AbsolutePath CreateFolder(AbsolutePath path)
        {
            try
            {
                if (path.GetPathType() == PathType.Folder)
                    return path;
                path.TryParent().IfHasValue(parent => parent.CreateFolder());
                AsDirectoryInfo(path).Create();
            }
            catch (IOException)
            {
                if (path.GetPathType() != PathType.Folder)
                    throw;
            }

            if (path.GetPathType() != PathType.Folder)
                throw new IOException("Failed to create folder " + path);
            return path;
        }

        public override void WriteAllText(AbsolutePath path, string text)
        {
            File.WriteAllText(path.ToString(), text);
        }

        public override void WriteAllLines(AbsolutePath path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path.ToString(), lines);
        }

        public override void WriteAllBytes(AbsolutePath path, byte[] bytes)
        {
            File.WriteAllBytes(path.ToString(), bytes);
        }

        public override IEnumerable<string> ReadLines(AbsolutePath path)
        {
            return File.ReadLines(path.ToString());
        }

        public override string ReadAllText(AbsolutePath path)
        {
            return File.ReadAllText(path.ToString());
        }

        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.TryParent().IfHasValue(parent => parent.Create(PathType.Folder));
                return Something(AsFileInfo(path).Open(fileMode, fileAccess));
            }
            catch (Exception ex)
            {
                return Nothing<Stream>(() => throw ex);
            }
        }
        
        public override AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false)
        {
            Directory.Delete(path.ToString(), recursive);

            return path;
        }

        public override PathType GetPathType(AbsolutePath path)
        {
            var str = path.ToString();
            if (File.Exists(str))
                return PathType.File;
            if (Directory.Exists(str))
                return PathType.Folder;
            return PathType.None;
        }

        public override IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            if (translation.Destination.Exists())
            {
                if (!overwrite)
                {
                    throw new IOException($"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" without overwriting the destination, but the destination already exists");
                }
                else
                {
                    translation.Destination.Delete();
                }
            }
            if (translation.Source.GetPathType() != PathType.File)
                throw new IOException(string.Format(
                    $"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" but the source path is not a file."));
            if (translation.Destination.GetPathType() != PathType.None)
                throw new IOException(string.Format(
                    $"An attempt was made to move \"{translation.Source}\" to \"{translation.Destination}\" but the destination path exists."));
            if (translation.Destination.IsDescendantOf(translation.Source))
                throw new IOException(string.Format(
                    $"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" but the destination path is a sub-path of the source path."));
            translation.Destination.TryParent().Value.Create(PathType.Folder);
            File.Move(translation.Source.ToString(), translation.Destination.ToString());
            return translation;
        }
    }
}