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

namespace MoreIO
{
    public static class IoExtensions
    {
        public static ISetChanges<PathSpec> ToLiveLinq(this PathSpec root,
            bool includeFileContentChanges = true, PathObservationMethod observationMethod = PathObservationMethod.Default)
        {
            return root.IoService.ToLiveLinq(root, includeFileContentChanges, observationMethod);
        }

        public static IEnumerable<PathSpec> GetDescendants(this PathSpec path)
        {
            return path.IoService.GetDescendants(path);
        }

        public static IEnumerable<TreeTraversal<string, PathSpec>> TraverseDescendants(this PathSpec path)
        {
            return path.IoService.TraverseDescendants(path);
        }

        public static IEnumerable<PathSpec> GetChildren(this PathSpec path, bool includeFolders = true,
            bool includeFiles = true)
        {
            return path.IoService.GetChildren(path, includeFolders, includeFiles);
        }

        public static IEnumerable<PathSpec> GetFiles(this PathSpec path)
        {
            return path.IoService.GetFiles(path);
        }

        public static IEnumerable<PathSpec> GetFolders(this PathSpec path)
        {
            return path.IoService.GetFolders(path);
        }

        public static PathSpec CreateEmptyFile(this PathSpec path)
        {
            return path.IoService.CreateEmptyFile(path);
        }

        public static FileStream CreateFile(this PathSpec path)
        {
            return path.IoService.CreateFile(path);
        }

        public static PathSpec DeleteFile(this PathSpec path)
        {
            return path.IoService.DeleteFile(path);
        }

        public static PathSpec ClearFolder(this PathSpec path)
        {
            return path.IoService.ClearFolder(path);
        }

        public static PathSpec Decrypt(this PathSpec path)
        {
            return path.IoService.Decrypt(path);
        }

        public static PathSpec Encrypt(this PathSpec path)
        {
            return path.IoService.Encrypt(path);
        }

        public static PathSpec Delete(this PathSpec path)
        {
            return path.IoService.Delete(path);
        }

        public static bool IsAncestorOf(this PathSpec path, PathSpec possibleDescendant)
        {
            return path.IoService.IsAncestorOf(path, possibleDescendant);
        }

        public static bool IsDescendantOf(this PathSpec path, PathSpec possibleAncestor)
        {
            return path.IoService.IsDescendantOf(path, possibleAncestor);
        }

        public static IEnumerable<string> Split(this PathSpec path)
        {
            return path.IoService.Split(path);
        }

        public static string LastPathComponent(this PathSpec path)
        {
            return path.IoService.LastPathComponent(path);
        }

