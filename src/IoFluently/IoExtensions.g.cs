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
    /// <summary>
    /// Contains extension methods on AbsolutePath, RelativePath, and IAbsolutePathTranslation that essentially wrap
    /// methods on the object's IoService property. That is, myAbsolutePath.RelativeTo(parameter1) is equivalent to
    /// myAbsolutePath.IoService.RelativeTo(myAbsolutePath, parameter1). This shorthand makes the syntax be fluent
    /// while allowing the IIoService to be dependency injectable.
    /// </summary>
    public static partial class IoExtensions
    {
        public static void SetDefaultRelativePathBase(this AbsolutePath defaultRelativePathBase) {
            defaultRelativePathBase.IoService.SetDefaultRelativePathBase(defaultRelativePathBase);
        }

        public static AbsolutePath Create(this AbsolutePath path, PathType pathType, Boolean createRecursively = false) {
            return path.IoService.Create(path, pathType, createRecursively);
        }

        public static AbsolutePath CreateFolder(this AbsolutePath path, Boolean createRecursively = false) {
            return path.IoService.CreateFolder(path, createRecursively);
        }

        public static AbsolutePath CreateEmptyFile(this AbsolutePath path, Boolean createRecursively = false) {
            return path.IoService.CreateEmptyFile(path, createRecursively);
        }

        public static Stream CreateFile(this AbsolutePath path, Boolean createRecursively = false) {
            return path.IoService.CreateFile(path, createRecursively);
        }

        public static AbsolutePath DeleteFolder(this AbsolutePath path, Boolean recursive = false) {
            return path.IoService.DeleteFolder(path, recursive);
        }

        public static AbsolutePath DeleteFile(this AbsolutePath path) {
            return path.IoService.DeleteFile(path);
        }

        public static AbsolutePath ClearFolder(this AbsolutePath path) {
            return path.IoService.ClearFolder(path);
        }

        public static AbsolutePath Delete(this AbsolutePath path, Boolean recursiveDeleteIfFolder = false) {
            return path.IoService.Delete(path, recursiveDeleteIfFolder);
        }

        public static AbsolutePath EnsureIsFolder(this AbsolutePath path, Boolean createRecursively = false) {
            return path.IoService.EnsureIsFolder(path, createRecursively);
        }

        public static AbsolutePath EnsureIsFile(this AbsolutePath path, Boolean createRecursively = false) {
            return path.IoService.EnsureIsFile(path, createRecursively);
        }

        public static AbsolutePath EnsureIsEmptyFolder(this AbsolutePath path, Boolean recursiveDeleteIfFolder = false, Boolean createRecursively = false) {
            return path.IoService.EnsureIsEmptyFolder(path, recursiveDeleteIfFolder, createRecursively);
        }

        public static AbsolutePath EnsureIsNotFolder(this AbsolutePath path, Boolean recursive = false) {
            return path.IoService.EnsureIsNotFolder(path, recursive);
        }

        public static AbsolutePath EnsureIsNotFile(this AbsolutePath path) {
            return path.IoService.EnsureIsNotFile(path);
        }

        public static AbsolutePath EnsureDoesNotExist(this AbsolutePath path, Boolean recursiveDeleteIfFolder = false) {
            return path.IoService.EnsureDoesNotExist(path, recursiveDeleteIfFolder);
        }

        public static Boolean HasExtension(this AbsolutePath path, string extension) {
            return path.IoService.HasExtension(path, extension);
        }

        public static AbsolutePath Decrypt(this AbsolutePath path) {
            return path.IoService.Decrypt(path);
        }

        public static AbsolutePath Encrypt(this AbsolutePath path) {
            return path.IoService.Encrypt(path);
        }

        public static IAbsolutePathTranslation Copy(this AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination) {
            return pathToBeCopied.IoService.Copy(pathToBeCopied, source, destination);
        }

        public static IAbsolutePathTranslation Copy(this AbsolutePath source, AbsolutePath destination) {
            return source.IoService.Copy(source, destination);
        }

        public static IAbsolutePathTranslation Move(this AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination) {
            return pathToBeCopied.IoService.Move(pathToBeCopied, source, destination);
        }

        public static IAbsolutePathTranslation Move(this AbsolutePath source, AbsolutePath destination) {
            return source.IoService.Move(source, destination);
        }

        public static IAbsolutePathTranslation Translate(this AbsolutePath pathToBeCopied, AbsolutePath source, AbsolutePath destination) {
            return pathToBeCopied.IoService.Translate(pathToBeCopied, source, destination);
        }

        public static IAbsolutePathTranslation Translate(this AbsolutePath source, AbsolutePath destination) {
            return source.IoService.Translate(source, destination);
        }

        public static void RenameTo(this AbsolutePath source, AbsolutePath target) {
            source.IoService.RenameTo(source, target);
        }

        public static IAbsolutePathTranslation Copy(this IAbsolutePathTranslation translation, Boolean overwrite = false) {
            return translation.IoService.Copy(translation, overwrite);
        }

        public static IAbsolutePathTranslation CopyFile(this IAbsolutePathTranslation translation, Boolean overwrite = false) {
            return translation.IoService.CopyFile(translation, overwrite);
        }

        public static IAbsolutePathTranslation CopyFolder(this IAbsolutePathTranslation translation, Boolean overwrite = false) {
            return translation.IoService.CopyFolder(translation, overwrite);
        }

        public static IAbsolutePathTranslation Move(this IAbsolutePathTranslation translation, Boolean overwrite = false) {
            return translation.IoService.Move(translation, overwrite);
        }

        public static IAbsolutePathTranslation MoveFile(this IAbsolutePathTranslation translation, Boolean overwrite = false) {
            return translation.IoService.MoveFile(translation, overwrite);
        }

        public static IAbsolutePathTranslation MoveFolder(this IAbsolutePathTranslation translation, Boolean overwrite = false) {
            return translation.IoService.MoveFolder(translation, overwrite);
        }

        public static IEnumerable<AbsolutePath> EnumerateChildren(this AbsolutePath path, string searchPattern = null, Boolean includeFolders = true, Boolean includeFiles = true) {
            return path.IoService.EnumerateChildren(path, searchPattern, includeFolders, includeFiles);
        }

        public static IEnumerable<AbsolutePath> EnumerateDescendants(this AbsolutePath path, string searchPattern = null, Boolean includeFolders = true, Boolean includeFiles = true) {
            return path.IoService.EnumerateDescendants(path, searchPattern, includeFolders, includeFiles);
        }

        public static RelativePath RelativeTo(this AbsolutePath path, AbsolutePath relativeTo) {
            return path.IoService.RelativeTo(path, relativeTo);
        }

        public static IMaybe<AbsolutePath> TryCommonWith(this AbsolutePath path, AbsolutePath that) {
            return path.IoService.TryCommonWith(path, that);
        }

        public static AbsolutePath CommonWith(this AbsolutePath path, AbsolutePath that) {
            return path.IoService.TryCommonWith(path, that).Value;
        }

        public static AbsolutePath Simplify(this AbsolutePath path) {
            return path.IoService.Simplify(path);
        }

        public static RelativePath Simplify(this RelativePath path) {
            return path.IoService.Simplify(path);
        }

        public static AbsolutePath Combine(this AbsolutePath path, String[] subsequentPathParts) {
            return path.IoService.Combine(path, subsequentPathParts);
        }

        public static AbsolutePaths GlobFiles(this AbsolutePath path, string pattern) {
            return path.IoService.GlobFiles(path, pattern);
        }

        public static IEnumerable<AbsolutePath> Ancestors(this AbsolutePath path, Boolean includeItself) {
            return path.IoService.Ancestors(path, includeItself);
        }

        public static IMaybe<AbsolutePath> TryDescendant(this AbsolutePath path, AbsolutePath[] paths) {
            return path.IoService.TryDescendant(path, paths);
        }

        public static AbsolutePath Descendant(this AbsolutePath path, AbsolutePath[] paths) {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        public static IMaybe<AbsolutePath> TryDescendant(this AbsolutePath path, String[] paths) {
            return path.IoService.TryDescendant(path, paths);
        }

        public static AbsolutePath Descendant(this AbsolutePath path, String[] paths) {
            return path.IoService.TryDescendant(path, paths).Value;
        }

        public static IMaybe<AbsolutePath> TryAncestor(this AbsolutePath path, int level) {
            return path.IoService.TryAncestor(path, level);
        }

        public static AbsolutePath Ancestor(this AbsolutePath path, int level) {
            return path.IoService.TryAncestor(path, level).Value;
        }

        public static Boolean IsAncestorOf(this AbsolutePath path, AbsolutePath possibleDescendant) {
            return path.IoService.IsAncestorOf(path, possibleDescendant);
        }

        public static Boolean IsDescendantOf(this AbsolutePath path, AbsolutePath possibleAncestor) {
            return path.IoService.IsDescendantOf(path, possibleAncestor);
        }

        public static IMaybe<AbsolutePath> TryGetCommonAncestry(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetCommonAncestry(path1, path2);
        }

        public static AbsolutePath GetCommonAncestry(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<Uri> TryGetCommonDescendants(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetCommonDescendants(path1, path2);
        }

        public static Uri GetCommonDescendants(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonDescendants(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetNonCommonDescendants(path1, path2).Value;
        }

        public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2);
        }

        public static Tuple<Uri, Uri> GetNonCommonAncestry(this AbsolutePath path1, AbsolutePath path2) {
            return path1.IoService.TryGetNonCommonAncestry(path1, path2).Value;
        }

        public static IMaybe<AbsolutePath> TryWithExtension(this AbsolutePath path, string differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this AbsolutePath path, string differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension).Value;
        }

        public static IMaybe<AbsolutePath> TryWithExtension(this AbsolutePath path, Func<string, string> differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension);
        }

        public static AbsolutePath WithExtension(this AbsolutePath path, Func<string, string> differentExtension) {
            return path.IoService.TryWithExtension(path, differentExtension).Value;
        }

        public static IEnumerable<string> ReadLines(this AbsolutePath path) {
            return path.IoService.ReadLines(path);
        }

        public static string ReadAllText(this AbsolutePath path) {
            return path.IoService.ReadAllText(path);
        }

        public static IEnumerable<string> ReadLines(this AbsolutePath path, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read, Encoding encoding = null, Boolean detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, Boolean leaveOpen = false) {
            return path.IoService.ReadLines(path, fileMode, fileAccess, fileShare, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static IMaybe<string> TryReadText(this AbsolutePath path, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read, Encoding encoding = null, Boolean detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, Boolean leaveOpen = false) {
            return path.IoService.TryReadText(path, fileMode, fileAccess, fileShare, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static string ReadText(this AbsolutePath path, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read, Encoding encoding = null, Boolean detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, Boolean leaveOpen = false) {
            return path.IoService.TryReadText(path, fileMode, fileAccess, fileShare, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen).Value;
        }

        public static IMaybe<StreamReader> TryOpenReader(this AbsolutePath path) {
            return path.IoService.TryOpenReader(path);
        }

        public static StreamReader OpenReader(this AbsolutePath path) {
            return path.IoService.TryOpenReader(path).Value;
        }

        public static void WriteAllText(this AbsolutePath path, string text, Boolean createRecursively = false) {
            path.IoService.WriteAllText(path, text, createRecursively);
        }

        public static void WriteAllLines(this AbsolutePath path, IEnumerable<string> lines, Boolean createRecursively = false) {
            path.IoService.WriteAllLines(path, lines, createRecursively);
        }

        public static void WriteAllBytes(this AbsolutePath path, Byte[] bytes, Boolean createRecursively = false) {
            path.IoService.WriteAllBytes(path, bytes, createRecursively);
        }

        public static IMaybe<StreamWriter> TryOpenWriter(this AbsolutePath absolutePath, Boolean createRecursively = false) {
            return absolutePath.IoService.TryOpenWriter(absolutePath, createRecursively);
        }

        public static StreamWriter OpenWriter(this AbsolutePath absolutePath, Boolean createRecursively = false) {
            return absolutePath.IoService.TryOpenWriter(absolutePath, createRecursively).Value;
        }

        public static void WriteText(this AbsolutePath absolutePath, IEnumerable<string> lines, FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None, Encoding encoding = null, int bufferSize = 4096, Boolean leaveOpen = false, Boolean createRecursively = false) {
            absolutePath.IoService.WriteText(absolutePath, lines, fileMode, fileAccess, fileShare, encoding, bufferSize, leaveOpen, createRecursively);
        }

        public static void WriteText(this AbsolutePath absolutePath, string text, FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None, Encoding encoding = null, int bufferSize = 4096, Boolean leaveOpen = false, Boolean createRecursively = false) {
            absolutePath.IoService.WriteText(absolutePath, text, fileMode, fileAccess, fileShare, encoding, bufferSize, leaveOpen, createRecursively);
        }

        public static IMaybe<Stream> TryOpen(this AbsolutePath path, FileMode fileMode, Boolean createRecursively = false) {
            return path.IoService.TryOpen(path, fileMode, createRecursively);
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode, Boolean createRecursively = false) {
            return path.IoService.TryOpen(path, fileMode, createRecursively).Value;
        }

        public static IMaybe<Stream> TryOpen(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess, Boolean createRecursively = false) {
            return path.IoService.TryOpen(path, fileMode, fileAccess, createRecursively);
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess, Boolean createRecursively = false) {
            return path.IoService.TryOpen(path, fileMode, fileAccess, createRecursively).Value;
        }

        public static IMaybe<Stream> TryOpen(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, Boolean createRecursively = false) {
            return path.IoService.TryOpen(path, fileMode, fileAccess, fileShare, createRecursively);
        }

        public static Stream Open(this AbsolutePath path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, Boolean createRecursively = false) {
            return path.IoService.TryOpen(path, fileMode, fileAccess, fileShare, createRecursively).Value;
        }

        public static ISetChanges<AbsolutePath> ToLiveLinq(this AbsolutePath path, Boolean includeFileContentChanges, Boolean includeSubFolders, string pattern) {
            return path.IoService.ToLiveLinq(path, includeFileContentChanges, includeSubFolders, pattern);
        }

        public static IObservable<Unit> ObserveChanges(this AbsolutePath path) {
            return path.IoService.ObserveChanges(path);
        }

        public static IObservable<Unit> ObserveChanges(this AbsolutePath path, NotifyFilters filters) {
            return path.IoService.ObserveChanges(path, filters);
        }

        public static IObservable<PathType> ObservePathType(this AbsolutePath path) {
            return path.IoService.ObservePathType(path);
        }

        public static IObservable<AbsolutePath> Renamings(this AbsolutePath path) {
            return path.IoService.Renamings(path);
        }

    }
}
