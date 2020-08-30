using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.RegularExpressions;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using ReactiveProcesses;
using SimpleMonads;
using TreeLinq;
using UnitsNet;

namespace IoFluently
{
    public interface IIoService
    {
        IQueryable<AbsolutePath> Query();
        
        #region Environmental stuff
        
        /// <summary>
        /// Determines whether the environment that this IIoService is for is case sensitive by default. 
        /// </summary>
        bool IsCaseSensitiveByDefault();
        
        string GetDefaultDirectorySeparatorForThisEnvironment();

        #endregion
        
        #region Creating and deleting
        
        AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false);
        AbsolutePath Create(AbsolutePath path, PathType pathType);
        AbsolutePath CreateFolder(AbsolutePath path);
        AbsolutePath CreateTemporaryPath(PathType type);
        AbsolutePath CreateEmptyFile(AbsolutePath path);
        Stream CreateFile(AbsolutePath path);
        AbsolutePath DeleteFile(AbsolutePath path);
        AbsolutePath ClearFolder(AbsolutePath path);
        AbsolutePath Delete(AbsolutePath path, bool recursiveDeleteIfFolder = false);
        AbsolutePath EnsureIsFolder(AbsolutePath path);

        AbsolutePath EnsureIsNotFolder(AbsolutePath path, bool recursive = false);

        AbsolutePath EnsureIsFile(AbsolutePath path);

        AbsolutePath EnsureIsNotFile(AbsolutePath path);

        AbsolutePath EnsureDoesNotExist(AbsolutePath path, bool recursiveDeleteIfFolder = false);

        AbsolutePath EnsureIsEmptyFolder(AbsolutePath path, bool recursiveDeleteIfFolder = false);

        #endregion
        
        #region Utilities
        
        void UpdateStorage();
        IReactiveProcessFactory ReactiveProcessFactory { get; }
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
        
        bool CanBeSimplified(AbsolutePath path);
        AbsolutePath CurrentDirectory { get; }
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
        FileAttributes Attributes(AbsolutePath attributes);
        DateTimeOffset CreationTime(AbsolutePath attributes);
        DateTimeOffset LastAccessTime(AbsolutePath attributes);
        DateTimeOffset LastWriteTime(AbsolutePath attributes);
        bool IsFile(AbsolutePath absolutePath);
        bool IsFolder(AbsolutePath absolutePath);
        IMaybe<bool> TryIsReadOnly(AbsolutePath path);
        IMaybe<Information> TryFileSize(AbsolutePath path);
        IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes);
        IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath attributes);
        IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath attributes);
        IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath attributes);

        #endregion
        
        #region File open, write, and read

        IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode);
        IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess);
        IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare);
        void WriteAllText(AbsolutePath path, string text);
        void WriteAllLines(AbsolutePath path, IEnumerable<string> lines);
        void WriteAllBytes(AbsolutePath path, byte[] bytes);
        IEnumerable<string> ReadLines(AbsolutePath path);
        string ReadAllText(AbsolutePath path);
        IMaybe<StreamWriter> TryOpenWriter(AbsolutePath absolutePath);
        IEnumerable<string> ReadLines(AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        IMaybe<string> TryReadText(AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            int bufferSize = 4096, bool leaveOpen = false);
        void WriteText(AbsolutePath absolutePath, IEnumerable<string> lines,
            FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false);
        void WriteText(AbsolutePath absolutePath, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false);
        IEnumerable<string> ReadLines(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        IEnumerable<string> ReadLinesBackwards(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        string TryReadText(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
        Stream Open(AbsolutePath path, FileMode fileMode);
        Stream Open(AbsolutePath path, FileMode fileMode, FileAccess fileAccess);
        Stream Open(AbsolutePath path, FileMode fileMode, FileAccess fileAccess,
            FileShare fileShare);
        StreamWriter OpenWriter(AbsolutePath absolutePath);
        IMaybe<StreamReader> TryOpenReader(AbsolutePath path);
        StreamReader OpenReader(AbsolutePath path);
        string ReadText(AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);

        #endregion
        
        #region Observable changes
        
        ISetChanges<AbsolutePath> ToLiveLinq(AbsolutePath path, bool includeFileContentChanges,
            bool includeSubFolders, string pattern);
        IObservable<Unit> ObserveChanges(AbsolutePath path);
        IObservable<Unit> ObserveChanges(AbsolutePath path, NotifyFilters filters);
        IObservable<PathType> ObservePathType(AbsolutePath path);
        IObservable<AbsolutePath> Renamings(AbsolutePath path);
        
        #endregion
    }
}