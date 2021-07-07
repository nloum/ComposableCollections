using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using Microsoft.Extensions.FileProviders;
using SimpleMonads;
using TreeLinq;
using UnitsNet;

namespace IoFluently
{
    public interface IIoService : IFileProvider
    {
        #region Regions - for implementations
        
        #region Environmental stuff
        #endregion
        #region Creating
        #endregion
        #region Deleting
        #endregion
        #region Ensuring is
        #endregion
        #region Ensuring is not
        #endregion
        #region Utilities
        #endregion
        #region Parsing paths
        #endregion
        #region Translation stuff
        #endregion
        #region Path building
        #endregion
        #region File metadata
        #endregion
        #region File reading
        #endregion
        #region File writing
        #endregion
        #region File open for reading or writing
        #endregion
        #region LINQ-style APIs
        #endregion
        
        #endregion
        
        #region Environmental stuff

        Information DefaultBufferSize { get; set; }

        int GetBufferSizeOrDefaultInBytes(Information? bufferSize);

        /// <summary>
        /// Indicates that empty directories can exist. This is true for every built-in implementation except the ZipIoService,
        /// because in zip files directories don't exist.
        /// </summary>
        bool CanEmptyDirectoriesExist { get; }
        void SetDefaultRelativePathBase(Folder defaultRelativePathBase);
        void UnsetDefaultRelativePathBase();
        Folder DefaultRelativePathBase { get; }
        /// <summary>
        /// Lists the file system roots. On Unix-like operating systems, there's only one file system root, and it is '/'.
        /// </summary>
        IObservableReadOnlySet<Folder> Roots { get; }

        string DefaultDirectorySeparator { get; }
        bool IsCaseSensitiveByDefault { get; }
        
        /// <summary>
        /// Returns the path to the user's temporary folder
        /// </summary>
        /// <returns>The path to the user's temporary folder</returns>
        Folder GetTemporaryFolder();

        #endregion
        
        #region Creating
        
        Folder CreateFolder(MissingPath path, bool createRecursively = false);

        #endregion

        #region Deleting
        
        /// <summary>
        /// Delete the specified folder. This throws an exception if the path is a file, doesn't exist, or the current
        /// process doesn't have permission to delete the folder. Also, if the directory still contains files or folders
        /// and recursive is false, then this will throw an exception as well.
        /// </summary>
        /// <param name="path">The folder to delete</param>
        /// <param name="recursive">Whether to delete the folder recursively.</param>
        /// <returns>The path that was deleted</returns>
        MissingPath DeleteFolder(Folder path, bool recursive = false);
        
        /// <summary>
        /// Deletes the specified file
        /// </summary>
        /// <param name="path">The file that should be deleted</param>
        /// <returns>The same path that was specified</returns>
        MissingPath DeleteFile(File path);
        
        /// <summary>
        /// Deletes the specified file or folder
        /// </summary>
        /// <param name="path">The path to the file or folder that should be deleted</param>
        /// <param name="recursiveDeleteIfFolder">If true and the specified path is a folder, then the folder contents are
        /// recursively deleted before the folder itself is deleted. If false and the specified path is a folder, and that
        /// folder contains other files or folders, then an IOException is thrown. If the path is a file, this parameter
        /// is ignored.</param>
        /// <returns>The path that was just deleted</returns>
        MissingPath Delete(FileOrFolder path, bool recursiveDeleteIfFolder = true);

        /// <summary>
        /// Delete the specified folder. This throws an exception if the path is a file, doesn't exist, or the current
        /// process doesn't have permission to delete the folder. Also, if the directory still contains files or folders
        /// and recursive is false, then this will throw an exception as well.
        /// </summary>
        /// <param name="path">The folder to delete</param>
        /// <param name="recursive">Whether to delete the folder recursively.</param>
        /// <returns>The path that was deleted</returns>
        Task<MissingPath> DeleteFolderAsync(Folder path, CancellationToken cancellationToken, bool recursive = false);
        
        /// <summary>
        /// Deletes the specified file
        /// </summary>
        /// <param name="path">The file that should be deleted</param>
        /// <returns>The same path that was specified</returns>
        Task<MissingPath> DeleteFileAsync(File path, CancellationToken cancellationToken);
        
        /// <summary>
        /// Deletes the specified file or folder
        /// </summary>
        /// <param name="path">The path to the file or folder that should be deleted</param>
        /// <param name="recursiveDeleteIfFolder">If true and the specified path is a folder, then the folder contents are
        /// recursively deleted before the folder itself is deleted. If false and the specified path is a folder, and that
        /// folder contains other files or folders, then an IOException is thrown. If the path is a file, this parameter
        /// is ignored.</param>
        /// <returns>The path that was just deleted</returns>
        Task<MissingPath> DeleteAsync(FileOrFolder path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true);
        
