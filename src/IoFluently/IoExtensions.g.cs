using System;
using System.Collections.Generic;
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
    public static partial class IoExtensions
    {
        public static AbsolutePath Ancestor(this AbsolutePath path, int level)
        {
            return path.IoService.Ancestor(path, level);
        }

        public static IEnumerable<AbsolutePath> Ancestors(this AbsolutePath path, Boolean includeItself = false)
        {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static FileAttributes Attributes(this AbsolutePath attributes)
        {
            return attributes.IoService.Attributes(attributes);
        }

        public static Boolean CanBeSimplified(this AbsolutePath path)
        {
            return path.IoService.CanBeSimplified(path);
        }

        public static AbsolutePath ClearFolder(this AbsolutePath path)
        {
            return path.IoService.ClearFolder(path);
        }

        public static AbsolutePath Combine(this AbsolutePath path, String[] subsequentPathParts)
        {
            return path.IoService.Combine(path, subsequentPathParts);
        }

        public static AbsolutePath CommonWith(this AbsolutePath path, AbsolutePath that)
        {
            return path.IoService.CommonWith(path, that);
        }

        public static Boolean ContainsFiles(this AbsolutePath path)
        {
            return path.IoService.ContainsFiles(path);
        }

        public static IAbsolutePathTranslation Copy(this IAbsolutePathTranslation translation,
            Boolean overwrite = false)
        {
            return translation.IoService.Copy(translation, overwrite);
        }

        public static IAbsolutePathTranslation CopyFile(this IAbsolutePathTranslation translation,
            Boolean overwrite = false)
        {
            return translation.IoService.CopyFile(translation, overwrite);
        }

        public static IAbsolutePathTranslation CopyFolder(this IAbsolutePathTranslation translation,
            Boolean overwrite = false)
        {
            return translation.IoService.CopyFolder(translation, overwrite);
        }

        public static AbsolutePath Create(this AbsolutePath path, PathType pathType)
        {
            return path.IoService.Create(path, pathType);
        }

        public static AbsolutePath CreateEmptyFile(this AbsolutePath path)
        {
            return path.IoService.CreateEmptyFile(path);
        }

        public static Stream CreateFile(this AbsolutePath path)
        {
            return path.IoService.CreateFile(path);
        }

        public static AbsolutePath CreateFolder(this AbsolutePath path)
        {
            return path.IoService.CreateFolder(path);
        }

        public static DateTimeOffset CreationTime(this AbsolutePath attributes)
        {
            return attributes.IoService.CreationTime(attributes);
        }

        public static AbsolutePath Decrypt(this AbsolutePath path)
        {
            return path.IoService.Decrypt(path);
        }

        public static AbsolutePath Delete(this AbsolutePath path, Boolean recursiveDeleteIfFolder = false)
        {
            return path.IoService.Delete(path, recursiveDeleteIfFolder);
        }

        public static AbsolutePath DeleteFile(this AbsolutePath path)
        {
            return path.IoService.DeleteFile(path);
        }

        public static AbsolutePath DeleteFolder(this AbsolutePath path, Boolean recursive = false)
        {
            return path.IoService.DeleteFolder(path, recursive);
        }

        public static AbsolutePath Descendant(this AbsolutePath path, String[] paths)
        {
            return path.IoService.Descendant(path, paths);
        }

        public static AbsolutePath Descendant(this AbsolutePath path, AbsolutePath[] paths)
        {
            return path.IoService.Descendant(path, paths);
        }

        public static AbsolutePath Encrypt(this AbsolutePath path)
        {
            return path.IoService.Encrypt(path);
        }

        public static AbsolutePath EnsureDoesNotExist(this AbsolutePath path, Boolean recursiveDeleteIfFolder = false)
        {
            return path.IoService.EnsureDoesNotExist(path, recursiveDeleteIfFolder);
        }

        public static AbsolutePath EnsureIsEmptyFolder(this AbsolutePath path, Boolean recursiveDeleteIfFolder = false)
        {
            return path.IoService.EnsureIsEmptyFolder(path, recursiveDeleteIfFolder);
        }

        public static AbsolutePath EnsureIsFile(this AbsolutePath path)
        {
            return path.IoService.EnsureIsFile(path);
        }

        public static AbsolutePath EnsureIsFolder(this AbsolutePath path)
        {
            return path.IoService.EnsureIsFolder(path);
        }

        public static AbsolutePath EnsureIsNotFile(this AbsolutePath path)
        {
            return path.IoService.EnsureIsNotFile(path);
        }

        public static AbsolutePath EnsureIsNotFolder(this AbsolutePath path, Boolean recursive = false)
        {
            return path.IoService.EnsureIsNotFolder(path, recursive);
        }

        public static Boolean Exists(this AbsolutePath path)
        {
            return path.IoService.Exists(path);
        }

        public static Information FileSize(this AbsolutePath path)
        {
            return path.IoService.FileSize(path);
        }

        public static Boolean FolderContainsFiles(this AbsolutePath path)
        {
            return path.IoService.FolderContainsFiles(path);
        }

        public static string FullName(this AbsolutePath attributes)
        {
            return attributes.IoService.FullName(attributes);
        }

        public static AbsolutePath GetCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.GetCommonAncestry(path1, path2);
        }

        public static Uri GetCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.GetCommonDescendants(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.GetNonCommonAncestry(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.GetNonCommonDescendants(path1, path2);
        }

        public static PathType GetPathType(this AbsolutePath path)
        {
            return path.IoService.GetPathType(path);
        }

        public static AbsolutePaths GlobFiles(this AbsolutePath path, string pattern)
        {
            return path.IoService.GlobFiles(path, pattern);
        }

        public static Boolean HasExtension(this AbsolutePath path)
        {
            return path.IoService.HasExtension(path);
        }

        public static Boolean HasExtension(this AbsolutePath path, string extension)
        {
            return path.IoService.HasExtension(path, extension);
        }

        public static Boolean IsAncestorOf(this AbsolutePath path, AbsolutePath possibleDescendant)
        {
            return path.IoService.IsAncestorOf(path, possibleDescendant);
        }

        public static Boolean IsDescendantOf(this AbsolutePath path, AbsolutePath possibleAncestor)
        {
            return path.IoService.IsDescendantOf(path, possibleAncestor);
        }

        public static Boolean IsFile(this AbsolutePath absolutePath)
        {
            return absolutePath.IoService.IsFile(absolutePath);
        }

        public static Boolean IsFolder(this AbsolutePath absolutePath)
        {
            return absolutePath.IoService.IsFolder(absolutePath);
        }

        public static Boolean IsReadOnly(this AbsolutePath path)
        {
            return path.IoService.IsReadOnly(path);
        }

        public static DateTimeOffset LastAccessTime(this AbsolutePath attributes)
        {
            return attributes.IoService.LastAccessTime(attributes);
        }

        public static DateTimeOffset LastWriteTime(this AbsolutePath attributes)
        {
            return attributes.IoService.LastWriteTime(attributes);
        }

        public static IAbsolutePathTranslation Move(this IAbsolutePathTranslation translation,
            Boolean overwrite = false)
        {
            return translation.IoService.Move(translation, overwrite);
        }

        public static IAbsolutePathTranslation MoveFile(this IAbsolutePathTranslation translation,
            Boolean overwrite = false)
        {
            return translation.IoService.MoveFile(translation, overwrite);
        }

        public static IAbsolutePathTranslation MoveFolder(this IAbsolutePathTranslation translation,
            Boolean overwrite = false)
        {
            return translation.IoService.MoveFolder(translation, overwrite);
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

        public static Stream Open(this AbsolutePath path, FileMode fileMode)
        {
            return path.IoService.Open(path, fileMode);
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess)
        {
            return path.IoService.Open(path, fileMode, fileAccess);
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            return path.IoService.Open(path, fileMode, fileAccess, fileShare);
        }

        public static StreamReader OpenReader(this AbsolutePath path)
        {
            return path.IoService.OpenReader(path);
        }

        public static StreamWriter OpenWriter(this AbsolutePath absolutePath)
        {
            return absolutePath.IoService.OpenWriter(absolutePath);
        }

        public static AbsolutePath Parent(this AbsolutePath path)
        {
            return path.IoService.Parent(path);
        }

        public static string ReadAllText(this AbsolutePath path)
        {
            return path.IoService.ReadAllText(path);
        }

        public static IEnumerable<string> ReadLines(this AbsolutePath path)
        {
            return path.IoService.ReadLines(path);
        }

        public static IEnumerable<string> ReadLines(this AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read, Encoding encoding = null,
            Boolean detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, Boolean leaveOpen = false)
        {
            return absolutePath.IoService.ReadLines(absolutePath, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static string ReadText(this AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read, Encoding encoding = null,
            Boolean detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, Boolean leaveOpen = false)
        {
            return absolutePath.IoService.ReadText(absolutePath, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static RelativePath RelativeTo(this AbsolutePath path, AbsolutePath relativeTo)
        {
            return path.IoService.RelativeTo(path, relativeTo);
        }

        public static void RenameTo(this AbsolutePath source, AbsolutePath target)
        {
            source.IoService.RenameTo(source, target);
        }

        public static IObservable<AbsolutePath> Renamings(this AbsolutePath path)
        {
            return path.IoService.Renamings(path);
        }

        public static AbsolutePath Root(this AbsolutePath path)
        {
            return path.IoService.Root(path);
        }

        public static RelativePath Simplify(this RelativePath path)
        {
            return path.IoService.Simplify(path);
        }

        public static AbsolutePath Simplify(this AbsolutePath path)
        {
            return path.IoService.Simplify(path);
        }

        public static IEnumerable<string> Split(this AbsolutePath path)
        {
            return path.IoService.Split(path);
        }

        public static ISetChanges<AbsolutePath> ToLiveLinq(this AbsolutePath path, Boolean includeFileContentChanges,
            Boolean includeSubFolders, string pattern)
        {
            return path.IoService.ToLiveLinq(path, includeFileContentChanges, includeSubFolders, pattern);
        }

        public static IAbsolutePathTranslation Translate(this AbsolutePath source, AbsolutePath destination)
        {
            return source.IoService.Translate(source, destination);
        }

        public static IAbsolutePathTranslation Translate(this AbsolutePath pathToBeCopied, AbsolutePath source,
            AbsolutePath destination)
        {
            return pathToBeCopied.IoService.Translate(pathToBeCopied, source, destination);
        }

        public static IMaybe<AbsolutePath> TryAncestor(this AbsolutePath path, int level)
        {
            return path.IoService.TryAncestor(path, level);
        }

        public static IMaybe<FileAttributes> TryAttributes(this AbsolutePath attributes)
        {
            return attributes.IoService.TryAttributes(attributes);
        }

        public static IMaybe<AbsolutePath> TryCommonWith(this AbsolutePath path, AbsolutePath that)
        {
            return path.IoService.TryCommonWith(path, that);
        }

        public static IMaybe<DateTimeOffset> TryCreationTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryCreationTime(attributes);
        }

        public static IMaybe<AbsolutePath> TryDescendant(this AbsolutePath path, AbsolutePath[] paths)
        {
            return path.IoService.TryDescendant(path, paths);
        }

        public static IMaybe<AbsolutePath> TryDescendant(this AbsolutePath path, String[] paths)
        {
            return path.IoService.TryDescendant(path, paths);
        }

        public static IMaybe<Information> TryFileSize(this AbsolutePath path)
        {
            return path.IoService.TryFileSize(path);
        }

        public static IMaybe<string> TryFullName(this AbsolutePath attributes)
        {
            return attributes.IoService.TryFullName(attributes);
        }

        public static IMaybe<AbsolutePath> TryGetCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetCommonAncestry(path1, path2);
        }

        public static IMaybe<Uri> TryGetCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetCommonDescendants(path1, path2);
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2);
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2)
        {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2);
        }

        public static IMaybe<Boolean> TryIsReadOnly(this AbsolutePath path)
        {
            return path.IoService.TryIsReadOnly(path);
        }

        public static IMaybe<DateTimeOffset> TryLastAccessTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryLastAccessTime(attributes);
        }

        public static IMaybe<DateTimeOffset> TryLastWriteTime(this AbsolutePath attributes)
        {
            return attributes.IoService.TryLastWriteTime(attributes);
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

        public static IMaybe<StreamReader> TryOpenReader(this AbsolutePath path)
        {
            return path.IoService.TryOpenReader(path);
        }

        public static IMaybe<StreamWriter> TryOpenWriter(this AbsolutePath absolutePath)
        {
            return absolutePath.IoService.TryOpenWriter(absolutePath);
        }

        public static IMaybe<AbsolutePath> TryParent(this AbsolutePath path)
        {
            return path.IoService.TryParent(path);
        }

        public static IMaybe<string> TryReadText(this AbsolutePath absolutePath, FileMode fileMode = FileMode.Open,
            FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read, Encoding encoding = null,
            Boolean detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, Boolean leaveOpen = false)
        {
            return absolutePath.IoService.TryReadText(absolutePath, fileMode, fileAccess, fileShare, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static IMaybe<AbsolutePath> TryWithExtension(this AbsolutePath path, string differentExtension)
        {
            return path.IoService.TryWithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this AbsolutePath path, Func<string, string> differentExtension)
        {
            return path.IoService.WithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this AbsolutePath path, string differentExtension)
        {
            return path.IoService.WithExtension(path, differentExtension);
        }

        public static AbsolutePath WithoutExtension(this AbsolutePath path)
        {
            return path.IoService.WithoutExtension(path);
        }

        public static void WriteAllBytes(this AbsolutePath path, Byte[] bytes)
        {
            path.IoService.WriteAllBytes(path, bytes);
        }

        public static void WriteAllLines(this AbsolutePath path, IEnumerable<string> lines)
        {
            path.IoService.WriteAllLines(path, lines);
        }

        public static void WriteAllText(this AbsolutePath path, string text)
        {
            path.IoService.WriteAllText(path, text);
        }

        public static void WriteText(this AbsolutePath absolutePath, string text, FileMode fileMode = FileMode.Create,
            FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None, Encoding encoding = null,
            int bufferSize = 4096, Boolean leaveOpen = false)
        {
            absolutePath.IoService.WriteText(absolutePath, text, fileMode, fileAccess, fileShare, encoding, bufferSize,
                leaveOpen);
        }

        public static void WriteText(this AbsolutePath absolutePath, IEnumerable<string> lines,
            FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write,
            FileShare fileShare = FileShare.None, Encoding encoding = null, int bufferSize = 4096,
            Boolean leaveOpen = false)
        {
            absolutePath.IoService.WriteText(absolutePath, lines, fileMode, fileAccess, fileShare, encoding, bufferSize,
                leaveOpen);
        }
    }
}