        public static IEnumerable<PathSpec> Ancestors(this PathSpec path, bool includeItself = false)
        {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IMaybe<PathSpec> Descendant(this PathSpec path, params PathSpec[] paths)
        {
            return path.IoService.Descendant(path, paths);
        }

        public static IMaybe<PathSpec> Descendant(this PathSpec path, params string[] paths)
        {
            return path.IoService.Descendant(path, paths);
        }

        public static IMaybe<PathSpec> Ancestor(this PathSpec path, int level)
        {
            return path.IoService.Ancestor(path, level);
        }

        public static bool HasExtension(this PathSpec path, string extension)
        {
            return path.IoService.HasExtension(path, extension);
        }

        public static IMaybe<PathSpec> WithExtension(this PathSpec path, string differentExtension)
        {
            return path.IoService.WithExtension(path, differentExtension);
        }

        public static IPathSpecTranslation Copy(this IPathSpecTranslation translation)
        {
            return translation.IoService.Copy(translation);
        }

        public static IPathSpecTranslation CopyFile(this IPathSpecTranslation translation)
        {
            return translation.IoService.CopyFile(translation);
        }

        public static IPathSpecTranslation CopyFolder(this IPathSpecTranslation translation)
        {
            return translation.IoService.CopyFolder(translation);
        }

        public static IPathSpecTranslation Move(this IPathSpecTranslation translation)
        {
            return translation.IoService.Move(translation);
        }

        public static IPathSpecTranslation MoveFile(this IPathSpecTranslation translation)
        {
            return translation.IoService.MoveFile(translation);
        }

        public static IPathSpecTranslation MoveFolder(this IPathSpecTranslation translation)
        {
            return translation.IoService.MoveFolder(translation);
        }

        public static bool ContainsFiles(this PathSpec path)
        {
            return path.IoService.ContainsFiles(path);
        }

        public static bool FolderContainsFiles(this PathSpec path)
        {
            return path.IoService.FolderContainsFiles(path);
        }

        public static IMaybe<PathSpec> GetCommonAncestry(this PathSpec path1, PathSpec path2)
        {
            return path1.IoService.GetCommonAncestry(path1, path2);
        }

        public static IMaybe<Uri> GetCommonDescendants(this PathSpec path1, PathSpec path2)
        {
            return path1.IoService.GetCommonDescendants(path1, path2);
        }

        public static IMaybe<Tuple<Uri, Uri>> GetNonCommonDescendants(this PathSpec path1, PathSpec path2)
        {
            return path1.IoService.GetNonCommonDescendants(path1, path2);
        }

        public static IMaybe<Tuple<Uri, Uri>> GetNonCommonAncestry(this PathSpec path1, PathSpec path2)
        {
            return path1.IoService.GetNonCommonAncestry(path1, path2);
        }

        public static IPathSpecTranslation Translate(this PathSpec pathToBeCopied, PathSpec source,
            PathSpec destination)
        {
            return pathToBeCopied.IoService.Translate(pathToBeCopied, source, destination);
        }

        public static IPathSpecTranslation Translate(this PathSpec source, PathSpec destination)
        {
            return source.IoService.Translate(source, destination);
        }

        public static FileInfo AsFileInfo(this PathSpec path)
        {
            return path.IoService.AsFileInfo(path);
        }

        public static DirectoryInfo AsDirectoryInfo(this PathSpec path)
        {
            return path.IoService.AsDirectoryInfo(path);
        }

        public static IMaybe<T> As<T>(T pathName, PathType pathType) where T : PathSpec
        {
            return pathName.IoService.As(pathName, pathType);
        }

        public static IMaybe<bool> IsReadOnly(this PathSpec path)
        {
            return path.IoService.IsReadOnly(path);
        }

        public static IMaybe<long> Length(this PathSpec path)
        {
            return path.IoService.Length(path);
        }

        public static IMaybe<FileAttributes> Attributes(this PathSpec attributes)
        {
            return attributes.IoService.Attributes(attributes);
        }

        public static IMaybe<DateTime> CreationTime(this PathSpec attributes)
        {
            return attributes.IoService.CreationTime(attributes);
        }

        public static IMaybe<DateTime> LastAccessTime(this PathSpec attributes)
        {
            return attributes.IoService.LastAccessTime(attributes);
        }

        public static IMaybe<DateTime> LastWriteTime(this PathSpec attributes)
        {
            return attributes.IoService.LastWriteTime(attributes);
        }

        public static IMaybe<string> FullName(this PathSpec attributes)
        {
            return attributes.IoService.FullName(attributes);
        }

        public static PathSpec Root(this PathSpec path)
        {
            return path.IoService.Root(path);
        }

        public static void RenameTo(this PathSpec source, PathSpec target)
        {
            source.IoService.RenameTo(source, target);
        }

        public static bool Exists(this PathSpec path)
        {
            return path.IoService.Exists(path);
        }

        public static PathType GetPathType(this PathSpec path)
        {
            return path.IoService.GetPathType(path);
        }

        public static PathSpec DeleteFolder(this PathSpec path, bool recursive = false)
        {
            return path.IoService.DeleteFolder(path, recursive);
        }

        public static void Create(this PathSpec path, PathType pathType)
        {
            path.IoService.Create(path, pathType);
        }

        public static IMaybe<FileStream> Open(this PathSpec path, FileMode fileMode)
        {
            return path.IoService.Open(path, fileMode);
        }

        public static IMaybe<FileStream> Open(this PathSpec path, FileMode fileMode, FileAccess fileAccess)
        {
            return path.IoService.Open(path, fileMode, fileAccess);
        }

        public static IMaybe<FileStream> Open(this PathSpec path, FileMode fileMode, FileAccess fileAccess,
            FileShare fileShare)
        {
            return path.IoService.Open(path, fileMode, fileAccess, fileShare);
        }

        public static PathSpec CreateFolder(this PathSpec path)
        {
            return path.IoService.CreateFolder(path);
        }

        public static void WriteAllText(this PathSpec path, string text)
        {
            path.IoService.WriteAllText(path, text);
        }

        public static void WriteAllLines(this PathSpec path, IEnumerable<string> lines)
        {
            path.IoService.WriteAllLines(path, lines);
        }

        public static void WriteAllLines(this PathSpec path, byte[] bytes)
        {
            path.IoService.WriteAllLines(path, bytes);
        }

        public static IEnumerable<string> ReadLines(this PathSpec path)
        {
            return path.IoService.ReadLines(path);
        }

        public static string ReadAllText(this PathSpec path)
        {
            return path.IoService.ReadAllText(path);
        }

        public static PathSpec ToAbsolute(this PathSpec path)
        {
            return path.IoService.ToAbsolute(path);
        }

        public static IReadOnlyObservableSet<PathSpec> Children(this PathSpec path)
        {
            return path.IoService.Children(path);
        }

        public static IReadOnlyObservableSet<PathSpec> Children(this PathSpec path, string pattern)
        {
            return path.IoService.Children(path, pattern);
        }

        public static IReadOnlyObservableSet<PathSpec> Descendants(this PathSpec path)
        {
            return path.IoService.Descendants(path);
        }

        public static IReadOnlyObservableSet<PathSpec> Descendants(this PathSpec path, string pattern)
        {
            return path.IoService.Descendants(path, pattern);
        }

        public static IObservable<Unit> ObserveChanges(this PathSpec path)
        {
            return path.IoService.ObserveChanges(path);
        }

        public static IObservable<Unit> ObserveChanges(this PathSpec path, NotifyFilters filters)
        {
            return path.IoService.ObserveChanges(path, filters);
        }

        public static IObservable<PathType> ObservePathType(this PathSpec path)
        {
            return path.IoService.ObservePathType(path);
        }

        public static IObservable<PathSpec> Renamings(this PathSpec path)
        {
            return path.IoService.Renamings(path);
        }

        public static PathSpec RelativeTo(this PathSpec path, PathSpec relativeTo)
        {
            return path.IoService.RelativeTo(path, relativeTo);
        }

        public static IMaybe<PathSpec> CommonWith(this PathSpec path, PathSpec that)
        {
            return path.IoService.CommonWith(path, that);
        }

        public static bool CanBeSimplified(this PathSpec path)
        {
            return path.IoService.CanBeSimplified(path);
        }

        public static PathSpec Simplify(this PathSpec path)
        {
            return path.IoService.Simplify(path);
        }

        public static IMaybe<PathSpec> Parent(this PathSpec path)
        {
            return path.IoService.Parent(path);
        }

        public static bool IsAbsolute(this PathSpec path)
        {
            return path.IoService.IsAbsolute(path);
        }

        public static bool IsRelative(this PathSpec path)
        {
            return path.IoService.IsRelative(path);
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, IEnumerable<string> descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, IEnumerable<string> descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, params string[] descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, params string[] descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, params IMaybe<string>[] descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, params IMaybe<string>[] descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, IEnumerable<string> descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, IEnumerable<string> descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root,
            IEnumerable<IMaybe<string>> descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, IEnumerable<IMaybe<string>> descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, params string[] descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, params string[] descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root,
            params IMaybe<string>[] descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, params IMaybe<string>[] descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IReadOnlyList<PathSpec> descendants)
        {
            var ioService = descendants.First().IoService;
            return ioService.Join(descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> descendants)
        {
            var ioService = descendants.First().IoService;
            return ioService.Join(descendants);
        }

        public static IMaybe<PathSpec> Join(this IReadOnlyList<IMaybe<PathSpec>> descendants)
        {
            var ioService = descendants.First(x => x.HasValue).Value.IoService;
            return ioService.Join(descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> descendants)
        {
            var ioService = descendants.First(x => x.HasValue).Value.IoService;
            return ioService.Join(descendants);
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, IEnumerable<PathSpec> descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, IEnumerable<PathSpec> descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, params PathSpec[] descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, params PathSpec[] descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, params IMaybe<PathSpec>[] descendants)
        {
            return root.SelectMany(r => r.IoService.Join(root, descendants));
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, params IMaybe<PathSpec>[] descendants)
        {
            return root.IoService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, IEnumerable<PathSpec> descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, IEnumerable<PathSpec> descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root,
            IEnumerable<IMaybe<PathSpec>> descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, params PathSpec[] descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, params PathSpec[] descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root,
            params IMaybe<PathSpec>[] descendants)
        {
            var ioService = root.First(x => x.HasValue).Value.IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, params IMaybe<PathSpec>[] descendants)
        {
            var ioService = root.First().IoService;
            return ioService.Join(root, descendants);
        }

        public static IMaybe<StreamWriter> CreateText(this PathSpec pathSpec)
        {
            return pathSpec.IoService.CreateText(pathSpec);
        }

        public static IEnumerable<string> ReadLines(this PathSpec pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return pathSpec.IoService.ReadLines(pathSpec, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks,
                bufferSize, leaveOpen);
        }

        public static IMaybe<string> ReadText(this PathSpec pathSpec, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return pathSpec.IoService.ReadText(pathSpec, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static void WriteText(this PathSpec pathSpec, IEnumerable<string> lines,
            FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            pathSpec.IoService.WriteText(pathSpec, lines, fileMode, fileAccess, fileShare, encoding, bufferSize,
                leaveOpen);
        }

        public static void WriteText(this PathSpec pathSpec, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            pathSpec.IoService.WriteText(pathSpec, text, fileMode, fileAccess, fileShare, encoding, bufferSize,
                leaveOpen);
        }

        public static bool IsFile(this PathSpec pathSpec)
        {
            return pathSpec.GetPathType() == PathType.File;
        }

        public static bool IsFolder(this PathSpec pathSpec)
        {
            return pathSpec.GetPathType() == PathType.Folder;
        }
    }
}