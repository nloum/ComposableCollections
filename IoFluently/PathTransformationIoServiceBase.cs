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
    public abstract class PathTransformationIoServiceBase : IoServiceBase
    {
        protected virtual File Transform(IFile absolutePath)
        {
            return Transform((IFileOrFolderOrMissingPath) absolutePath).ExpectFile();
        }

        protected virtual Folder Transform(IFolder absolutePath)
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

        protected PathTransformationIoServiceBase(IOpenFilesTrackingService openFilesTrackingService, bool isCaseSensitiveByDefault, string defaultDirectorySeparator,
            bool canEmptyDirectoriesExist = true, EmptyFolderMode? emptyFolderMode = null) : base(openFilesTrackingService, isCaseSensitiveByDefault, defaultDirectorySeparator)
        {
            CanEmptyDirectoriesExist = canEmptyDirectoriesExist;
            EmptyFolderMode = emptyFolderMode ?? EmptyFolderMode.FoldersOnlyExistIfTheyContainFiles;
        }

        protected abstract AbsolutePath Transform(IFileOrFolderOrMissingPath absolutePath);

        public override EmptyFolderMode EmptyFolderMode { get; }
        public override bool CanEmptyDirectoriesExist { get; }

        public override Folder CreateFolder(IMissingPath path, bool createRecursively = false)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            decorated.CreateFolder(transformedPath, createRecursively);
            return path.ExpectFolder();
        }

        public override MissingPath DeleteFolder(IFolder path, bool recursive = false)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            decorated.DeleteFolder(transformedPath, recursive);
            return path.ExpectMissingPath();
        }

        public override MissingPath DeleteFile(IFile path)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            decorated.DeleteFile(transformedPath);
            return path.ExpectMissingPath();
        }

        public override async Task<MissingPath> DeleteFolderAsync(IFolder path, CancellationToken cancellationToken, bool recursive = false)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            await decorated.DeleteFolderAsync(transformedPath, cancellationToken, recursive);
            return path.ExpectMissingPath();
        }

        public override async Task<MissingPath> DeleteFileAsync(IFile path, CancellationToken cancellationToken)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            await decorated.DeleteFileAsync(transformedPath, cancellationToken);
            return path.ExpectMissingPath();
        }

        public override IEnumerable<IFileOrFolder> EnumerateChildren(IFolder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            return decorated.EnumerateChildren(transformedPath, searchPattern, includeFolders, includeFiles)
                .Select(x => x.Collapse(
                    file => (IFileOrFolder)new File(path.Components.Concat(new[]{file.Name}).ToList(), path.IsCaseSensitive, path.DirectorySeparator, this),
                    folder => new Folder(path.Components.Concat(new[]{folder.Name}).ToList(), path.IsCaseSensitive, path.DirectorySeparator, this)));
        }

        public override PathType Type(IFileOrFolderOrMissingPath path)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            return decorated.Type(transformedPath);
        }

        public override Information FileSize(IFile path)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            return decorated.FileSize(transformedPath);
        }

        public override FileAttributes Attributes(IFile attributes)
        {
            var transformedPath = Transform   (attributes);
            var decorated = transformedPath.IoService;
            return decorated.Attributes(transformedPath);
        }

        public override DateTimeOffset CreationTime(IFile attributes)
        {
            var transformedPath = Transform   (attributes);
            var decorated = transformedPath.IoService;
            return decorated.CreationTime(transformedPath);
        }

        public override DateTimeOffset LastAccessTime(IFile attributes)
        {
            var transformedPath = Transform   (attributes);
            var decorated = transformedPath.IoService;
            return decorated.LastAccessTime(transformedPath);
        }

        public override DateTimeOffset LastWriteTime(IFile attributes)
        {
            var transformedPath = Transform   (attributes);
            var decorated = transformedPath.IoService;
            return decorated.LastWriteTime(transformedPath);
        }

        public override Stream Open(IFileOrMissingPath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.None | FileOptions.SequentialScan | FileOptions.Asynchronous,
            Information? bufferSize = default, bool createRecursively = false)
        {
            var transformedPath = Transform   (path);
            var decorated = transformedPath.IoService;
            return decorated.Open(transformedPath, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public override IQueryable<AbsolutePath> Query()
        {
            throw new NotImplementedException();
        }

        public override ISetChanges<AbsolutePath> ToLiveLinq(IFolder path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            throw new NotImplementedException();
        }
    }
}