using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiveLinq.Set;
using SimpleMonads;
using UnitsNet;

namespace IoFluently
{
    /// <summary>
    /// An IIoService implementation that is backed by a zip file
    /// </summary>
    public class ZipIoService : IoServiceBase
    {
        private Folder _temporaryFolder;
        private bool _hasZipFileBeenCreatedYet = false;
        
        /// <summary>
        /// The path to the zip file
        /// </summary>
        public FileOrMissingPath ZipFilePath { get; }

        public ZipFolderMode FolderMode { get; set; } = ZipFolderMode.AllNonExistentPathsAreFolders;
        public override bool CanEmptyDirectoriesExist => FolderMode == ZipFolderMode.EmptyFilesAreDirectories;

        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Fastest;

        /// <summary>
        /// Creates a zip file IIoService
        /// </summary>
        /// <param name="zipFilePath">The path to the zip file</param>
        /// <param name="newline">The newline character(s) (e.g. '\n' or '\r\n')</param>
        /// <param name="enableOpenFilesTracking">Whether to enable the tracking of open files</param>
        public ZipIoService(FileOrMissingPath zipFilePath, bool enableOpenFilesTracking = false) : base(new OpenFilesTrackingService(enableOpenFilesTracking), true, "/")
        {
            if (zipFilePath == null)
            {
                throw new ArgumentNullException(nameof(zipFilePath));
            }
            
            ZipFilePath = zipFilePath;
            var path = new AbsolutePath(true, DefaultDirectorySeparator, this, new[] {"/"});
            DefaultRelativePathBase = new Folder(path);

            if (DefaultRelativePathBase == null)
            {
                throw new ArgumentNullException(nameof(DefaultRelativePathBase));
            }
        }

        public void Unzip(FolderOrMissingPath targetDirectory)
        {
            Copy(ParseAbsolutePath("/"), targetDirectory.Path);
        }

        public void Unzip(Folder targetDirectory)
        {
            Copy(ParseAbsolutePath("/"), targetDirectory.Path);
        }

        public void Zip(IHasAbsolutePath sourcePath, IHasAbsolutePath relativeTo)
        {
            Copy(sourcePath.Path, relativeTo.Path, ParseAbsolutePath("/"));
        }

        /// <inheritdoc />
        public override Folder DefaultRelativePathBase { get; protected set; }

        public override void SetDefaultRelativePathBase(Folder defaultRelativePathBase)
        {
            throw new NotImplementedException();
        }

        public override void UnsetDefaultRelativePathBase()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override IObservableReadOnlySet<Folder> Roots { get; } = new ObservableSet<Folder>();

        /// <inheritdoc />
        public override IQueryable<AbsolutePath> Query()
        {
            var archive = OpenZipArchive(false, true);
            var queryable = archive.Entries
                .Select(zipEntry => TryParseAbsolutePath(zipEntry.FullName, DefaultRelativePathBase).Value)
                .AsQueryable();
            return new QueryableWithDisposable<AbsolutePath>(queryable, archive);
        }

        /// <inheritdoc />
        public override ISetChanges<AbsolutePath> ToLiveLinq(Folder path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            throw new NotImplementedException();
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
        public override Folder GetTemporaryFolder()
        {
            return _temporaryFolder;
        }

        /// <inheritdoc />
        public override void UpdateRoots()
        {
            // There is only one root in a Zip file, so simply ignore this call
        }

        /// <inheritdoc />
        public override IEnumerable<FileOrFolder> Children(Folder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var regex = FileNamePatternToRegex(searchPattern);
                
                return archive.Entries.Select(entry =>
                    TryParseAbsolutePath(entry.FullName, DefaultRelativePathBase).Value).Where(child => child.IoService.TryParent(child).Value == path.Path)
                    .Where(x => regex.IsMatch(x))
                    .Select(path => path.ExpectFileOrFolder());
            }
        }

        /// <inheritdoc />
        public override IEnumerable<FileOrFolder> Descendants(Folder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var regex = FileNamePatternToRegex(searchPattern);
                
                return archive.Entries.Select(entry =>
                        TryParseAbsolutePath(entry.FullName, DefaultRelativePathBase).Value).Where(child => child.Ancestors
                        .Any(ancestor => ancestor == path.Path))
                    .Where(x => regex.IsMatch(x))
                    .Select(path => path.ExpectFileOrFolder());
            }
        }

        private ZipArchiveEntry GetZipArchiveEntry(ZipArchive archive, IHasAbsolutePath path)
        {
            var result = archive.GetEntry(path.Path.RelativeTo(DefaultRelativePathBase.Path).Simplify())
                ?? archive.GetEntry(path.Path.Simplify());
            return result;
        }

        /// <inheritdoc />
        public override MissingPath DeleteFile(File path)
        {
            using (var archive = OpenZipArchive(true, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, path.Path);
                if (zipEntry == null)
                {
                    throw new IOException($"Cannot delete {path} because it doesn't exist");
                }
                zipEntry.Delete();
            }
            
            return new MissingPath(path.Path);
        }

