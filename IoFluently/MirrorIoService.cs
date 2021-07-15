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
    public abstract class MirrorIoService : IoServiceBase
    {
        private IIoService _decorated;

        protected abstract File Wrap(IFile absolutePath);
        protected abstract File Unwrap(IFile absolutePath);
        protected abstract Folder Wrap(IFolder absolutePath);
        protected abstract Folder Unwrap(IFolder absolutePath);
        protected abstract IFileOrFolder Wrap(IFileOrFolder absolutePath);
        protected abstract IFileOrFolder Unwrap(IFileOrFolder absolutePath);
        protected abstract IFileOrMissingPath Wrap(IFileOrMissingPath absolutePath);
        protected abstract IFileOrMissingPath Unwrap(IFileOrMissingPath absolutePath);
        protected abstract MissingPath Wrap(IMissingPath absolutePath);
        protected abstract MissingPath Unwrap(IMissingPath absolutePath);
        protected abstract AbsolutePath Wrap(IFileOrFolderOrMissingPath absolutePath);
        protected abstract AbsolutePath Unwrap(IFileOrFolderOrMissingPath absolutePath);

        protected MirrorIoService(IIoService decorated) : base(decorated.OpenFilesTrackingService, decorated.IsCaseSensitiveByDefault, decorated.DefaultDirectorySeparator)
        {
            _decorated = decorated;
        }

        public override EmptyFolderMode EmptyFolderMode => _decorated.EmptyFolderMode;
        public override bool CanEmptyDirectoriesExist => _decorated.CanEmptyDirectoriesExist;

        public override Folder CreateFolder(IMissingPath path, bool createRecursively = false)
        {
            return Wrap(_decorated.CreateFolder(Unwrap(path), createRecursively));
        }

        public override MissingPath DeleteFolder(IFolder path, bool recursive = false)
        {
            return Wrap(_decorated.DeleteFolder(Unwrap(path), recursive));
        }

        public override MissingPath DeleteFile(IFile path)
        {
            return Wrap(_decorated.DeleteFile(Unwrap(path)));
        }

        public override async Task<MissingPath> DeleteFolderAsync(IFolder path, CancellationToken cancellationToken, bool recursive = false)
        {
            return Wrap(await _decorated.DeleteFolderAsync(Unwrap(path), cancellationToken, recursive));
        }

        public override async Task<MissingPath> DeleteFileAsync(IFile path, CancellationToken cancellationToken)
        {
            return Wrap(await _decorated.DeleteFileAsync(Unwrap(path), cancellationToken));
        }

        public override IEnumerable<IFileOrFolder> Children(IFolder path, string searchPattern = null, bool includeFolders = true, bool includeFiles = true)
        {
            return _decorated.Children(Unwrap(path), searchPattern, includeFolders, includeFiles)
                .Select(x => Wrap(x));
        }

        public override PathType Type(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Type(Unwrap(path));
        }

        public override Information FileSize(IFile path)
        {
            return _decorated.FileSize(Unwrap(path));
        }

        public override FileAttributes Attributes(IFile attributes)
        {
            return _decorated.Attributes(Unwrap(attributes));
        }

        public override DateTimeOffset CreationTime(IFile attributes)
        {
            return _decorated.CreationTime(Unwrap(attributes));
        }

        public override DateTimeOffset LastAccessTime(IFile attributes)
        {
            return _decorated.LastAccessTime(Unwrap(attributes));
        }

        public override DateTimeOffset LastWriteTime(IFile attributes)
        {
            return _decorated.LastWriteTime(Unwrap(attributes));
        }

        public override Stream Open(IFileOrMissingPath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.None | FileOptions.SequentialScan | FileOptions.Asynchronous,
            Information? bufferSize = default, bool createRecursively = false)
        {
            return _decorated.Open(Unwrap(path), fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
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