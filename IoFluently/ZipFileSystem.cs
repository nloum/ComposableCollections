using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiveLinq.Set;
using UnitsNet;

namespace IoFluently
{
    public static class ZipIoExtensions
    {
        public static ZipFileSystem ExpectZipFileOrMissingPath(this IFileOrFolderOrMissingPath path, bool enableOpenFilesTracking = false)
        {
            return new ZipFileSystem(path.ExpectFileOrMissingPath(), enableOpenFilesTracking);
        }
        
        public static ZipFileSystem ExpectZipFile(this IFileOrFolderOrMissingPath path, bool enableOpenFilesTracking = false)
        {
            return new ZipFileSystem(path.ExpectFile(), enableOpenFilesTracking);
        }
    }
    
    /// <summary>
    /// An IIoService implementation that is backed by a zip file
    /// </summary>
    public class ZipFileSystem : FileSystemBase
    {
        private bool _hasZipFileBeenCreatedYet = false;
        private EmptyFolderMode _emptyFolderMode = EmptyFolderMode.AllNonExistentPathsAreFolders;
        private readonly FolderPath _root;
        private readonly ObservableSet<FolderPath> _roots = new();
        
        /// <summary>
        /// The path to the zip file
        /// </summary>
        public IFileOrMissingPath ZipFilePath { get; }

        public override EmptyFolderMode EmptyFolderMode => _emptyFolderMode;

        public void UpdateEmptyFolderMode(EmptyFolderMode newMode)
        {
            _emptyFolderMode = newMode;
        }

        /// <inheritdoc />
        public override bool CanEmptyDirectoriesExist => EmptyFolderMode == EmptyFolderMode.EmptyFilesAreFolders;

        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Fastest;

        /// <inheritdoc />
        public override IObservableReadOnlySet<FolderPath> Roots => _roots;

        public override FolderPath DefaultRoot => ParseAbsolutePath("/").ExpectFolder();

        /// <summary>
        /// Creates a zip file IIoService
        /// </summary>
        /// <param name="zipFilePath">The path to the zip file</param>
        /// <param name="newline">The newline character(s) (e.g. '\n' or '\r\n')</param>
        /// <param name="enableOpenFilesTracking">Whether to enable the tracking of open files</param>
        public ZipFileSystem(IFileOrMissingPath zipFilePath, bool enableOpenFilesTracking = false) : base(new OpenFilesTrackingService(enableOpenFilesTracking), true, "/")
        {
            if (zipFilePath == null)
            {
                throw new ArgumentNullException(nameof(zipFilePath));
            }
            
            ZipFilePath = zipFilePath;
            var path = new FileOrFolderOrMissingPath(new[] {"/"}, true, DefaultDirectorySeparator, this);
            _root = new FolderPath(path.Components, path.IsCaseSensitive, path.DirectorySeparator, this, true);
            _roots.Add(_root);
        }

        public void Unzip(IFolderOrMissingPath targetDirectory)
        {
            Copy(ParseAbsolutePath("/"), targetDirectory );
        }

        public void Unzip(IFolderPath targetDirectory)
        {
            Copy(ParseAbsolutePath("/"), targetDirectory);
        }

        public void Zip(IFileOrFolderOrMissingPath sourcePath, IFileOrFolderOrMissingPath relativeTo)
        {
            Copy(sourcePath , relativeTo , ParseAbsolutePath("/"));
        }

        /// <inheritdoc />
        public override IQueryable<FileOrFolderOrMissingPath> Query()
        {
            var archive = OpenZipArchive(false, true);
            var queryable = archive.Entries
                .Select(zipEntry => TryParseAbsolutePath(zipEntry.FullName, _root).Value)
                .AsQueryable();
            return new QueryableWithDisposable<FileOrFolderOrMissingPath>(queryable, archive);
        }

        /// <inheritdoc />
        public override ISetChanges<FileOrFolderOrMissingPath> ToLiveLinq(IFolderPath folderPath, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void UpdateRoots()
        {
            // There is only one root in a Zip file, so simply ignore this call
        }

        /// <inheritdoc />
        public override IEnumerable<IFileOrFolderPath> EnumerateChildren(IFolderPath folderPath, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var regex = FileNamePatternToRegex(searchPattern);
                
                return archive.Entries.Select(entry =>
                    TryParseAbsolutePath(entry.FullName, _root).Value).Where(child => child.FileSystem.TryParent(child).Value == folderPath )
                    .Where(x => regex.IsMatch(x))
                    .Select(path => path.ExpectFileOrFolder());
            }
        }

