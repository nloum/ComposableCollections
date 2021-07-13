using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using LiveLinq.Set;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using SimpleMonads;
using UnitsNet;

namespace IoFluently
{
    public abstract class IoServiceDecoratorBase : IIoService
    {
        private readonly IIoService _decorated;

        public IoServiceDecoratorBase(IIoService decorated)
        {
            _decorated = decorated;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return _decorated.GetFileInfo(subpath);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return _decorated.GetDirectoryContents(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return _decorated.Watch(filter);
        }

        public Information DefaultBufferSize
        {
            get => _decorated.DefaultBufferSize;
            set => _decorated.DefaultBufferSize = value;
        }

        public int GetBufferSizeOrDefaultInBytes(Information? bufferSize)
        {
            return _decorated.GetBufferSizeOrDefaultInBytes(bufferSize);
        }

        public EmptyFolderMode EmptyFolderMode => _decorated.EmptyFolderMode;

        public bool CanEmptyDirectoriesExist => _decorated.CanEmptyDirectoriesExist;

        public void SetDefaultRelativePathBase(IFolder defaultRelativePathBase)
        {
            _decorated.SetDefaultRelativePathBase(defaultRelativePathBase);
        }

        public void UnsetDefaultRelativePathBase()
        {
            _decorated.UnsetDefaultRelativePathBase();
        }

        public Folder DefaultRelativePathBase => _decorated.DefaultRelativePathBase;

        public IObservableReadOnlySet<Folder> Roots => _decorated.Roots;

        public string DefaultDirectorySeparator => _decorated.DefaultDirectorySeparator;

        public bool IsCaseSensitiveByDefault => _decorated.IsCaseSensitiveByDefault;

        public Folder GetTemporaryFolder()
        {
            return _decorated.GetTemporaryFolder();
        }

        public Folder CreateFolder(IMissingPath path, bool createRecursively = false)
        {
            return _decorated.CreateFolder(path, createRecursively);
        }

        public MissingPath DeleteFolder(IFolder path, bool recursive = false)
        {
            return _decorated.DeleteFolder(path, recursive);
        }

        public MissingPath DeleteFile(IFile path)
        {
            return _decorated.DeleteFile(path);
        }

        public MissingPath Delete(IFileOrFolder path, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.Delete(path, recursiveDeleteIfFolder);
        }

        public Task<MissingPath> DeleteFolderAsync(IFolder path, CancellationToken cancellationToken, bool recursive = false)
        {
            return _decorated.DeleteFolderAsync(path, cancellationToken, recursive);
        }

        public Task<MissingPath> DeleteFileAsync(IFile path, CancellationToken cancellationToken)
        {
            return _decorated.DeleteFileAsync(path, cancellationToken);
        }

        public Task<MissingPath> DeleteAsync(IFileOrFolder path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.DeleteAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public Folder EnsureIsFolder(IFileOrFolderOrMissingPath path, bool createRecursively = false)
        {
            return _decorated.EnsureIsFolder(path, createRecursively);
        }

        public Folder EnsureIsEmptyFolder(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true,
            bool createRecursively = false)
        {
            return _decorated.EnsureIsEmptyFolder(path, recursiveDeleteIfFolder, createRecursively);
        }

        public Task<Folder> EnsureIsFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool createRecursively = false)
        {
            return _decorated.EnsureIsFolderAsync(path, cancellationToken, createRecursively);
        }

        public Task<Folder> EnsureIsEmptyFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true, bool createRecursively = false)
        {
            return _decorated.EnsureIsEmptyFolderAsync(path, cancellationToken, recursiveDeleteIfFolder, createRecursively);
        }

        public IFileOrMissingPath EnsureIsNotFolder(IFileOrFolderOrMissingPath path, bool recursive = false)
        {
            return _decorated.EnsureIsNotFolder(path, recursive);
        }

        public IFolderOrMissingPath EnsureIsNotFile(IFileOrFolderOrMissingPath path)
        {
            return _decorated.EnsureIsNotFile(path);
        }

        public MissingPath EnsureDoesNotExist(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.EnsureDoesNotExist(path, recursiveDeleteIfFolder);
        }

        public Task<IFileOrMissingPath> EnsureIsNotFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursive = false)
        {
            return _decorated.EnsureIsNotFolderAsync(path, cancellationToken, recursive);
        }

