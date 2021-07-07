using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Algorithms.Sorting;
using GenericNumbers;
using LiveLinq;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using SimpleMonads;
using UnitsNet;
using static SimpleMonads.Utility;

namespace IoFluently
{
    /// <summary>
    /// An implementation of <see cref="IIoService"/> that keeps the folder, the files, and the file contents in memory.
    /// Useful for unit testing code that uses IoFluently.
    /// </summary>
    public class InMemoryIoService : IoServiceBase
    {
        /// <summary>
        /// Represents a file in memory.
        /// </summary>
        public class InMemoryFile
        {
            /// <summary>
            /// Whether the file is read-only
            /// </summary>
            public bool IsReadOnly => Attributes.HasFlag(FileAttributes.ReadOnly);
            
            /// <summary>
            /// Whether the file is encrypted
            /// </summary>
            public bool IsEncrypted { get; set; }
            
            /// <summary>
            /// The file attributes (hidden, read-only, compressed, etc.)
            /// </summary>
            public FileAttributes Attributes { get; set; }
            
            /// <summary>
            /// The timestamp when the file was created
            /// </summary>
            public DateTimeOffset CreationTime { get; set; }
            
            /// <summary>
            /// The timestamp when the file was last accessed
            /// </summary>
            public DateTimeOffset LastAccessTime { get; set; }
            
            /// <summary>
            /// The timestamp when the file was last modified
            /// </summary>
            public DateTimeOffset LastWriteTime { get; set; }
            
            /// <summary>
            /// The contents of the file
            /// </summary>
            public byte[] Contents { get; set; }
            
            /// <summary>
            /// An object used to lock the file for reading and/or writing
            /// </summary>
            public ReaderWriterLock Lock { get; } = new ReaderWriterLock();
        }

        /// <summary>
        /// Represents a folder in memory.
        /// </summary>
        public class InMemoryFolder
        {
            /// <summary>
            /// The files that this folder contains
            /// </summary>
            public Dictionary<string, InMemoryFile> Files { get; } = new Dictionary<string, InMemoryFile>();
            
            /// <summary>
            /// The sub-folders that this folder contains
            /// </summary>
            public Dictionary<string, InMemoryFolder> Folders { get; } = new Dictionary<string, InMemoryFolder>();
        }
        
        /// <inheritdoc />
        public override IQueryable<AbsolutePath> Query()
        {
            throw new NotImplementedException();
        }

        public override bool CanEmptyDirectoriesExist => true;

        private Folder _currentDirectory;
        private Folder _temporaryFolder = null;
        
        /// <summary>
        /// The root folders in this in-memory file system. E.g., if this is a Unix-like file system then this would have
        /// just '/'. If this was a Windows-like file system then this might contain 'C:' and 'D:'.
        /// </summary>
        public ObservableDictionary<string, InMemoryFolder> RootFolders { get; } = new ObservableDictionary<string, InMemoryFolder>();

        /// <inheritdoc />
        public override Folder DefaultRelativePathBase => _currentDirectory;

        /// <summary>
        /// Changes the current working directory
        /// </summary>
        /// <param name="newCurrentDirectory">The new current directory</param>
        public void SetCurrentDirectory(Folder newCurrentDirectory)
        {
            _currentDirectory = newCurrentDirectory;
        }

        /// <summary>
        /// Constructs a new in-memory IIoService.
        /// </summary>
        /// <param name="newline">The newline separate. E.g., '\n' for Unix-like environments or '\r\n' for Windows-like
        /// environments.</param>
        /// <param name="isCaseSensitiveByDefault">Whether this IIoService will treat paths as case sensitive by default</param>
        /// <param name="defaultDirectorySeparatorForThisEnvironment">The default directory separator for this IIoService.
        /// E.g., '/' for Unix-like environments, '\\' for Windows-like environments.</param>
        /// <param name="enableOpenFilesTracking">Whether to track which files are open (useful for debugging, but comes
        /// with a performance hit)</param>
        public InMemoryIoService(bool? isCaseSensitiveByDefault = null, string defaultDirectorySeparatorForThisEnvironment = null, bool enableOpenFilesTracking = false)
            : base(new OpenFilesTrackingService(enableOpenFilesTracking), isCaseSensitiveByDefault ?? ShouldBeCaseSensitiveByDefault(), defaultDirectorySeparatorForThisEnvironment ?? GetDefaultDirectorySeparatorForThisEnvironment())
        {
            if (defaultDirectorySeparatorForThisEnvironment == "/")
            {
                RootFolders.Add("/", new InMemoryFolder());
                _currentDirectory = ParseAbsolutePath("/");
            }
            else
            {
                RootFolders.Add("C:", new InMemoryFolder());
                _currentDirectory = ParseAbsolutePath("C:\\");
            }
        }

        /// <inheritdoc />
        public override Folder GetTemporaryFolder()
        {
            return _temporaryFolder;
        }
        
        /// <summary>
        /// Sets the temporary folder path
        /// </summary>
        /// <param name="absolutePath">The new temporary folder path</param>
        public void SetTemporaryFolder(AbsolutePath absolutePath)
        {
            _temporaryFolder = absolutePath;
        }

