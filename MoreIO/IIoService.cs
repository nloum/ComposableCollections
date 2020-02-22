using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using SimpleMonads;

namespace MoreIO
{
    public interface IIoService
    {
        /// <summary>
        /// Given a bunch of files or folders in different places that may have the same name,
        /// create unique names for those files and folders based on their original name and the
        /// paths to those files and folders.
        /// </summary>
        /// <param name="paths">The paths to the files and folders that may have the same name but
        /// be in different locations.</param>
        /// <returns>A mapping from the original file path to the new suggested file name.</returns>
        IEnumerable<KeyValuePair<PathSpec, string>> ProposeUniqueNamesForMovingPathsToSameFolder(
            IEnumerable<PathSpec> paths);
        IDictionaryChangesStrict<PathSpec, PathSpec> ToLiveLinq(PathSpec root, bool includeFileChanges = true);
        IEnumerable<PathSpec> GetChildren(PathSpec path, bool includeFolders = true, bool includeFiles = true);
        IEnumerable<PathSpec> GetFiles(PathSpec path);
        IEnumerable<PathSpec> GetFolders(PathSpec path);
        PathSpec CreateEmptyFile(PathSpec path);
        FileStream CreateFile(PathSpec path);
        PathSpec DeleteFile(PathSpec path);
        PathSpec ClearFolder(PathSpec path);
        PathSpec Decrypt(PathSpec path);
        PathSpec Encrypt(PathSpec path);
        PathSpec Delete(PathSpec path);
        string SurroundWithDoubleQuotesIfNecessary(string str);
        bool IsAncestorOf(PathSpec path, PathSpec possibleDescendant);
        bool IsDescendantOf(PathSpec path, PathSpec possibleAncestor);
        IEnumerable<string> Split(PathSpec path);
        string LastPathComponent(PathSpec path);

        /// <summary>
        /// Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from). For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        /// C:\Users\myusername
        /// C:\Users
        /// C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        IEnumerable<PathSpec> Ancestors(PathSpec path, bool includeItself = false);

        IMaybe<PathSpec> Descendant(PathSpec path, params PathSpec[] paths);
        IMaybe<PathSpec> Descendant(PathSpec path, params string[] paths);
        IMaybe<PathSpec> Ancestor(PathSpec path, int level);
        bool HasExtension(PathSpec path, string extension);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        IMaybe<PathSpec> WithExtension(PathSpec path, string differentExtension);

        IPathSpecTranslation Copy(IPathSpecTranslation translation);
        IPathSpecTranslation CopyFile(IPathSpecTranslation translation);
        IPathSpecTranslation CopyFolder(IPathSpecTranslation translation);
        IPathSpecTranslation Move(IPathSpecTranslation translation);
        IPathSpecTranslation MoveFile(IPathSpecTranslation translation);
        IPathSpecTranslation MoveFolder(IPathSpecTranslation translation);
        bool ContainsFiles(PathSpec path);
        bool FolderContainsFiles(PathSpec path);
        IMaybe<PathSpec> GetCommonAncestry(PathSpec path1, PathSpec path2);
        IMaybe<Uri> GetCommonDescendants(PathSpec path1, PathSpec path2);
        IMaybe<Tuple<Uri, Uri>> GetNonCommonDescendants(PathSpec path1, PathSpec path2);
        IMaybe<Tuple<Uri, Uri>> GetNonCommonAncestry(PathSpec path1, PathSpec path2);
        IPathSpecTranslation Translate(PathSpec pathToBeCopied, PathSpec source, PathSpec destination);
        IPathSpecTranslation Translate(PathSpec source, PathSpec destination);
        Uri Child(Uri parent, Uri child);
        FileInfo AsFileInfo(PathSpec path);
        DirectoryInfo AsDirectoryInfo(PathSpec path);

        IMaybe<T> As<T>(T pathName, PathType pathType)
            where T : PathSpec;

        IMaybe<bool> IsReadOnly(PathSpec path);
        IMaybe<long> Length(PathSpec path);
        IMaybe<FileAttributes> Attributes(PathSpec attributes);
        IMaybe<DateTime> CreationTime(PathSpec attributes);
        IMaybe<DateTime> LastAccessTime(PathSpec attributes);
        IMaybe<DateTime> LastWriteTime(PathSpec attributes);
        IMaybe<string> FullName(PathSpec attributes);

        /// <summary>
        /// Includes the period character ".". For example, function would return ".exe" if the file pointed to a file named was "test.exe".
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        IMaybe<string> Extension(string pathName);

        bool IsImageUri(Uri uri);
        bool IsVideoUri(Uri uri);
        string StripQuotes(string str);
        PathSpec Root(PathSpec path);
        void RenameTo(PathSpec source, PathSpec target);
        bool Exists(PathSpec path);
        PathType GetPathType(PathSpec path);
        PathSpec DeleteFolder(PathSpec path, bool recursive = false);
        bool MayCreateFile(FileMode fileMode);
        void Create(PathSpec path, PathType pathType);
        IMaybe<FileStream> Open(PathSpec path, FileMode fileMode);

        IMaybe<FileStream> Open(PathSpec path, FileMode fileMode,
            FileAccess fileAccess);

        IMaybe<FileStream> Open(PathSpec path, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare);

