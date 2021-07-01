using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ComposableCollections.Dictionary;
using LiveLinq;
using LiveLinq.Core;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using SimpleMonads;
using UnitsNet;
using UtilityDisposables;
using static SimpleMonads.Utility;

namespace IoFluently
{
    public class IoService : IoServiceBase
    {
        /// <summary>
        /// The default IoService. Use this for environments where dependency injection isn't necessary.
        /// E.g., Nuke.build scripts or bootstrapping a larger program.
        /// </summary>
        public static IIoService Default { get; set; }
    
        private bool? _isCaseSensitiveByDefault = null;

        public override bool CanEmptyDirectoriesExist => true;
        
        public override IQueryable<AbsolutePath> Query()
        {
            return new Queryable<AbsolutePath>(new QueryContext());
        }

        private AbsolutePath _defaultRelativePathBase;
        
        public override AbsolutePath DefaultRelativePathBase => _defaultRelativePathBase ?? TryParseAbsolutePath(Environment.CurrentDirectory).Value;
        public TimeSpan DeleteOrCreateSpinPeriod { get; set; } = TimeSpan.FromMilliseconds(100);
        public TimeSpan DeleteOrCreateTimeout { get; set; } = TimeSpan.FromSeconds(5);

        public override void SetDefaultRelativePathBase(AbsolutePath defaultRelativePathBase)
        {
            _defaultRelativePathBase = defaultRelativePathBase;
        }

        public override void UnsetDefaultRelativePathBase()
        {
            _defaultRelativePathBase = null;
        }
        
        public IoService() : this(false)
        {
            UpdateRoots();
        }
        
        public IoService(bool enableOpenFilesTracking) : base(new OpenFilesTrackingService(enableOpenFilesTracking), ShouldBeCaseSensitiveByDefault(), GetDefaultDirectorySeparatorForThisEnvironment(), Environment.NewLine)
        {
            PathObservationMethod = GetDefaultPathObservationMethod();
            UpdateRoots();
        }

        public PathObservationMethod GetDefaultPathObservationMethod()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? PathObservationMethod.FileSystemWatcher : PathObservationMethod.FsWatchDefault;
        }
        
        public PathObservationMethod PathObservationMethod { get; set; }

        /// <inheritdoc />
        public override ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            if (PathObservationMethod == PathObservationMethod.FileSystemWatcher)
            {
                return ToLiveLinqWithFileSystemWatcher(path, includeFileContentChanges, includeSubFolders, pattern)
                    .ToDictionaryLiveLinq(x => x, x => Type(x)).KeysAsSet();
            }

            return ToLiveLinqWithFsWatch(path, includeFileContentChanges, includeSubFolders, pattern);
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFsWatch(AbsolutePath root, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            // TODO - add support for FSWatch events on Windows and Linux as well. Although I think I already support all the ones on Linux
            // and the FileSystemWatcher class on Windows should be sufficient, it would be nice to have this support for
            // completeness' sake.

            var args = "";
            var recursiveArg = includeSubFolders ? "--recursive" : string.Empty;
            
            if (PathObservationMethod == PathObservationMethod.FsWatchDefault)
            {
                args = $"-0 {recursiveArg} \"{root}\"";
            }
            else if (PathObservationMethod == PathObservationMethod.FsWatchPollMonitor)
            {
                args = $"--monitor=poll_monitor -0 {recursiveArg} \"{root}\"";
            }
            else if (PathObservationMethod == PathObservationMethod.FsWatchFsEventsMonitor)
            {
                args = $"--monitor=fsevents_monitor -0 {recursiveArg} \"{root}\"";
            }
            else if (PathObservationMethod == PathObservationMethod.FsWatchKQueueMonitor)
            {
                args = $"--monitor=kqueue_monitor -0 {recursiveArg} \"{root}\"";
            }
            else
            {
                throw new ArgumentException($"Unknown path observation method: {PathObservationMethod}");
            }
            
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("fswatch", args)
                {
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                },
            };

            process.Start();

            var initialState = includeSubFolders ? root.Descendants()
                .ToImmutableDictionary(x => x, x => Type(x))
                : root.Children().ToImmutableDictionary(x => x, x => Type(x));