        /// <inheritdoc />
        public override ISetChanges<AbsolutePath> ToLiveLinq(Folder path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override IObservableReadOnlySet<Folder> Roots => RootFolders.ToLiveLinq().KeysAsSet().Select(x => ParseAbsolutePath(x).ExpectFolder()).ToReadOnlyObservableSet();

        private IMaybe<InMemoryFile> GetFile(AbsolutePath path)
        {
            path = Simplify(path);
            var components = path.Path.Components;
            if (RootFolders.ContainsKey(components[0]))
            {
                return GetFile(RootFolders[components[0]], components.Skip(1).ToList(), path);
            }
            return Nothing<InMemoryFile>(() => throw new InvalidOperationException($"The root folder of {path} does not exist"));
        }

        private IMaybe<InMemoryFile> GetFile(InMemoryFolder folder, IReadOnlyList<string> components, AbsolutePath originalPath)
        {
            if (components.Count == 0)
            {
                return Nothing<InMemoryFile>(() => throw new InvalidOperationException("There are no components in the specified path"));
            }
            
            if (folder.Files.ContainsKey(components[0]))
            {
                if (components.Count > 1)
                {
                    return Nothing<InMemoryFile>(() => throw new InvalidOperationException($"The {components[0]} folder in the path {originalPath} is actually a file, not a folder"));
                }

                return folder.Files[components[0]].ToMaybe();
            }

            if (folder.Folders.ContainsKey(components[0]))
            {
                return GetFile(folder.Folders[components[0]], components.Skip(1).ToList(), originalPath);
            }

            return Nothing<InMemoryFile>(() => throw new InvalidOperationException($"The {components[0]} part of the path {originalPath} is missing"));
        }
        
        private IMaybe<InMemoryFolder> GetFolder(IHasAbsolutePath path)
        {
            path = Simplify(path.Path);
            var components = path.Path.Path.Components;
            if (RootFolders.ContainsKey(components[0]))
            {
                return GetFolder(RootFolders[components[0]], components.Skip(1).ToList(), path.Path);
            }
            return Nothing<InMemoryFolder>(() => throw new InvalidOperationException($"The root folder of {path} does not exist"));
        }

        private IMaybe<InMemoryFolder> GetFolder(InMemoryFolder folder, IReadOnlyList<string> components, AbsolutePath originalPath)
        {
            if (components.Count == 0)
            {
                return folder.ToMaybe();
            }
            
            if (folder.Files.ContainsKey(components[0]))
            {
                return Nothing<InMemoryFolder>(() => throw new InvalidOperationException($"The {components[0]} folder in the path {originalPath} is actually a file, not a folder"));
            }

            if (folder.Folders.ContainsKey(components[0]))
            {
                return GetFolder(folder.Folders[components[0]], components.Skip(1).ToList(), originalPath);
            }

            return Nothing<InMemoryFolder>(() => throw new InvalidOperationException($"The {components[0]} part of the path {originalPath} is missing"));
        }

        /// <inheritdoc />
        public override void UpdateRoots()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override IEnumerable<FileOrFolder> Children(Folder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            var folder = GetFolder(path.Path);
            if (!folder.HasValue)
            {
                return Enumerable.Empty<FileOrFolder>();
            }

            var regex = FileNamePatternToRegex(searchPattern);
            
            return folder.Value.Files.Select(file => path / file.Key)
                .Concat(folder.Value.Folders.Select(subfolder => path / subfolder.Key))
                .Where(x => regex.IsMatch(x))
                .Select(path => path.ExpectFileOrFolder());
        }

        public override IEnumerable<FileOrFolder> Descendants(Folder path, string searchPattern = null, bool includeFolders = true,
            bool includeFiles = true)
        {
            var folder = GetFolder(path.Path);
            if (!folder.HasValue)
            {
                return Enumerable.Empty<FileOrFolder>();
            }

            var regex = FileNamePatternToRegex(searchPattern);

            return folder.Value.Files.Select(file => path / file.Key)
                .Concat(folder.Value.Folders.Select(subfolder => path / subfolder.Key))
                .Where(x => regex.IsMatch(x))
                .Select(path => path.ExpectFileOrFolder());
        }

        public override MissingPath DeleteFile(IoFluently.File path)
        {
            path = Simplify(path.Path);
            var parentFolder = GetFolder(TryParent(path.Path).Value).Value;
            parentFolder.Files.Remove(path.Path.Name);
            return new MissingPath(path.Path);
        }

        /// <inheritdoc />
        public override AbsolutePath Decrypt(AbsolutePath path)
        {
            GetFile(path).Value.IsEncrypted = false;

            return path;
        }

        /// <inheritdoc />
        public override AbsolutePath Encrypt(AbsolutePath path)
        {
            GetFile(path).Value.IsEncrypted = true;

            return path;
        }

        /// <inheritdoc />
        public override IMaybe<bool> TryIsReadOnly(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.IsReadOnly);
        }

        /// <inheritdoc />
        public override IMaybe<Information> TryFileSize(AbsolutePath path)
        {
            return GetFile(path).Select(x => Information.FromBytes(x.Contents.LongLength));
         }

