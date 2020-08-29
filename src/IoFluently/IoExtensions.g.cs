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
public static ISetChanges<AbsolutePath> ToLiveLinq(this  AbsolutePath path,  Boolean includeFileContentChanges,  Boolean includeSubFolders,  string pattern) {
return path.IoService.ToLiveLinq(path, includeFileContentChanges, includeSubFolders, pattern);
}

public static AbsolutePath CreateEmptyFile(this  AbsolutePath path) {
return path.IoService.CreateEmptyFile(path);
}

public static Stream CreateFile(this  AbsolutePath path) {
return path.IoService.CreateFile(path);
}

public static AbsolutePath DeleteFile(this  AbsolutePath path) {
return path.IoService.DeleteFile(path);
}

public static AbsolutePath ClearFolder(this  AbsolutePath path) {
return path.IoService.ClearFolder(path);
}

public static AbsolutePath Decrypt(this  AbsolutePath path) {
return path.IoService.Decrypt(path);
}

public static AbsolutePath Encrypt(this  AbsolutePath path) {
return path.IoService.Encrypt(path);
}

public static AbsolutePath Delete(this  AbsolutePath path,  Boolean recursiveDeleteIfFolder = false) {
return path.IoService.Delete(path, recursiveDeleteIfFolder);
}

public static Boolean IsAncestorOf(this  AbsolutePath path,  AbsolutePath possibleDescendant) {
return path.IoService.IsAncestorOf(path, possibleDescendant);
}

public static Boolean IsDescendantOf(this  AbsolutePath path,  AbsolutePath possibleAncestor) {
return path.IoService.IsDescendantOf(path, possibleAncestor);
}

public static IEnumerable<string> Split(this  AbsolutePath path) {
return path.IoService.Split(path);
}

public static IEnumerable<AbsolutePath> Ancestors(this  AbsolutePath path,  Boolean includeItself = false) {
return path.IoService.Ancestors(path, includeItself);
}

public static IMaybe<AbsolutePath> TryDescendant(this  AbsolutePath path,  AbsolutePath[] paths) {
return path.IoService.TryDescendant(path, paths);
}

public static IMaybe<AbsolutePath> TryDescendant(this  AbsolutePath path,  String[] paths) {
return path.IoService.TryDescendant(path, paths);
}

public static IMaybe<AbsolutePath> TryAncestor(this  AbsolutePath path,  int level) {
return path.IoService.TryAncestor(path, level);
}

public static Boolean HasExtension(this  AbsolutePath path,  string extension) {
return path.IoService.HasExtension(path, extension);
}

public static IMaybe<AbsolutePath> TryWithExtension(this  AbsolutePath path,  string differentExtension) {
return path.IoService.TryWithExtension(path, differentExtension);
}

public static Boolean ContainsFiles(this  AbsolutePath path) {
return path.IoService.ContainsFiles(path);
}

public static Boolean FolderContainsFiles(this  AbsolutePath path) {
return path.IoService.FolderContainsFiles(path);
}

public static IMaybe<AbsolutePath> TryGetCommonAncestry(this  AbsolutePath path1,  AbsolutePath path2) {
return path1.IoService.TryGetCommonAncestry(path1, path2);
}

public static IMaybe<Uri> TryGetCommonDescendants(this  AbsolutePath path1,  AbsolutePath path2) {
return path1.IoService.TryGetCommonDescendants(path1, path2);
}

public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonDescendants(this  AbsolutePath path1,  AbsolutePath path2) {
return path1.IoService.TryGetNonCommonDescendants(path1, path2);
}

public static IMaybe<Tuple<Uri, Uri>> TryGetNonCommonAncestry(this  AbsolutePath path1,  AbsolutePath path2) {
return path1.IoService.TryGetNonCommonAncestry(path1, path2);
}

public static IAbsolutePathTranslation Translate(this  AbsolutePath pathToBeCopied,  AbsolutePath source,  AbsolutePath destination) {
return pathToBeCopied.IoService.Translate(pathToBeCopied, source, destination);
}

public static IAbsolutePathTranslation Translate(this  AbsolutePath source,  AbsolutePath destination) {
return source.IoService.Translate(source, destination);
}

public static IMaybe<Boolean> TryIsReadOnly(this  AbsolutePath path) {
return path.IoService.TryIsReadOnly(path);
}

public static IMaybe<Information> TryFileSize(this  AbsolutePath path) {
return path.IoService.TryFileSize(path);
}

public static IMaybe<FileAttributes> TryAttributes(this  AbsolutePath attributes) {
return attributes.IoService.TryAttributes(attributes);
}

public static IMaybe<DateTimeOffset> TryCreationTime(this  AbsolutePath attributes) {
return attributes.IoService.TryCreationTime(attributes);
}

public static IMaybe<DateTimeOffset> TryLastAccessTime(this  AbsolutePath attributes) {
return attributes.IoService.TryLastAccessTime(attributes);
}

public static IMaybe<DateTimeOffset> TryLastWriteTime(this  AbsolutePath attributes) {
return attributes.IoService.TryLastWriteTime(attributes);
}

public static IMaybe<string> TryFullName(this  AbsolutePath attributes) {
return attributes.IoService.TryFullName(attributes);
}

public static AbsolutePath Root(this  AbsolutePath path) {
return path.IoService.Root(path);
}