        /// <inheritdoc />
        public override IEnumerable<IFileOrFolderPath> EnumerateDescendants(IFolderPath folderPath, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var regex = FileNamePatternToRegex(searchPattern);
                
                return archive.Entries.Select(entry =>
                        TryParseAbsolutePath(entry.FullName, _root).Value).Where(child => child.Ancestors(true)
                        .Any(ancestor => ancestor.FullName.Equals(folderPath.FullName)))
                    .Where(x => regex.IsMatch(x))
                    .Select(path => path.ExpectFileOrFolder());
            }
        }

        private ZipArchiveEntry GetZipArchiveEntry(ZipArchive archive, IFileOrFolderOrMissingPath path)
        {
            var result = archive.GetEntry(path .RelativeTo(_root).Simplify())
                ?? archive.GetEntry(path .Simplify());
            return result;
        }

        /// <inheritdoc />
        public override MissingPath DeleteFile(IFilePath path)
        {
            using (var archive = OpenZipArchive(true, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, path );
                if (zipEntry == null)
                {
                    throw new IOException($"Cannot delete {path} because it doesn't exist");
                }
                zipEntry.Delete();
            }
            
            return new MissingPath(path );
        }

        /// <inheritdoc />
        public override Information FileSize(IFilePath path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, path);
                return Information.FromBytes(zipEntry.Length);
            }
        }

        /// <inheritdoc />
        public override FileAttributes Attributes(IFilePath attributes)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, attributes);
                if (zipEntry == null)
                {
                    throw new InvalidOperationException($"No such file {attributes}");
                }
                return ZipFilePath .FileSystem.Attributes(ZipFilePath.ExpectFile());
            }
        }

        /// <inheritdoc />
        public override DateTimeOffset CreationTime(IFilePath path)
        {
            return ZipFilePath.FileSystem.CreationTime(ZipFilePath.ExpectFile());
        }

        /// <inheritdoc />
        public override DateTimeOffset LastAccessTime(IFilePath path)
        {
            return ZipFilePath.FileSystem.LastAccessTime(ZipFilePath.ExpectFile());
        }

        /// <inheritdoc />
        public override DateTimeOffset LastWriteTime(IFilePath path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, path);
                return zipEntry.LastWriteTime;
            }
        }

        /// <inheritdoc />
        public override PathType Type(IFileOrFolderOrMissingPath path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, path);
                if (zipEntry == null)
                {
                    if (EmptyFolderMode == EmptyFolderMode.AllNonExistentPathsAreFolders)
                    {
                        return IoFluently.PathType.Folder;
                    }

                    if (EmptyFolderMode == EmptyFolderMode.FoldersOnlyExistIfTheyContainFiles)
                    {
                        var hasDescendants = archive.Entries.Any(entry =>
                            TryParseAbsolutePath(entry.FullName, _root).Value.Ancestors(true)
                                .Any(ancestor => ancestor.FullName.Equals(path.FullName)));

                        if (hasDescendants)
                        {
                            return IoFluently.PathType.Folder;
                        }
                    }

                    return IoFluently.PathType.MissingPath;
                }

                if (EmptyFolderMode == EmptyFolderMode.EmptyFilesAreFolders && zipEntry.Length == 0)
                {
                    return IoFluently.PathType.Folder;
                }
                
                return IoFluently.PathType.File;
            }
        }

        public override async Task<MissingPath> DeleteFolderAsync(IFolderPath folderPath, CancellationToken cancellationToken,  bool recursive = true)
        {
            return DeleteFolder(folderPath, recursive);
        }

        public override async Task<MissingPath> DeleteFileAsync(IFilePath path, CancellationToken cancellationToken)
        {
            return DeleteFile(path);
        }

        /// <inheritdoc />
        public override MissingPath DeleteFolder(IFolderPath folderPath,  bool recursive = true)
        {
            if (Type(folderPath) != IoFluently.PathType.Folder)
            {
                throw new IOException($"The path {folderPath} is not a folder");
            }

            using (var zipArchive = OpenZipArchive(true, true))
            {
                foreach (var subZipEntry in zipArchive.Entries.Where(entry =>
                    TryParseAbsolutePath(entry.FullName, _root).Value.Ancestors(true).Any(ancestor => ancestor == folderPath )))
                {
                    if (!recursive)
                    {
                        throw new IOException(
                            $"Cannot delete path {folderPath} because it has files and/or folders inside it, and" +
                            $" the recursive parameter is false");
                    }
                    
                    subZipEntry.Delete();
                }
            }

            return new MissingPath(folderPath );
        }

        public override Stream Open(IFileOrMissingPath fileOrMissingPath, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.None | FileOptions.SequentialScan,
            Information? bufferSize = default,  bool createRecursively = true)
        {
            if (!createRecursively)
            {
                // Ignore this flag because the default is true and it's impractical to pay attention to it
                // throw new InvalidOperationException(
                //     $"{nameof(createRecursively)} must always be true for {nameof(ZipIoService)}");
            }

            var path = fileOrMissingPath .Simplify();
            var archive = OpenZipArchive(fileAccess != FileAccess.Read, true);
            
            var entry = GetZipArchiveEntry(archive, path);

            if (entry == null)
            {
                if (fileMode == FileMode.Create || fileMode == FileMode.CreateNew || fileMode == FileMode.OpenOrCreate)
                {
                    var entryName = path.Simplify();
                    entry = archive.CreateEntry(entryName.FullName, CompressionLevel);

                    return new StreamCloseDecorator(entry.Open(), () =>
                    {
                        archive.Dispose();
                    });
                }
                
                throw new IOException($"Cannot open the file {path} since doesn't exist");
            }
            else
            {
                if (fileMode == FileMode.CreateNew)
                {
                    throw new IOException($"Cannot create the file {path} since it already exists");
                }
                
                return new StreamCloseDecorator(entry.Open(), () =>
                {
                    archive.Dispose();
                });
            }
        }

        /// <inheritdoc />
        public override FolderPath CreateFolder(IMissingPath path, bool createRecursively = true)
        {
            if (!createRecursively)
            {
                // Ignore this flag because the default is true and it's impractical to pay attention to it
                // throw new InvalidOperationException(
                //     $"{nameof(createRecursively)} must always be true for {nameof(ZipIoService)}");
            }
            // Zip files don't have folders, they just include a path in their name.
            // https://archive.fo/qIIIZ#24.12%
            return new FolderPath(path);
        }

        private ZipArchive OpenZipArchive(bool willBeWriting, bool willBeReading)
        {
            var pathType = ZipFilePath .Type;
            if (pathType == IoFluently.PathType.MissingPath)
            {
                var stream = ZipFilePath .FileSystem.Open(ZipFilePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, FileOptions.None);
                var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, false);
                zipArchive.Dispose();
                
                if (willBeReading)
                {
                    zipArchive.Dispose();
                    stream = ZipFilePath .FileSystem.Open(ZipFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None, FileOptions.None);
                    zipArchive = new ZipArchive(stream, ZipArchiveMode.Update, false);
                    return zipArchive;
                }
                else
                {
                    return zipArchive;
                }
            }
            else if (pathType == IoFluently.PathType.Folder)
            {
                throw new IOException($"Cannot open {ZipFilePath} as a zip file because it is a folder");
            }
            else
            {
                var stream = ZipFilePath .FileSystem.Open(ZipFilePath, FileMode.Open, willBeWriting ? FileAccess.ReadWrite : FileAccess.Read,
                    willBeWriting ? FileShare.None : FileShare.Read, FileOptions.None);
                return new ZipArchive(stream, willBeWriting ? ZipArchiveMode.Update : ZipArchiveMode.Read, false);
            }
        }

        // private void EnsureZipFileExists()
        // {
        //     if (_hasZipFileBeenCreatedYet)
        //     {
        //         return;
        //     }
        //
        //     var pathType = ZipFilePath.GetPathType();
        //     switch (pathType)
        //     {
        //         case PathType.File:
        //             return;
        //         case PathType.Folder:
        //             throw new IOException(
        //                 $"The path {ZipFilePath} was supposed to be a ZIP file but it's a folder instead");
        //         default:
        //             using (var stream = ZipFilePath.Open(IFileMode.CreateNew, FileAccess.Write))
        //             {
        //                 // Write an empty zip file
        //                 var buffer = new byte[]
        //                 {
        //                     80, 75, 5, 6, 0, 0, 0, 0, 0,
        //                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        //                 };
        //                 stream.Write(buffer, 0, buffer.Length);
        //                 stream.Flush();
        //             }
        //
        //             break;
        //     }
        //     
        //     _hasZipFileBeenCreatedYet = true;
        // }
    }
}