        PathSpec CreateFolder(PathSpec path);
        void WriteAllText(PathSpec path, string text);
        void WriteAllLines(PathSpec path, IEnumerable<string> lines);
        void WriteAllLines(PathSpec path, byte[] bytes);
        IEnumerable<string> ReadLines(PathSpec path);
        string ReadAllText(PathSpec path);
        PathSpec ToAbsolute(PathSpec path);
        IReadOnlySet<PathSpec> Children(PathSpec path);
        IReadOnlySet<PathSpec> Children(PathSpec path, string pattern);
        IReadOnlySet<PathSpec> Descendants(PathSpec path);
        IReadOnlySet<PathSpec> Descendants(PathSpec path, string pattern);
        IObservable<Unit> ObserveChanges(PathSpec path);
        IObservable<Unit> ObserveChanges(PathSpec path, NotifyFilters filters);
        IObservable<PathType> ObservePathType(PathSpec path);
        IObservable<PathSpec> Renamings(PathSpec path);
        PathSpec RelativeTo(PathSpec path, PathSpec relativeTo);
        IMaybe<PathSpec> CommonWith(PathSpec path, PathSpec that);
        bool CanBeSimplified(PathSpec path);
        PathSpec Simplify(PathSpec path);
        IMaybe<PathSpec> Parent(PathSpec path);
        bool IsAbsolute(PathSpec path);
        bool IsRelative(PathSpec path);
        IMaybe<PathSpec> Join(IReadOnlyList<string> descendants);
        IMaybe<PathSpec> Join(IEnumerable<string> descendants);
        IMaybe<PathSpec> Join(IReadOnlyList<IMaybe<string>> descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<string>> descendants);
        IMaybe<PathSpec> Join(PathSpec root, IEnumerable<string> descendants);
        IMaybe<PathSpec> Join(IMaybe<PathSpec> root, IEnumerable<string> descendants);
        IMaybe<PathSpec> Join(IMaybe<PathSpec> root, IEnumerable<IMaybe<string>> descendants);
        IMaybe<PathSpec> Join(PathSpec root, IEnumerable<IMaybe<string>> descendants);
        IMaybe<PathSpec> Join(PathSpec root, params string[] descendants);
        IMaybe<PathSpec> Join(IMaybe<PathSpec> root, params string[] descendants);
        IMaybe<PathSpec> Join(IMaybe<PathSpec> root, params IMaybe<string>[] descendants);
        IMaybe<PathSpec> Join(PathSpec root, params IMaybe<string>[] descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, IEnumerable<string> descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, IEnumerable<string> descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, IEnumerable<IMaybe<string>> descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, IEnumerable<IMaybe<string>> descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, params string[] descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, params string[] descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, params IMaybe<string>[] descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, params IMaybe<string>[] descendants);
        IMaybe<PathSpec> Join(IReadOnlyList<PathSpec> descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> descendants);
        IMaybe<PathSpec> Join(IReadOnlyList<IMaybe<PathSpec>> descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> descendants);
        IMaybe<PathSpec> Join(PathSpec root, IEnumerable<PathSpec> descendants);
        IMaybe<PathSpec> Join(IMaybe<PathSpec> root, IEnumerable<PathSpec> descendants);
        IMaybe<PathSpec> Join(IMaybe<PathSpec> root, IEnumerable<IMaybe<PathSpec>> descendants);
        IMaybe<PathSpec> Join(PathSpec root, IEnumerable<IMaybe<PathSpec>> descendants);
        IMaybe<PathSpec> Join(PathSpec root, params PathSpec[] descendants);
        IMaybe<PathSpec> Join(IMaybe<PathSpec> root, params PathSpec[] descendants);
        IMaybe<PathSpec> Join(IMaybe<PathSpec> root, params IMaybe<PathSpec>[] descendants);
        IMaybe<PathSpec> Join(PathSpec root, params IMaybe<PathSpec>[] descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, IEnumerable<PathSpec> descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, IEnumerable<PathSpec> descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, IEnumerable<IMaybe<PathSpec>> descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, IEnumerable<IMaybe<PathSpec>> descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, params PathSpec[] descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, params PathSpec[] descendants);
        IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, params IMaybe<PathSpec>[] descendants);
        IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, params IMaybe<PathSpec>[] descendants);
        IMaybe<PathSpec> ToPath(string path, PathFlags flags);
        IMaybe<PathSpec> ToPath(string path);
        bool IsAbsoluteWindowsPath(string path);
        bool IsAbsoluteUnixPath(string path);
        StringComparison ToStringComparison(PathFlags pathFlags);
        StringComparison ToStringComparison(PathFlags pathFlags, PathFlags otherPathFlags);

        IMaybe<StreamWriter> CreateText(PathSpec pathSpec);

        IEnumerable<string> ReadLines(PathSpec pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);

        IMaybe<string> ReadText(PathSpec pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            int bufferSize = 4096, bool leaveOpen = false);

        void WriteText(PathSpec pathSpec, IEnumerable<string> lines,
            FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false);

        void WriteText(PathSpec pathSpec, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false);

        IEnumerable<string> ReadLines(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);

        IEnumerable<string> ReadLinesBackwards(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);

        string ReadText(Stream stream, Encoding encoding = null,
            bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);
    }
}