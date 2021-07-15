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
    public interface IFileSystem : IFileProvider
    {
        #region Regions - for implementations
        
        #region Settings
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
        
        #region Settings

        /// <summary>
        /// Lists the file system roots. On Unix-like operating systems, there's only one file system root, and it is '/'.
        /// </summary>
        IObservableReadOnlySet<FolderPath> Roots { get; }
        
        /// <summary>
        /// On Windows this is typically `C:`. On Linux or Mac this is `/`.
        /// </summary>
        FolderPath DefaultRoot { get; }
        
        /// <summary>
        /// Updates the file system roots. On Unix-like operating systems, there's only one file system root, and it is '/'.
        /// On Windows, there can be multiple, e.g. 'C:', 'D:', 'E:'. This method is only useful on Windows.
        /// </summary>
        void UpdateRoots();
        
        Information DefaultBufferSize { get; set; }
        EmptyFolderMode EmptyFolderMode { get; }
        
        int GetBufferSizeOrDefaultInBytes(Information? bufferSize);

        /// <summary>
        /// Indicates that empty directories can exist. This is true for every built-in implementation except the ZipIoService,
        /// because in zip files directories don't exist.
        /// </summary>
        bool CanEmptyDirectoriesExist { get; }
        string DefaultDirectorySeparator { get; }
        bool IsCaseSensitiveByDefault { get; }
        
        #endregion
        
        #region Creating
        
        FolderPath CreateFolder(IMissingPath path,  bool createRecursively = true);

        #endregion

        #region Deleting
        
        /// <summary>
        /// Delete the specified folder. This throws an exception if the path is a file, doesn't exist, or the current
        /// process doesn't have permission to delete the folder. Also, if the directory still contains files or folders
        /// and recursive is false, then this will throw an exception as well.
        /// </summary>
        /// <param name="folderPathfolder to delete</param>
        /// <param name="recursive">Whether to delete the folder recursively.</param>
        /// <returns>The path that was deleted</returns>
        MissingPath DeleteFolder(IFolderPath  folderPath,  bool recursive = true);
        
        /// <summary>
        /// Deletes the specified file
        /// </summary>
        /// <param name="path">The file that should be deleted</param>
        /// <returns>The same path that was specified</returns>
        MissingPath DeleteFile(IFilePath path);
        
        /// <summary>
        /// Deletes the specified file or folder
        /// </summary>
        /// <param name="path">The path to the file or folder that should be deleted</param>
        /// <param name="recursiveDeleteIfFolder">If true and the specified path is a folder, then the folder contents are
        /// recursively deleted before the folder itself is deleted. If false and the specified path is a folder, and that
        /// folder contains other files or folders, then an IOException is thrown. If the path is a file, this parameter
        /// is ignored.</param>
        /// <returns>The path that was just deleted</returns>
        MissingPath Delete(IFileOrFolderPath path, bool recursiveDeleteIfFolder = true);

        /// <summary>
        /// Delete the specified folder. This throws an exception if the path is a file, doesn't exist, or the current
        /// process doesn't have permission to delete the folder. Also, if the directory still contains files or folders
        /// and recursive is false, then this will throw an exception as well.
        /// </summary>
        /// <param name="folderPathfolder to delete</param>
        /// <param name="recursive">Whether to delete the folder recursively.</param>
        /// <returns>The path that was deleted</returns>
        Task<MissingPath> DeleteFolderAsync(IFolderPath folderPath, CancellationToken cancellationToken,  bool recursive = true);
        
        /// <summary>
        /// Deletes the specified file
        /// </summary>
        /// <param name="path">The file that should be deleted</param>
        /// <returns>The same path that was specified</returns>
        Task<MissingPath> DeleteFileAsync(IFilePath path, CancellationToken cancellationToken);
        
        /// <summary>
        /// Deletes the specified file or folder
        /// </summary>
        /// <param name="path">The path to the file or folder that should be deleted</param>
        /// <param name="recursiveDeleteIfFolder">If true and the specified path is a folder, then the folder contents are
        /// recursively deleted before the folder itself is deleted. If false and the specified path is a folder, and that
        /// folder contains other files or folders, then an IOException is thrown. If the path is a file, this parameter
        /// is ignored.</param>
        /// <returns>The path that was just deleted</returns>
        Task<MissingPath> DeleteAsync(IFileOrFolderPath path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true);
        
        #endregion
        
        #region Ensuring is
        
        /// <summary>
        /// Creates the path as a folder if it isn't already. If the path is a file, throws an IOException.
        /// </summary>
        /// <param name="path">The path that should be a folder</param>
        /// <returns>The same path that was specified</returns>
        FolderPath EnsureIsFolder(IFileOrFolderOrMissingPath path,  bool createRecursively = true);

        FolderPath EnsureIsEmptyFolder(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true,  bool createRecursively = true);

        /// <summary>
        /// Creates the path as a folder if it isn't already. If the path is a file, throws an IOException.
        /// </summary>
        /// <param name="path">The path that should be a folder</param>
        /// <returns>The same path that was specified</returns>
        Task<FolderPath> EnsureIsFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,  bool createRecursively = true);

        Task<FolderPath> EnsureIsEmptyFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true,  bool createRecursively = true);

        #endregion
        
        #region Ensuring is not
        
        /// <summary>
        /// Deletes the specified path if it is a folder. If the path is a file or doesn't exist, this returns without
        /// throwing an exception. If recursive is false and the path is a non-empty folder, throws an IOException.
        /// </summary>
        /// <param name="path">The path that may be a folder</param>
        /// <param name="recursive">Whether to recursively delete the contents of the path if the path is a non-empty folder</param>
        /// <returns>The same path that was specified</returns>
        IFileOrMissingPath EnsureIsNotFolder(IFileOrFolderOrMissingPath path,  bool recursive = true);

        IFolderOrMissingPath EnsureIsNotFile(IFileOrFolderOrMissingPath path);

        MissingPath EnsureDoesNotExist(IFileOrFolderOrMissingPath path, bool recursiveDeleteIfFolder = true);

        /// <summary>
        /// Deletes the specified path if it is a folder. If the path is a file or doesn't exist, this returns without
        /// throwing an exception. If recursive is false and the path is a non-empty folder, throws an IOException.
        /// </summary>
        /// <param name="path">The path that may be a folder</param>
        /// <param name="recursive">Whether to recursively delete the contents of the path if the path is a non-empty folder</param>
        /// <returns>The same path that was specified</returns>
        Task<IFileOrMissingPath> EnsureIsNotFolderAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken,  bool recursive = true);

        Task<IFolderOrMissingPath> EnsureIsNotFileAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken);

        Task<MissingPath> EnsureDoesNotExistAsync(IFileOrFolderOrMissingPath path, CancellationToken cancellationToken, bool recursiveDeleteIfFolder = true);

        #endregion

        #region Utilities
        
        bool HasExtension(IFileOrFolderOrMissingPath path, string extension);
        string Name(IFileOrFolderOrMissingPath path);
        string? Extension(IFileOrFolderOrMissingPath path);
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
        IEnumerable<KeyValuePair<FileOrFolderOrMissingPath, string>> ProposeUniqueNamesForMovingPathsToSameFolder(
            IEnumerable<FileOrFolderOrMissingPath> paths);

        #endregion
        
        #region Parsing paths
        
        bool IsAbsoluteWindowsPath(string path);
        bool IsAbsoluteUnixPath(string path);
        StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode);
        StringComparison ToStringComparison(CaseSensitivityMode caseSensitivityMode, CaseSensitivityMode otherCaseSensitivityMode);
        
        bool IsRelativePath(string path);
        bool IsAbsolutePath(string path);
        bool ComponentsAreAbsolute(IReadOnlyList<string> path);
        IMaybe<RelativePath> TryParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        IMaybe<FileOrFolderOrMissingPath> TryParseAbsolutePath(string path, IFolderPath optionallyRelativeTo, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        IMaybe<FileOrFolderOrMissingPath> TryParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        RelativePath ParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        FileOrFolderOrMissingPath ParseAbsolutePath(string path, IFolderPath optionallyRelativeTo, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        FileOrFolderOrMissingPath ParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        
        #endregion
        
        #region Translation stuff
        
        #region Stuff that needs to be implemented
        
        Task<IPathTranslation> CopyFileAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false);
        Task<IPathTranslation> CopyFolderAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false);
        Task<IPathTranslation> MoveFileAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false);
        Task<IPathTranslation> MoveFolderAsync(IPathTranslation translation, CancellationToken cancellationToken,
            Information? bufferSize = default, bool overwrite = false);
        
        IPathTranslation CopyFile(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        IPathTranslation CopyFolder(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        IPathTranslation MoveFile(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        IPathTranslation MoveFolder(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);

        #endregion
        
        IPathTranslation Translate(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination);
        IPathTranslation Translate(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination);

        IPathTranslation Copy(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false);
        IPathTranslation Copy(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false);
        IPathTranslation Move(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false);
        IPathTranslation Move(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            Information? bufferSize = default, bool overwrite = false);

        IPathTranslation RenameTo(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            Information? bufferSize = default, bool overwrite = false);
        
        IPathTranslation Copy(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        IPathTranslation Move(IPathTranslation translation,
            Information? bufferSize = default, bool overwrite = false);
        
        Task<IPathTranslation> CopyAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        Task<IPathTranslation> CopyAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        Task<IPathTranslation> MoveAsync(IFileOrFolderOrMissingPath pathToBeCopied, IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        Task<IPathTranslation> MoveAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath destination,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);

        Task<IPathTranslation> RenameToAsync(IFileOrFolderOrMissingPath source, IFileOrFolderOrMissingPath target,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        
        Task<IPathTranslation> CopyAsync(IPathTranslation translation,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);
        Task<IPathTranslation> MoveAsync(IPathTranslation translation,
            CancellationToken cancellationToken, Information? bufferSize = default, bool overwrite = false);

        #endregion

        #region Path building

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <returns>The children of this path</returns>
        ChildFilesOrFolders Children(IFolderPath folderPath, string searchPattern);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <returns>The children of this path</returns>
        ChildFiles ChildFiles(IFolderPath folderPath, string searchPattern);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <returns>The children of this path</returns>
        ChildFolders ChildFolders(IFolderPath folderPath, string searchPattern);
        
        DescendantFilesOrFolders Descendants(IFolderPath folderPath, string searchPattern);
        DescendantFolders DescendantFolders(IFolderPath folderPath, string searchPattern);
        DescendantFiles DescendantFiles(IFolderPath folderPath, string searchPattern);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <returns>The children of this path</returns>
        ChildFilesOrFolders Children(IFolderPath folderPath);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <returns>The children of this path</returns>
        ChildFiles ChildFiles(IFolderPath folderPath);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <returns>The children of this path</returns>
        ChildFolders ChildFolders(IFolderPath folderPath);
        
        DescendantFilesOrFolders Descendants(IFolderPath folderPath);
        DescendantFolders DescendantFolders(IFolderPath folderPath);
        DescendantFiles DescendantFiles(IFolderPath folderPath);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<IFileOrFolderPath> EnumerateChildren(IFolderPath folderPath, string searchPattern,
            bool includeFolders = true, bool includeFiles = true);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<FilePath> EnumerateChildFiles(IFolderPath folderPath, string searchPattern);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<FolderPath> EnumerateChildFolders(IFolderPath folderPath, string searchPattern);
        
        IEnumerable<IFileOrFolderPath> EnumerateDescendants(IFolderPath folderPath, string searchPattern,
            bool includeFolders = true, bool includeFiles = true);
        IEnumerable<FolderPath> EnumerateDescendantFolders(IFolderPath folderPath, string searchPattern);
        IEnumerable<FilePath> EnumerateDescendantFiles(IFolderPath folderPath, string searchPattern);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<IFileOrFolderPath> EnumerateChildren(IFolderPath folderPath);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<FilePath> EnumerateChildFiles(IFolderPath folderPath);

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="folderPathparent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<FolderPath> EnumerateChildFolders(IFolderPath folderPath);
        
        IEnumerable<IFileOrFolderPath> EnumerateDescendants(IFolderPath folderPath);
        IEnumerable<FolderPath> EnumerateDescendantFolders(IFolderPath folderPath);
        IEnumerable<FilePath> EnumerateDescendantFiles(IFolderPath folderPath);

        bool CanBeSimplified(IFileOrFolderOrMissingPath path);
        FolderPath Root(IFileOrFolderOrMissingPath path);
        RelativePath RelativeTo(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath relativeTo);
        IMaybe<FileOrFolderOrMissingPath> TryCommonWith(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath that);
        FileOrFolderOrMissingPath Simplify(IFileOrFolderOrMissingPath path);
        RelativePath Simplify(RelativePath path);
        IMaybe<FileOrFolderOrMissingPath> TryParent(IFileOrFolderOrMissingPath path);
        FolderPath Parent(IFilePath path);
        IMaybe<FolderPath> TryParent(IFolderPath folderPath);

        /// <summary>
        /// Equivalent to Path.Combine. You can also use the / operator to build paths, like this:
        /// _ioService.CurrentDirectory / "folder1" / "folder2" / "file.txt"
        /// </summary>
        FileOrFolderOrMissingPath Combine(IFolderPath folderPath, params string[] subsequentPathParts);
        FileOrFolderOrMissingPath WithoutExtension(IFileOrFolderOrMissingPath path);
        Uri Child(Uri parent, Uri child);
        FilesOrFoldersOrMissingPaths GlobFiles(IFolderPath folderPath, string pattern);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="folderPathram>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<FolderPath> Ancestors(IFolderPath folderPath, bool includeItself);

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
        IEnumerable<IFileOrFolderPath> Ancestors(IFilePath path, bool includeItself);

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
        IEnumerable<IFolderOrMissingPath> Ancestors(IMissingPath path, bool includeItself);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="folderPathram>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<FolderPath> Ancestors(IFolderPath folderPath);

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
        IEnumerable<FolderPath> Ancestors(IFilePath path);

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
        IEnumerable<IFolderOrMissingPath> Ancestors(IMissingPath path);

        IEnumerable<FileOrFolderOrMissingPath> Ancestors(IFileOrFolderOrMissingPath path, bool includeItself);

        /// <summary>
        ///     Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from).
        ///     For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        ///     C:\Users\myusername
        ///     C:\Users
        ///     C:
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IEnumerable<FileOrFolderOrMissingPath> Ancestors(IFileOrFolderOrMissingPath path);

        IMaybe<FileOrFolderOrMissingPath> TryDescendant(IFileOrFolderOrMissingPath path, params IFileOrFolderOrMissingPath[] paths);
        IMaybe<FileOrFolderOrMissingPath> TryDescendant(IFileOrFolderOrMissingPath path, params string[] paths);
        IMaybe<FolderPath> TryAncestor(IFileOrFolderOrMissingPath path, int level);
        bool IsAncestorOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleDescendant);
        bool IsDescendantOf(IFileOrFolderOrMissingPath path, IFileOrFolderOrMissingPath possibleAncestor);
        IMaybe<FileOrFolderOrMissingPath> TryGetCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2);
        IMaybe<Uri> TryGetCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2);
        IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2);
        IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(IFileOrFolderOrMissingPath path1, IFileOrFolderOrMissingPath path2);

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        IMaybe<FileOrFolderOrMissingPath> TryWithExtension(IFileOrFolderOrMissingPath path, string differentExtension);

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        IMaybe<FileOrFolderOrMissingPath> TryWithExtension(IFileOrFolderOrMissingPath path, Func<string, string> differentExtension);

        #endregion

        #region File metadata
        
        bool Exists(IFileOrFolderOrMissingPath path);
        PathType Type(IFileOrFolderOrMissingPath path);
        bool HasExtension(IFileOrFolderOrMissingPath path);
        bool IsFile(IFileOrFolderOrMissingPath path);
        bool IsFolder(IFileOrFolderOrMissingPath path);
        bool IsReadOnly(IFilePath path);
        Information FileSize(IFilePath path);
        FileAttributes Attributes(IFilePath attributes);
        DateTimeOffset CreationTime(IFilePath attributes);
        DateTimeOffset LastAccessTime(IFilePath attributes);
        DateTimeOffset LastWriteTime(IFilePath attributes);

        #endregion
        
        #region File reading

        BufferEnumerator ReadBuffers(IFilePath path, FileShare fileShare = FileShare.None,
            Information? bufferSize = default, int paddingAtStart = 0, int paddingAtEnd = 0);
        #endregion
        
        #region File writing
        FilePath WriteAllBytes(IFileOrMissingPath path, byte[] bytes,  bool createRecursively = true);
        #endregion
        
        #region File open for reading or writing
        Stream Open(IFileOrMissingPath path, FileMode fileMode,
            FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None,
            FileOptions fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan,
            Information? bufferSize = default,  bool createRecursively = true);
        #endregion
        
        #region LINQ-style APIs
        
        /// <summary>
        /// Returns an IQueryable that converts expressions like AbsolutePaths.Where(path => path.Contains("test")) into
        /// efficient calls to the .NET file system APIs.
        /// </summary>
        IQueryable<FileOrFolderOrMissingPath> Query();

        ISetChanges<FileOrFolderOrMissingPath> ToLiveLinq(IFolderPath folderPath, bool includeFileContentChanges,
            bool includeSubFolders, string pattern);
        
        #endregion
    }
}