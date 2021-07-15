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
    public class LocalFileSystem : FileSystemBase, IEnvironment
    {
        /// <summary>
        /// The default IoService. Use this for environments where dependency injection isn't necessary.
        /// E.g., Nuke.build scripts or bootstrapping a larger program.
        /// </summary>
        public static LocalFileSystem Default { get; set; }
    
        private bool? _isCaseSensitiveByDefault = null;

        /// <inheritdoc />
        public override bool CanEmptyDirectoriesExist => true;

        /// <inheritdoc />
        public override EmptyFolderMode EmptyFolderMode { get; }

        /// <inheritdoc />
        public override IQueryable<AbsolutePath> Query()
        {
            return new Queryable<AbsolutePath>(new QueryContext());
        }

        private Folder _temporaryFolder;

        public Folder TemporaryFolder
        {
            get
            {
                return _temporaryFolder ?? ParseAbsolutePath(Path.GetTempPath()).ExpectFolder();
            }

            set
            {
                _temporaryFolder = value;
            }
        }

        public override Folder DefaultRoot
        {
            get
            {
                if (DefaultDirectorySeparator == "/")
                {
                    return ParseAbsolutePath("/").ExpectFolder();
                }

                return Roots.First();
            }
        }

        /// <inheritdoc />
        public MissingPath GenerateUniqueTemporaryPath(string extension = null)
        {
            var result = TemporaryFolder / Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(extension))
            {
                result = result.WithExtension(extension);
            }

            return new MissingPath(result);
        }
                
        /// <inheritdoc />
        public override void UpdateRoots()
        {
            if (DefaultDirectorySeparator == "/")
            {
                if (_storage.Count == 0)
                {
                    _storage.Add(ParseAbsolutePath("/").ExpectFolder());
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
                if (!currentStorage.Contains(drive + "\\"))
                    drivesThatWereRemoved.Add(drive);

            foreach (var driveThatWasRemoved in drivesThatWereRemoved) _storage.Remove(driveThatWasRemoved);
        }

        public override IObservableReadOnlySet<Folder> Roots => _storage;

        private readonly ObservableSet<Folder> _storage = new ObservableSet<Folder>();

        /// <inheritdoc />
        public Folder CurrentDirectory
        {
            get
            {
                return TryParseAbsolutePath(Environment.CurrentDirectory).Value.ExpectFolder();
            }

            set
            {
                Environment.CurrentDirectory = value.FullName;
            }
        }
        
        /// <inheritdoc />
        public virtual AbsolutePath ParsePathRelativeToWorkingDirectory(string path)
        {
            return TryParseAbsolutePath(path, CurrentDirectory).Value;
        }
        
        public TimeSpan DeleteOrCreateSpinPeriod { get; set; } = TimeSpan.FromMilliseconds(100);
        
        public TimeSpan DeleteOrCreateTimeout { get; set; } = TimeSpan.FromSeconds(5);

        public LocalFileSystem() : this(false)
        {
            UpdateRoots();
        }
        
        public LocalFileSystem(bool enableOpenFilesTracking) : base(new OpenFilesTrackingService(enableOpenFilesTracking), ShouldBeCaseSensitiveByDefault(), GetDefaultDirectorySeparatorForThisEnvironment())
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
        public override ISetChanges<AbsolutePath> ToLiveLinq(IFolder path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            if (PathObservationMethod == PathObservationMethod.FileSystemWatcher)
            {
                return ToLiveLinqWithFileSystemWatcher(path, includeFileContentChanges, includeSubFolders, pattern)
                    .ToDictionaryLiveLinq(x => x, x => Type(x)).KeysAsSet();
            }

            return ToLiveLinqWithFsWatch(path, includeFileContentChanges, includeSubFolders, pattern);
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFsWatch(IFolder root, bool includeFileContentChanges, bool includeSubFolders, string pattern)
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

            var initialState = includeSubFolders
                ? root.FileSystem.EnumerateDescendants(root).Select(x => new AbsolutePath(x)).ToImmutableDictionary(x => x, x => Type(x))
                : root.FileSystem.EnumerateChildren(root).Select(x => new AbsolutePath(x)).ToImmutableDictionary(x => x, x => Type(x));

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
                result = result.Where(path => regex.IsMatch(path.FileSystem.Name(path)));
            }
            
            return result;
        }

        private ISetChanges<AbsolutePath> ToLiveLinqWithFileSystemWatcher(IFolder root, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            var observable = Observable.Create<ISetChange<AbsolutePath>>(observer =>
            {
                var watcher = new FileSystemWatcher
                {
                    Path = root.FullName,
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
                        var path = root.FileSystem.TryParseAbsolutePath(args.FullPath).Value;
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
                        var path = root.FileSystem.TryParseAbsolutePath(args.FullPath).Value;
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
                        var path = root.FileSystem.TryParseAbsolutePath(args.FullPath).Value;
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
                        var oldPath = root.FileSystem.TryParseAbsolutePath(args.OldFullPath).Value;
                        var path = root.FileSystem.TryParseAbsolutePath(args.FullPath).Value;
                        observer.OnNext(LiveLinq.Utility.SetChange(CollectionChangeType.Remove, oldPath));
                        observer.OnNext(LiveLinq.Utility.SetChange(CollectionChangeType.Add, path));
                    });

                var initialSetChange = LiveLinq.Utility.SetChange(CollectionChangeType.Add, includeSubFolders
                    ? root.FileSystem.EnumerateDescendants(root).Select(x => new AbsolutePath(x))
                    : root.FileSystem.EnumerateChildren(root).Select(x => new AbsolutePath(x)).AsEnumerable());

                observer.OnNext(initialSetChange);

                watcher.EnableRaisingEvents = true;

                return new DisposableCollector(renamesSubscription, changesSubscription, deletionsSubscription, creationsSubscription);
            });


            return observable.ToLiveLinq();
        }

        public override async Task<MissingPath> DeleteFolderAsync(IFolder path, CancellationToken cancellationToken,  bool recursive = true)
        {
            var pathString = path.FullName;
            Directory.Delete(pathString, recursive);

            var timeoutTimeSpan = DeleteOrCreateTimeout;
            var start = DateTimeOffset.UtcNow;
            while ( Directory.Exists(pathString) && !cancellationToken.IsCancellationRequested )
            {
                var processingTime = DateTimeOffset.UtcNow - start;
                if ( processingTime > timeoutTimeSpan )
                {
                    throw new TimeoutException($"The delete operation on {path} timed out.");
                }

                await Task.Delay(DeleteOrCreateSpinPeriod, cancellationToken);
            }

            return new MissingPath(path );
        }

        public override async Task<MissingPath> DeleteFileAsync(IFile path, CancellationToken cancellationToken)
        {
            var pathString = path.FullName;
            System.IO.File.Delete(pathString);

            var timeoutTimeSpan = DeleteOrCreateTimeout;
            var start = DateTimeOffset.UtcNow;
            while ( System.IO.File.Exists(pathString) && !cancellationToken.IsCancellationRequested )
            {
                var processingTime = DateTimeOffset.UtcNow - start;
                if (processingTime > timeoutTimeSpan)
                {
                    throw new TimeoutException($"The delete operation on {path} timed out.");
                }

                await Task.Delay(DeleteOrCreateSpinPeriod, cancellationToken);
            }
            
            return new MissingPath(path );
        }

        /// <inheritdoc />
        public override Stream Open(IFileOrMissingPath path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan,
            Information? bufferSize = default,  bool createRecursively = true)
        {
            if (MayCreateFile(fileMode))
                TryParent(path ).IfHasValue(parent => EnsureIsFolder(parent));
            var fileStream = new FileStream(path.FullName, fileMode, fileAccess, fileShare,
                GetBufferSizeOrDefaultInBytes(bufferSize), fileOptions);
            return fileStream;
        }

        /// <inheritdoc />
        public override FileAttributes Attributes(IFile attributes) 
        {
            return AsFileInfo(attributes).Attributes;
        }

        public override DateTimeOffset CreationTime(IFile attributes)
        {
            return AsFileInfo(attributes).CreationTime;
        }

        public override DateTimeOffset LastAccessTime(IFile attributes)
        {
            return AsFileInfo(attributes).LastAccessTime;
        }

        public override DateTimeOffset LastWriteTime(IFile attributes)
        {
            return AsFileInfo(attributes).LastWriteTime;
        }

        public override Information FileSize(IFile path)
        {
            return Information.FromBytes(AsFileInfo(path).Length);
        }

        public override bool IsReadOnly(IFile path)
        {
            return AsFileInfo(path).IsReadOnly;
        }

        private FileInfo AsFileInfo(IFileOrFolderOrMissingPath path)
        {
            return new FileInfo(path.FullName);
        }

        private DirectoryInfo AsDirectoryInfo(IFileOrFolderOrMissingPath path)
        {
            return new DirectoryInfo(path.FullName);
        }

        public override MissingPath DeleteFile(IFile path)
        {
            System.IO.File.Delete(path.FullName);

            return new MissingPath(path);
        }

        public override IEnumerable<IFileOrFolder> EnumerateDescendants(IFolder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            return EnumerateDescendantsOrChildren(path, searchPattern ?? "*", SearchOption.AllDirectories,
                includeFolders, includeFiles);
        }

        public override IEnumerable<IFileOrFolder> EnumerateChildren(IFolder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            return EnumerateDescendantsOrChildren(path, searchPattern ?? "*", SearchOption.TopDirectoryOnly,
                includeFolders, includeFiles);
        }

        private IEnumerable<IFileOrFolder> EnumerateDescendantsOrChildren(IFolder path, string searchPattern, SearchOption searchOption, bool includeFolders, bool includeFiles)
        {
            var fullName = AsDirectoryInfo(path).FullName;

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

            return ImmutableArray<IFileOrFolder>.Empty;
        }

        /// <inheritdoc />
        public override Folder CreateFolder(IMissingPath path, bool createRecursively = true)
        {
            var pathString = path.FullName;
        
            if (createRecursively)
            {
                var ancestors = Ancestors(path, true).ToList();
                ancestors.Reverse();
                foreach (var ancestor in ancestors)
                {
                    ancestor.ForEach(folder => { }, missingPath => Directory.CreateDirectory(pathString));
                }
            }
            else
            {
                Directory.CreateDirectory(pathString);
            }

            return new Folder(path);
        }

        public override File WriteAllBytes(IFileOrMissingPath path, byte[] bytes, bool createRecursively = true)
        {
            var pathString = path.FullName;
                    
            if (createRecursively)
            {
                foreach (var ancestor in Ancestors(path ))
                {
                    
                }
                var parent = TryParent(path);
                if (parent.HasValue)
                {
                    parent.Value.EnsureIsFolder(true);
                }
            }
            System.IO.File.WriteAllBytes(pathString, bytes);
            return path.ExpectFile();
        }

        public override MissingPath DeleteFolder(IFolder path,  bool recursive = true)
        {
            Directory.Delete(path.FullName, recursive);

            return new MissingPath(path);
        }

        public override PathType Type(IFileOrFolderOrMissingPath path)
        {
            var str = path.FullName;
            if (System.IO.File.Exists(str))
                return IoFluently.PathType.File;
            if (Directory.Exists(str))
                return IoFluently.PathType.Folder;
            return IoFluently.PathType.MissingPath;
        }

    }
}