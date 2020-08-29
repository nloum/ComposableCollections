using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using GenericNumbers;
using LiveLinq;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using ReactiveProcesses;
using SimpleMonads;
using UnitsNet;
using static SimpleMonads.Utility;

namespace IoFluently
{
    public class InMemoryIoService : IoServiceBase
    {
        public class File
        {
            public bool IsReadOnly => Attributes.HasFlag(FileAttributes.ReadOnly);
            public bool IsEncrypted { get; set; }
            public FileAttributes Attributes { get; set; }
            public DateTimeOffset CreationTime { get; set; }
            public DateTimeOffset LastAccessTime { get; set; }
            public DateTimeOffset LastWriteTime { get; set; }
            public byte[] Contents { get; set; }
            public ReaderWriterLock Lock { get; } = new ReaderWriterLock();
        }

        public class Folder
        {
            public Dictionary<string, File> Files { get; } = new Dictionary<string, File>();
            public Dictionary<string, Folder> Folders { get; } = new Dictionary<string, Folder>();
        }

        private readonly PathFlags _defaultPathFlags;
        public ObservableDictionary<string, Folder> RootFolders { get; } = new ObservableDictionary<string, Folder>();

        public InMemoryIoService(string newline, PathFlags defaultPathFlags, bool enableOpenFilesTracking = false, IReactiveProcessFactory reactiveProcessFactory = null) : base(new OpenFilesTrackingService(enableOpenFilesTracking), reactiveProcessFactory ?? new ReactiveProcessFactory(), newline)
        {
            _newline = newline;
            _defaultPathFlags = defaultPathFlags;
        }

        public override ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyObservableSet<AbsolutePath> Storage => RootFolders.ToLiveLinq().KeysAsSet().Select(x => ParseAbsolutePath(x)).ToReadOnlyObservableSet();

        private IMaybe<File> GetFile(AbsolutePath path)
        {
            path = path.Simplify();
            var components = path.Path.Components;
            if (RootFolders.ContainsKey(components[0]))
            {
                return GetFile(RootFolders[components[0]], components.Skip(1).ToList(), path);
            }
            return Nothing<File>(() => throw new InvalidOperationException($"The root folder of {path} does not exist"));
        }

        private IMaybe<File> GetFile(Folder folder, IReadOnlyList<string> components, AbsolutePath originalPath)
        {
            if (components.Count == 0)
            {
                return Nothing<File>(() => throw new InvalidOperationException("There are no components in the specified path"));
            }
            
            if (folder.Files.ContainsKey(components[0]))
            {
                if (components.Count > 1)
                {
                    return Nothing<File>(() => throw new InvalidOperationException($"The {components[0]} folder in the path {originalPath} is actually a file, not a folder"));
                }

                return folder.Files[components[0]].ToMaybe();
            }

            if (folder.Folders.ContainsKey(components[0]))
            {
                return GetFile(folder.Folders[components[0]], components.Skip(1).ToList(), originalPath);
            }

            return Nothing<File>(() => throw new InvalidOperationException($"The {components[0]} part of the path {originalPath} is missing"));
        }
        
        private IMaybe<Folder> GetFolder(AbsolutePath path)
        {
            path = path.Simplify();
            var components = path.Path.Components;
            if (RootFolders.ContainsKey(components[0]))
            {
                return GetFolder(RootFolders[components[0]], components.Skip(1).ToList(), path);
            }
            return Nothing<Folder>(() => throw new InvalidOperationException($"The root folder of {path} does not exist"));
        }

        private IMaybe<Folder> GetFolder(Folder folder, IReadOnlyList<string> components, AbsolutePath originalPath)
        {
            if (components.Count == 0)
            {
                return folder.ToMaybe();
            }
            
            if (folder.Files.ContainsKey(components[0]))
            {
                return Nothing<Folder>(() => throw new InvalidOperationException($"The {components[0]} folder in the path {originalPath} is actually a file, not a folder"));
            }

            if (folder.Folders.ContainsKey(components[0]))
            {
                return GetFolder(folder.Folders[components[0]], components.Skip(1).ToList(), originalPath);
            }

            return Nothing<Folder>(() => throw new InvalidOperationException($"The {components[0]} part of the path {originalPath} is missing"));
        }
        
