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
    public abstract class FileSystemDecoratorBase : IFileSystem
    {
        private readonly IFileSystem _decorated;

        public FileSystemDecoratorBase(IFileSystem decorated)
        {
            _decorated = decorated;
        }

        public AbsolutePathChildren Children(IFolderPath path, string searchPattern)
        {
            return _decorated.Children(path, searchPattern);
        }

        public AbsolutePathChildFiles ChildFiles(IFolderPath path, string searchPattern)
        {
            return _decorated.ChildFiles(path, searchPattern);
        }

        public AbsolutePathChildFolders ChildFolders(IFolderPath path, string searchPattern)
        {
            return _decorated.ChildFolders(path, searchPattern);
        }

        public AbsolutePathDescendants Descendants(IFolderPath path, string searchPattern)
        {
            return _decorated.Descendants(path, searchPattern);
        }

        public AbsolutePathDescendantFolders DescendantFolders(IFolderPath path, string searchPattern)
        {
            return _decorated.DescendantFolders(path, searchPattern);
        }

        public AbsolutePathDescendantFiles DescendantFiles(IFolderPath path, string searchPattern)
        {
            return _decorated.DescendantFiles(path, searchPattern);
        }

        public AbsolutePathChildren Children(IFolderPath path)
        {
            return _decorated.Children(path);
        }

        public AbsolutePathChildFiles ChildFiles(IFolderPath path)
        {
            return _decorated.ChildFiles(path);
        }

        public AbsolutePathChildFolders ChildFolders(IFolderPath path)
        {
            return _decorated.ChildFolders(path);
        }

        public AbsolutePathDescendants Descendants(IFolderPath path)
        {
            return _decorated.Descendants(path);
        }

        public AbsolutePathDescendantFolders DescendantFolders(IFolderPath path)
        {
            return _decorated.DescendantFolders(path);
        }

        public AbsolutePathDescendantFiles DescendantFiles(IFolderPath path)
        {
            return _decorated.DescendantFiles(path);
        }

        public IObservableReadOnlySet<FolderPath> Roots => _decorated.Roots;

        public FolderPath DefaultRoot => _decorated.DefaultRoot;

        public void UpdateRoots()
        {
            _decorated.UpdateRoots();
        }

        public virtual IFileInfo GetFileInfo(string subpath)
        {
            return _decorated.GetFileInfo(subpath);
        }

        public virtual IDirectoryContents GetDirectoryContents(string subpath)
        {
            return _decorated.GetDirectoryContents(subpath);
        }

        public virtual IChangeToken Watch(string filter)
        {
            return _decorated.Watch(filter);
        }

        public virtual Information DefaultBufferSize
        {
            get => _decorated.DefaultBufferSize;
            set => _decorated.DefaultBufferSize = value;
        }

        public virtual int GetBufferSizeOrDefaultInBytes(Information? bufferSize)
        {
            return _decorated.GetBufferSizeOrDefaultInBytes(bufferSize);
        }

        public virtual EmptyFolderMode EmptyFolderMode => _decorated.EmptyFolderMode;

        public virtual bool CanEmptyDirectoriesExist => _decorated.CanEmptyDirectoriesExist;

        public virtual string DefaultDirectorySeparator => _decorated.DefaultDirectorySeparator;

        public virtual bool IsCaseSensitiveByDefault => _decorated.IsCaseSensitiveByDefault;

        public virtual FolderPath CreateFolder(IMissingPath path,  bool createRecursively = true)
        {
            return _decorated.CreateFolder(path, createRecursively);
        }

        public virtual MissingPath DeleteFolder(IFolderPath path,  bool recursive = true)
        {
            return _decorated.DeleteFolder(path, recursive);
        }

        public virtual MissingPath DeleteFile(IFilePath path)
        {
            return _decorated.DeleteFile(path);
        }

        public virtual MissingPath Delete(IFileOrFolder path, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.Delete(path, recursiveDeleteIfFolder);
        }

        public virtual Task<MissingPath> DeleteFolderAsync(IFolderPath path, CancellationToken cancellationToken,  bool recursive = true)
        {
            return _decorated.DeleteFolderAsync(path, cancellationToken, recursive);
        }

        public virtual Task<MissingPath> DeleteFileAsync(IFilePath path, CancellationToken cancellationToken)
        {
            return _decorated.DeleteFileAsync(path, cancellationToken);
        }

        public virtual Task<MissingPath> DeleteAsync(IFileOrFolder path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.DeleteAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public virtual FolderPath EnsureIsFolder(IFileOrFolderOrMissingPath path,  bool createRecursively = true)
        {
            return _decorated.EnsureIsFolder(path, createRecursively);
        }

        public virtual FolderPath EnsureIsEmptyFolder(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true,
             bool createRecursively = true)
        {
            return _decorated.EnsureIsEmptyFolder(path, recursiveDeleteIfFolder, createRecursively);
        }

        public virtual Task<FolderPath> EnsureIsFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
             bool createRecursively = true)
        {
            return _decorated.EnsureIsFolderAsync(path, cancellationToken, createRecursively);
        }

        public virtual Task<FolderPath> EnsureIsEmptyFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true,  bool createRecursively = true)
        {
            return _decorated.EnsureIsEmptyFolderAsync(path, cancellationToken, recursiveDeleteIfFolder, createRecursively);
        }

        public virtual IFileOrMissingPath EnsureIsNotFolder(IFileOrFolderOrMissingPath path,  bool recursive = true)
        {
            return _decorated.EnsureIsNotFolder(path, recursive);
        }

        public virtual IFolderOrMissingPath EnsureIsNotFile(IFileOrFolderOrMissingPath path)
        {
            return _decorated.EnsureIsNotFile(path);
        }

        public virtual MissingPath EnsureDoesNotExist(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true)
        {
            return _decorated.EnsureDoesNotExist(path, recursiveDeleteIfFolder);
        }

        public virtual Task<IFileOrMissingPath> EnsureIsNotFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
             bool recursive = true)
        {
            return _decorated.EnsureIsNotFolderAsync(path, cancellationToken, recursive);
        }

        public virtual Task<IFolderOrMissingPath> EnsureIsNotFileAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken)
        {
            return _decorated.EnsureIsNotFileAsync(path, cancellationToken);
        }

        public virtual Task<MissingPath> EnsureDoesNotExistAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,
            bool recursiveDeleteIfFolder = true)
        {
            return _decorated.EnsureDoesNotExistAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public virtual bool HasExtension(IFileOrFolderOrMissingPath path, string extension)
        {
            return _decorated.HasExtension(path, extension);
        }

        public virtual string Name(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Name(path);
        }

        public virtual string? Extension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Extension(path);
        }

        public virtual bool MayCreateFile(FileMode fileMode)
        {
            return _decorated.MayCreateFile(fileMode);
        }

        public virtual bool IsImageUri(Uri uri)
        {
            return _decorated.IsImageUri(uri);
        }

        public virtual bool IsVideoUri(Uri uri)
        {
            return _decorated.IsVideoUri(uri);
        }

        public virtual string StripQuotes(string str)
        {
            return _decorated.StripQuotes(str);
        }

        public virtual string SurroundWithDoubleQuotesIfNecessary(string str)
        {
            return _decorated.SurroundWithDoubleQuotesIfNecessary(str);
        }

        public virtual Regex FileNamePatternToRegex(string pattern)
        {
            return _decorated.FileNamePatternToRegex(pattern);
        }

        public virtual IOpenFilesTrackingService OpenFilesTrackingService => _decorated.OpenFilesTrackingService;

        public virtual IEnumerable<KeyValuePair<AbsolutePath, string>> ProposeUniqueNamesForMovingPathsToSameFolder(IEnumerable<AbsolutePath> paths)
        {
            return _decorated.ProposeUniqueNamesForMovingPathsToSameFolder(paths);
        }

        public virtual bool IsAbsoluteWindowsPath(string path)
        {
            return _decorated.IsAbsoluteWindowsPath(path);
        }

        public virtual bool IsAbsoluteUnixPath(string path)
        {
            return _decorated.IsAbsoluteUnixPath(path);
        }

        public virtual StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode)
        {
            return _decorated.ToStringComparison(caseSensitivityMode);
        }

        public virtual StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode,
            CaseSensitivityMode otherCaseSensitivityMode)
        {
            return _decorated.ToStringComparison(caseSensitivityMode, otherCaseSensitivityMode);
        }
        
        public virtual bool IsRelativePath(string path)
        {
            return _decorated.IsRelativePath(path);
        }

        public virtual bool IsAbsolutePath(string path)
        {
            return _decorated.IsAbsolutePath(path);
        }

        public virtual bool ComponentsAreAbsolute(IReadOnlyList<string> path)
        {
            return _decorated.ComponentsAreAbsolute(path);
        }

        public virtual IMaybe<RelativePath> TryParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseRelativePath(path, flags);
        }

        public virtual IMaybe<AbsolutePath> TryParseAbsolutePath(string path, IFolderPath optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseAbsolutePath(path, optionallyRelativeTo, flags);
        }

        public virtual IMaybe<AbsolutePath> TryParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.TryParseAbsolutePath(path, flags);
        }

        public virtual RelativePath ParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseRelativePath(path, flags);
        }

        public virtual AbsolutePath ParseAbsolutePath(string path, IFolderPath optionallyRelativeTo,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseAbsolutePath(path, optionallyRelativeTo, flags);
        }

        public virtual AbsolutePath ParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath)
        {
            return _decorated.ParseAbsolutePath(path, flags);
        }

        public virtual Task<IAbsolutePathTranslation> CopyFileAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> CopyFolderAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> MoveFileAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> MoveFolderAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyFile(translation, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation CopyFolder(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyFolder(translation, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveFile(translation, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation MoveFolder(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveFolder(translation, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation Translate(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination)
        {
            return _decorated.Translate(pathToBeCopied, source, destination);
        }

        public virtual IAbsolutePathTranslation Translate(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination)
        {
            return _decorated.Translate(source, destination);
        }

        public virtual IAbsolutePathTranslation Copy(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Copy(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation Copy(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Copy(source, destination, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation Move(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Move(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation Move(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.Move(source, destination, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation RenameTo(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.RenameTo(source, target, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.Copy(translation, bufferSize, overwrite);
        }

        public virtual IAbsolutePathTranslation Move(IAbsolutePathTranslation translation, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.Move(translation, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> CopyAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.CopyAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> CopyAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> MoveAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source,
            IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Information? bufferSize = default,
            bool overwrite = false)
        {
            return _decorated.MoveAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> MoveAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> RenameToAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.RenameToAsync(source, target, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> CopyAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.CopyAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual Task<IAbsolutePathTranslation> MoveAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false)
        {
            return _decorated.MoveAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public virtual IEnumerable<IFileOrFolder> EnumerateChildren(IFolderPath path, string searchPattern, bool includeFolders = true, bool includeFiles = true)
        {
            return _decorated.EnumerateChildren(path, searchPattern, includeFolders, includeFiles);
        }

        public virtual IEnumerable<FilePath> EnumerateChildFiles(IFolderPath path, string searchPattern)
        {
            return _decorated.EnumerateChildFiles(path, searchPattern);
        }

        public virtual IEnumerable<FolderPath> EnumerateChildFolders(IFolderPath path, string searchPattern)
        {
            return _decorated.EnumerateChildFolders(path, searchPattern);
        }

        public virtual IEnumerable<IFileOrFolder> EnumerateDescendants(IFolderPath path, string searchPattern, bool includeFolders = true, bool includeFiles = true)
        {
            return _decorated.EnumerateDescendants(path, searchPattern, includeFolders, includeFiles);
        }

        public virtual IEnumerable<FolderPath> EnumerateDescendantFolders(IFolderPath path, string searchPattern)
        {
            return _decorated.EnumerateDescendantFolders(path, searchPattern);
        }

        public virtual IEnumerable<FilePath> EnumerateDescendantFiles(IFolderPath path, string searchPattern)
        {
            return _decorated.EnumerateDescendantFiles(path, searchPattern);
        }

        public virtual IEnumerable<IFileOrFolder> EnumerateChildren(IFolderPath path)
        {
            return _decorated.EnumerateChildren(path);
        }

        public virtual IEnumerable<FilePath> EnumerateChildFiles(IFolderPath path)
        {
            return _decorated.EnumerateChildFiles(path);
        }

        public virtual IEnumerable<FolderPath> EnumerateChildFolders(IFolderPath path)
        {
            return _decorated.EnumerateChildFolders(path);
        }

        public virtual IEnumerable<IFileOrFolder> EnumerateDescendants(IFolderPath path)
        {
            return _decorated.EnumerateDescendants(path);
        }

        public virtual IEnumerable<FolderPath> EnumerateDescendantFolders(IFolderPath path)
        {
            return _decorated.EnumerateDescendantFolders(path);
        }

        public virtual IEnumerable<FilePath> EnumerateDescendantFiles(IFolderPath path)
        {
            return _decorated.EnumerateDescendantFiles(path);
        }

        public virtual bool CanBeSimplified(IFileOrFolderOrMissingPath path)
        {
            return _decorated.CanBeSimplified(path);
        }

        public virtual FolderPath Root(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Root(path);
        }

        public virtual RelativePath RelativeTo(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath relativeTo)
        {
            return _decorated.RelativeTo(path, relativeTo);
        }

        public virtual IMaybe<AbsolutePath> TryCommonWith(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that)
        {
            return _decorated.TryCommonWith(path, that);
        }

        public virtual AbsolutePath Simplify(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Simplify(path);
        }

        public virtual RelativePath Simplify(RelativePath path)
        {
            return _decorated.Simplify(path);
        }

        public virtual IMaybe<AbsolutePath> TryParent(IFileOrFolderOrMissingPath path)
        {
            return _decorated.TryParent(path);
        }

        public virtual FolderPath Parent(IFilePath path)
        {
            return _decorated.Parent(path);
        }

        public virtual IMaybe<FolderPath> TryParent(IFolderPath path)
        {
            return _decorated.TryParent(path);
        }

        public virtual AbsolutePath Combine(IFolderPath path, params string[] subsequentPathParts)
        {
            return _decorated.Combine(path, subsequentPathParts);
        }

        public virtual AbsolutePath WithoutExtension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.WithoutExtension(path);
        }

        public virtual Uri Child(Uri parent, Uri child)
        {
            return _decorated.Child(parent, child);
        }

        public virtual AbsolutePaths GlobFiles(IFolderPath path, string pattern)
        {
            return _decorated.GlobFiles(path, pattern);
        }

        public virtual IEnumerable<FolderPath> Ancestors(IFolderPath path, bool includeItself)
        {
            return _decorated.Ancestors(path, includeItself);
        }

        public virtual IEnumerable<IFileOrFolder> Ancestors(IFilePath path, bool includeItself)
        {
            return _decorated.Ancestors(path, includeItself);
        }

        public virtual IEnumerable<IFolderOrMissingPath> Ancestors(IMissingPath path, bool includeItself)
        {
            return _decorated.Ancestors(path, includeItself);
        }

        public virtual IEnumerable<FolderPath> Ancestors(IFolderPath path)
        {
            return _decorated.Ancestors(path);
        }

        public virtual IEnumerable<FolderPath> Ancestors(IFilePath path)
        {
            return _decorated.Ancestors(path);
        }

        public virtual IEnumerable<IFolderOrMissingPath> Ancestors(IMissingPath path)
        {
            return _decorated.Ancestors(path);
        }

        public virtual IEnumerable<AbsolutePath> Ancestors(IFileOrFolderOrMissingPath path, bool includeItself)
        {
            return _decorated.Ancestors(path, includeItself);
        }

        public virtual IEnumerable<AbsolutePath> Ancestors(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Ancestors(path);
        }

        public virtual IMaybe<AbsolutePath> TryDescendant(IFileOrFolderOrMissingPath path, params IFileOrFolderOrMissingPath[] paths)
        {
            return _decorated.TryDescendant(path, paths);
        }

        public virtual IMaybe<AbsolutePath> TryDescendant(IFileOrFolderOrMissingPath path, params string[] paths)
        {
            return _decorated.TryDescendant(path, paths);
        }

        public virtual IMaybe<FolderPath> TryAncestor(IFileOrFolderOrMissingPath path, int level)
        {
            return _decorated.TryAncestor(path, level);
        }

        public virtual bool IsAncestorOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleDescendant)
        {
            return _decorated.IsAncestorOf(path, possibleDescendant);
        }

        public virtual bool IsDescendantOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleAncestor)
        {
            return _decorated.IsDescendantOf(path, possibleAncestor);
        }

        public virtual IMaybe<AbsolutePath> TryGetCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetCommonAncestry(path1, path2);
        }

        public virtual IMaybe<Uri> TryGetCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetCommonDescendants(path1, path2);
        }

        public virtual IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetNonCommonDescendants(path1, path2);
        }

        public virtual IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2)
        {
            return _decorated.TryGetNonCommonAncestry(path1, path2);
        }

        public virtual IMaybe<AbsolutePath> TryWithExtension(IFileOrFolderOrMissingPath path, string differentExtension)
        {
            return _decorated.TryWithExtension(path, differentExtension);
        }

        public virtual IMaybe<AbsolutePath> TryWithExtension(IFileOrFolderOrMissingPath path, Func<string, string> differentExtension)
        {
            return _decorated.TryWithExtension(path, differentExtension);
        }

        public virtual bool Exists(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Exists(path);
        }

        public virtual PathType Type(IFileOrFolderOrMissingPath path)
        {
            return _decorated.Type(path);
        }

        public virtual bool HasExtension(IFileOrFolderOrMissingPath path)
        {
            return _decorated.HasExtension(path);
        }

        public virtual bool IsFile(IFileOrFolderOrMissingPath path)
        {
            return _decorated.IsFile(path);
        }

        public virtual bool IsFolder(IFileOrFolderOrMissingPath path)
        {
            return _decorated.IsFolder(path);
        }

        public virtual bool IsReadOnly(IFilePath path)
        {
            return _decorated.IsReadOnly(path);
        }

        public virtual Information FileSize(IFilePath path)
        {
            return _decorated.FileSize(path);
        }

        public virtual FileAttributes Attributes(IFilePath attributes)
        {
            return _decorated.Attributes(attributes);
        }

        public virtual DateTimeOffset CreationTime(IFilePath attributes)
        {
            return _decorated.CreationTime(attributes);
        }

        public virtual DateTimeOffset LastAccessTime(IFilePath attributes)
        {
            return _decorated.LastAccessTime(attributes);
        }

        public virtual DateTimeOffset LastWriteTime(IFilePath attributes)
        {
            return _decorated.LastWriteTime(attributes);
        }

        public virtual BufferEnumerator ReadBuffers(IFilePath path, FileShare fileShare = FileShare.None, Information? bufferSize = default,
            int paddingAtStart = 0, int paddingAtEnd = 0)
        {
            return _decorated.ReadBuffers(path, fileShare, bufferSize, paddingAtStart, paddingAtEnd);
        }

        public virtual FilePath WriteAllBytes(IFileOrMissingPath path, byte[] bytes,  bool createRecursively = true)
        {
            return _decorated.WriteAllBytes(path, bytes, createRecursively);
        }

        public virtual Stream Open(IFileOrMissingPath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite,
            FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.None | FileOptions.SequentialScan | FileOptions.Asynchronous,
            Information? bufferSize = default,  bool createRecursively = true)
        {
            return _decorated.Open(path, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public virtual IQueryable<AbsolutePath> Query()
        {
            return _decorated.Query();
        }

        public virtual ISetChanges<AbsolutePath> ToLiveLinq(IFolderPath path, bool includeFileContentChanges, bool includeSubFolders, string pattern)
        {
            return _decorated.ToLiveLinq(path, includeFileContentChanges, includeSubFolders, pattern);
        }
    }
}