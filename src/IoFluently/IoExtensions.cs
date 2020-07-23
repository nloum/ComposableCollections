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
using UnitsNet;

namespace IoFluently
{
    public static class IoExtensions
    {
        public static AbsolutePath WithoutExtension(this AbsolutePath path)
        {
            if (!path.HasExtension())
            {
                return path;
            }

            var newComponents = new List<string>();

            for (var i = 0; i < path.Path.Components.Count - 1; i++)
            {
                newComponents.Add(path.Path.Components[i]);
            }

            var name = path.Name;
            newComponents.Add(name.Substring(0, name.LastIndexOf('.')));
            
            return new AbsolutePath(path.Flags, path.DirectorySeparator, path.IoService, newComponents);
        }

        public static bool HasExtension(this AbsolutePath path)
        {
            return path.Extension.HasValue;
        }

        public static AbsolutePath CreateEmptyFile(this AbsolutePath path)
        {
            return path.IoService.CreateEmptyFile(path);
        }

        public static Stream CreateFile(this AbsolutePath path)
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

        public static AbsolutePath Delete(this AbsolutePath path, bool recursiveDeleteIfFolder = false)
        {
            return path.IoService.Delete(path, recursiveDeleteIfFolder);
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

        public static IEnumerable<AbsolutePath> Ancestors(this AbsolutePath path, bool includeItself = false)
        {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IMaybe<AbsolutePath> TryDescendant(this AbsolutePath path, params AbsolutePath[] paths)
        {
            return path.IoService.TryDescendant(path, paths);
        }

        public static IMaybe<AbsolutePath> TryDescendant(this AbsolutePath path, params string[] paths)
        {
            return path.IoService.TryDescendant(path, paths);
        }

        public static IMaybe<AbsolutePath> TryAncestor(this AbsolutePath path, int level)
        {
            return path.IoService.TryAncestor(path, level);
        }

        public static AbsolutePath Descendant(this AbsolutePath path, params AbsolutePath[] paths)
        {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        public static AbsolutePath Descendant(this AbsolutePath path, params string[] paths)
        {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        public static AbsolutePath Ancestor(this AbsolutePath path, int level)
        {
            return path.IoService.TryAncestor(path, level).Value;
        }

        public static bool HasExtension(this AbsolutePath path, string extension)
        {
            return path.IoService.HasExtension(path, extension);
        }

        public static IMaybe<AbsolutePath> TryWithExtension(this AbsolutePath path, string differentExtension)
        {
            return path.IoService.TryWithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this AbsolutePath path, string differentExtension)
        {
            return path.IoService.TryWithExtension(path, differentExtension).Value;
        }

        public static AbsolutePath WithExtension(this AbsolutePath path, Func<string, string> differentExtension)
        {
            return path.IoService.TryWithExtension(path, differentExtension(path.Extension.ValueOrDefault ?? string.Empty)).Value;
        }

        public static IAbsolutePathTranslation Copy(this IAbsolutePathTranslation translation, bool overwrite = false)
        {
            return translation.IoService.Copy(translation, overwrite);
        }

        public static IAbsolutePathTranslation CopyFile(this IAbsolutePathTranslation translation, bool overwrite = false)
        {
            return translation.IoService.CopyFile(translation, overwrite);
        }

        public static IAbsolutePathTranslation CopyFolder(this IAbsolutePathTranslation translation, bool overwrite = false)
        {
            return translation.IoService.CopyFolder(translation, overwrite);
        }

        public static IAbsolutePathTranslation Move(this IAbsolutePathTranslation translation, bool overwrite = false)
        {
            return translation.IoService.Move(translation, overwrite);
        }

        public static IAbsolutePathTranslation MoveFile(this IAbsolutePathTranslation translation, bool overwrite = false)
        {
            return translation.IoService.MoveFile(translation, overwrite);
        }

        public static IAbsolutePathTranslation MoveFolder(this IAbsolutePathTranslation translation, bool overwrite = false)
        {
            return translation.IoService.MoveFolder(translation, overwrite);
        }

        public static bool ContainsFiles(this AbsolutePath path)
        {
            return path.IoService.ContainsFiles(path);
        }

        public static bool FolderContainsFiles(this AbsolutePath path)
        {
            return path.IoService.FolderContainsFiles(path);
        }

        public static IMaybe<AbsolutePath> TryGetCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetCommonAncestry(path1, path2);
        }

        public static AbsolutePath GetCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<Uri> TryGetCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetCommonDescendants(path1, path2);
        }

        public static Uri GetCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2).Value;
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

        public static IMaybe<bool> TryIsReadOnly(this AbsolutePath path)
        {
            return path.IoService.TryIsReadOnly(path);
        }

        public static bool IsReadOnly(this AbsolutePath path)
        {
            return path.IoService.TryIsReadOnly(path).Value;
        }

        public static IMaybe<Information> TryFileSize(this AbsolutePath path)
        {
            return path.IoService.TryFileSize(path);
        }

        public static Information FileSize(this AbsolutePath path)
        {
            return path.IoService.TryFileSize(path).Value;
        }

        public static IMaybe<FileAttributes> TryAttributes(this AbsolutePath attributes)
        {
            return attributes.IoService.TryAttributes(attributes);
        }

        public static FileAttributes Attributes(this AbsolutePath attributes)
        {
            return attributes.IoService.TryAttributes(attributes).Value;
        }

        public static IMaybe<DateTimeOffset> TryCreationTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryCreationTime(attributes);
        }

