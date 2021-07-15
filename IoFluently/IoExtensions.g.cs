using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reactive;
using System.Text;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using SimpleMonads;
using TreeLinq;
using UnitsNet;

namespace IoFluently
{
    /// <summary>
    /// Contains extension methods on AbsolutePath, RelativePath, and IAbsolutePathTranslation that essentially wrap
    /// methods on the object's IoService property. That is, myAbsolutePath.RelativeTo(parameter1) is equivalent to
    /// myAbsolutePath.IoService.RelativeTo(myAbsolutePath, parameter1). This shorthand makes the syntax be fluent
    /// while allowing the IIoService to be dependency injectable.
    /// </summary>
    public static partial class IoExtensions
    {
        public static IEnumerable<Folder> EnumerateDescendantFolders(this IFolder path) {
            return path.IoService.EnumerateDescendantFolders(path);
        }

        public static IEnumerable<File> EnumerateDescendantFiles(this IFolder path) {
            return path.IoService.EnumerateDescendantFiles(path);
        }

        public static RelativePath RelativeTo(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath relativeTo) {
            return path.IoService.RelativeTo(path, relativeTo);
        }

        public static IMaybe<AbsolutePath> TryCommonWith(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that) {
            return path.IoService.TryCommonWith(path, that);
        }

        public static AbsolutePath CommonWith(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that) {
            return path.IoService.TryCommonWith(path, that).Value;
        }

        public static AbsolutePath Simplify(this IFileOrFolderOrMissingPath path) {
            return path.IoService.Simplify(path);
        }

        public static RelativePath Simplify(this RelativePath path) {
            return path.IoService.Simplify(path);
        }

        public static AbsolutePath Combine(this IFolder path, String[] subsequentPathParts) {
            return path.IoService.Combine(path, subsequentPathParts);
        }

        public static AbsolutePaths GlobFiles(this IFolder path, string pattern) {
            return path.IoService.GlobFiles(path, pattern);
        }

