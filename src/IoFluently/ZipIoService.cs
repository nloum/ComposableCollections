using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
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
        private AbsolutePath _temporaryFolder;
        private bool _hasZipFileBeenCreatedYet = false;
        
        /// <summary>
        /// The path to the zip file
        /// </summary>
        public AbsolutePath ZipFilePath { get; }

        /// <summary>
        /// Creates a zip file IIoService
        /// </summary>
        /// <param name="zipFilePath">The path to the zip file</param>
        /// <param name="enableOpenFilesTracking">Whether to enable the tracking of open files</param>
        public ZipIoService(AbsolutePath zipFilePath, bool enableOpenFilesTracking = false) : base(new OpenFilesTrackingService(enableOpenFilesTracking), "\n")
        {
            ZipFilePath = zipFilePath;
            CurrentDirectory = ParseAbsolutePath("/");
        }

        /// <summary>
        /// Creates a zip file IIoService
        /// </summary>
        /// <param name="zipFilePath">The path to the zip file</param>
        /// <param name="newline">The newline character(s) (e.g. '\n' or '\r\n')</param>
        /// <param name="enableOpenFilesTracking">Whether to enable the tracking of open files</param>
        public ZipIoService(AbsolutePath zipFilePath, string newline, bool enableOpenFilesTracking = false) : base(new OpenFilesTrackingService(enableOpenFilesTracking), newline)
        {
            ZipFilePath = zipFilePath;
            CurrentDirectory = ParseAbsolutePath("/");
        }

        /// <inheritdoc />
        public override AbsolutePath CurrentDirectory { get; }

        /// <inheritdoc />
        public override IObservableReadOnlySet<AbsolutePath> Roots { get; } = new ObservableSet<AbsolutePath>();

        /// <inheritdoc />
        public override IQueryable<AbsolutePath> Query()
        {
            var archive = OpenZipArchive(false, true);
            var queryable = archive.Entries
                .Select(zipEntry => ParseAbsolutePath(zipEntry.FullName, CurrentDirectory))
                .AsQueryable();
            return new QueryableWithDisposable<AbsolutePath>(queryable, archive);
        }

        /// <inheritdoc />
        public override string GetDefaultDirectorySeparatorForThisEnvironment()
        {
            // This is part of the ZIP spec
            return "/";
        }

        /// <inheritdoc />
        public override ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override IMaybe<StreamWriter> TryOpenWriter(AbsolutePath pathSpec)
        {
            return TryOpen(pathSpec, FileMode.OpenOrCreate, FileAccess.Write).Select(stream => new StreamWriter(stream));
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
        public override AbsolutePath GetTemporaryFolder()
        {
            return _temporaryFolder;
        }

        /// <inheritdoc />
        public override bool IsCaseSensitiveByDefault()
        {
            // Technically zip files can contain multiple files with the same name, so to be closer to this reality we just
            // act like case sensitivity is always on for zip files.
            return true;
        }

        /// <inheritdoc />
        public override void UpdateRoots()
        {
            // There is never any storage, so simply ignore this call
        }

        /// <inheritdoc />
        public override IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, bool includeFolders = true, bool includeFiles = true)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                return archive.Entries.Select(entry =>
                    ParseAbsolutePath(entry.FullName, CurrentDirectory)).Where(child => child.Ancestors()
                        .Any(ancestor => ancestor == path));
            }
        }

        /// <inheritdoc />
        public override AbsolutePath DeleteFile(AbsolutePath path)
        {
            using (var archive = OpenZipArchive(true, true))
            {
                var zipEntry = archive.GetEntry(path.RelativeTo(CurrentDirectory).ToString());
                if (zipEntry == null)
                {
                    throw new IOException($"Cannot delete {path} because it doesn't exist");
                }
                zipEntry.Delete();
            }
            
            return path;
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
        public override IMaybe<Information> TryFileSize(AbsolutePath path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = archive.GetEntry(path.RelativeTo(CurrentDirectory).ToString());
                if (zipEntry == null)
                {
                    return Maybe<Information>.Nothing();
                }
                return Information.FromBytes(zipEntry.Length).ToMaybe();
            }
        }

        /// <inheritdoc />
        public override IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = archive.GetEntry(ZipFilePath.RelativeTo(CurrentDirectory).ToString());
                if (zipEntry == null)
                {
                    return Maybe<FileAttributes>.Nothing();
                }
                return ZipFilePath.TryAttributes();
            }
        }

        /// <inheritdoc />
        public override IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath path)
        {
            return Maybe<DateTimeOffset>.Nothing();
        }

        /// <inheritdoc />
        public override IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath path)
        {
            return Maybe<DateTimeOffset>.Nothing();
        }

        /// <inheritdoc />
        public override IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = archive.GetEntry(path.RelativeTo(CurrentDirectory).ToString());
                if (zipEntry == null)
                {
                    return Maybe<DateTimeOffset>.Nothing();
                }
                return zipEntry.LastWriteTime.ToMaybe();
            }
        }

        /// <inheritdoc />
        public override PathType GetPathType(AbsolutePath path)
        {
            using (var archive = OpenZipArchive(false, true))
            {
                var zipEntry = archive.GetEntry(path.RelativeTo(CurrentDirectory).ToString());
                if (zipEntry == null)
                {
                    var hasDescendants = archive.Entries.Any(entry =>
                            ParseAbsolutePath(entry.FullName, CurrentDirectory).Ancestors()
                                .Any(ancestor => ancestor == path));

                    if (hasDescendants)
                    {
                        return PathType.Folder;
                    }

                    return PathType.None;
                }

                return PathType.File;
            }
        }

        /// <inheritdoc />
        public override AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false)
        {
            if (GetPathType(path) != PathType.Folder)
            {
                throw new IOException($"The path {path} is not a folder");
            }

            using (var zipArchive = OpenZipArchive(true, true))
            {
                foreach (var subZipEntry in zipArchive.Entries.Where(entry =>
                    ParseAbsolutePath(entry.FullName, CurrentDirectory).Ancestors().Any(ancestor => ancestor == path)))
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

            return path;
        }

        /// <inheritdoc />
        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            var archive = OpenZipArchive(fileAccess != FileAccess.Read, true);
            
            var entry = archive.GetEntry(path.RelativeTo(CurrentDirectory).ToString());

            if (entry == null)
            {
                if (fileMode == FileMode.Create || fileMode == FileMode.CreateNew || fileMode == FileMode.OpenOrCreate)
                {
                    entry = archive.CreateEntry(path.RelativeTo(CurrentDirectory).ToString());
                    
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
        public override AbsolutePath CreateFolder(AbsolutePath path)
        {
            // Zip files don't have folders, they just include a path in their name.
            // https://archive.fo/qIIIZ#24.12%
            return path;
        }

        /// <inheritdoc />
        public override IObservable<AbsolutePath> Renamings(AbsolutePath path)
        {
            throw new NotImplementedException();
        }

        private ZipArchive OpenZipArchive(bool willBeWriting, bool willBeReading)
        {
            var pathType = ZipFilePath.GetPathType();
            if (pathType == PathType.None)
            {
                var stream = ZipFilePath.Open(FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
                var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, false);
                zipArchive.Dispose();
                
                if (willBeReading)
                {
                    zipArchive.Dispose();
                    stream = ZipFilePath.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    zipArchive = new ZipArchive(stream, ZipArchiveMode.Update, false);
                    return zipArchive;
                }
                else
                {
                    return zipArchive;
                }
            }
            else if (pathType == PathType.Folder)
            {
                throw new IOException($"Cannot open {ZipFilePath} as a zip file because it is a folder");
            }
            else
            {
                var stream = ZipFilePath.Open(FileMode.Open, willBeWriting ? FileAccess.ReadWrite : FileAccess.Read,
                    willBeWriting ? FileShare.None : FileShare.Read);
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