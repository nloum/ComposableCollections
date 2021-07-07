using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
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

        private Folder _defaultRelativePathBase;
        
        public override Folder DefaultRelativePathBase => _defaultRelativePathBase ?? TryParseAbsolutePath(Environment.CurrentDirectory).Value;
        public TimeSpan DeleteOrCreateSpinPeriod { get; set; } = TimeSpan.FromMilliseconds(100);
        public TimeSpan DeleteOrCreateTimeout { get; set; } = TimeSpan.FromSeconds(5);

        public override void SetDefaultRelativePathBase(Folder defaultRelativePathBase)
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
        
        public IoService(bool enableOpenFilesTracking) : base(new OpenFilesTrackingService(enableOpenFilesTracking), ShouldBeCaseSensitiveByDefault(), GetDefaultDirectorySeparatorForThisEnvironment())
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
        public override ISetChanges<AbsolutePath> ToLiveLinq(Folder path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            if (PathObservationMethod == PathObservationMethod.FileSystemWatcher)
            {
                return ToLiveLinqWithFileSystemWatcher(path, includeFileContentChanges, includeSubFolders, pattern)
                    .ToDictionaryLiveLinq(x => x, x => Type(x)).KeysAsSet();
            }

            return ToLiveLinqWithFsWatch(path, includeFileContentChanges, includeSubFolders, pattern);
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFsWatch(Folder root, bool includeFileContentChanges, bool includeSubFolders, string pattern)
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

            var initialState = includeSubFolders ? root.Descendants
                .ToImmutableDictionary(x => x, x => Type(x))
                : root.Children.ToImmutableDictionary(x => x, x => Type(x));

            var resultObservable = process.StandardOutput
                .Observe(new []{ (char)0 })
                .Scan(new {State = initialState, LastEvents = (IDictionaryChangeStrict<AbsolutePath, PathType>[]) null},
                    (state, itemString) =>
                    {
                        var item = TryParseAbsolutePath(itemString).Value;
                        if (state.State.ContainsKey(item))
                        {
                            if (System.IO.File.Exists(itemString) || Directory.Exists(itemString))
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

        private ISetChanges<AbsolutePath> ToLiveLinqWithFileSystemWatcher(Folder root, bool includeFileContentChanges, bool includeSubFolders, string pattern)
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

                var initialSetChange = LiveLinq.Utility.SetChange(CollectionChangeType.Add, includeSubFolders ? root.Descendants
                    : root.Children.AsEnumerable());

                observer.OnNext(initialSetChange);

                watcher.EnableRaisingEvents = true;

                return new DisposableCollector(renamesSubscription, changesSubscription, deletionsSubscription, creationsSubscription);
            });


            return observable.ToLiveLinq();
        }

        public override async Task<MissingPath> DeleteFolderAsync(Folder path, CancellationToken cancellationToken, bool recursive = false)
        {
            Directory.Delete(path.Path, recursive);

            var timeoutTimeSpan = DeleteOrCreateTimeout;
            var start = DateTimeOffset.UtcNow;
            while ( Directory.Exists(path.Path) && !cancellationToken.IsCancellationRequested )
            {
                var processingTime = DateTimeOffset.UtcNow - start;
                if ( processingTime > timeoutTimeSpan )
                {
                    throw new TimeoutException($"The delete operation on {path} timed out.");
                }

                await Task.Delay(DeleteOrCreateSpinPeriod, cancellationToken);
            }

            return new MissingPath(path.Path);
        }

        public override async Task<MissingPath> DeleteFileAsync(File path, CancellationToken cancellationToken)
        {
            System.IO.File.Delete(path.Path);

            var timeoutTimeSpan = DeleteOrCreateTimeout;
            var start = DateTimeOffset.UtcNow;
            while ( System.IO.File.Exists(path.Path) && !cancellationToken.IsCancellationRequested )
            {
                var processingTime = DateTimeOffset.UtcNow - start;
                if (processingTime > timeoutTimeSpan)
                {
                    throw new TimeoutException($"The delete operation on {path} timed out.");
                }

                await Task.Delay(DeleteOrCreateSpinPeriod, cancellationToken);
            }
            
            return new MissingPath(path.Path);
        }

        /// <inheritdoc />
        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan,
            Information? bufferSize = default, bool createRecursively = false)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    TryParent(path).IfHasValue(parent => EnsureIsFolder(parent));
                var fileStream = new FileStream(path, fileMode, fileAccess, fileShare,
                    GetBufferSizeOrDefaultInBytes(bufferSize), fileOptions);
                return Something<Stream>(fileStream);
            }
            catch (Exception ex)
            {
                return Nothing<Stream>(() => throw ex);
            }
        }

        /// <inheritdoc />
        public override FileAttributes Attributes(File attributes) 
        {
            return AsFileInfo(attributes).Attributes;
        }

        public override DateTimeOffset CreationTime(File attributes)
        {
            return AsFileInfo(attributes).CreationTime;
        }

        public override DateTimeOffset LastAccessTime(File attributes)
        {
            return AsFileInfo(attributes).LastAccessTime;
        }

        public override DateTimeOffset LastWriteTime(File attributes)
        {
            return AsFileInfo(attributes).LastWriteTime;
        }

        public override Information FileSize(File path)
        {
            return Information.FromBytes(AsFileInfo(path).Length);
        }

        public override bool IsReadOnly(File path)
        {
            return AsFileInfo(path).IsReadOnly;
        }

        private FileInfo AsFileInfo(IHasAbsolutePath path)
        {
            return new FileInfo(path.Path.ToString());
        }

        private DirectoryInfo AsDirectoryInfo(IHasAbsolutePath path)
        {
            return new DirectoryInfo(path.Path.ToString());
        }

        public override MissingPath DeleteFile(File path)
        {
            System.IO.File.Delete(path.Path);

            return new MissingPath(path.Path);
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

        public override IObservableReadOnlySet<Folder> Roots => _storage;

        private readonly ObservableSet<Folder> _storage = new ObservableSet<Folder>();

        public override IEnumerable<FileOrFolder> Descendants(Folder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            return EnumerateDescendantsOrChildren(path, searchPattern ?? "*", SearchOption.AllDirectories,
                includeFolders, includeFiles);
        }

        public override IEnumerable<FileOrFolder> Children(Folder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            return EnumerateDescendantsOrChildren(path, searchPattern ?? "*", SearchOption.TopDirectoryOnly,
                includeFolders, includeFiles);
        }

        private IEnumerable<FileOrFolder> EnumerateDescendantsOrChildren(Folder path, string searchPattern, SearchOption searchOption, bool includeFolders, bool includeFiles)
        {
            var fullName = AsDirectoryInfo(path.Path).FullName;

            if (includeFiles && includeFolders)
            {
                return Directory.GetFileSystemEntries(fullName, searchPattern, searchOption).Select(x => ParseAbsolutePath(x).ExpectFileOrFolder());
            }

            if (includeFiles)
            {
                return Directory.GetFiles(fullName, searchPattern, searchOption).Select(x => ParseAbsolutePath(x).ExpectFileOrFolder());
            }
            
            if (includeFolders)
            {
                return Directory.GetDirectories(fullName, searchPattern, searchOption).Select(x => ParseAbsolutePath(x).ExpectFileOrFolder());
            }

            return ImmutableArray<FileOrFolder>.Empty;
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
                var drivePath = TryParseAbsolutePath(drive).Value.ExpectFolder();
                if (!_storage.Contains(drivePath))
                    _storage.Add(drivePath);
            }

            var drivesThatWereRemoved = new List<Folder>();

            foreach (var drive in _storage)
                if (!currentStorage.Contains(drive.Path + "\\"))
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
        public override Folder GetTemporaryFolder()
        {
            return ParseAbsolutePath(Path.GetTempPath()).ExpectFolder();
        }

        /// <inheritdoc />
        public override Folder CreateFolder(MissingPath path, bool createRecursively = true)
        {
            if (createRecursively)
            {
                var ancestors = Ancestors(path, true).ToList();
                ancestors.Reverse();
                foreach (var ancestor in ancestors)
                {
                    ancestor.ForEach(folder => { }, missingPath => Directory.CreateDirectory(missingPath.Path));
                }
            }
            else
            {
                Directory.CreateDirectory(path.Path);
            }

            return new Folder(path.Path);
        }

        public override AbsolutePath WriteAllBytes(AbsolutePath path, byte[] bytes, bool createRecursively = true)
        {
            if (createRecursively)
            {
                foreach (var ancestor in Ancestors(path))
                {
                    
                }
                var parent = TryParent(path);
                if (parent.HasValue)
                {
                    CreateFolder(parent.Value, true);
                }
            }
            System.IO.File.WriteAllBytes(path.ToString(), bytes);
            return path;
        }

        public override MissingPath DeleteFolder(Folder path, bool recursive = false)
        {
            Directory.Delete(path.Path, recursive);

            return new MissingPath(path.Path);
        }

        public override PathType Type(AbsolutePath path)
        {
            var str = path.ToString();
            if (System.IO.File.Exists(str))
                return IoFluently.PathType.File;
            if (Directory.Exists(str))
                return IoFluently.PathType.Folder;
            return IoFluently.PathType.MissingPath;
        }

    }
}