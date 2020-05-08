using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using ReactiveProcesses;
using SimpleMonads;
using TreeLinq;

namespace IoFluently
{
    public interface IIoService
    {
        IReactiveProcessFactory ReactiveProcessFactory { get; }
        AbsolutePath CurrentDirectory { get; }
        
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

        IEnumerable<TreeTraversal<string, AbsolutePath>> TraverseDescendants(AbsolutePath path);
        IEnumerable<AbsolutePath> GetDescendants(AbsolutePath path);
        IDictionaryChangesStrict<AbsolutePath, PathType> ToLiveLinq(AbsolutePath root, bool includeFileContentChanges = true, PathObservationMethod observationMethod = PathObservationMethod.Default);
        IEnumerable<AbsolutePath> GetChildren(AbsolutePath path, bool includeFolders = true, bool includeFiles = true);
        IEnumerable<AbsolutePath> GetFiles(AbsolutePath path);
        IEnumerable<AbsolutePath> GetFolders(AbsolutePath path);
        AbsolutePath CreateEmptyFile(AbsolutePath path);
        FileStream CreateFile(AbsolutePath path);
        AbsolutePath DeleteFile(AbsolutePath path);
        AbsolutePath ClearFolder(AbsolutePath path);
        AbsolutePath Decrypt(AbsolutePath path);
        AbsolutePath Encrypt(AbsolutePath path);
        AbsolutePath Delete(AbsolutePath path);
        string SurroundWithDoubleQuotesIfNecessary(string str);
        bool IsAncestorOf(AbsolutePath path, AbsolutePath possibleDescendant);
        bool IsDescendantOf(AbsolutePath path, AbsolutePath possibleAncestor);
        IEnumerable<string> Split(AbsolutePath path);
        string LastPathComponent(AbsolutePath path);

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
        IEnumerable<AbsolutePath> Ancestors(AbsolutePath path, bool includeItself = false);

        IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params AbsolutePath[] paths);
        IMaybe<AbsolutePath> TryDescendant(AbsolutePath path, params string[] paths);
        IMaybe<AbsolutePath> TryAncestor(AbsolutePath path, int level);
        bool HasExtension(AbsolutePath path, string extension);

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        IMaybe<AbsolutePath> TryWithExtension(AbsolutePath path, string differentExtension);

