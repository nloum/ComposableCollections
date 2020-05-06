using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using SimpleMonads;
using TreeLinq;

namespace IoFluently
{
    public static class IoExtensions
    {
        public static IDictionaryChangesStrict<AbsolutePath, PathType> ToLiveLinq(this AbsolutePath root,
            bool includeFileContentChanges = true, PathObservationMethod observationMethod = PathObservationMethod.Default)
        {
            return root.IoService.ToLiveLinq(root, includeFileContentChanges, observationMethod);
        }

        public static IEnumerable<AbsolutePath> GetDescendants(this AbsolutePath path)
        {
            return path.IoService.GetDescendants(path);
        }

        public static IEnumerable<TreeTraversal<string, AbsolutePath>> TraverseDescendants(this AbsolutePath path)
        {
            return path.IoService.TraverseDescendants(path);
        }

        public static IEnumerable<AbsolutePath> GetChildren(this AbsolutePath path, bool includeFolders = true,
            bool includeFiles = true)
        {
            return path.IoService.GetChildren(path, includeFolders, includeFiles);
        }

        public static IEnumerable<AbsolutePath> GetFiles(this AbsolutePath path)
        {
            return path.IoService.GetFiles(path);
        }

        public static IEnumerable<AbsolutePath> GetFolders(this AbsolutePath path)
        {
            return path.IoService.GetFolders(path);
        }

        public static AbsolutePath CreateEmptyFile(this AbsolutePath path)
        {
            return path.IoService.CreateEmptyFile(path);
        }

        public static FileStream CreateFile(this AbsolutePath path)
        {
            return path.IoService.CreateFile(path);
        }

        public static AbsolutePath DeleteFile(this AbsolutePath path)
        {
            return path.IoService.DeleteFile(path);
        }

        public static AbsolutePath ClearFolder(this AbsolutePath path)
        {
            return path.IoService.ClearFolder(path);
        }

        public static AbsolutePath Decrypt(this AbsolutePath path)
        {
            return path.IoService.Decrypt(path);
        }

        public static AbsolutePath Encrypt(this AbsolutePath path)
        {
            return path.IoService.Encrypt(path);
        }

        public static AbsolutePath Delete(this AbsolutePath path)
        {
            return path.IoService.Delete(path);
        }

        public static bool IsAncestorOf(this AbsolutePath path, AbsolutePath possibleDescendant)
        {
            return path.IoService.IsAncestorOf(path, possibleDescendant);
        }

        public static bool IsDescendantOf(this AbsolutePath path, AbsolutePath possibleAncestor)
        {
            return path.IoService.IsDescendantOf(path, possibleAncestor);
        }

        public static IEnumerable<string> Split(this AbsolutePath path)
        {
            return path.IoService.Split(path);
        }

        public static string LastPathComponent(this AbsolutePath path)
        {
            return path.IoService.LastPathComponent(path);
        }