        #endregion
        
        #region Ensuring is
        
        /// <summary>
        /// Creates the path as a folder if it isn't already. If the path is a file, throws an IOException.
        /// </summary>
        /// <param name="path">The path that should be a folder</param>
        /// <returns>The same path that was specified</returns>
        Folder EnsureIsFolder(AbsolutePath path, bool createRecursively = false);

        Folder EnsureIsEmptyFolder(AbsolutePath path, bool recursiveDeleteIfFolder = true, bool createRecursively = false);

        /// <summary>
        /// Creates the path as a folder if it isn't already. If the path is a file, throws an IOException.
        /// </summary>
        /// <param name="path">The path that should be a folder</param>
        /// <returns>The same path that was specified</returns>
        Task<Folder> EnsureIsFolderAsync(AbsolutePath path, CancellationToken cancellationToken, bool createRecursively = false);

        Task<Folder> EnsureIsEmptyFolderAsync(AbsolutePath path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true, bool createRecursively = false);

        #endregion
        
        #region Ensuring is not
        
        /// <summary>
        /// Deletes the specified path if it is a folder. If the path is a file or doesn't exist, this returns without
        /// throwing an exception. If recursive is false and the path is a non-empty folder, throws an IOException.
        /// </summary>
        /// <param name="path">The path that may be a folder</param>
        /// <param name="recursive">Whether to recursively delete the contents of the path if the path is a non-empty folder</param>
        /// <returns>The same path that was specified</returns>
        FileOrMissingPath EnsureIsNotFolder(AbsolutePath path, bool recursive = false);

        FolderOrMissingPath EnsureIsNotFile(AbsolutePath path);

        MissingPath EnsureDoesNotExist(AbsolutePath path, bool recursiveDeleteIfFolder = true);

        /// <summary>
        /// Deletes the specified path if it is a folder. If the path is a file or doesn't exist, this returns without
        /// throwing an exception. If recursive is false and the path is a non-empty folder, throws an IOException.
        /// </summary>
        /// <param name="path">The path that may be a folder</param>
        /// <param name="recursive">Whether to recursively delete the contents of the path if the path is a non-empty folder</param>
        /// <returns>The same path that was specified</returns>
        Task<FileOrMissingPath> EnsureIsNotFolderAsync(AbsolutePath path, CancellationToken cancellationToken, bool recursive = false);

        Task<FolderOrMissingPath> EnsureIsNotFileAsync(AbsolutePath path, CancellationToken cancellationToken);

        Task<MissingPath> EnsureDoesNotExistAsync(AbsolutePath path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true);

        #endregion

        #region Utilities
        
        /// <summary>
        /// Updates the file system roots. On Unix-like operating systems, there's only one file system root, and it is '/'.
        /// On Windows, there can be multiple, e.g. 'C:', 'D:', 'E:'. This method is only useful on Windows.
        /// </summary>
        void UpdateRoots();
        bool HasExtension(IHasAbsolutePath path, string extension);
        bool MayCreateFile(FileMode fileMode);
        bool IsImageUri(Uri uri);
        bool IsVideoUri(Uri uri);
        string StripQuotes(string str);
        string SurroundWithDoubleQuotesIfNecessary(string str);

        /// <summary>
        ///     Returns a regex that filters files the same as the specified pattern.
        ///     From here: http://www.java2s.com/Code/CSharp/Regular-Expressions/Checksifnamematchespatternwithandwildcards.htm
        ///     Copyright:   Julijan ?ribar, 2004-2007
        ///     This software is provided 'as-is', without any express or implied
        ///     warranty.  In no event will the author(s) be held liable for any damages
        ///     arising from the use of this software.
        ///     Permission is granted to anyone to use this software for any purpose,
        ///     including commercial applications, and to alter it and redistribute it
        ///     freely, subject to the following restrictions:
        ///     1. The origin of this software must not be misrepresented; you must not
        ///     claim that you wrote the original software. If you use this software
        ///     in a product, an acknowledgment in the product documentation would be
        ///     appreciated but is not required.
        ///     2. Altered source versions must be plainly marked as such, and must not be
        ///     misrepresented as being the original software.
        ///     3. This notice may not be removed or altered from any source distribution.
        /// </summary>
        /// <param name="filename">
        ///     Name to match.
        /// </param>
        /// <param name="pattern">
        ///     Pattern to match to.
        /// </param>
        /// <returns>
        ///     <c>true</c> if name matches pattern, otherwise <c>false</c>.
        /// </returns>
        Regex FileNamePatternToRegex(string pattern);
        