        public Task<IFolderOrMissingPath> EnsureIsNotFileAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken)
        {
            return _decorated.EnsureIsNotFileAsync(path, cancellationToken);
        }

        public Task<MissingPath> EnsureDoesNotExistAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true)
        {
            return _decorated.EnsureDoesNotExistAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public void UpdateRoots()
        {
            _decorated.UpdateRoots();
        }

        public bool HasExtension(IFileOrFolderOrMissingPath path, string extension)
        {
            return _decorated.HasExtension(path, extension);
        }

        public string Name(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Name(path);
        }

        public string? Extension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Extension(path);
        }

        public bool MayCreateFile(FileMode fileMode)
        {
            return _decorated.MayCreateFile(fileMode);
        }

        public bool IsImageUri(Uri uri)
        {
            return _decorated.IsImageUri(uri);
        }

        public bool IsVideoUri(Uri uri)
        {
            return _decorated.IsVideoUri(uri);
        }

        public string StripQuotes(string str)
        {
            return _decorated.StripQuotes(str);
        }

        public string SurroundWithDoubleQuotesIfNecessary(string str)
        {
            return _decorated.SurroundWithDoubleQuotesIfNecessary(str);
        }

        public Regex FileNamePatternToRegex(string pattern)
        {
            return _decorated.FileNamePatternToRegex(pattern);
        }

        public IOpenFilesTrackingService OpenFilesTrackingService => _decorated.OpenFilesTrackingService;

        public IEnumerable<KeyValuePair<AbsolutePath, string>> ProposeUniqueNamesForMovingPathsToSameFolder(IEnumerable<AbsolutePath> paths)
        {
            return _decorated.ProposeUniqueNamesForMovingPathsToSameFolder(paths);
        }

        public bool IsAbsoluteWindowsPath(string path)
        {
            return _decorated.IsAbsoluteWindowsPath(path);
        }

        public bool IsAbsoluteUnixPath(string path)
        {
            return _decorated.IsAbsoluteUnixPath(path);
        }

        public StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode)
        {
            return _decorated.ToStringComparison(caseSensitivityMode);
        }

        public StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode,
            CaseSensitivityMode otherCaseSensitivityMode)
        {
            return _decorated.ToStringComparison(caseSensitivityMode, otherCaseSensitivityMode);
        }

        public AbsolutePath ParsePathRelativeToDefault(string path)
        {
            return _decorated.ParsePathRelativeToDefault(path);
        }

        public bool IsRelativePath(string path)
        {
            return _decorated.IsRelativePath(path);
        }

        public bool IsAbsolutePath(string path)
        {
            return _decorated.IsAbsolutePath(path);
        }

        public bool ComponentsAreAbsolute(IReadOnlyList<string> path)
        {
            return _decorated.ComponentsAreAbsolute(path);
        }

        public IMaybe<RelativePath> TryParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseRelativePath(path, flags);
        }

        public IMaybe<AbsolutePath> TryParseAbsolutePath(string path, IFolder optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseAbsolutePath(path, optionallyRelativeTo, flags);
        }

        public IMaybe<AbsolutePath> TryParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseAbsolutePath(path, flags);
        }

        public RelativePath ParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseRelativePath(path, flags);
        }

        public AbsolutePath ParseAbsolutePath(string path, IFolder optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseAbsolutePath(path, optionallyRelativeTo, flags);
        }

        public AbsolutePath ParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseAbsolutePath(path, flags);
        }

        public Task<IAbsolutePathTranslation> CopyFileAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> CopyFolderAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> MoveFileAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> MoveFolderAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyFile(translation, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation CopyFolder(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyFolder(translation, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveFile(translation, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation MoveFolder(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveFolder(translation, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation Translate(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination)
        {
            return _decorated.Translate(pathToBeCopied, source, destination);
        }

        public IAbsolutePathTranslation Translate(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination)
        {
            return _decorated.Translate(source, destination);
        }

        public IAbsolutePathTranslation Copy(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Copy(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation Copy(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Copy(source, destination, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation Move(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Move(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation Move(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Move(source, destination, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation RenameTo(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.RenameTo(source, target, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.Copy(translation, bufferSize, overwrite);
        }

        public IAbsolutePathTranslation Move(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.Move(translation, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> CopyAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> CopyAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> MoveAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> MoveAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> RenameToAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.RenameToAsync(source, target, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> CopyAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public Task<IAbsolutePathTranslation> MoveAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public MissingPath GenerateUniqueTemporaryPath(string extension = null)
        {
            return _decorated.GenerateUniqueTemporaryPath(extension);
        }

        public IEnumerable<IFileOrFolder> Children(IFolder path, string searchPattern, bool includeFolders = true, bool includeFiles = true)
        {
            return _decorated.Children(path, searchPattern, includeFolders, includeFiles);
        }

        public IEnumerable<File> ChildFiles(IFolder path, string searchPattern)
        {
            return _decorated.ChildFiles(path, searchPattern);
        }

        public IEnumerable<Folder> ChildFolders(IFolder path, string searchPattern)
        {
            return _decorated.ChildFolders(path, searchPattern);
        }

        public IEnumerable<IFileOrFolder> Descendants(IFolder path, string searchPattern, bool includeFolders = true, bool includeFiles = true)
        {
            return _decorated.Descendants(path, searchPattern, includeFolders, includeFiles);
        }

        public IEnumerable<Folder> DescendantFolders(IFolder path, string searchPattern)
        {
            return _decorated.DescendantFolders(path, searchPattern);
        }

        public IEnumerable<File> DescendantFiles(IFolder path, string searchPattern)
        {
            return _decorated.DescendantFiles(path, searchPattern);
        }

        public IEnumerable<IFileOrFolder> Children(IFolder path)
        {
            return _decorated.Children(path);
        }

        public IEnumerable<File> ChildFiles(IFolder path)
        {
            return _decorated.ChildFiles(path);
        }

        public IEnumerable<Folder> ChildFolders(IFolder path)
        {
            return _decorated.ChildFolders(path);
        }

        public IEnumerable<IFileOrFolder> Descendants(IFolder path)
        {
            return _decorated.Descendants(path);
        }

        public IEnumerable<Folder> DescendantFolders(IFolder path)
        {
            return _decorated.DescendantFolders(path);
        }

        public IEnumerable<File> DescendantFiles(IFolder path)
        {
            return _decorated.DescendantFiles(path);
        }

        public bool CanBeSimplified(IFileOrFolderOrMissingPath path)
        {
            return _decorated.CanBeSimplified(path);
        }

        public Folder Root(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Root(path);
        }

        public RelativePath RelativeTo(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath relativeTo)
        {
            return _decorated.RelativeTo(path, relativeTo);
        }

        public IMaybe<AbsolutePath> TryCommonWith(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that)
        {
            return _decorated.TryCommonWith(path, that);
        }

        public AbsolutePath Simplify(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Simplify(path);
        }

        public RelativePath Simplify(RelativePath path)
        {
            return _decorated.Simplify(path);
        }

        public IMaybe<AbsolutePath> TryParent(IFileOrFolderOrMissingPath path)
        {
            return _decorated.TryParent(path);
        }

        public Folder Parent(IFile path)
        {
            return _decorated.Parent(path);
        }

        public IMaybe<Folder> TryParent(IFolder path)
        {
            return _decorated.TryParent(path);
        }

        public AbsolutePath Combine(IFolder path, params string[] subsequentPathParts)
        {
            return _decorated.Combine(path, subsequentPathParts);
        }

        public AbsolutePath WithoutExtension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.WithoutExtension(path);
        }

        public Uri Child(Uri parent, Uri child)
        {
            return _decorated.Child(parent, child);
        }

        public AbsolutePaths GlobFiles(IFolder path, string pattern)
        {
            return _decorated.GlobFiles(path, pattern);
        }

        public IEnumerable<Folder> Ancestors(IFolder path, bool includeItself)
        {
            return _decorated.Ancestors(path, includeItself);
        }

        public IEnumerable<IFileOrFolder> Ancestors(IFile path, bool includeItself)
        {
            return _decorated.Ancestors(path, includeItself);
        }

        public IEnumerable<IFolderOrMissingPath> Ancestors(IMissingPath path, bool includeItself)
        {
            return _decorated.Ancestors(path, includeItself);
        }

        public IEnumerable<Folder> Ancestors(IFolder path)
        {
            return _decorated.Ancestors(path);
        }

        public IEnumerable<Folder> Ancestors(IFile path)
        {
            return _decorated.Ancestors(path);
        }

        public IEnumerable<IFolderOrMissingPath> Ancestors(IMissingPath path)
        {
            return _decorated.Ancestors(path);
        }

        public IEnumerable<AbsolutePath> Ancestors(IFileOrFolderOrMissingPath path, bool includeItself)
        {
            return _decorated.Ancestors(path, includeItself);
        }

        public IEnumerable<AbsolutePath> Ancestors(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Ancestors(path);
        }

        public IMaybe<AbsolutePath> TryDescendant(IFileOrFolderOrMissingPath path, params IFileOrFolderOrMissingPath[] paths)
        {
            return _decorated.TryDescendant(path, paths);
        }

        public IMaybe<AbsolutePath> TryDescendant(IFileOrFolderOrMissingPath path, params string[] paths)
        {
            return _decorated.TryDescendant(path, paths);
        }

        public IMaybe<Folder> TryAncestor(IFileOrFolderOrMissingPath path, int level)
        {
            return _decorated.TryAncestor(path, level);
        }

        public bool IsAncestorOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleDescendant)
        {
            return _decorated.IsAncestorOf(path, possibleDescendant);
        }

        public bool IsDescendantOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleAncestor)
        {
            return _decorated.IsDescendantOf(path, possibleAncestor);
        }

        public IMaybe<AbsolutePath> TryGetCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetCommonAncestry(path1, path2);
        }

        public IMaybe<Uri> TryGetCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetCommonDescendants(path1, path2);
        }

        public IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetNonCommonDescendants(path1, path2);
        }

        public IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetNonCommonAncestry(path1, path2);
        }

        public IMaybe<AbsolutePath> TryWithExtension(IFileOrFolderOrMissingPath path, string differentExtension)
        {
            return _decorated.TryWithExtension(path, differentExtension);
        }

        public IMaybe<AbsolutePath> TryWithExtension(IFileOrFolderOrMissingPath path, Func<string, string> differentExtension)
        {
            return _decorated.TryWithExtension(path, differentExtension);
        }

        public bool Exists(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Exists(path);
        }

        public PathType Type(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Type(path);
        }

        public bool HasExtension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.HasExtension(path);
        }

        public bool IsFile(IFileOrFolderOrMissingPath path)
        {
            return _decorated.IsFile(path);
        }

        public bool IsFolder(IFileOrFolderOrMissingPath path)
        {
            return _decorated.IsFolder(path);
        }

        public bool IsReadOnly(IFile path)
        {
            return _decorated.IsReadOnly(path);
        }

        public Information FileSize(IFile path)
        {
            return _decorated.FileSize(path);
        }

        public FileAttributes Attributes(IFile attributes)
        {
            return _decorated.Attributes(attributes);
        }

        public DateTimeOffset CreationTime(IFile attributes)
        {
            return _decorated.CreationTime(attributes);
        }

        public DateTimeOffset LastAccessTime(IFile attributes)
        {
            return _decorated.LastAccessTime(attributes);
        }

        public DateTimeOffset LastWriteTime(IFile attributes)
        {
            return _decorated.LastWriteTime(attributes);
        }

        public BufferEnumerator ReadBuffers(IFile path, FileShare fileShare = FileShare.None, Information? bufferSize = default,
            int paddingAtStart = 0, int paddingAtEnd = 0)
        {
            return _decorated.ReadBuffers(path, fileShare, bufferSize, paddingAtStart, paddingAtEnd);
        }

        public File WriteAllBytes(IFileOrMissingPath path, byte[] bytes, bool createRecursively = false)
        {
            return _decorated.WriteAllBytes(path, bytes, createRecursively);
        }

        public Stream Open(IFileOrMissingPath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.None | FileOptions.SequentialScan | FileOptions.Asynchronous,
            Information? bufferSize = default, bool createRecursively = false)
        {
            return _decorated.Open(path, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public IQueryable<AbsolutePath> Query()
        {
            return _decorated.Query();
        }

        public ISetChanges<AbsolutePath> ToLiveLinq(IFolder path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            return _decorated.ToLiveLinq(path, includeFileContentChanges, includeSubFolders, pattern);
        }
    }
}