        /// <inheritdoc />
        public override IMaybe<FileAttributes> TryAttributes(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.Attributes);
        }

        /// <inheritdoc />
        public override IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.CreationTime);

        }

        /// <inheritdoc />
        public override IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.LastAccessTime);

        }

        /// <inheritdoc />
        public override IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.LastWriteTime);

        }

        /// <inheritdoc />
        public override PathType Type(AbsolutePath path)
        {
            path = Simplify(path);

            var file = GetFile(path);
            if (file.HasValue)
            {
                return IoFluently.PathType.File;
            }

            var folder = GetFolder(path);
            if (folder.HasValue)
            {
                return IoFluently.PathType.Folder;
            }
            
            return IoFluently.PathType.MissingPath;
        }

        /// <inheritdoc />
        public override Task<MissingPath> DeleteFolderAsync(Folder path, CancellationToken cancellationToken, bool recursive = false)
        {
            return Task.Run(() => DeleteFolder(path, recursive), cancellationToken);
        }

        /// <inheritdoc />
        public override Task<MissingPath> DeleteFileAsync(File path, CancellationToken cancellationToken)
        {
            return Task.Run(() => DeleteFile(path), cancellationToken);
        }

        /// <inheritdoc />
        public override MissingPath DeleteFolder(Folder path, bool recursive = false)
        {
            var parentFolder = GetFolder(TryParent(path.Path).Value);
            parentFolder.Value.Folders.Remove(Simplify(path.Path).Name);
            return new MissingPath(path.Path);
        }

        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.None | FileOptions.SequentialScan,
            Information? bufferSize = default, bool createRecursively = false)
        {
            InMemoryFile file = null;
            var maybeFile = GetFile(path);
            if (!maybeFile.HasValue)
            {
                if (fileMode == FileMode.Create || fileMode == FileMode.CreateNew ||
                    fileMode == FileMode.OpenOrCreate)
                {
                    var pathParent = TryParent(path).Value;
                    InMemoryFolder parentFolder = null;
                    var maybeParentFolder = GetFolder(pathParent);
                    if (!maybeParentFolder.HasValue)
                    {
                        CreateFolder(pathParent);
                        parentFolder = GetFolder(pathParent).Value;
                    }
                    else
                    {
                        parentFolder = maybeParentFolder.Value;
                    }
                
                    var now = DateTimeOffset.UtcNow;
                    file = new InMemoryFile()
                    {
                        Attributes = FileAttributes.Normal,
                        Contents = new byte[0],
                        CreationTime = now,
                        IsEncrypted = false,
                        LastAccessTime = now,
                        LastWriteTime = now,
                    };
                
                    parentFolder.Files.Add(path.Name, file);
                    
                    file.Lock.AcquireReaderLock(0);
                    var memoryStream = new MemoryStream();
                    
                    return ((Stream)new StreamCloseDecorator(memoryStream, () =>
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        file.Contents = memoryStream.ToArray();
                        file.Lock.ReleaseReaderLock();
                    })).ToMaybe();
                }
                else
                {
                    throw new InvalidOperationException(
                        $"None of the necessary flags for creating a file (FileMode.Create, FileMode.CreateNew, FileMode.OpenOrCreate) were specified but {path} does not exist");
                }
            }
            else
            {
                if (fileMode == FileMode.CreateNew)
                {
                    return Nothing<FileStream>(() => throw new InvalidOperationException($"The FileMode.CreateNew flag was specified, but the file {path} did indeed exist"));
                }
                
                file = GetFile(path).Value;
                
                file.Lock.AcquireReaderLock(0);
                var memoryStream = new MemoryStream();
                memoryStream.Write(file.Contents, 0, file.Contents.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return ((Stream)new StreamCloseDecorator(memoryStream, () =>
                {
                    file.Contents = memoryStream.GetBuffer();
                    file.Lock.ReleaseReaderLock();
                })).ToMaybe();
            }
        }

        /// <inheritdoc />
        public override Folder CreateFolder(MissingPath path, bool createRecursively = false)
        {
            var folder = GetFolder(TryParent(path.Path).Value).Value;
            EnsureFolderExists(folder, new[]{path.Path.Name});
            return new Folder(path.Path);
        }

        private void EnsureFolderExists(InMemoryFolder folder, IReadOnlyList<string> components)
        {
            if (folder.Files.ContainsKey(components[0]))
            {
                throw new IOException("Cannot create folder because a file already has that name");
            }

            InMemoryFolder childFolder = null;
            if (folder.Folders.ContainsKey(components[0]))
            {
                childFolder = folder.Folders[components[0]];
            }
            else
            {
                childFolder = new InMemoryFolder();
                folder.Folders[components[0]] = childFolder;
            }

            if (components.Count == 1)
            {
                return;
            }
            
            EnsureFolderExists(childFolder, components.Skip(1).ToList());
        }

        /// <inheritdoc />
        public override IObservable<AbsolutePath> Renamings(AbsolutePath path)
        {
            throw new NotImplementedException();
        }
    }
}