        public static IEnumerable<Folder> Ancestors(this IFolder path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IEnumerable<IFileOrFolder> Ancestors(this IFile path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IEnumerable<IFolderOrMissingPath> Ancestors(this IMissingPath path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IEnumerable<AbsolutePath> Ancestors(this IFileOrFolderOrMissingPath path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IMaybe<AbsolutePath> TryDescendant(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath[] paths) {
            return path.IoService.TryDescendant(path, paths);
        }

        public static AbsolutePath Descendant(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath[] paths) {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        public static IMaybe<AbsolutePath> TryDescendant(this IFileOrFolderOrMissingPath path, String[] paths) {
            return path.IoService.TryDescendant(path, paths);
        }

        public static AbsolutePath Descendant(this IFileOrFolderOrMissingPath path, String[] paths) {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        public static IMaybe<Folder> TryAncestor(this IFileOrFolderOrMissingPath path, int level) {
            return path.IoService.TryAncestor(path, level);
        }

        public static Folder Ancestor(this IFileOrFolderOrMissingPath path, int level) {
            return path.IoService.TryAncestor(path, level).Value;
        }

        public static Boolean IsAncestorOf(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleDescendant) {
            return path.IoService.IsAncestorOf(path, possibleDescendant);
        }

        public static Boolean IsDescendantOf(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleAncestor) {
            return path.IoService.IsDescendantOf(path, possibleAncestor);
        }

        public static IMaybe<AbsolutePath> TryGetCommonAncestry(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.IoService.TryGetCommonAncestry(path1, path2);
        }

        public static AbsolutePath GetCommonAncestry(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.IoService.TryGetCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<Uri> TryGetCommonDescendants(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.IoService.TryGetCommonDescendants(path1, path2);
        }

        public static Uri GetCommonDescendants(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.IoService.TryGetCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonDescendants(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonAncestry(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<AbsolutePath> TryWithExtension(this IFileOrFolderOrMissingPath path, string differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this IFileOrFolderOrMissingPath path, string differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension).Value;
        }

        public static IMaybe<AbsolutePath> TryWithExtension(this IFileOrFolderOrMissingPath path, Func<string, string> differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this IFileOrFolderOrMissingPath path, Func<string, string> differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension).Value;
        }

        public static BufferEnumerator ReadBuffers(this IFile path, FileShare fileShare = FileShare.None, Nullable<Information> bufferSize = null, int paddingAtStart = 0, int paddingAtEnd = 0) {
            return path.IoService.ReadBuffers(path, fileShare, bufferSize, paddingAtStart, paddingAtEnd);
        }

        public static File WriteAllBytes(this IFileOrMissingPath path, Byte[] bytes, Boolean createRecursively = false) {
            return path.IoService.WriteAllBytes(path, bytes, createRecursively);
        }

        public static Stream Open(this IFileOrMissingPath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None, FileOptions fileOptions = FileOptions.SequentialScan | FileOptions.Asynchronous, Nullable<Information> bufferSize = null, Boolean createRecursively = false) {
            return path.IoService.Open(path, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public static ISetChanges<AbsolutePath> ToLiveLinq(this IFolder path, Boolean includeFileContentChanges, Boolean includeSubFolders, string pattern) {
            return path.IoService.ToLiveLinq(path, includeFileContentChanges, includeSubFolders, pattern);
        }

        public static Folder CreateFolder(this IMissingPath path, Boolean createRecursively = false) {
            return path.IoService.CreateFolder(path, createRecursively);
        }

        public static MissingPath DeleteFolder(this IFolder path, Boolean recursive = false) {
            return path.IoService.DeleteFolder(path, recursive);
        }

        public static MissingPath DeleteFile(this IFile path) {
            return path.IoService.DeleteFile(path);
        }

        public static MissingPath Delete(this IFileOrFolder path, Boolean recursiveDeleteIfFolder = true) {
            return path.IoService.Delete(path, recursiveDeleteIfFolder);
        }

        public static Task<MissingPath> DeleteFolderAsync(this IFolder path, CancellationToken cancellationToken, Boolean recursive = false) {
            return path.IoService.DeleteFolderAsync(path, cancellationToken, recursive);
        }

        public static Task<MissingPath> DeleteFileAsync(this IFile path, CancellationToken cancellationToken) {
            return path.IoService.DeleteFileAsync(path, cancellationToken);
        }

        public static Task<MissingPath> DeleteAsync(this IFileOrFolder path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true) {
            return path.IoService.DeleteAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public static Folder EnsureIsFolder(this IFileOrFolderOrMissingPath path, Boolean createRecursively = false) {
            return path.IoService.EnsureIsFolder(path, createRecursively);
        }

        public static Folder EnsureIsEmptyFolder(this IFileOrFolderOrMissingPath path, Boolean recursiveDeleteIfFolder = true, Boolean createRecursively = false) {
            return path.IoService.EnsureIsEmptyFolder(path, recursiveDeleteIfFolder, createRecursively);
        }

        public static Task<Folder> EnsureIsFolderAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, Boolean createRecursively = false) {
            return path.IoService.EnsureIsFolderAsync(path, cancellationToken, createRecursively);
        }

        public static Task<Folder> EnsureIsEmptyFolderAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true, Boolean createRecursively = false) {
            return path.IoService.EnsureIsEmptyFolderAsync(path, cancellationToken, recursiveDeleteIfFolder, createRecursively);
        }

        public static IFileOrMissingPath EnsureIsNotFolder(this IFileOrFolderOrMissingPath path, Boolean recursive = false) {
            return path.IoService.EnsureIsNotFolder(path, recursive);
        }

        public static IFolderOrMissingPath EnsureIsNotFile(this IFileOrFolderOrMissingPath path) {
            return path.IoService.EnsureIsNotFile(path);
        }

        public static MissingPath EnsureDoesNotExist(this IFileOrFolderOrMissingPath path, Boolean recursiveDeleteIfFolder = true) {
            return path.IoService.EnsureDoesNotExist(path, recursiveDeleteIfFolder);
        }

        public static Task<IFileOrMissingPath> EnsureIsNotFolderAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, Boolean recursive = false) {
            return path.IoService.EnsureIsNotFolderAsync(path, cancellationToken, recursive);
        }

        public static Task<IFolderOrMissingPath> EnsureIsNotFileAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken) {
            return path.IoService.EnsureIsNotFileAsync(path, cancellationToken);
        }

        public static Task<MissingPath> EnsureDoesNotExistAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true) {
            return path.IoService.EnsureDoesNotExistAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public static Boolean HasExtension(this IFileOrFolderOrMissingPath path, string extension) {
            return path.IoService.HasExtension(path, extension);
        }

        public static Task<IAbsolutePathTranslation> CopyFileAsync(this IAbsolutePathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.CopyFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> CopyFolderAsync(this IAbsolutePathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.CopyFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> MoveFileAsync(this IAbsolutePathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.MoveFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> MoveFolderAsync(this IAbsolutePathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.MoveFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation CopyFile(this IAbsolutePathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.CopyFile(translation, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation CopyFolder(this IAbsolutePathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.CopyFolder(translation, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation MoveFile(this IAbsolutePathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.MoveFile(translation, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation MoveFolder(this IAbsolutePathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.MoveFolder(translation, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Translate(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination) {
            return pathToBeCopied.IoService.Translate(pathToBeCopied, source, destination);
        }

        public static IAbsolutePathTranslation Translate(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination) {
            return source.IoService.Translate(source, destination);
        }

        public static IAbsolutePathTranslation Copy(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.IoService.Copy(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Copy(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.Copy(source, destination, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Move(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.IoService.Move(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Move(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.Move(source, destination, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation RenameTo(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.RenameTo(source, target, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Copy(this IAbsolutePathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.Copy(translation, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Move(this IAbsolutePathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.Move(translation, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> CopyAsync(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.IoService.CopyAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> CopyAsync(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.CopyAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> MoveAsync(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.IoService.MoveAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> MoveAsync(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.MoveAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> RenameToAsync(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.RenameToAsync(source, target, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> CopyAsync(this IAbsolutePathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.CopyAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> MoveAsync(this IAbsolutePathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.MoveAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static IEnumerable<IFileOrFolder> EnumerateChildren(this IFolder path, string searchPattern, Boolean includeFolders = true, Boolean includeFiles = true) {
            return path.IoService.EnumerateChildren(path, searchPattern, includeFolders, includeFiles);
        }

        public static IEnumerable<File> EnumerateChildFiles(this IFolder path, string searchPattern) {
            return path.IoService.EnumerateChildFiles(path, searchPattern);
        }

        public static IEnumerable<Folder> EnumerateChildFolders(this IFolder path, string searchPattern) {
            return path.IoService.EnumerateChildFolders(path, searchPattern);
        }

        public static IEnumerable<IFileOrFolder> EnumerateDescendants(this IFolder path, string searchPattern, Boolean includeFolders = true, Boolean includeFiles = true) {
            return path.IoService.EnumerateDescendants(path, searchPattern, includeFolders, includeFiles);
        }

        public static IEnumerable<Folder> EnumerateDescendantFolders(this IFolder path, string searchPattern) {
            return path.IoService.EnumerateDescendantFolders(path, searchPattern);
        }

        public static IEnumerable<File> EnumerateDescendantFiles(this IFolder path, string searchPattern) {
            return path.IoService.EnumerateDescendantFiles(path, searchPattern);
        }

        public static IEnumerable<IFileOrFolder> EnumerateChildren(this IFolder path) {
            return path.IoService.EnumerateChildren(path);
        }

        public static IEnumerable<File> EnumerateChildFiles(this IFolder path) {
            return path.IoService.EnumerateChildFiles(path);
        }

        public static IEnumerable<Folder> EnumerateChildFolders(this IFolder path) {
            return path.IoService.EnumerateChildFolders(path);
        }

        public static IEnumerable<IFileOrFolder> EnumerateDescendants(this IFolder path) {
            return path.IoService.EnumerateDescendants(path);
        }

    }
}