        public override IMaybe<StreamWriter> TryOpenWriter(AbsolutePath absolutePath)
        {
            throw new NotImplementedException();
        }

        public override PathFlags GetDefaultFlagsForThisEnvironment()
        {
            return _defaultPathFlags;
        }

        public override void UpdateStorage()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, bool includeFolders = true, bool includeFiles = true)
        {
            throw new NotImplementedException();
        }

        public override AbsolutePath DeleteFile(AbsolutePath path)
        {
            path = path.Simplify();
            var parentFolder = GetFolder(path.Parent()).Value;
            parentFolder.Files.Remove(path.Name);
            return path;
        }

        public override AbsolutePath Decrypt(AbsolutePath path)
        {
            GetFile(path).Value.IsEncrypted = false;

            return path;
        }

        public override AbsolutePath Encrypt(AbsolutePath path)
        {
            GetFile(path).Value.IsEncrypted = true;

            return path;
        }

        public override IMaybe<bool> TryIsReadOnly(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.IsReadOnly);
        }

        public override IMaybe<Information> TryFileSize(AbsolutePath path)
        {
            return GetFile(path).Select(x => Information.FromBytes(x.Contents.LongLength));
         }

        public override IMaybe<FileAttributes> TryAttributes(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.Attributes);
        }

        public override IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.CreationTime);

        }

        public override IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.LastAccessTime);

        }

        public override IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath path)
        {
            return GetFile(path).Select(x => x.LastWriteTime);

        }

        public override PathType GetPathType(AbsolutePath path)
        {
            path = path.Simplify();

            var file = GetFile(path);
            if (file.HasValue)
            {
                return PathType.File;
            }

            var folder = GetFolder(path);
            if (folder.HasValue)
            {
                return PathType.Folder;
            }
            
            return PathType.None;
        }

        public override AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false)
        {
            var parentFolder = GetFolder(path.Parent());
            parentFolder.Value.Folders.Remove(path.Simplify().Name);
            return path;
        }

        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            File file = null;
            var maybeFile = GetFile(path);
            if (!maybeFile.HasValue && fileMode.HasFlag(FileMode.Create) || fileMode.HasFlag(FileMode.CreateNew) ||
                fileMode.HasFlag(FileMode.OpenOrCreate))
            {
                var pathParent = path.Parent();
                Folder parentFolder = null;
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
                file = new File()
                {
                    Attributes = FileAttributes.Normal,
                    Contents = new byte[0],
                    CreationTime = now,
                    IsEncrypted = false,
                    LastAccessTime = now,
                    LastWriteTime = now,
                };
                
                parentFolder.Files.Add(path.Name, file);
            }
            else
            {
                if (fileMode.HasFlag(FileMode.CreateNew))
                {
                    return Nothing<FileStream>(() => throw new InvalidOperationException($"The FileMode.CreateNew flag was specified, but the file {path} did indeed exist"));
                }
                
                file = GetFile(path).Value;
            }
            
            file.Lock.AcquireReaderLock(0);
            var memoryStream = new MemoryStream(file.Contents);

            return ((Stream)new StreamCloseDisposeDecorator(memoryStream, () =>
            {
                //file.Lock.ReleaseReaderLock();
            }, () =>
            {
                file.Lock.ReleaseReaderLock();
            })).ToMaybe();
        }

        public override AbsolutePath CreateFolder(AbsolutePath path)
        {
            var components = path.Path.Components;
            var rootFolder = RootFolders[components[0]];
            EnsureFolderExists(rootFolder, components.Skip(1).ToList());
            return path;
        }

        private void EnsureFolderExists(Folder folder, IReadOnlyList<string> components)
        {
            if (folder.Files.ContainsKey(components[0]))
            {
                throw new IOException("Cannot create folder because a file already has that name");
            }

            Folder childFolder = null;
            if (folder.Folders.ContainsKey(components[0]))
            {
                childFolder = folder.Folders[components[0]];
            }
            else
            {
                childFolder = new Folder();
                folder.Folders[components[0]] = childFolder;
            }
            
            EnsureFolderExists(childFolder, components.Skip(1).ToList());
        }

        public override IObservable<AbsolutePath> Renamings(AbsolutePath path)
        {
            throw new NotImplementedException();
        }
    }
}