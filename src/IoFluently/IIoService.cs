using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Indicates that empty directories can exist. This is true for every built-in implementation except the ZipIoService,
        /// because in zip files directories don't exist.
        /// </summary>
        bool CanEmptyDirectoriesExist { get; }
        void SetDefaultRelativePathBase(AbsolutePath defaultRelativePathBase);
        void UnsetDefaultRelativePathBase();
        AbsolutePath DefaultRelativePathBase { get; }
        /// <summary>
        /// Lists the file system roots. On Unix-like operating systems, there's only one file system root, and it is '/'.
        /// </summary>
        IObservableReadOnlySet<AbsolutePath> Roots { get; }

        string DefaultDirectorySeparator { get; }
        bool IsCaseSensitiveByDefault { get; }
        
        /// <summary>
        /// Returns the path to the user's temporary folder
        /// </summary>
        /// <returns>The path to the user's temporary folder</returns>
        AbsolutePath GetTemporaryFolder();

        #endregion
        
        #region Creating
        
        AbsolutePath Create(AbsolutePath path, PathType pathType, bool createRecursively = false);
        AbsolutePath CreateFolder(AbsolutePath path, bool createRecursively = false);
        /// <summary>
        /// Creates a file or folder in the temporary folder
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        AbsolutePath CreateTemporaryPath(PathType type);
        /// <summary>
        /// Creates an empty file at the specified path
        /// </summary>
        /// <param name="path">The path that should become an empty file</param>
        /// <returns>The same path that was specified</returns>
        AbsolutePath CreateEmptyFile(AbsolutePath path, bool createRecursively = false);
        
        /// <summary>
        /// Creates an empty file at the specified path, and opens it for writing
        /// </summary>
        /// <param name="path">The path that should become a file</param>
        /// <returns>The stream to be used to write to the specified file</returns>
        Stream CreateFile(AbsolutePath path, bool createRecursively = false);


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
        AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false);
        /// <summary>
        /// Deletes the specified file
        /// </summary>
        /// <param name="path">The file that should be deleted</param>
        /// <returns>The same path that was specified</returns>
        AbsolutePath DeleteFile(AbsolutePath path);
        
        /// <summary>
        /// Empties the specified folder
        /// </summary>
        /// <param name="path">The path that should become an empty folder</param>
        /// <returns>The same path that was specified</returns>
        AbsolutePath ClearFolder(AbsolutePath path);
        /// <summary>
        /// Deletes the specified file or folder
        /// </summary>
        /// <param name="path">The path to the file or folder that should be deleted</param>
        /// <param name="recursiveDeleteIfFolder">If true and the specified path is a folder, then the folder contents are
        /// recursively deleted before the folder itself is deleted. If false and the specified path is a folder, and that
        /// folder contains other files or folders, then an IOException is thrown. If the path is a file, this parameter
        /// is ignored.</param>
        /// <returns>The path that was just deleted</returns>
        AbsolutePath Delete(AbsolutePath path, bool recursiveDeleteIfFolder = false);

        
        #endregion
        
        #region Ensuring is
        
        /// <summary>
        /// Creates the path as a folder if it isn't already. If the path is a file, throws an IOException.
        /// </summary>
        /// <param name="path">The path that should be a folder</param>
        /// <returns>The same path that was specified</returns>
        AbsolutePath EnsureIsFolder(AbsolutePath path, bool createRecursively = false);

        AbsolutePath EnsureIsFile(AbsolutePath path, bool createRecursively = false);

        AbsolutePath EnsureIsEmptyFolder(AbsolutePath path, bool recursiveDeleteIfFolder = false, bool createRecursively = false);

        #endregion
        
        #region Ensuring is not
        
        /// <summary>
        /// Deletes the specified path if it is a folder. If the path is a file or doesn't exist, this returns without
        /// throwing an exception. If recursive is false and the path is a non-empty folder, throws an IOException.
        /// </summary>
        /// <param name="path">The path that may be a folder</param>
        /// <param name="recursive">Whether to recursively delete the contents of the path if the path is a non-empty folder</param>
        /// <returns>The same path that was specified</returns>
        AbsolutePath EnsureIsNotFolder(AbsolutePath path, bool recursive = false);

        AbsolutePath EnsureIsNotFile(AbsolutePath path);

        AbsolutePath EnsureDoesNotExist(AbsolutePath path, bool recursiveDeleteIfFolder = false);

        #endregion

        #region Utilities
        
        /// <summary>
        /// Updates the file system roots. On Unix-like operating systems, there's only one file system root, and it is '/'.
        /// On Windows, there can be multiple, e.g. 'C:', 'D:', 'E:'. This method is only useful on Windows.
        /// </summary>
        void UpdateRoots();
        bool HasExtension(AbsolutePath path, string extension);
        bool ContainsFiles(AbsolutePath path);
        bool FolderContainsFiles(AbsolutePath path);
        bool MayCreateFile(FileMode fileMode);
        bool IsImageUri(Uri uri);
        bool IsVideoUri(Uri uri);
        string StripQuotes(string str);
        AbsolutePath Decrypt(AbsolutePath path);
        AbsolutePath Encrypt(AbsolutePath path);
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

        bool ComponentsAreAbsolute(IReadOnlyList<string> path);
        RelativePath ParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        IMaybe<RelativePath> TryParseRelativePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        bool TryParseRelativePath(string path, out RelativePath relativePath,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        bool TryParseRelativePath(string path, out RelativePath relativePath, out string error,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        AbsolutePath ParseAbsolutePath(string path, AbsolutePath optionallyRelativeTo, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        IEither<AbsolutePath, RelativePath> ParsePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        bool IsRelativePath(string path);
        bool IsAbsolutePath(string path);
        AbsolutePath ParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        IMaybe<AbsolutePath> TryParseAbsolutePath(string path, CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        bool TryParseAbsolutePath(string path, out AbsolutePath absolutePath,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        bool TryParseAbsolutePath(string path, out AbsolutePath absolutePath, out string error,
            CaseSensitivityMode flags = CaseSensitivityMode.UseDefaultsForGivenPath);
        
        #endregion
        
        #region Translation stuff
        
        IAbsolutePathTranslation Copy(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination);
        IAbsolutePathTranslation Copy(AbsolutePath source, AbsolutePath destination);
        IAbsolutePathTranslation Move(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination);
        IAbsolutePathTranslation Move(AbsolutePath source, AbsolutePath destination);

        IAbsolutePathTranslation Translate(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination);
        IAbsolutePathTranslation Translate(AbsolutePath source, AbsolutePath destination);
        void RenameTo(AbsolutePath source, AbsolutePath target);
        IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation, bool overwrite = false);
        IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation, bool overwrite = false);
        IAbsolutePathTranslation CopyFolder(IAbsolutePathTranslation translation, bool overwrite = false);
        IAbsolutePathTranslation Move(IAbsolutePathTranslation translation, bool overwrite = false);
        IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation, bool overwrite = false);
        IAbsolutePathTranslation MoveFolder(IAbsolutePathTranslation translation, bool overwrite = false);
        
        #endregion

        #region Path building

        /// <summary>
        /// Returns a lazily-enumerated list of child files and/or folders
        /// </summary>
        /// <param name="path">The parent folder</param>
        /// <param name="includeFolders">Whether to include sub-folders in the return value</param>
        /// <param name="includeFiles">Whether to include sub-files in the return value</param>
        /// <returns>The children of this path</returns>
        IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, bool includeFolders = true,
            bool includeFiles = true);
        bool CanBeSimplified(AbsolutePath path);
        AbsolutePath Root(AbsolutePath path);
        RelativePath RelativeTo(AbsolutePath path, AbsolutePath relativeTo);
        IMaybe<AbsolutePath> TryCommonWith(AbsolutePath path, AbsolutePath that);
        AbsolutePath Simplify(AbsolutePath path);
        RelativePath Simplify(RelativePath path);
        IMaybe<AbsolutePath> TryParent(AbsolutePath path);

        /// <summary>
        /// Equivalent to Path.Combine. You can also use the / operator to build paths, like this:
        /// _ioService.CurrentDirectory / "folder1" / "folder2" / "file.txt"
        /// </summary>
        AbsolutePath Combine(AbsolutePath path, params string[] subsequentPathParts);
        AbsolutePath WithoutExtension(AbsolutePath path);
        AbsolutePath Descendant(AbsolutePath path, params AbsolutePath[] paths);
        AbsolutePath Descendant(AbsolutePath path, params string[] paths);
        AbsolutePath Ancestor(AbsolutePath path, int level);
        AbsolutePath WithExtension(AbsolutePath path, string differentExtension);
        AbsolutePath WithExtension(AbsolutePath path, Func<string, string> differentExtension);
        AbsolutePath GetCommonAncestry(AbsolutePath path1, AbsolutePath path2);
        Uri GetCommonDescendants(AbsolutePath path1, AbsolutePath path2);
        Tuple<Uri, Uri> GetNonCommonDescendants(AbsolutePath path1, AbsolutePath path2);
        Tuple<Uri, Uri> GetNonCommonAncestry(AbsolutePath path1, AbsolutePath path2);
        Uri Child(Uri parent, Uri child);
        AbsolutePaths GlobFiles(AbsolutePath path, string pattern);

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
        IMaybe<AbsolutePath> TryAncestor(AbsolutePath path, int level);
        bool IsAncestorOf(AbsolutePath path, AbsolutePath possibleDescendant);
        bool IsDescendantOf(AbsolutePath path, AbsolutePath possibleAncestor);
        IMaybe<AbsolutePath> TryGetCommonAncestry(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Uri> TryGetCommonDescendants(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(AbsolutePath path1, AbsolutePath path2);
        AbsolutePath CommonWith(AbsolutePath path, AbsolutePath that);
        AbsolutePath Parent(AbsolutePath path);

        /// <summary>
        /// Returns the newline character used by this IIoService when writing text to a file (e.g., '\n' or '\r\n')
        /// </summary>
        /// <returns>The newline character used by this IIoService when writing text to a file</returns>
        string GetNewlineCharacter();
        
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        IMaybe<AbsolutePath> TryWithExtension(AbsolutePath path, string differentExtension);

        #endregion

        #region File metadata
        
        bool Exists(AbsolutePath path);
        PathType GetPathType(AbsolutePath path);
        bool HasExtension(AbsolutePath path);
        bool IsReadOnly(AbsolutePath path);
        Information FileSize(AbsolutePath path);
        FileAttributes Attributes(AbsolutePath path);
        DateTimeOffset CreationTime(AbsolutePath path);
        DateTimeOffset LastAccessTime(AbsolutePath path);
        DateTimeOffset LastWriteTime(AbsolutePath path);
        bool IsFile(AbsolutePath path);
        bool IsFolder(AbsolutePath path);
        IMaybe<bool> TryIsReadOnly(AbsolutePath path);
        IMaybe<Information> TryFileSize(AbsolutePath path);
        IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes);
        IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath attributes);
        IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath attributes);
        IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath attributes);

        #endregion
        
        #region File reading
        IEnumerable<string> ReadLines(AbsolutePath path);
        string ReadAllText(AbsolutePath path);
        IEnumerable<string> ReadLines(AbsolutePath path, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        IMaybe<string> TryReadText(AbsolutePath path, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            int bufferSize = 4096, bool leaveOpen = false);
        IEnumerable<string> ReadLines(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        IEnumerable<string> ReadLinesBackwards(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        string TryReadText(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        IMaybe<StreamReader> TryOpenReader(AbsolutePath path);
        StreamReader OpenReader(AbsolutePath path);
        string ReadText(AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        #endregion
        
        #region File writing
        void WriteAllText(AbsolutePath path, string text, bool createRecursively = false);
        void WriteAllLines(AbsolutePath path, IEnumerable<string> lines, bool createRecursively = false);
        void WriteAllBytes(AbsolutePath path, byte[] bytes, bool createRecursively = false);
        IMaybe<StreamWriter> TryOpenWriter(AbsolutePath absolutePath, bool createRecursively = false);
        void WriteText(AbsolutePath absolutePath, IEnumerable<string> lines,
            FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false, bool createRecursively = false);
        void WriteText(AbsolutePath absolutePath, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false, bool createRecursively = false);
        StreamWriter OpenWriter(AbsolutePath absolutePath, bool createRecursively = false);
        #endregion
        
        #region File open for reading or writing
        IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode, bool createRecursively = false);
        IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode, FileAccess fileAccess, bool createRecursively = false);
        IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, bool createRecursively = false);
        Stream Open(AbsolutePath path, FileMode fileMode, bool createRecursively = false);
        Stream Open(AbsolutePath path, FileMode fileMode, FileAccess fileAccess, bool createRecursively = false);
        Stream Open(AbsolutePath path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, bool createRecursively = false);
        #endregion
        
        #region LINQ-style APIs
        
        /// <summary>
        /// Returns an IQueryable that converts expressions like AbsolutePaths.Where(path => path.Contains("test")) into
        /// efficient calls to the .NET file system APIs.
        /// </summary>
        IQueryable<AbsolutePath> Query();

        ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges,
            bool includeSubFolders, string pattern);
        IObservable<Unit> ObserveChanges(AbsolutePath path);
        IObservable<Unit> ObserveChanges(AbsolutePath path, NotifyFilters filters);
        IObservable<PathType> ObservePathType(AbsolutePath path);
        IObservable<AbsolutePath> Renamings(AbsolutePath path);
        
        #endregion
    }
}