        /// <inheritdoc />
        public override AbsolutePath Decrypt(AbsolutePath path)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override AbsolutePath Encrypt(AbsolutePath path)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override Information FileSize(File path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, path);
                return Information.FromBytes(zipEntry.Length);
            }
        }

        /// <inheritdoc />
        public override FileAttributes Attributes(File attributes)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, attributes);
                if (zipEntry == null)
                {
                    throw new InvalidOperationException($"No such file {attributes}");
                }
                return ZipFilePath.Path.IoService.Attributes(ZipFilePath.Path);
            }
        }

        /// <inheritdoc />
        public override DateTimeOffset CreationTime(File path)
        {
            return ZipFilePath.IoService.CreationTime(ZipFilePath);
        }

        /// <inheritdoc />
        public override DateTimeOffset LastAccessTime(File path)
        {
            return ZipFilePath.IoService.LastAccessTime(ZipFilePath);
        }

        /// <inheritdoc />
        public override DateTimeOffset LastWriteTime(File path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, path);
                return zipEntry.LastWriteTime;
            }
        }

        /// <inheritdoc />
        public override PathType Type(AbsolutePath path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = GetZipArchiveEntry(archive, path);
                if (zipEntry == null)
                {
                    if (FolderMode == ZipFolderMode.AllNonExistentPathsAreFolders)
                    {
                        return IoFluently.PathType.Folder;
                    }

                    if (FolderMode == ZipFolderMode.DirectoriesExistIfTheyContainFiles)
                    {
                        var hasDescendants = archive.Entries.Any(entry =>
                            TryParseAbsolutePath(entry.FullName, DefaultRelativePathBase).Value.Ancestors
                                .Any(ancestor => ancestor == path));

                        if (hasDescendants)
                        {
                            return IoFluently.PathType.Folder;
                        }
                    }

                    return IoFluently.PathType.MissingPath;
                }

                if (FolderMode == ZipFolderMode.EmptyFilesAreDirectories && zipEntry.Length == 0)
                {
                    return IoFluently.PathType.Folder;
                }
                
                return IoFluently.PathType.File;
            }
        }

        public override async Task<MissingPath> DeleteFolderAsync(Folder path, CancellationToken cancellationToken, bool recursive = false)
        {
            return DeleteFolder(path, recursive);
        }

        public override async Task<MissingPath> DeleteFileAsync(File path, CancellationToken cancellationToken)
        {
            return DeleteFile(path);
        }

        /// <inheritdoc />
        public override MissingPath DeleteFolder(Folder path, bool recursive = false)
        {
            if (Type(path.Path) != IoFluently.PathType.Folder)
            {
                throw new IOException($"The path {path} is not a folder");
            }

            using (var zipArchive = OpenZipArchive(true, true))
            {
                foreach (var subZipEntry in zipArchive.Entries.Where(entry =>
                    TryParseAbsolutePath(entry.FullName, DefaultRelativePathBase).Value.Ancestors.Any(ancestor => ancestor == path.Path)))
                {
                    if (!recursive)
                    {
                        throw new IOException(
                            $"Cannot delete path {path} because it has files and/or folders inside it, and" +
                            $" the recursive parameter is false");
                    }
                    
                    subZipEntry.Delete();
                }
            }

            return new MissingPath(path.Path);
        }

        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.None | FileOptions.SequentialScan,
            Information? bufferSize = default, bool createRecursively = false)
        {
            if (!createRecursively)
            {
                // Ignore this flag because the default is true and it's impractical to pay attention to it
                // throw new InvalidOperationException(
                //     $"{nameof(createRecursively)} must always be true for {nameof(ZipIoService)}");
            }

            path = path.Simplify();
            var archive = OpenZipArchive(fileAccess != FileAccess.Read, true);
            
            var entry = GetZipArchiveEntry(archive, path);

            if (entry == null)
            {
                if (fileMode == FileMode.Create || fileMode == FileMode.CreateNew || fileMode == FileMode.OpenOrCreate)
                {
                    var entryName = path.Simplify();
                    entry = archive.CreateEntry(entryName.ToString(), CompressionLevel);
                    
                    return new StreamCloseDecorator(entry.Open(), () =>
                    {
                        archive.Dispose();
                    }).ToMaybe();
                }
                
                return Maybe<Stream>.Nothing(() => throw new IOException($"Cannot open the file {path} since doesn't exist"));
            }
            else
            {
                if (fileMode == FileMode.CreateNew)
                {
                    return Maybe<Stream>.Nothing(() => throw new IOException($"Cannot create the file {path} since it already exists"));
                }
                
                return new StreamCloseDecorator(entry.Open(), () =>
                {
                    archive.Dispose();
                }).ToMaybe();
            }
        }

        /// <inheritdoc />
        public override Folder CreateFolder(MissingPath path, bool createRecursively = true)
        {
            if (!createRecursively)
            {
                // Ignore this flag because the default is true and it's impractical to pay attention to it
                // throw new InvalidOperationException(
                //     $"{nameof(createRecursively)} must always be true for {nameof(ZipIoService)}");
            }
            // Zip files don't have folders, they just include a path in their name.
            // https://archive.fo/qIIIZ#24.12%
            return new Folder(path.Path);
        }

        /// <inheritdoc />
        public override IObservable<AbsolutePath> Renamings(AbsolutePath path)
        {
            throw new NotImplementedException();
        }

        private ZipArchive OpenZipArchive(bool willBeWriting, bool willBeReading)
        {
            var pathType = ZipFilePath.Path.Type;
            if (pathType == IoFluently.PathType.MissingPath)
            {
                var stream = ZipFilePath.Path.IoService.TryOpen(ZipFilePath.Path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, FileOptions.None).Value;
                var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, false);
                zipArchive.Dispose();
                
                if (willBeReading)
                {
                    zipArchive.Dispose();
                    stream = ZipFilePath.Path.IoService.TryOpen(ZipFilePath.Path, FileMode.Open, FileAccess.ReadWrite, FileShare.None, FileOptions.None).Value;
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
                var stream = ZipFilePath.Path.IoService.TryOpen(ZipFilePath.Path, FileMode.Open, willBeWriting ? FileAccess.ReadWrite : FileAccess.Read,
                    willBeWriting ? FileShare.None : FileShare.Read, FileOptions.None).Value;
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
        //             using (var stream = ZipFilePath.Open(FileMode.CreateNew, FileAccess.Write))
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