public static void RenameTo(this  AbsolutePath source,  AbsolutePath target) {
source.IoService.RenameTo(source, target);
}

public static Boolean Exists(this  AbsolutePath path) {
return path.IoService.Exists(path);
}

public static PathType GetPathType(this  AbsolutePath path) {
return path.IoService.GetPathType(path);
}

public static AbsolutePath DeleteFolder(this  AbsolutePath path,  Boolean recursive = false) {
return path.IoService.DeleteFolder(path, recursive);
}

public static AbsolutePath Create(this  AbsolutePath path,  PathType pathType) {
return path.IoService.Create(path, pathType);
}

public static IMaybe<Stream> TryOpen(this  AbsolutePath path,  FileMode fileMode) {
return path.IoService.TryOpen(path, fileMode);
}

public static IMaybe<Stream> TryOpen(this  AbsolutePath path,  FileMode fileMode,  FileAccess fileAccess) {
return path.IoService.TryOpen(path, fileMode, fileAccess);
}

public static IMaybe<Stream> TryOpen(this  AbsolutePath path,  FileMode fileMode,  FileAccess fileAccess,  FileShare fileShare) {
return path.IoService.TryOpen(path, fileMode, fileAccess, fileShare);
}

public static AbsolutePath CreateFolder(this  AbsolutePath path) {
return path.IoService.CreateFolder(path);
}

public static void WriteAllText(this  AbsolutePath path,  string text) {
path.IoService.WriteAllText(path, text);
}

public static void WriteAllLines(this  AbsolutePath path,  IEnumerable<string> lines) {
path.IoService.WriteAllLines(path, lines);
}

public static void WriteAllBytes(this  AbsolutePath path,  Byte[] bytes) {
path.IoService.WriteAllBytes(path, bytes);
}

public static IEnumerable<string> ReadLines(this  AbsolutePath path) {
return path.IoService.ReadLines(path);
}

public static string ReadAllText(this  AbsolutePath path) {
return path.IoService.ReadAllText(path);
}

public static IObservable<Unit> ObserveChanges(this  AbsolutePath path) {
return path.IoService.ObserveChanges(path);
}

public static IObservable<Unit> ObserveChanges(this  AbsolutePath path,  NotifyFilters filters) {
return path.IoService.ObserveChanges(path, filters);
}

public static IObservable<PathType> ObservePathType(this  AbsolutePath path) {
return path.IoService.ObservePathType(path);
}

public static IObservable<AbsolutePath> Renamings(this  AbsolutePath path) {
return path.IoService.Renamings(path);
}

public static RelativePath RelativeTo(this  AbsolutePath path,  AbsolutePath relativeTo) {
return path.IoService.RelativeTo(path, relativeTo);
}

public static IMaybe<AbsolutePath> TryCommonWith(this  AbsolutePath path,  AbsolutePath that) {
return path.IoService.TryCommonWith(path, that);
}

public static Boolean CanBeSimplified(this  AbsolutePath path) {
return path.IoService.CanBeSimplified(path);
}

public static AbsolutePath Simplify(this  AbsolutePath path) {
return path.IoService.Simplify(path);
}

public static RelativePath Simplify(this  RelativePath path) {
return path.IoService.Simplify(path);
}

public static IMaybe<AbsolutePath> TryParent(this  AbsolutePath path) {
return path.IoService.TryParent(path);
}

public static IMaybe<StreamWriter> TryOpenWriter(this  AbsolutePath absolutePath) {
return absolutePath.IoService.TryOpenWriter(absolutePath);
}

public static IEnumerable<string> ReadLines(this  AbsolutePath absolutePath,  FileMode fileMode = FileMode.Open,  FileAccess fileAccess = FileAccess.Read,  FileShare fileShare = FileShare.Read,  Encoding encoding = null,  Boolean detectEncodingFromByteOrderMarks = true,  int bufferSize = 4096,  Boolean leaveOpen = false) {
return absolutePath.IoService.ReadLines(absolutePath, fileMode, fileAccess, fileShare, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
}

public static IMaybe<string> TryReadText(this  AbsolutePath absolutePath,  FileMode fileMode = FileMode.Open,  FileAccess fileAccess = FileAccess.Read,  FileShare fileShare = FileShare.Read,  Encoding encoding = null,  Boolean detectEncodingFromByteOrderMarks = true,  int bufferSize = 4096,  Boolean leaveOpen = false) {
return absolutePath.IoService.TryReadText(absolutePath, fileMode, fileAccess, fileShare, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
}

public static void WriteText(this  AbsolutePath absolutePath,  IEnumerable<string> lines,  FileMode fileMode = FileMode.Create,  FileAccess fileAccess = FileAccess.Write,  FileShare fileShare = FileShare.None,  Encoding encoding = null,  int bufferSize = 4096,  Boolean leaveOpen = false) {
absolutePath.IoService.WriteText(absolutePath, lines, fileMode, fileAccess, fileShare, encoding, bufferSize, leaveOpen);
}

public static void WriteText(this  AbsolutePath absolutePath,  string text,  FileMode fileMode = FileMode.Create,  FileAccess fileAccess = FileAccess.Write,  FileShare fileShare = FileShare.None,  Encoding encoding = null,  int bufferSize = 4096,  Boolean leaveOpen = false) {
absolutePath.IoService.WriteText(absolutePath, text, fileMode, fileAccess, fileShare, encoding, bufferSize, leaveOpen);
}

    }
}