        IAbsolutePathTranslation Copy(IAbsolutePathTranslation translation);
        IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation);
        IAbsolutePathTranslation CopyFolder(IAbsolutePathTranslation translation);
        IAbsolutePathTranslation Move(IAbsolutePathTranslation translation);
        IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation);
        IAbsolutePathTranslation MoveFolder(IAbsolutePathTranslation translation);
        bool ContainsFiles(AbsolutePath path);
        bool FolderContainsFiles(AbsolutePath path);
        IMaybe<AbsolutePath> TryGetCommonAncestry(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Uri> TryGetCommonDescendants(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(AbsolutePath path1, AbsolutePath path2);
        IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(AbsolutePath path1, AbsolutePath path2);
        IAbsolutePathTranslation Translate(AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination);
        IAbsolutePathTranslation Translate(AbsolutePath source, AbsolutePath destination);
        Uri Child(Uri parent, Uri child);
        FileInfo AsFileInfo(AbsolutePath path);
        DirectoryInfo AsDirectoryInfo(AbsolutePath path);

        IMaybe<T> TryAs<T>(T pathName, PathType pathType)
            where T : AbsolutePath;

        IMaybe<bool> TryIsReadOnly(AbsolutePath path);
        IMaybe<long> TryLength(AbsolutePath path);
        IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes);
        IMaybe<DateTime> TryCreationTime(AbsolutePath attributes);
        IMaybe<DateTime> TryLastAccessTime(AbsolutePath attributes);
        IMaybe<DateTime> TryLastWriteTime(AbsolutePath attributes);
        IMaybe<string> TryFullName(AbsolutePath attributes);

        /// <summary>
        ///     Includes the period character ".". For example, function would return ".exe" if the file pointed to a file named
        ///     was "test.exe".
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        IMaybe<string> TryExtension(string pathName);

        bool IsImageUri(Uri uri);
        bool IsVideoUri(Uri uri);
        string StripQuotes(string str);
        AbsolutePath Root(AbsolutePath path);
        void RenameTo(AbsolutePath source, AbsolutePath target);
        bool Exists(AbsolutePath path);
        PathType GetPathType(AbsolutePath path);
        AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false);
        bool MayCreateFile(FileMode fileMode);
        void Create(AbsolutePath path, PathType pathType);
        IMaybe<FileStream> TryOpen(AbsolutePath path, FileMode fileMode);

        IMaybe<FileStream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess);

        IMaybe<FileStream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare);

        AbsolutePath CreateFolder(AbsolutePath path);
        void WriteAllText(AbsolutePath path, string text);
        void WriteAllLines(AbsolutePath path, IEnumerable<string> lines);
        void WriteAllLines(AbsolutePath path, byte[] bytes);
        IEnumerable<string> ReadLines(AbsolutePath path);
        string ReadAllText(AbsolutePath path);
        AbsolutePath ToAbsolute(AbsolutePath path);
        IReadOnlyObservableSet<AbsolutePath> Children(AbsolutePath path);
        IReadOnlyObservableSet<AbsolutePath> Children(AbsolutePath path, string pattern);
        IReadOnlyObservableSet<AbsolutePath> Descendants(AbsolutePath path);
        IReadOnlyObservableSet<AbsolutePath> Descendants(AbsolutePath path, string pattern);
        IObservable<Unit> ObserveChanges(AbsolutePath path);
        IObservable<Unit> ObserveChanges(AbsolutePath path, NotifyFilters filters);
        IObservable<PathType> ObservePathType(AbsolutePath path);
        IObservable<AbsolutePath> Renamings(AbsolutePath path);
        AbsolutePath RelativeTo(AbsolutePath path, AbsolutePath relativeTo);
        IMaybe<AbsolutePath> TryCommonWith(AbsolutePath path, AbsolutePath that);
        bool CanBeSimplified(AbsolutePath path);
        AbsolutePath Simplify(AbsolutePath path);
        IMaybe<AbsolutePath> TryParent(AbsolutePath path);
        bool IsAbsolute(AbsolutePath path);
        bool IsRelative(AbsolutePath path);
        IMaybe<AbsolutePath> TryJoin(IReadOnlyList<string> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<string> descendants);
        IMaybe<AbsolutePath> TryJoin(IReadOnlyList<IMaybe<string>> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<string>> descendants);
        IMaybe<AbsolutePath> TryJoin(AbsolutePath root, IEnumerable<string> descendants);
        IMaybe<AbsolutePath> TryJoin(IMaybe<AbsolutePath> root, IEnumerable<string> descendants);
        IMaybe<AbsolutePath> TryJoin(IMaybe<AbsolutePath> root, IEnumerable<IMaybe<string>> descendants);
        IMaybe<AbsolutePath> TryJoin(AbsolutePath root, IEnumerable<IMaybe<string>> descendants);
        IMaybe<AbsolutePath> TryJoin(AbsolutePath root, params string[] descendants);
        IMaybe<AbsolutePath> TryJoin(IMaybe<AbsolutePath> root, params string[] descendants);
        IMaybe<AbsolutePath> TryJoin(IMaybe<AbsolutePath> root, params IMaybe<string>[] descendants);
        IMaybe<AbsolutePath> TryJoin(AbsolutePath root, params IMaybe<string>[] descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> root, IEnumerable<string> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> root, IEnumerable<string> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> root, IEnumerable<IMaybe<string>> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> root, IEnumerable<IMaybe<string>> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> root, params string[] descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> root, params string[] descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> root, params IMaybe<string>[] descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> root, params IMaybe<string>[] descendants);
        IMaybe<AbsolutePath> TryJoin(IReadOnlyList<AbsolutePath> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> descendants);
        IMaybe<AbsolutePath> TryJoin(IReadOnlyList<IMaybe<AbsolutePath>> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> descendants);
        IMaybe<AbsolutePath> TryJoin(AbsolutePath root, IEnumerable<AbsolutePath> descendants);
        IMaybe<AbsolutePath> TryJoin(IMaybe<AbsolutePath> root, IEnumerable<AbsolutePath> descendants);
        IMaybe<AbsolutePath> TryJoin(IMaybe<AbsolutePath> root, IEnumerable<IMaybe<AbsolutePath>> descendants);
        IMaybe<AbsolutePath> TryJoin(AbsolutePath root, IEnumerable<IMaybe<AbsolutePath>> descendants);
        IMaybe<AbsolutePath> TryJoin(AbsolutePath root, params AbsolutePath[] descendants);
        IMaybe<AbsolutePath> TryJoin(IMaybe<AbsolutePath> root, params AbsolutePath[] descendants);
        IMaybe<AbsolutePath> TryJoin(IMaybe<AbsolutePath> root, params IMaybe<AbsolutePath>[] descendants);
        IMaybe<AbsolutePath> TryJoin(AbsolutePath root, params IMaybe<AbsolutePath>[] descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> root, IEnumerable<AbsolutePath> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> root, IEnumerable<AbsolutePath> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> root, IEnumerable<IMaybe<AbsolutePath>> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> root, IEnumerable<IMaybe<AbsolutePath>> descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> root, params AbsolutePath[] descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> root, params AbsolutePath[] descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<IMaybe<AbsolutePath>> root, params IMaybe<AbsolutePath>[] descendants);
        IMaybe<AbsolutePath> TryJoin(IEnumerable<AbsolutePath> root, params IMaybe<AbsolutePath>[] descendants);
        IMaybe<AbsolutePath> TryToAbsolutePath(string path, PathFlags flags);
        IMaybe<AbsolutePath> TryToAbsolutePath(string path);
        AbsolutePath ToAbsolutePath(string path, PathFlags flags);
        AbsolutePath ToAbsolutePath(string path);
        bool IsAbsoluteWindowsPath(string path);
        bool IsAbsoluteUnixPath(string path);
        StringComparison ToStringComparison(PathFlags pathFlags);
        StringComparison ToStringComparison(PathFlags pathFlags, PathFlags otherPathFlags);

        IMaybe<StreamWriter> TryCreateText(AbsolutePath pathSpec);

        IEnumerable<string> ReadLines(AbsolutePath pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false);

        IMaybe<string> TryReadText(AbsolutePath pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true,
            int bufferSize = 4096, bool leaveOpen = false);

        void WriteText(AbsolutePath pathSpec, IEnumerable<string> lines,
            FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false);

        void WriteText(AbsolutePath pathSpec, string text, FileMode fileMode = FileMode.Create,
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
    }
}