        public static IEnumerable<AbsolutePath> Ancestors(this AbsolutePath path, bool includeItself = false)
        {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IMaybe<AbsolutePath> Descendant(this AbsolutePath path, params AbsolutePath[] paths)
        {
            return path.IoService.Descendant(path, paths);
        }

        public static IMaybe<AbsolutePath> Descendant(this AbsolutePath path, params string[] paths)
        {
            return path.IoService.Descendant(path, paths);
        }

        public static IMaybe<AbsolutePath> Ancestor(this AbsolutePath path, int level)
        {
            return path.IoService.Ancestor(path, level);
        }

        public static bool HasExtension(this AbsolutePath path, string extension)
        {
            return path.IoService.HasExtension(path, extension);
        }

        public static IMaybe<AbsolutePath> WithExtension(this AbsolutePath path, string differentExtension)
        {
            return path.IoService.WithExtension(path, differentExtension);
        }

        public static IAbsolutePathTranslation Copy(this IAbsolutePathTranslation translation)
        {
            return translation.IoService.Copy(translation);
        }

        public static IAbsolutePathTranslation CopyFile(this IAbsolutePathTranslation translation)
        {
            return translation.IoService.CopyFile(translation);
        }

        public static IAbsolutePathTranslation CopyFolder(this IAbsolutePathTranslation translation)
        {
            return translation.IoService.CopyFolder(translation);
        }

        public static IAbsolutePathTranslation Move(this IAbsolutePathTranslation translation)
        {
            return translation.IoService.Move(translation);
        }

        public static IAbsolutePathTranslation MoveFile(this IAbsolutePathTranslation translation)
        {
            return translation.IoService.MoveFile(translation);
        }

        public static IAbsolutePathTranslation MoveFolder(this IAbsolutePathTranslation translation)
        {
            return translation.IoService.MoveFolder(translation);
        }

        public static bool ContainsFiles(this AbsolutePath path)
        {
            return path.IoService.ContainsFiles(path);
        }

        public static bool FolderContainsFiles(this AbsolutePath path)
        {
            return path.IoService.FolderContainsFiles(path);
        }

        public static IMaybe<AbsolutePath> GetCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.GetCommonAncestry(path1, path2);
        }

        public static IMaybe<Uri> GetCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.GetCommonDescendants(path1, path2);
        }

