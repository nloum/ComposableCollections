using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiveLinq.Set;
using UnitsNet;

namespace IoFluently
{
    public abstract class PathTransformationFileSystemBase : FileSystemBase
    {
        protected virtual FilePath Transform(IFilePath absolutePath)
        {
            return Transform((IFileOrFolderOrMissingPath) absolutePath).ExpectFile();
        }

        protected virtual FolderPath Transform(IFolderPath absolutePath)
        {
            return Transform((IFileOrFolderOrMissingPath) absolutePath).ExpectFolder();
        }

        protected virtual IFileOrFolder Transform(IFileOrFolder absolutePath)
        {
            return Transform((IFileOrFolderOrMissingPath) absolutePath).ExpectFileOrFolder();
        }

        protected virtual IFileOrMissingPath Transform(IFileOrMissingPath absolutePath)
        {
            return Transform((IFileOrFolderOrMissingPath) absolutePath).ExpectFileOrMissingPath();
        }

        protected virtual MissingPath Transform(IMissingPath absolutePath)
        {
            return Transform((IFileOrFolderOrMissingPath) absolutePath).ExpectMissingPath();
        }

        protected PathTransformationFileSystemBase(IOpenFilesTrackingService openFilesTrackingService, bool isCaseSensitiveByDefault, string defaultDirectorySeparator,
            bool canEmptyDirectoriesExist = true, EmptyFolderMode? emptyFolderMode = null) : base(openFilesTrackingService, isCaseSensitiveByDefault, defaultDirectorySeparator)
        {
            CanEmptyDirectoriesExist = canEmptyDirectoriesExist;
            EmptyFolderMode = emptyFolderMode ?? EmptyFolderMode.FoldersOnlyExistIfTheyContainFiles;
        }

        protected abstract AbsolutePath Transform(IFileOrFolderOrMissingPath absolutePath);

        public override EmptyFolderMode EmptyFolderMode { get; }
        public override bool CanEmptyDirectoriesExist { get; }

        public override FolderPath CreateFolder(IMissingPath path,  bool createRecursively = true)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            decorated.CreateFolder(transformedPath, createRecursively);
            return path.ExpectFolder();
        }

        public override MissingPath DeleteFolder(IFolderPath path,  bool recursive = true)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            decorated.DeleteFolder(transformedPath, recursive);
            return path.ExpectMissingPath();
        }

        public override MissingPath DeleteFile(IFilePath path)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            decorated.DeleteFile(transformedPath);
            return path.ExpectMissingPath();
        }

        public override async Task<MissingPath> DeleteFolderAsync(IFolderPath path, CancellationToken cancellationToken,  bool recursive = true)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            await decorated.DeleteFolderAsync(transformedPath, cancellationToken, recursive);
            return path.ExpectMissingPath();
        }

        public override async Task<MissingPath> DeleteFileAsync(IFilePath path, CancellationToken cancellationToken)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            await decorated.DeleteFileAsync(transformedPath, cancellationToken);
            return path.ExpectMissingPath();
        }

        public override IEnumerable<IFileOrFolder> EnumerateChildren(IFolderPath path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            return decorated.EnumerateChildren(transformedPath, searchPattern, includeFolders, includeFiles)
                .Select(x => x.Collapse(
                    file => (IFileOrFolder)new FilePath(path.Components.Concat(new[]{file.Name}).ToList(), path.IsCaseSensitive, path.DirectorySeparator, this),
                    folder => new FolderPath(path.Components.Concat(new[]{folder.Name}).ToList(), path.IsCaseSensitive, path.DirectorySeparator, this)));
        }

        public override PathType Type(IFileOrFolderOrMissingPath path)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            return decorated.Type(transformedPath);
        }

        public override Information FileSize(IFilePath path)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            return decorated.FileSize(transformedPath);
        }

        public override FileAttributes Attributes(IFilePath attributes)
        {
            var transformedPath = Transform   (attributes);
            var decorated = transformedPath.FileSystem;
            return decorated.Attributes(transformedPath);
        }

        public override DateTimeOffset CreationTime(IFilePath attributes)
        {
            var transformedPath = Transform   (attributes);
            var decorated = transformedPath.FileSystem;
            return decorated.CreationTime(transformedPath);
        }

        public override DateTimeOffset LastAccessTime(IFilePath attributes)
        {
            var transformedPath = Transform   (attributes);
            var decorated = transformedPath.FileSystem;
            return decorated.LastAccessTime(transformedPath);
        }

        public override DateTimeOffset LastWriteTime(IFilePath attributes)
        {
            var transformedPath = Transform   (attributes);
            var decorated = transformedPath.FileSystem;
            return decorated.LastWriteTime(transformedPath);
        }

        public override Stream Open(IFileOrMissingPath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.None | FileOptions.SequentialScan | FileOptions.Asynchronous,
            Information? bufferSize = default,  bool createRecursively = true)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.FileSystem;
            return decorated.Open(transformedPath, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public override IQueryable<AbsolutePath> Query()
        {
            throw new NotImplementedException();
        }

        public override ISetChanges<AbsolutePath> ToLiveLinq(IFolderPath path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            throw new NotImplementedException();
        }
    }
}