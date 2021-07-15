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
        public static IEnumerable<IFileOrFolderPath> EnumerateChildren(this IFolderPath folderPath, string searchPattern, Boolean includeFolders = true, Boolean includeFiles = true) {
            return folderPath.FileSystem.EnumerateChildren(folderPath, searchPattern, includeFolders, includeFiles);
        }

        public static IEnumerable<FilePath> EnumerateChildFiles(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.EnumerateChildFiles(folderPath, searchPattern);
        }

        public static IEnumerable<FolderPath> EnumerateChildFolders(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.EnumerateChildFolders(folderPath, searchPattern);
        }

        public static IEnumerable<IFileOrFolderPath> EnumerateDescendants(this IFolderPath folderPath, string searchPattern, Boolean includeFolders = true, Boolean includeFiles = true) {
            return folderPath.FileSystem.EnumerateDescendants(folderPath, searchPattern, includeFolders, includeFiles);
        }

        public static IEnumerable<FolderPath> EnumerateDescendantFolders(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.EnumerateDescendantFolders(folderPath, searchPattern);
        }

        public static IEnumerable<FilePath> EnumerateDescendantFiles(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.EnumerateDescendantFiles(folderPath, searchPattern);
        }

        public static IEnumerable<IFileOrFolderPath> EnumerateChildren(this IFolderPath folderPath) {
            return folderPath.FileSystem.EnumerateChildren(folderPath);
        }

        public static IEnumerable<FilePath> EnumerateChildFiles(this IFolderPath folderPath) {
            return folderPath.FileSystem.EnumerateChildFiles(folderPath);
        }

        public static IEnumerable<FolderPath> EnumerateChildFolders(this IFolderPath folderPath) {
            return folderPath.FileSystem.EnumerateChildFolders(folderPath);
        }

        public static IEnumerable<IFileOrFolderPath> EnumerateDescendants(this IFolderPath folderPath) {
            return folderPath.FileSystem.EnumerateDescendants(folderPath);
        }

        public static IEnumerable<FolderPath> EnumerateDescendantFolders(this IFolderPath folderPath) {
            return folderPath.FileSystem.EnumerateDescendantFolders(folderPath);
        }

        public static IEnumerable<FilePath> EnumerateDescendantFiles(this IFolderPath folderPath) {
            return folderPath.FileSystem.EnumerateDescendantFiles(folderPath);
        }

        public static RelativePath RelativeTo(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath relativeTo) {
            return path.FileSystem.RelativeTo(path, relativeTo);
        }

        public static IMaybe<FileOrFolderOrMissingPath> TryCommonWith(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that) {
            return path.FileSystem.TryCommonWith(path, that);
        }

        public static FileOrFolderOrMissingPath CommonWith(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that) {
            return path.FileSystem.TryCommonWith(path, that).Value;
        }

        public static FileOrFolderOrMissingPath Simplify(this IFileOrFolderOrMissingPath path) {
            return path.FileSystem.Simplify(path);
        }

        public static RelativePath Simplify(this RelativePath path) {
            return path.FileSystem.Simplify(path);
        }

        public static FileOrFolderOrMissingPath Combine(this IFolderPath folderPath, String[] subsequentPathParts) {
            return folderPath.FileSystem.Combine(folderPath, subsequentPathParts);
        }

        public static FilesOrFoldersOrMissingPaths GlobFiles(this IFolderPath folderPath, string pattern) {
            return folderPath.FileSystem.GlobFiles(folderPath, pattern);
        }

        public static IEnumerable<FolderPath> Ancestors(this IFolderPath folderPath, Boolean includeItself) {
            return folderPath.FileSystem.Ancestors(folderPath, includeItself);
        }

        public static IEnumerable<IFileOrFolderPath> Ancestors(this IFilePath path, Boolean includeItself) {
            return path.FileSystem.Ancestors(path, includeItself);
        }

        public static IEnumerable<IFolderOrMissingPath> Ancestors(this IMissingPath path, Boolean includeItself) {
            return path.FileSystem.Ancestors(path, includeItself);
        }

        public static IEnumerable<FileOrFolderOrMissingPath> Ancestors(this IFileOrFolderOrMissingPath path, Boolean includeItself) {
            return path.FileSystem.Ancestors(path, includeItself);
        }

        public static IMaybe<FileOrFolderOrMissingPath> TryDescendant(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath[] paths) {
            return path.FileSystem.TryDescendant(path, paths);
        }

        public static FileOrFolderOrMissingPath Descendant(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath[] paths) {
            return path.FileSystem.TryDescendant(path, paths).Value;
        }

        public static IMaybe<FileOrFolderOrMissingPath> TryDescendant(this IFileOrFolderOrMissingPath path, String[] paths) {
            return path.FileSystem.TryDescendant(path, paths);
        }

        public static FileOrFolderOrMissingPath Descendant(this IFileOrFolderOrMissingPath path, String[] paths) {
            return path.FileSystem.TryDescendant(path, paths).Value;
        }

        public static IMaybe<FolderPath> TryAncestor(this IFileOrFolderOrMissingPath path, int level) {
            return path.FileSystem.TryAncestor(path, level);
        }

        public static FolderPath Ancestor(this IFileOrFolderOrMissingPath path, int level) {
            return path.FileSystem.TryAncestor(path, level).Value;
        }

        public static Boolean IsAncestorOf(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleDescendant) {
            return path.FileSystem.IsAncestorOf(path, possibleDescendant);
        }

        public static Boolean IsDescendantOf(this IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleAncestor) {
            return path.FileSystem.IsDescendantOf(path, possibleAncestor);
        }

        public static IMaybe<FileOrFolderOrMissingPath> TryGetCommonAncestry(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.FileSystem.TryGetCommonAncestry(path1, path2);
        }

        public static FileOrFolderOrMissingPath GetCommonAncestry(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.FileSystem.TryGetCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<Uri> TryGetCommonDescendants(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.FileSystem.TryGetCommonDescendants(path1, path2);
        }

        public static Uri GetCommonDescendants(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.FileSystem.TryGetCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.FileSystem.TryGetNonCommonDescendants(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonDescendants(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.FileSystem.TryGetNonCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.FileSystem.TryGetNonCommonAncestry(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonAncestry(this IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2) {
            return path1.FileSystem.TryGetNonCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<FileOrFolderOrMissingPath> TryWithExtension(this IFileOrFolderOrMissingPath path, string differentExtension) {
            return path.FileSystem.TryWithExtension(path, differentExtension);
        }

        public static FileOrFolderOrMissingPath WithExtension(this IFileOrFolderOrMissingPath path, string differentExtension) {
            return path.FileSystem.TryWithExtension(path, differentExtension).Value;
        }

        public static IMaybe<FileOrFolderOrMissingPath> TryWithExtension(this IFileOrFolderOrMissingPath path, Func<string, string> differentExtension) {
            return path.FileSystem.TryWithExtension(path, differentExtension);
        }

        public static FileOrFolderOrMissingPath WithExtension(this IFileOrFolderOrMissingPath path, Func<string, string> differentExtension) {
            return path.FileSystem.TryWithExtension(path, differentExtension).Value;
        }

        public static BufferEnumerator ReadBuffers(this IFilePath path, FileShare fileShare = FileShare.None, Nullable<Information> bufferSize = null, int paddingAtStart = 0, int paddingAtEnd = 0) {
            return path.FileSystem.ReadBuffers(path, fileShare, bufferSize, paddingAtStart, paddingAtEnd);
        }

        public static FilePath WriteAllBytes(this IFileOrMissingPath path, Byte[] bytes, Boolean createRecursively = true) {
            return path.FileSystem.WriteAllBytes(path, bytes, createRecursively);
        }

        public static Stream Open(this IFileOrMissingPath path, FileMode fileMode, FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None, FileOptions fileOptions = FileOptions.SequentialScan | FileOptions.Asynchronous, Nullable<Information> bufferSize = null, Boolean createRecursively = true) {
            return path.FileSystem.Open(path, fileMode, fileAccess, fileShare, fileOptions, bufferSize, createRecursively);
        }

        public static ISetChanges<FileOrFolderOrMissingPath> ToLiveLinq(this IFolderPath folderPath, Boolean includeFileContentChanges, Boolean includeSubFolders, string pattern) {
            return folderPath.FileSystem.ToLiveLinq(folderPath, includeFileContentChanges, includeSubFolders, pattern);
        }

        public static FolderPath CreateFolder(this IMissingPath path, Boolean createRecursively = true) {
            return path.FileSystem.CreateFolder(path, createRecursively);
        }

        public static MissingPath DeleteFolder(this IFolderPath folderPath, Boolean recursive = true) {
            return folderPath.FileSystem.DeleteFolder(folderPath, recursive);
        }

        public static MissingPath DeleteFile(this IFilePath path) {
            return path.FileSystem.DeleteFile(path);
        }

        public static MissingPath Delete(this IFileOrFolderPath path, Boolean recursiveDeleteIfFolder = true) {
            return path.FileSystem.Delete(path, recursiveDeleteIfFolder);
        }

        public static Task<MissingPath> DeleteFolderAsync(this IFolderPath folderPath, CancellationToken cancellationToken, Boolean recursive = true) {
            return folderPath.FileSystem.DeleteFolderAsync(folderPath, cancellationToken, recursive);
        }

        public static Task<MissingPath> DeleteFileAsync(this IFilePath path, CancellationToken cancellationToken) {
            return path.FileSystem.DeleteFileAsync(path, cancellationToken);
        }

        public static Task<MissingPath> DeleteAsync(this IFileOrFolderPath path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true) {
            return path.FileSystem.DeleteAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public static FolderPath EnsureIsFolder(this IFileOrFolderOrMissingPath path, Boolean createRecursively = true) {
            return path.FileSystem.EnsureIsFolder(path, createRecursively);
        }

        public static FolderPath EnsureIsEmptyFolder(this IFileOrFolderOrMissingPath path, Boolean recursiveDeleteIfFolder = true, Boolean createRecursively = true) {
            return path.FileSystem.EnsureIsEmptyFolder(path, recursiveDeleteIfFolder, createRecursively);
        }

        public static Task<FolderPath> EnsureIsFolderAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, Boolean createRecursively = true) {
            return path.FileSystem.EnsureIsFolderAsync(path, cancellationToken, createRecursively);
        }

        public static Task<FolderPath> EnsureIsEmptyFolderAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true, Boolean createRecursively = true) {
            return path.FileSystem.EnsureIsEmptyFolderAsync(path, cancellationToken, recursiveDeleteIfFolder, createRecursively);
        }

        public static IFileOrMissingPath EnsureIsNotFolder(this IFileOrFolderOrMissingPath path, Boolean recursive = true) {
            return path.FileSystem.EnsureIsNotFolder(path, recursive);
        }

        public static IFolderOrMissingPath EnsureIsNotFile(this IFileOrFolderOrMissingPath path) {
            return path.FileSystem.EnsureIsNotFile(path);
        }

        public static MissingPath EnsureDoesNotExist(this IFileOrFolderOrMissingPath path, Boolean recursiveDeleteIfFolder = true) {
            return path.FileSystem.EnsureDoesNotExist(path, recursiveDeleteIfFolder);
        }

        public static Task<IFileOrMissingPath> EnsureIsNotFolderAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, Boolean recursive = true) {
            return path.FileSystem.EnsureIsNotFolderAsync(path, cancellationToken, recursive);
        }

        public static Task<IFolderOrMissingPath> EnsureIsNotFileAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken) {
            return path.FileSystem.EnsureIsNotFileAsync(path, cancellationToken);
        }

        public static Task<MissingPath> EnsureDoesNotExistAsync(this IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, Boolean recursiveDeleteIfFolder = true) {
            return path.FileSystem.EnsureDoesNotExistAsync(path, cancellationToken, recursiveDeleteIfFolder);
        }

        public static Boolean HasExtension(this IFileOrFolderOrMissingPath path, string extension) {
            return path.FileSystem.HasExtension(path, extension);
        }

        public static Task<IPathTranslation> CopyFileAsync(this IPathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.CopyFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> CopyFolderAsync(this IPathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.CopyFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> MoveFileAsync(this IPathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.MoveFileAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> MoveFolderAsync(this IPathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.MoveFolderAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static IPathTranslation CopyFile(this IPathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.CopyFile(translation, bufferSize, overwrite);
        }

        public static IPathTranslation CopyFolder(this IPathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.CopyFolder(translation, bufferSize, overwrite);
        }

        public static IPathTranslation MoveFile(this IPathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.MoveFile(translation, bufferSize, overwrite);
        }

        public static IPathTranslation MoveFolder(this IPathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.MoveFolder(translation, bufferSize, overwrite);
        }

        public static IPathTranslation Translate(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination) {
            return pathToBeCopied.FileSystem.Translate(pathToBeCopied, source, destination);
        }

        public static IPathTranslation Translate(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination) {
            return source.FileSystem.Translate(source, destination);
        }

        public static IPathTranslation Copy(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.FileSystem.Copy(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public static IPathTranslation Copy(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.FileSystem.Copy(source, destination, bufferSize, overwrite);
        }

        public static IPathTranslation Move(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.FileSystem.Move(pathToBeCopied, source, destination, bufferSize, overwrite);
        }

        public static IPathTranslation Move(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.FileSystem.Move(source, destination, bufferSize, overwrite);
        }

        public static IPathTranslation RenameTo(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.FileSystem.RenameTo(source, target, bufferSize, overwrite);
        }

        public static IPathTranslation Copy(this IPathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.Copy(translation, bufferSize, overwrite);
        }

        public static IPathTranslation Move(this IPathTranslation translation, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.Move(translation, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> CopyAsync(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.FileSystem.CopyAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> CopyAsync(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.FileSystem.CopyAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> MoveAsync(this IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return pathToBeCopied.FileSystem.MoveAsync(pathToBeCopied, source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> MoveAsync(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.FileSystem.MoveAsync(source, destination, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> RenameToAsync(this IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return source.FileSystem.RenameToAsync(source, target, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> CopyAsync(this IPathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.CopyAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static Task<IPathTranslation> MoveAsync(this IPathTranslation translation, CancellationToken cancellationToken, Nullable<Information> bufferSize = null, Boolean overwrite = false) {
            return translation.FileSystem.MoveAsync(translation, cancellationToken, bufferSize, overwrite);
        }

        public static ChildFilesOrFolders Children(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.Children(folderPath, searchPattern);
        }

        public static ChildFiles ChildFiles(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.ChildFiles(folderPath, searchPattern);
        }

        public static ChildFolders ChildFolders(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.ChildFolders(folderPath, searchPattern);
        }

        public static DescendantFilesOrFolders Descendants(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.Descendants(folderPath, searchPattern);
        }

        public static DescendantFolders DescendantFolders(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.DescendantFolders(folderPath, searchPattern);
        }

        public static DescendantFiles DescendantFiles(this IFolderPath folderPath, string searchPattern) {
            return folderPath.FileSystem.DescendantFiles(folderPath, searchPattern);
        }

    }
}