        public static IMaybe<Tuple<Uri, Uri>> GetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.GetNonCommonDescendants(path1, path2);
        }

        public static IMaybe<Tuple<Uri, Uri>> GetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.GetNonCommonAncestry(path1, path2);
        }

        public static IAbsolutePathTranslation Translate(this AbsolutePath pathToBeCopied, AbsolutePath source,
            AbsolutePath destination)
        {
            return pathToBeCopied.IoService.Translate(pathToBeCopied, source, destination);
        }

        public static IAbsolutePathTranslation Translate(this AbsolutePath source, AbsolutePath destination)
        {
            return source.IoService.Translate(source, destination);
        }

        public static FileInfo AsFileInfo(this AbsolutePath path)
        {
            return path.IoService.AsFileInfo(path);
        }

        public static DirectoryInfo AsDirectoryInfo(this AbsolutePath path)
        {
            return path.IoService.AsDirectoryInfo(path);
        }

        public static IMaybe<T> As<T>(T pathName, PathType pathType) where T : AbsolutePath
        {
            return pathName.IoService.As(pathName, pathType);
        }

        public static IMaybe<bool> IsReadOnly(this AbsolutePath path)
        {
            return path.IoService.IsReadOnly(path);
        }

        public static IMaybe<long> Length(this AbsolutePath path)
        {
            return path.IoService.Length(path);
        }

        public static IMaybe<FileAttributes> Attributes(this AbsolutePath attributes)
        {
            return attributes.IoService.Attributes(attributes);
        }

        public static IMaybe<DateTime> CreationTime(this AbsolutePath attributes)
        {
            return attributes.IoService.CreationTime(attributes);
        }

        public static IMaybe<DateTime> LastAccessTime(this AbsolutePath attributes)
        {
            return attributes.IoService.LastAccessTime(attributes);
        }

        public static IMaybe<DateTime> LastWriteTime(this AbsolutePath attributes)
        {
            return attributes.IoService.LastWriteTime(attributes);
        }

        public static IMaybe<string> FullName(this AbsolutePath attributes)
        {
            return attributes.IoService.FullName(attributes);
        }

        public static AbsolutePath Root(this AbsolutePath path)
        {
            return path.IoService.Root(path);
        }

        public static void RenameTo(this AbsolutePath source, AbsolutePath target)
        {
            source.IoService.RenameTo(source, target);
        }

        public static bool Exists(this AbsolutePath path)
        {
            return path.IoService.Exists(path);
        }

        public static PathType GetPathType(this AbsolutePath path)
        {
            return path.IoService.GetPathType(path);
        }

        public static AbsolutePath DeleteFolder(this AbsolutePath path, bool recursive = false)
        {
            return path.IoService.DeleteFolder(path, recursive);
        }

        public static void Create(this AbsolutePath path, PathType pathType)
        {
            path.IoService.Create(path, pathType);
        }

        public static IMaybe<FileStream> Open(this AbsolutePath path, FileMode fileMode)
        {
            return path.IoService.Open(path, fileMode);
        }

        public static IMaybe<FileStream> Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess)
        {
            return path.IoService.Open(path, fileMode, fileAccess);
        }

        public static IMaybe<FileStream> Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess,
            FileShare fileShare)
        {
            return path.IoService.Open(path, fileMode, fileAccess, fileShare);
        }

        public static AbsolutePath CreateFolder(this AbsolutePath path)
        {
            return path.IoService.CreateFolder(path);
        }

        public static void WriteAllText(this AbsolutePath path, string text)
        {
            path.IoService.WriteAllText(path, text);
        }

        public static void WriteAllLines(this AbsolutePath path, IEnumerable<string> lines)
        {
            path.IoService.WriteAllLines(path, lines);
        }

        public static void WriteAllLines(this AbsolutePath path, byte[] bytes)
        {
            path.IoService.WriteAllLines(path, bytes);
        }

        public static IEnumerable<string> ReadLines(this AbsolutePath path)
        {
            return path.IoService.ReadLines(path);
        }

        public static string ReadAllText(this AbsolutePath path)
        {
            return path.IoService.ReadAllText(path);
        }

        public static AbsolutePath ToAbsolute(this AbsolutePath path)
        {
            return path.IoService.ToAbsolute(path);
        }

        public static IReadOnlyObservableSet<AbsolutePath> Children(this AbsolutePath path)
        {
            return path.IoService.Children(path);
        }

        public static IReadOnlyObservableSet<AbsolutePath> Children(this AbsolutePath path, string pattern)
        {
            return path.IoService.Children(path, pattern);
        }

        public static IReadOnlyObservableSet<AbsolutePath> Descendants(this AbsolutePath path)
        {
            return path.IoService.Descendants(path);
        }

        public static IReadOnlyObservableSet<AbsolutePath> Descendants(this AbsolutePath path, string pattern)
        {
            return path.IoService.Descendants(path, pattern);
        }

        public static IObservable<Unit> ObserveChanges(this AbsolutePath path)
        {
            return path.IoService.ObserveChanges(path);
        }

        public static IObservable<Unit> ObserveChanges(this AbsolutePath path, NotifyFilters filters)
        {
            return path.IoService.ObserveChanges(path, filters);
        }

        public static IObservable<PathType> ObservePathType(this AbsolutePath path)
        {
            return path.IoService.ObservePathType(path);
        }

        public static IObservable<AbsolutePath> Renamings(this AbsolutePath path)
        {
            return path.IoService.Renamings(path);
        }

        public static AbsolutePath RelativeTo(this AbsolutePath path, AbsolutePath relativeTo)
        {
            return path.IoService.RelativeTo(path, relativeTo);
        }

        public static IMaybe<AbsolutePath> CommonWith(this AbsolutePath path, AbsolutePath that)
        {
            return path.IoService.CommonWith(path, that);
        }

        public static bool CanBeSimplified(this AbsolutePath path)
        {
            return path.IoService.CanBeSimplified(path);
        }

        public static AbsolutePath Simplify(this AbsolutePath path)
        {
            return path.IoService.Simplify(path);
        }

        public static IMaybe<AbsolutePath> Parent(this AbsolutePath path)
        {
            return path.IoService.Parent(path);
        }

        public static bool IsAbsolute(this AbsolutePath path)
        {
            return path.IoService.IsAbsolute(path);
        }

        public static bool IsRelative(this AbsolutePath path)
        {
            return path.IoService.IsRelative(path);
        }

        public static IMaybe<AbsolutePath> Join(this AbsolutePath root, IEnumerable<string> descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IMaybe<AbsolutePath> root, IEnumerable<string> descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<AbsolutePath> Join(this IMaybe<AbsolutePath> root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<AbsolutePath> Join(this AbsolutePath root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this AbsolutePath root, params string[] descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IMaybe<AbsolutePath> root, params string[] descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<AbsolutePath> Join(this IMaybe<AbsolutePath> root, params IMaybe<string>[] descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<AbsolutePath> Join(this AbsolutePath root, params IMaybe<string>[] descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> root, IEnumerable<string> descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> root, IEnumerable<string> descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> root,
            IEnumerable<IMaybe<string>> descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> root, IEnumerable<IMaybe<string>> descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> root, params string[] descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> root, params string[] descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> root,
            params IMaybe<string>[] descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> root, params IMaybe<string>[] descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IReadOnlyList<AbsolutePath> descendants)
        {
            var ioService = descendants.First().IoService;
            return ioService.Join(descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> descendants)
        {
            var ioService = descendants.First().IoService;
            return ioService.Join(descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IReadOnlyList<IMaybe<AbsolutePath>> descendants)
        {
            var ioService = descendants.First(x => x.HasValue).Value.IoService;
            return ioService.Join(descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> descendants)
        {
            var ioService = descendants.First(x => x.HasValue).Value.IoService;
            return ioService.Join(descendants);
        }

        public static IMaybe<AbsolutePath> Join(this AbsolutePath root, IEnumerable<AbsolutePath> descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IMaybe<AbsolutePath> root, IEnumerable<AbsolutePath> descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<AbsolutePath> Join(this IMaybe<AbsolutePath> root, IEnumerable<IMaybe<AbsolutePath>> descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<AbsolutePath> Join(this AbsolutePath root, IEnumerable<IMaybe<AbsolutePath>> descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this AbsolutePath root, params AbsolutePath[] descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IMaybe<AbsolutePath> root, params AbsolutePath[] descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<AbsolutePath> Join(this IMaybe<AbsolutePath> root, params IMaybe<AbsolutePath>[] descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<AbsolutePath> Join(this AbsolutePath root, params IMaybe<AbsolutePath>[] descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> root, IEnumerable<AbsolutePath> descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> root, IEnumerable<AbsolutePath> descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> root,
            IEnumerable<IMaybe<AbsolutePath>> descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> root, IEnumerable<IMaybe<AbsolutePath>> descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> root, params AbsolutePath[] descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> root, params AbsolutePath[] descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<IMaybe<AbsolutePath>> root,
            params IMaybe<AbsolutePath>[] descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<AbsolutePath> Join(this IEnumerable<AbsolutePath> root, params IMaybe<AbsolutePath>[] descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<StreamWriter> CreateText(this AbsolutePath pathSpec)
        {
            return pathSpec.IoService.CreateText(pathSpec);
        }

        public static IEnumerable<string> ReadLines(this AbsolutePath pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return pathSpec.IoService.ReadLines(pathSpec, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks,
                bufferSize, leaveOpen);
        }

        public static IMaybe<string> ReadText(this AbsolutePath pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return pathSpec.IoService.ReadText(pathSpec, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static void WriteText(this AbsolutePath pathSpec, IEnumerable<string> lines,
            FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            pathSpec.IoService.WriteText(pathSpec, lines, fileMode, fileAccess, fileShare, encoding, bufferSize,
                leaveOpen);
        }

        public static void WriteText(this AbsolutePath pathSpec, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            pathSpec.IoService.WriteText(pathSpec, text, fileMode, fileAccess, fileShare, encoding, bufferSize,
                leaveOpen);
        }

        public static bool IsFile(this AbsolutePath pathSpec)
        {
            return pathSpec.GetPathType() == PathType.File;
        }

        public static bool IsFolder(this AbsolutePath pathSpec)
        {
            return pathSpec.GetPathType() == PathType.Folder;
        }
    }
}