            var resultObservable = process.StandardOutput
                .Observe(new []{ (char)0 })
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
                                            LiveLinq.Utility.DictionaryRemove(new KeyValue<AbsolutePath, PathType>(item, state.State[item])),
                                            LiveLinq.Utility.DictionaryAdd(new KeyValue<AbsolutePath, PathType>(item, Type(item)))
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
                                        LiveLinq.Utility.DictionaryRemove(new KeyValue<AbsolutePath, PathType>(item, state.State[item])),
                                    }
                                };
                            }
                        }
                        else
                        {
                            return new
                            {
                                State = state.State.Add(item, Type(item)),
                                LastEvents = new IDictionaryChangeStrict<AbsolutePath, PathType>[]
                                {
                                    LiveLinq.Utility.DictionaryAdd(new KeyValue<AbsolutePath, PathType>(item, Type(item))),
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

        public override async Task<AbsolutePath> DeleteFolderAsync(AbsolutePath path, CancellationToken cancellationToken, bool recursive = false)
        {
            Directory.Delete(path, recursive);

            var timeoutTimeSpan = DeleteOrCreateTimeout;
            var start = DateTimeOffset.UtcNow;
            while ( Directory.Exists(path) && !cancellationToken.IsCancellationRequested )
            {
                var processingTime = DateTimeOffset.UtcNow - start;
                if ( processingTime > timeoutTimeSpan )
                {
                    throw new TimeoutException($"The delete operation on {path} timed out.");
                }

                await Task.Delay(DeleteOrCreateSpinPeriod, cancellationToken);
            }

            return path;
        }

        public override async Task<AbsolutePath> DeleteFileAsync(AbsolutePath path, CancellationToken cancellationToken)
        {
            File.Delete(path);

            var timeoutTimeSpan = DeleteOrCreateTimeout;
            var start = DateTimeOffset.UtcNow;
            while ( File.Exists(path) && !cancellationToken.IsCancellationRequested )
            {
                var processingTime = DateTimeOffset.UtcNow - start;
                if (processingTime > timeoutTimeSpan)
                {
                    throw new TimeoutException($"The delete operation on {path} timed out.");
                }

                await Task.Delay(DeleteOrCreateSpinPeriod, cancellationToken);
            }
            
            return path;
        }

        /// <inheritdoc />
        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan,
            int bufferSize = Constants.DefaultBufferSize, bool createRecursively = false)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    TryParent(path).IfHasValue(parent => CreateFolder(parent, createRecursively));
                var fileStream = new FileStream(path, fileMode, fileAccess, fileShare,
                    bufferSize, fileOptions);
                return Something<Stream>(fileStream);
            }
            catch (Exception ex)
            {
                return Nothing<Stream>(() => throw ex);
            }
        }

        /// <inheritdoc />
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

        public override IMaybe<Information> TryFileSize(AbsolutePath path)
        {
            try
            {
                return Something(Information.FromBytes(AsFileInfo(path).Length));
            }
            catch (Exception ex)
            {
                return Nothing<Information>(() => throw ex);
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
            File.Delete(path);

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

        public override IObservableReadOnlySet<AbsolutePath> Roots => _storage;

        private readonly ObservableSet<AbsolutePath> _storage = new ObservableSet<AbsolutePath>();

        public override IEnumerable<AbsolutePath> EnumerateDescendants(AbsolutePath path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            return EnumerateDescendantsOrChildren(path, searchPattern ?? "*", SearchOption.AllDirectories,
                includeFolders, includeFiles);
        }

        public override IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            return EnumerateDescendantsOrChildren(path, searchPattern ?? "*", SearchOption.TopDirectoryOnly,
                includeFolders, includeFiles);
        }

        private IEnumerable<AbsolutePath> EnumerateDescendantsOrChildren(AbsolutePath path, string searchPattern, SearchOption searchOption, bool includeFolders, bool includeFiles)
        {
            if (!IsFolder(path)) return ImmutableArray<AbsolutePath>.Empty;

            var fullName = AsDirectoryInfo(path).FullName;

            if (includeFiles && includeFolders)
            {
                return Directory.GetFileSystemEntries(fullName, searchPattern, searchOption).Select(x => ParseAbsolutePath(x));
            }

            if (includeFiles)
            {
                return Directory.GetFiles(fullName, searchPattern, searchOption).Select(x => ParseAbsolutePath(x));
            }
            
            if (includeFolders)
            {
                return Directory.GetDirectories(fullName, searchPattern, searchOption).Select(x => ParseAbsolutePath(x));
            }

            return ImmutableArray<AbsolutePath>.Empty;
        }

        /// <inheritdoc />
        public override void UpdateRoots()
        {
            if (DefaultDirectorySeparator == "/")
            {
                if (_storage.Count == 0)
                {
                    _storage.Add(ParseAbsolutePath("/"));
                }
                return;
            }
            
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
            var parent = TryParent(path);
            if (!parent.HasValue) return Observable.Return(path);

            return Observable.Create<AbsolutePath>(
                async (observer, token) =>
                {
                    var currentPath = path;
                    while (!token.IsCancellationRequested)
                    {
                        var watcher = new FileSystemWatcher(TryParent(currentPath).Value.ToString())
                        {
                            IncludeSubdirectories = false,
                            Filter = currentPath.Name
                        };

                        var tcs = new TaskCompletionSource<AbsolutePath>();

                        RenamedEventHandler handler = (_, args) =>
                        {
                            tcs.SetResult(new AbsolutePath(path.IsCaseSensitive, path.DirectorySeparator, this, new[]{args.FullPath}));
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

        /// <inheritdoc />
        public override AbsolutePath GetTemporaryFolder()
        {
            return ParseAbsolutePath(Path.GetTempPath());
        }

        /// <inheritdoc />
        public override AbsolutePath CreateFolder(AbsolutePath path, bool createRecursively = true)
        {
            try
            {
                if (Type(path) == IoFluently.PathType.Folder)
                    return path;
                if (createRecursively)
                {
                    var ancestors = Ancestors(path, true).ToList();
                    ancestors.Reverse();
                    foreach (var ancestor in ancestors)
                    {
                        switch (Type(ancestor))
                        {
                            case IoFluently.PathType.File:
                                throw new IOException($"The path {ancestor} is a file, not a folder");
                            case IoFluently.PathType.None:
                                Directory.CreateDirectory(path);
                                break;
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (IOException)
            {
                if (Type(path) != IoFluently.PathType.Folder)
                    throw;
            }

            if (Type(path) != IoFluently.PathType.Folder)
                throw new IOException("Failed to create folder " + path);
            return path;
        }

        public override AbsolutePath WriteAllBytes(AbsolutePath path, byte[] bytes, bool createRecursively = true)
        {
            if (createRecursively)
            {
                var parent = TryParent(path);
                if (parent.HasValue)
                {
                    CreateFolder(parent.Value, true);
                }
            }
            File.WriteAllBytes(path.ToString(), bytes);
            return path;
        }

        public override AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false)
        {
            Directory.Delete(path.ToString(), recursive);

            return path;
        }

        public override PathType Type(AbsolutePath path)
        {
            var str = path.ToString();
            if (File.Exists(str))
                return IoFluently.PathType.File;
            if (Directory.Exists(str))
                return IoFluently.PathType.Folder;
            return IoFluently.PathType.None;
        }

    }
}