        public static DateTimeOffset CreationTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryCreationTime(attributes).Value;
        }

        public static IMaybe<DateTimeOffset> TryLastAccessTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryLastAccessTime(attributes);
        }

        public static DateTimeOffset LastAccessTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryLastAccessTime(attributes).Value;
        }

        public static IMaybe<DateTimeOffset> TryLastWriteTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryLastWriteTime(attributes);
        }

        public static DateTimeOffset LastWriteTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryLastWriteTime(attributes).Value;
        }

        public static IMaybe<string> TryFullName(this AbsolutePath attributes)
        {
            return attributes.IoService.TryFullName(attributes);
        }

        public static string FullName(this AbsolutePath attributes)
        {
            return attributes.IoService.TryFullName(attributes).Value;
        }

        public static AbsolutePath Root(this AbsolutePath path)
        {
            return path.IoService.Root(path);
        }

        public static void RenameTo(this AbsolutePath source, AbsolutePath target)
        {
            source.IoService.RenameTo(source, target);
        }

        public static AbsolutePaths GlobFiles(this AbsolutePath path, string pattern)
        {
            Func<AbsolutePath, IEnumerable<RelativePath>> patternFunc = absPath => absPath.Children(pattern).Select(x => new RelativePath(x.Flags, x.DirectorySeparator, x.IoService, new[]{x.Name}));
            return path / patternFunc;
        }
        
        public static bool Exists(this AbsolutePath path)
        {
            return path.IoService.Exists(path);
        }

        public static PathType GetPathType(this AbsolutePath path)
        {
            return path.IoService.GetPathType(path);
        }

        public static AbsolutePath EnsureIsFolder(this AbsolutePath path)
        {
            if (!path.IsFolder())
            {
                path.CreateFolder();
            }

            return path;
        }

        public static AbsolutePath EnsureIsNotFolder(this AbsolutePath path, bool recursive = false)
        {
            if (path.IsFolder())
            {
                path.DeleteFolder(recursive);
            }

            return path;
        }

        public static AbsolutePath EnsureIsFile(this AbsolutePath path)
        {
            if (!path.IsFile())
            {
                path.CreateEmptyFile();
            }

            return path;
        }

        public static AbsolutePath EnsureIsNotFile(this AbsolutePath path)
        {
            if (path.IsFile())
            {
                path.DeleteFile();
            }

            return path;
        }

        public static AbsolutePath EnsureDoesNotExist(this AbsolutePath path, bool recursiveDeleteIfFolder = false)
        {
            if (path.Exists())
            {
                path.Delete(recursiveDeleteIfFolder);
            }

            return path;
        }

        public static AbsolutePath EnsureIsEmptyFolder(this AbsolutePath path, bool recursiveDeleteIfFolder = false)
        {
            if (path.Exists())
            {
                path.Delete(recursiveDeleteIfFolder);
            }

            path.CreateFolder();

            return path;
        }

        public static AbsolutePath DeleteFolder(this AbsolutePath path, bool recursive = false)
        {
            return path.IoService.DeleteFolder(path, recursive);
        }

        public static AbsolutePath Create(this AbsolutePath path, PathType pathType)
        {
            return path.IoService.Create(path, pathType);
        }

        public static IMaybe<Stream> TryOpen(this AbsolutePath path, FileMode fileMode)
        {
            return path.IoService.TryOpen(path, fileMode);
        }

        public static IMaybe<Stream> TryOpen(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess)
        {
            return path.IoService.TryOpen(path, fileMode, fileAccess);
        }

        public static IMaybe<Stream> TryOpen(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess,
            FileShare fileShare)
        {
            return path.IoService.TryOpen(path, fileMode, fileAccess, fileShare);
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode)
        {
            return path.IoService.TryOpen(path, fileMode).Value;
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess)
        {
            return path.IoService.TryOpen(path, fileMode, fileAccess).Value;
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess,
            FileShare fileShare)
        {
            return path.IoService.TryOpen(path, fileMode, fileAccess, fileShare).Value;
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

        public static void WriteAllBytes(this AbsolutePath path, byte[] bytes)
        {
            path.IoService.WriteAllBytes(path, bytes);
        }

        public static IEnumerable<string> ReadLines(this AbsolutePath path)
        {
            return path.IoService.ReadLines(path);
        }

        public static string ReadAllText(this AbsolutePath path)
        {
            return path.IoService.ReadAllText(path);
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

        public static RelativePath RelativeTo(this AbsolutePath path, AbsolutePath relativeTo)
        {
            return path.IoService.RelativeTo(path, relativeTo);
        }

        public static IMaybe<AbsolutePath> TryCommonWith(this AbsolutePath path, AbsolutePath that)
        {
            return path.IoService.TryCommonWith(path, that);
        }

        public static AbsolutePath CommonWith(this AbsolutePath path, AbsolutePath that)
        {
            return path.IoService.TryCommonWith(path, that).Value;
        }

        public static bool CanBeSimplified(this AbsolutePath path)
        {
            return path.IoService.CanBeSimplified(path);
        }

        public static AbsolutePath Simplify(this AbsolutePath path)
        {
            return path.IoService.Simplify(path);
        }

        public static IMaybe<AbsolutePath> TryParent(this AbsolutePath path)
        {
            return path.IoService.TryParent(path);
        }

        public static AbsolutePath Parent(this AbsolutePath path)
        {
            return path.IoService.TryParent(path).Value;
        }

        public static IMaybe<StreamWriter> TryOpenWriter(this AbsolutePath absolutePath)
        {
            return absolutePath.IoService.TryOpenWriter(absolutePath);
        }

        public static StreamWriter OpenWriter(this AbsolutePath absolutePath)
        {
            return absolutePath.IoService.TryOpenWriter(absolutePath).Value;
        }

        public static IMaybe<StreamReader> TryOpenReader(this AbsolutePath path)
        {
            return path.TryOpen(FileMode.Open, FileAccess.Read, FileShare.Read).Select(stream => new StreamReader(stream));
        }

        public static StreamReader OpenReader(this AbsolutePath path)
        {
            return path.TryOpenReader().Value;
        }

        public static IEnumerable<string> ReadLines(this AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return absolutePath.IoService.ReadLines(absolutePath, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks,
                bufferSize, leaveOpen);
        }

        public static IMaybe<string> TryReadText(this AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return absolutePath.IoService.TryReadText(absolutePath, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static string ReadText(this AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096,
            bool leaveOpen = false)
        {
            return absolutePath.IoService.TryReadText(absolutePath, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, leaveOpen).Value;
        }

        public static void WriteText(this AbsolutePath absolutePath, IEnumerable<string> lines,
            FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            absolutePath.IoService.WriteText(absolutePath, lines, fileMode, fileAccess, fileShare, encoding, bufferSize,
                leaveOpen);
        }

        public static void WriteText(this AbsolutePath absolutePath, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
        {
            absolutePath.IoService.WriteText(absolutePath, text, fileMode, fileAccess, fileShare, encoding, bufferSize,
                leaveOpen);
        }

        public static bool IsFile(this AbsolutePath absolutePath)
        {
            return absolutePath.GetPathType() == PathType.File;
        }

        public static bool IsFolder(this AbsolutePath absolutePath)
        {
            return absolutePath.GetPathType() == PathType.Folder;
        }
    }
}