        IOpenFilesTrackingService OpenFilesTrackingService { get; }
        
        /// <summary>
        ///     Given a bunch of files or folders in different places that may have the same name,
        ///     create unique names for those files and folders based on their original name and the
        ///     paths to those files and folders.
        /// </summary>
        /// <param name="paths">
        ///     The paths to the files and folders that may have the same name but
        ///     be in different locations.
        /// </param>
        /// <returns>A mapping from the original file path to the new suggested file name.</returns>
        IEnumerable<KeyValuePair<AbsolutePath, string>> ProposeUniqueNamesForMovingPathsToSameFolder(
            IEnumerable<AbsolutePath> paths);

        #endregion
        
        #region Parsing paths
        
        bool IsAbsoluteWindowsPath(string path);
        bool IsAbsoluteUnixPath(string path);
        StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode);
        StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode, CaseSensitivityMode otherCaseSensitivityMode);
        
        /// <summary>
        /// Parses the path. If the path is a relative path, assumes that it is relative to <see cref="IoService.DefaultRelativePathBase"/>.
        /// </summary>
        AbsolutePath ParsePathRelativeToDefault(string path);
        
        bool IsRelativePath(string path);
        bool IsAbsolutePath(string path);
        bool ComponentsAreAbsolute(IReadOnlyList<string> path);
        IMaybe<RelativePath> TryParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        IMaybe<AbsolutePath> TryParseAbsolutePath(string path, Folder optionallyRelativeTo, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        IMaybe<AbsolutePath> TryParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        RelativePath ParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        AbsolutePath ParseAbsolutePath(string path, Folder optionallyRelativeTo, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        AbsolutePath ParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        
        #endregion
        
        #region Translation stuff
        
        #region Stuff that needs to be implemented
        
        Task<IAbsolutePathTranslation> CopyFileAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false);
        Task<IAbsolutePathTranslation> CopyFolderAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false);
        Task<IAbsolutePathTranslation> MoveFileAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false);
        Task<IAbsolutePathTranslation> MoveFolderAsync(IAbsolutePathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false);
        
        IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        IAbsolutePathTranslation CopyFolder(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        IAbsolutePathTranslation MoveFolder(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);

        #endregion
        
        IAbsolutePathTranslation Translate(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination);
        IAbsolutePathTranslation Translate(AbsolutePath source, AbsolutePath destination);

        IAbsolutePathTranslation Copy(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination,
            Information? bufferSize = default, bool overwrite = false);
        IAbsolutePathTranslation Copy(AbsolutePath source, AbsolutePath destination,
            Information? bufferSize = default, bool overwrite = false);
        IAbsolutePathTranslation Move(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination,
            Information? bufferSize = default, bool overwrite = false);
        IAbsolutePathTranslation Move(AbsolutePath source, AbsolutePath destination,
            Information? bufferSize = default, bool overwrite = false);

        IAbsolutePathTranslation RenameTo(AbsolutePath source, AbsolutePath target,
            Information? bufferSize = default, bool overwrite = false);
        
        IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        IAbsolutePathTranslation Move(IAbsolutePathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        
        Task<IAbsolutePathTranslation> CopyAsync(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        Task<IAbsolutePathTranslation> CopyAsync(AbsolutePath source, AbsolutePath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        Task<IAbsolutePathTranslation> MoveAsync(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        Task<IAbsolutePathTranslation> MoveAsync(AbsolutePath source, AbsolutePath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);

        Task<IAbsolutePathTranslation> RenameToAsync(AbsolutePath source, AbsolutePath target,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        
        Task<IAbsolutePathTranslation> CopyAsync(IAbsolutePathTranslation translation,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        Task<IAbsolutePathTranslation> MoveAsync(IAbsolutePathTranslation translation,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);

        #endregion

        #region Path building

        /// <summary>
        /// Creates a non-existent path that is unique. The parent folder of this path is guaranteed to exist.
        /// This does not create a file or folder for this path.
        /// </summary>
        /// <param name="extension">The file extension for the path. If null, the resulting path has no extension.</param>
        /// <returns>A path that does not exist but whose parent folder exists</returns>
        MissingPath GenerateUniqueTemporaryPath(string extension = null);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="path">The parent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<FileOrFolder> Children(Folder path, string searchPattern = null,
            bool includeFolders = true, bool includeFiles = true);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="path">The parent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<File> ChildFiles(Folder path, string searchPattern = null);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="path">The parent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<Folder> ChildFolders(Folder path, string searchPattern = null);
        
        IEnumerable<FileOrFolder> Descendants(Folder path, string searchPattern = null,
            bool includeFolders = true, bool includeFiles = true);
        IEnumerable<Folder> DescendantFolders(Folder path, string searchPattern = null);
        IEnumerable<File> DescendantFiles(Folder path, string searchPattern = null);
        
        bool CanBeSimplified(AbsolutePath path);
        Folder Root(AbsolutePath path);
        RelativePath RelativeTo(AbsolutePath path, AbsolutePath relativeTo);
        IMaybe<AbsolutePath> TryCommonWith(AbsolutePath path, AbsolutePath that);
        AbsolutePath Simplify(AbsolutePath path);
        RelativePath Simplify(RelativePath path);
        IMaybe<AbsolutePath> TryParent(AbsolutePath path);
        Folder Parent(File path);
        IMaybe<Folder> TryParent(Folder path);

        /// <summary>
        /// Equivalent to Path.Combine. You can also use the / operator to build paths, like this:
        /// _ioService.CurrentDirectory / "folder1" / "folder2" / "file.txt"
        /// </summary>
        AbsolutePath Combine(Folder path, params string[] subsequentPathParts);
        AbsolutePath WithoutExtension(AbsolutePath path);
        Uri Child(Uri parent, Uri child);
        AbsolutePaths GlobFiles(Folder path, string pattern);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<Folder> Ancestors(Folder path, bool includeItself);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<FileOrFolder> Ancestors(File path, bool includeItself);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<FolderOrMissingPath> Ancestors(MissingPath path, bool includeItself);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<Folder> Ancestors(Folder path);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<Folder> Ancestors(File path);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<FolderOrMissingPath> Ancestors(MissingPath path);

        IEnumerable<AbsolutePath> Ancestors(AbsolutePath path, bool includeItself);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IEnumerable<AbsolutePath> Ancestors(AbsolutePath path);

        IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params AbsolutePath[] paths);
        IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params string[] paths);
        IMaybe<Folder> TryAncestor(AbsolutePath path, int level);
        bool IsAncestorOf(AbsolutePath path, AbsolutePath possibleDescendant);
        bool IsDescendantOf(AbsolutePath path, AbsolutePath possibleAncestor);
        IMaybe<AbsolutePath> TryGetCommonAncestry(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Uri> TryGetCommonDescendants(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(AbsolutePath path1, AbsolutePath path2);

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        IMaybe<AbsolutePath> TryWithExtension(AbsolutePath path, string differentExtension);

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        IMaybe<AbsolutePath> TryWithExtension(AbsolutePath path, Func<string, string> differentExtension);

        #endregion

        #region File metadata
        
        bool Exists(AbsolutePath path);
        PathType Type(AbsolutePath path);
        bool HasExtension(IHasAbsolutePath path);
        bool IsFile(AbsolutePath path);
        bool IsFolder(AbsolutePath path);
        bool IsReadOnly(File path);
        Information FileSize(File path);
        FileAttributes Attributes(File attributes);
        DateTimeOffset CreationTime(File attributes);
        DateTimeOffset LastAccessTime(File attributes);
        DateTimeOffset LastWriteTime(File attributes);

        #endregion
        
        #region File reading

        BufferEnumerator ReadBuffers(File path, FileShare fileShare = FileShare.None,
            Information? bufferSize = default, int paddingAtStart = 0, int paddingAtEnd = 0);
        #endregion
        
        #region File writing
        File WriteAllBytes(FileOrMissingPath path, byte[] bytes, bool createRecursively = false);
        File WriteAllBytes(File path, byte[] bytes, bool createRecursively = false);
        #endregion
        
        #region File open for reading or writing
        Stream Open(FileOrMissingPath path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan,
            Information? bufferSize = default, bool createRecursively = false);
        Stream Open(File path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan,
            Information? bufferSize = default, bool createRecursively = false);
        #endregion
        
        #region LINQ-style APIs
        
        /// <summary>
        /// Returns an IQueryable that converts expressions like AbsolutePaths.Where(path => path.Contains("test")) into
        /// efficient calls to the .NET file system APIs.
        /// </summary>
        IQueryable<AbsolutePath> Query();

        ISetChanges<AbsolutePath> ToLiveLinq(Folder path, bool includeFileContentChanges,
            bool includeSubFolders, string pattern);
        IObservable<Unit> ObserveChanges(AbsolutePath path);
        IObservable<Unit> ObserveChanges(AbsolutePath path, NotifyFilters filters);
        IObservable<PathType> ObservePathType(AbsolutePath path);
        IObservable<AbsolutePath> Renamings(AbsolutePath path);
        
        #endregion
    }
}