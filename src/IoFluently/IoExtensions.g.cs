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
        public static RelativePath RelativeTo(this AbsolutePath path, AbsolutePath relativeTo) {
            return path.IoService.RelativeTo(path, relativeTo);
        }

        public static IMaybe<AbsolutePath> TryCommonWith(this AbsolutePath path, AbsolutePath that) {
            return path.IoService.TryCommonWith(path, that);
        }

        public static AbsolutePath CommonWith(this AbsolutePath path, AbsolutePath that) {
            return path.IoService.TryCommonWith(path, that).Value;
        }

        public static AbsolutePath Simplify(this AbsolutePath path) {
            return path.IoService.Simplify(path);
        }

        public static RelativePath Simplify(this RelativePath path) {
            return path.IoService.Simplify(path);
        }

        public static AbsolutePath Combine(this Folder path, String[] subsequentPathParts) {
            return path.IoService.Combine(path, subsequentPathParts);
        }

        public static AbsolutePaths GlobFiles(this Folder path, string pattern) {
            return path.IoService.GlobFiles(path, pattern);
        }

        public static IEnumerable<Folder> Ancestors(this Folder path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IEnumerable<FileOrFolder> Ancestors(this File path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IEnumerable<FolderOrMissingPath> Ancestors(this MissingPath path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IEnumerable<AbsolutePath> Ancestors(this AbsolutePath path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IMaybe<AbsolutePath> TryDescendant(this AbsolutePath path, AbsolutePath[] paths) {
            return path.IoService.TryDescendant(path, paths);
        }

        public static AbsolutePath Descendant(this AbsolutePath path, AbsolutePath[] paths) {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        public static IMaybe<AbsolutePath> TryDescendant(this AbsolutePath path, String[] paths) {
            return path.IoService.TryDescendant(path, paths);
        }

        public static AbsolutePath Descendant(this AbsolutePath path, String[] paths) {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        public static IMaybe<Folder> TryAncestor(this AbsolutePath path, int level) {
            return path.IoService.TryAncestor(path, level);
        }

        public static Folder Ancestor(this AbsolutePath path, int level) {
            return path.IoService.TryAncestor(path, level).Value;
        }

        public static Boolean IsAncestorOf(this AbsolutePath path, AbsolutePath possibleDescendant) {
            return path.IoService.IsAncestorOf(path, possibleDescendant);
        }

        public static Boolean IsDescendantOf(this AbsolutePath path, AbsolutePath possibleAncestor) {
            return path.IoService.IsDescendantOf(path, possibleAncestor);
        }

        public static IMaybe<AbsolutePath> TryGetCommonAncestry(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetCommonAncestry(path1, path2);
        }

        public static AbsolutePath GetCommonAncestry(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<Uri> TryGetCommonDescendants(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetCommonDescendants(path1, path2);
        }

        public static Uri GetCommonDescendants(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<AbsolutePath> TryWithExtension(this AbsolutePath path, string differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this AbsolutePath path, string differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension).Value;
        }

        public static IMaybe<AbsolutePath> TryWithExtension(this AbsolutePath path, Func<string, string> differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this AbsolutePath path, Func<string, string> differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension).Value;
        }

        public static BufferEnumerator ReadBuffers(this AbsolutePath path, FileShare fileShare = FileShare.None, Nullable<Information> bufferSize = null, int paddingAtStart = 0, int paddingAtEnd = 0) {
            return path.IoService.ReadBuffers(path, fileShare, bufferSize, paddingAtStart, paddingAtEnd);
        }

        public static AbsolutePath WriteAllBytes(this AbsolutePath path, Byte[] bytes, Boolean createRecursively = false) {
            return path.IoService.WriteAllBytes(path, bytes, createRecursively);
        }

        public static IMaybe<Stream> TryOpen(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None, FileOptions fileOptions = FileOptions.SequentialScan | FileOptions.Asynchronous, Nullable<Information> bufferSize = null, Boolean createRecursively = false) {
            return path.IoService.TryOpen(path, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None, FileOptions fileOptions = FileOptions.SequentialScan | FileOptions.Asynchronous, Nullable<Information> bufferSize = null, Boolean createRecursively = false) {
            return path.IoService.TryOpen(path, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively).Value;
        }

        public static ISetChanges<AbsolutePath> ToLiveLinq(this AbsolutePath path, Boolean includeFileContentChanges, Boolean includeSubFolders, string pattern) {
            return path.IoService.ToLiveLinq(path, includeFileContentChanges, includeSubFolders, pattern);
        }

        public static IObservable<Unit> ObserveChanges(this AbsolutePath path) {
            return path.IoService.ObserveChanges(path);
        }

        public static IObservable<Unit> ObserveChanges(this AbsolutePath path, NotifyFilters filters) {
            return path.IoService.ObserveChanges(path, filters);
        }

        public static IObservable<PathType> ObservePathType(this AbsolutePath path) {
            return path.IoService.ObservePathType(path);
        }

        public static IObservable<AbsolutePath> Renamings(this AbsolutePath path) {
            return path.IoService.Renamings(path);
        }

        public static void SetDefaultRelativePathBase(this Folder defaultRelativePathBase) {
            defaultRelativePathBase.IoService.SetDefaultRelativePathBase(defaultRelativePathBase);
        }

        public static Folder CreateFolder(this MissingPath path, Boolean createRecursively = false) {
            return path.IoService.CreateFolder(path, createRecursively);
        }

        public static MissingPath DeleteFolder(this Folder path, Boolean recursive = false) {
            return path.IoService.DeleteFolder(path, recursive);
        }

        public static MissingPath DeleteFile(this File path) {
            return path.IoService.DeleteFile(path);
        }

        public static MissingPath Delete(this FileOrFolder path, Boolean recursiveDeleteIfFolder = true) {
            return path.IoService.Delete(path, recursiveDeleteIfFolder);
        }

        public static Task<MissingPath> DeleteFolderAsync(this Folder path, CancellationToken cancellationToken, Boolean recursive = false) {
            return path.IoService.DeleteFolderAsync(path, cancellationToken, recursive);
        }

        public static Task<MissingPath> DeleteFileAsync(this File path, CancellationToken cancellationToken) {
            return path.IoService.DeleteFileAsync(path, cancellationToken);
        }

        public static Task<MissingPath> DeleteAsync(this FileOrFolder path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true) {
            return path.IoService.DeleteAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public static Folder EnsureIsFolder(this AbsolutePath path, Boolean createRecursively = false) {
            return path.IoService.EnsureIsFolder(path, createRecursively);
        }

        public static Folder EnsureIsEmptyFolder(this AbsolutePath path, Boolean recursiveDeleteIfFolder = true, Boolean createRecursively = false) {
            return path.IoService.EnsureIsEmptyFolder(path, recursiveDeleteIfFolder, createRecursively);
        }

        public static Task<Folder> EnsureIsFolderAsync(this AbsolutePath path, CancellationToken cancellationToken, Boolean createRecursively = false) {
            return path.IoService.EnsureIsFolderAsync(path, cancellationToken, createRecursively);
        }

        public static Task<Folder> EnsureIsEmptyFolderAsync(this AbsolutePath path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true, Boolean createRecursively = false) {
            return path.IoService.EnsureIsEmptyFolderAsync(path, cancellationToken, recursiveDeleteIfFolder, createRecursively);
        }

        public static FileOrMissingPath EnsureIsNotFolder(this AbsolutePath path, Boolean recursive = false) {
            return path.IoService.EnsureIsNotFolder(path, recursive);
        }

        public static FolderOrMissingPath EnsureIsNotFile(this AbsolutePath path) {
            return path.IoService.EnsureIsNotFile(path);
        }

        public static MissingPath EnsureDoesNotExist(this AbsolutePath path, Boolean recursiveDeleteIfFolder = true) {
            return path.IoService.EnsureDoesNotExist(path, recursiveDeleteIfFolder);
        }

        public static Task<FileOrMissingPath> EnsureIsNotFolderAsync(this AbsolutePath path, CancellationToken cancellationToken, Boolean recursive = false) {
            return path.IoService.EnsureIsNotFolderAsync(path, cancellationToken, recursive);
        }

        public static Task<FolderOrMissingPath> EnsureIsNotFileAsync(this AbsolutePath path, CancellationToken cancellationToken) {
            return path.IoService.EnsureIsNotFileAsync(path, cancellationToken);
        }

        public static Task<MissingPath> EnsureDoesNotExistAsync(this AbsolutePath path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true) {
            return path.IoService.EnsureDoesNotExistAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public static Boolean HasExtension(this AbsolutePath path, string extension) {
            return path.IoService.HasExtension(path, extension);
        }

        public static AbsolutePath Decrypt(this AbsolutePath path) {
            return path.IoService.Decrypt(path);
        }

        public static AbsolutePath Encrypt(this AbsolutePath path) {
            return path.IoService.Encrypt(path);
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

        public static IAbsolutePathTranslation Translate(this AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination) {
            return pathToBeCopied.IoService.Translate(pathToBeCopied, source, destination);
        }

        public static IAbsolutePathTranslation Translate(this AbsolutePath source, AbsolutePath destination) {
            return source.IoService.Translate(source, destination);
        }

        public static IAbsolutePathTranslation Copy(this AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.IoService.Copy(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Copy(this AbsolutePath source, AbsolutePath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.Copy(source, destination, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Move(this AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.IoService.Move(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Move(this AbsolutePath source, AbsolutePath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.Move(source, destination, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation RenameTo(this AbsolutePath source, AbsolutePath target, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.RenameTo(source, target, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Copy(this IAbsolutePathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.Copy(translation, bufferSize, overwrite);
        }

        public static IAbsolutePathTranslation Move(this IAbsolutePathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.Move(translation, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> CopyAsync(this AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.IoService.CopyAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> CopyAsync(this AbsolutePath source, AbsolutePath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.CopyAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> MoveAsync(this AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.IoService.MoveAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> MoveAsync(this AbsolutePath source, AbsolutePath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.MoveAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> RenameToAsync(this AbsolutePath source, AbsolutePath target, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.IoService.RenameToAsync(source, target, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> CopyAsync(this IAbsolutePathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.CopyAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IAbsolutePathTranslation> MoveAsync(this IAbsolutePathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.IoService.MoveAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static IEnumerable<FileOrFolder> EnumerateChildren(this Folder path, string searchPattern = null, Boolean includeFolders = true, Boolean includeFiles = true) {
            return path.IoService.Children(path, searchPattern, includeFolders, includeFiles);
        }

        public static IEnumerable<File> EnumerateChildFiles(this Folder path, string searchPattern = null) {
            return path.IoService.ChildFiles(path, searchPattern);
        }

        public static IEnumerable<Folder> EnumerateChildFolders(this Folder path, string searchPattern = null) {
            return path.IoService.ChildFolders(path, searchPattern);
        }

        public static IEnumerable<FileOrFolder> EnumerateDescendants(this Folder path, string searchPattern = null, Boolean includeFolders = true, Boolean includeFiles = true) {
            return path.IoService.Descendants(path, searchPattern, includeFolders, includeFiles);
        }

        public static IEnumerable<Folder> EnumerateDescendantFolders(this Folder path, string searchPattern = null) {
            return path.IoService.DescendantFolders(path, searchPattern);
        }

        public static IEnumerable<File> EnumerateDescendantFiles(this Folder path, string searchPattern = null) {
            return path.IoService.DescendantFiles(path, searchPattern);
        }
    }
}
