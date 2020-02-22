using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MoreCollections;
using UtilityDisposables;
using LiveLinq.Set;
using System.Text.RegularExpressions;
using SimpleMonads;
using static SimpleMonads.Utility;

namespace MoreIO
{
    public class IoService : IIoService
    {
        #region File and folder extension methods

        public IEnumerable<PathSpec> GetChildren(PathSpec path, bool includeFolders = true, bool includeFiles = true)
        {
            if (includeFiles && includeFolders)
            {
                return Directory.GetFileSystemEntries(path.AsDirectoryInfo().FullName).Select(x => ParsePathSpec(x));
            }
            if (includeFiles)
            {
                return Directory.GetFiles(path.AsDirectoryInfo().FullName).Select(x => ParsePathSpec(x));
            }
            if (includeFolders)
            {
                return Directory.GetDirectories(path.AsDirectoryInfo().FullName).Select(x => ParsePathSpec(x));
            }
            return ImmutableArray<PathSpec>.Empty;
        }

        public IEnumerable<PathSpec> GetFiles(PathSpec path)
        {
            return GetChildren(path, false, true);
        }
        
        public IEnumerable<PathSpec> GetFolders(PathSpec path)
        {
            return GetChildren(path, true, false);
        }

        public PathSpec CreateEmptyFile(PathSpec path)
        {
            path.CreateFile().Dispose();
            if (path.GetPathType() != PathType.File)
                throw new IOException("Could not create file " + path);
            return path;
        }

        public FileStream CreateFile(PathSpec path)
        {
            if (path.Parent().Value.GetPathType() != PathType.Folder)
                path.Parent().Value.Create(PathType.Folder);
            var result = path.AsFileInfo().Create();
            if (path.GetPathType() != PathType.File)
                throw new IOException("Could not create file " + path);
            return result;
        }

        public PathSpec DeleteFile(PathSpec path)
        {
            if (path.GetPathType() == PathType.None)
                return path;
            try
            {
                path.AsFileInfo().Delete();
            }
            catch (IOException)
            {
                path.AsFileInfo().Delete();
            }
            catch (UnauthorizedAccessException)
            {
                path.AsFileInfo().Delete();
            }
            return path;
        }

        public PathSpec Decrypt(PathSpec path)
        {
            path.AsFileInfo().Decrypt();
            return path;
        }

        public PathSpec Encrypt(PathSpec path)
        {
            path.AsFileInfo().Encrypt();
            return path;
        }

        public PathSpec Delete(PathSpec path)
        {
            if (path.GetPathType() == PathType.File)
            {
                return path.DeleteFile();
            }
            if (path.GetPathType() == PathType.Folder)
            {
                return path.DeleteFolder(true);
            }
            return path;
        }

        public string SurroundWithDoubleQuotesIfNecessary(string str)
        {
            if (str.Contains(" "))
            {
                if (!str.StartsWith("\""))
                    str = "\"" + str;
                if (!str.EndsWith("\""))
                    str = str + "\"";
            }
            return str;
        }

        public bool IsAncestorOf(PathSpec path, PathSpec possibleDescendant)
        {
            return IsDescendantOf(possibleDescendant, path);
        }

        public bool IsDescendantOf(PathSpec path, PathSpec possibleAncestor)
        {
            var possibleDescendantStr = Path.GetFullPath(path.ToString()).ToLower();
            var possibleAncestorStr = Path.GetFullPath(possibleAncestor.ToString()).ToLower();
            return possibleDescendantStr.StartsWith(possibleAncestorStr);
        }

        public IEnumerable<string> Split(PathSpec path)
        {
            return Ancestors(path, true).Select(pathName => Path.GetFileName(pathName.ToString())).Reverse();
        }

        public string LastPathComponent(PathSpec path)
        {
            return path.ToString().Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Last(str => !String.IsNullOrEmpty(str));
        }

        /// <summary>
        /// Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from). For example, the ancestors of the path C:\Users\myusername\Documents would be these, in order:
        /// C:\Users\myusername
        /// C:\Users
        /// C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        public IEnumerable<PathSpec> Ancestors(PathSpec path, bool includeItself = false)
        {
            if (includeItself)
                yield return path;
            while (true)
            {
                var maybePath = path.Parent();
                if (maybePath.HasValue)
                {
                    yield return maybePath.Value;
                    path = maybePath.Value;
                }
                else
                    break;
            }
        }

        public IMaybe<PathSpec> Descendant(PathSpec path, params PathSpec[] paths)
        {
            return path.Descendant(paths.Select(p => p.ToString()).ToArray());
        }

        public IMaybe<PathSpec> Descendant(PathSpec path, params string[] paths)
        {
            var pathStr = path.ToString();
            // Make sure that pathStr is treated as a directory.
            if (!pathStr.EndsWith(@"\"))
                pathStr += path.DirectorySeparator;

            return ToPath(Path.Combine(pathStr, Path.Combine(paths)));
        }

        public IMaybe<PathSpec> Ancestor(PathSpec path, int level)
        {
            IMaybe<PathSpec> maybePath = path.ToMaybe();
            for (int i = 0; i < level; i++)
            {
                maybePath = maybePath.Select(p => p.Parent()).SelectMany(x => x);
                if (!maybePath.HasValue)
                    return Maybe<PathSpec>.Nothing;
            }
            return maybePath;
        }

        public bool HasExtension(PathSpec path, string extension)
        {
            var actualExtension = Path.GetExtension(path.ToString());
            if (actualExtension == extension)
                return true;
            if (actualExtension == null)
                return false;
            return actualExtension.Equals(extension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        public IMaybe<PathSpec> WithExtension(PathSpec path, string differentExtension)
        {
            return ToPath(Path.ChangeExtension(path.ToString(), differentExtension));
        }

        public IPathSpecTranslation Copy(IPathSpecTranslation translation)
        {
            switch (translation.Source.GetPathType())
            {
                case PathType.File:
                    translation.CopyFile();
                    break;
                case PathType.Folder:
                    translation.CopyFolder();
                    break;
                case PathType.None:
                    throw new IOException(
                        String.Format("An attempt was made to copy \"{0}\" to \"{1}\", but the source path doesn't exist.",
                                      translation.Source, translation.Destination));
            }
            return translation;
        }

        public IPathSpecTranslation CopyFile(IPathSpecTranslation translation)
        {
            if (translation.Source.GetPathType() != PathType.File)
                throw new IOException(String.Format("An attempt was made to copy a file from \"{0}\" to \"{1}\" but the source path is not a file.",
                    translation.Source, translation.Destination));
            if (translation.Destination.GetPathType() != PathType.None)
                throw new IOException(String.Format("An attempt was made to copy \"{0}\" to \"{1}\" but the destination path exists.",
                    translation.Source, translation.Destination));
            translation.Destination.Parent().Value.Create(PathType.Folder);
            File.Copy(translation.Source.ToString(), translation.Destination.ToString());
            return translation;
        }

        public IPathSpecTranslation CopyFolder(IPathSpecTranslation translation)
        {
            if (translation.Source.GetPathType() != PathType.Folder)
                throw new IOException(String.Format("An attempt was made to copy a folder from \"{0}\" to \"{1}\" but the source path is not a folder.",
                    translation.Source, translation.Destination));
            translation.Destination.Create(PathType.Folder);
            return translation;
        }

        public IPathSpecTranslation Move(IPathSpecTranslation translation)
        {
            switch (translation.Source.GetPathType())
            {
                case PathType.File:
                    translation.MoveFile();
                    break;
                case PathType.Folder:
                    translation.MoveFolder();
                    break;
                case PathType.None:
                    throw new IOException(
                        String.Format("An attempt was made to move \"{0}\" to \"{1}\", but the source path doesn't exist.",
                                      translation.Source, translation.Destination));
            }
            return translation;
        }

        public IPathSpecTranslation MoveFile(IPathSpecTranslation translation)
        {
            if (translation.Source.GetPathType() != PathType.File)
                throw new IOException(String.Format("An attempt was made to move a file from \"{0}\" to \"{1}\" but the source path is not a file.",
                    translation.Source, translation.Destination));
            if (translation.Destination.GetPathType() != PathType.None)
                throw new IOException(String.Format("An attempt was made to move \"{0}\" to \"{1}\" but the destination path exists.",
                    translation.Source, translation.Destination));
            if (translation.Destination.IsDescendantOf(translation.Source))
                throw new IOException(String.Format("An attempt was made to move a file from \"{0}\" to \"{1}\" but the destination path is a sub-path of the source path.",
                    translation.Source, translation.Destination));
            translation.Destination.Parent().Value.Create(PathType.Folder);
            File.Move(translation.Source.ToString(), translation.Destination.ToString());
            return translation;
        }
        
        public IPathSpecTranslation MoveFolder(IPathSpecTranslation translation)
        {
            if (translation.Source.GetPathType() != PathType.Folder)
                throw new IOException(String.Format("An attempt was made to move a folder from \"{0}\" to \"{1}\" but the source path is not a folder.",
                    translation.Source, translation.Destination));
            if (translation.Destination.GetPathType() == PathType.File)
                throw new IOException(String.Format("An attempt was made to move \"{0}\" to \"{1}\" but the destination path is a file.",
                    translation.Source, translation.Destination));
            if (translation.Destination.IsDescendantOf(translation.Source))
                throw new IOException(String.Format("An attempt was made to move a file from \"{0}\" to \"{1}\" but the destination path is a sub-path of the source path.",
                    translation.Source, translation.Destination));
            if (translation.Source.Children().Any())
                throw new IOException(String.Format("An attempt was made to move the non-empty folder \"{0}\". This is not allowed because all the files should be moved first, and only then can the folder be moved, because the move operation deletes the source folder, which would of course also delete the files and folders within the source folder.",
                    translation.Source));
            translation.Destination.Create(PathType.Folder);
            if (!translation.Source.Children().Any())
                translation.Source.DeleteFolder(false);
            return translation;
        }

        public bool ContainsFiles(PathSpec path)
        {
            if (path.GetPathType() == PathType.File)
                return true;
            return path.Children().All(child => child.ContainsFiles());
        }

        public bool FolderContainsFiles(PathSpec path)
        {
            if (path.GetPathType() == PathType.File)
                return false;
            return path.ContainsFiles();
        }

        public IMaybe<PathSpec> GetCommonAncestry(PathSpec path1, PathSpec path2)
        {
            return ToPath(path1.ToString().GetCommonBeginning(path2.ToString()).Trim('\\'));
        }

        public IMaybe<Uri> GetCommonDescendants(PathSpec path1, PathSpec path2)
        {
            try
            {
                return new Maybe<Uri>(new Uri(path1.ToString().GetCommonEnding(path2.ToString()).Trim('\\'), UriKind.Relative));
            }
            catch (ArgumentNullException)
            {
                return Maybe<Uri>.Nothing;
            }
            catch (UriFormatException)
            {
                return Maybe<Uri>.Nothing;
            }
        }

        public IMaybe<Tuple<Uri, Uri>> GetNonCommonDescendants(PathSpec path1, PathSpec path2)
        {
            try
            {
                var commonAncestry = path1.ToString().GetCommonBeginning(path2.ToString()).Trim('\\');
                return new Maybe<Tuple<Uri, Uri>>(new Tuple<Uri, Uri>(new Uri(path1.ToString().Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative),
                                                 new Uri(path2.ToString().Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative)));
            }
            catch (ArgumentNullException)
            {
                return Maybe<Tuple<Uri, Uri>>.Nothing;
            }
            catch (UriFormatException)
            {
                return Maybe<Tuple<Uri, Uri>>.Nothing;
            }
        }

        public IMaybe<Tuple<Uri, Uri>> GetNonCommonAncestry(PathSpec path1, PathSpec path2)
        {
            try
            {
                var commonDescendants = path1.ToString().GetCommonEnding(path2.ToString()).Trim('\\');
                return new Maybe<Tuple<Uri, Uri>>(new Tuple<Uri, Uri>(new Uri(path1.ToString().Substring(0, path1.ToString().Length - commonDescendants.Length).Trim('\\')),
                                                 new Uri(path2.ToString().Substring(0, path2.ToString().Length - commonDescendants.Length).Trim('\\'))));
            }
            catch (ArgumentNullException)
            {
                return Maybe<Tuple<Uri, Uri>>.Nothing;
            }
            catch (UriFormatException)
            {
                return Maybe<Tuple<Uri, Uri>>.Nothing;
            }
        }

        public IPathSpecTranslation Translate(PathSpec pathToBeCopied, PathSpec source, PathSpec destination)
        {
            return new CalculatedPathSpecTranslation(pathToBeCopied, source, destination, this);
        }

        public IPathSpecTranslation Translate(PathSpec source, PathSpec destination)
        {
            return new PathSpecTranslation(source, destination, this);
        }

        public Uri Child(Uri parent, Uri child)
        {
            var parentLocalPath = parent.ToString();
            if (!parentLocalPath.EndsWith(Path.DirectorySeparatorChar.ToString())
                && !parentLocalPath.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                parentLocalPath += Path.DirectorySeparatorChar;
            return new Uri(parentLocalPath).MakeRelativeUri(child);
        }

        public readonly List<string> VideoFileExtensions = new List<string>
            {
                    "3gpp",
                    "3g2",
                    "3gp",
                    "3gp2",
                    "asf",
                    "mov",
                    "avi",
                    "mts",
                    "m2ts",
                    "m2t",
                    "m4v",
                    "m3u",
                    "asx",
                    "mpe",
                    "mpeg",
                    "mpg",
                    "m1v",
                    "m2v",
                    "mp2v",
                    "mpv2",
                    "mp4",
                    "mp4v",
                    "ts",
                    "wm",
                    "wpl",
                    "wmx",
                    "wmv",
                    "wvx"
                };

        public readonly List<string> ImageFileExtensions = new List<string>
            {
                    "ani",
                    "b3d",
                    "bmp",
                    "dib",
                    "cam",
                    "clp",
                    "crw",
                    "cr2",
                    "cur",
                    "dcm",
                    "acr",
                    "ima",
                    "dcx",
                    "dds",
                    "djvu",
                    "iw44",
                    "dxf",
                    "ecw",
                    "emf",
                    "eps",
                    "ps",
                    "exr",
                    "fpx",
                    "g3",
                    "gif",
                    "hdp",
                    "jxr",
                    "wdp",
                    "icl",
                    "ico",
                    "iff",
                    "lbm",
                    "img",
                    "jls",
                    "jp2",
                    "jpc",
                    "j2k",
                    "jpf",
                    "jpg",
                    "jpeg",
                    "jpe",
                    "jpm",
                    "kdc",
                    "mng",
                    "jng",
                    "pbm",
                    "pcd",
                    "pcx",
                    "pgm",
                    "png",
                    "ppm",
                    "psd",
                    "psp",
                    "ras",
                    "sun",
                    "raw",
                    "rle",
                    "sff",
                    "sfw",
                    "sgi",
                    "rgb",
                    "sid",
                    "tga",
                    "tif",
                    "tiff",
                    "wbmp",
                    "webp",
                    "wmf",
                    "xbm",
                    "xpm"
                };

        public FileInfo AsFileInfo(PathSpec path)
        {
            return new FileInfo(path.ToString());
        }

        public DirectoryInfo AsDirectoryInfo(PathSpec path)
        {
            return new DirectoryInfo(path.ToString());
        }

        public IMaybe<T> As<T>(T pathName, PathType pathType)
            where T : PathSpec
        {
            if (pathName.GetPathType() == pathType)
                return new Maybe<T>(pathName);
            return Maybe<T>.Nothing;
        }

        public IMaybe<bool> IsReadOnly(PathSpec path)
        {
            try
            {
                return new Maybe<bool>(path.AsFileInfo().IsReadOnly);
            }
            catch (IOException)
            {
                return Maybe<bool>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<bool>.Nothing;
            }
            catch (ArgumentException)
            {
                return Maybe<bool>.Nothing;
            }
        }

        public IMaybe<long> Length(PathSpec path)
        {
            try
            {
                return new Maybe<long>(path.AsFileInfo().Length);
            }
            catch (IOException)
            {
                return Maybe<long>.Nothing;
            }
        }

        public IMaybe<FileAttributes> Attributes(PathSpec attributes)
        {
            try
            {
                return new Maybe<FileAttributes>(attributes.AsFileInfo().Attributes);
            }
            catch (IOException)
            {
                return Maybe<FileAttributes>.Nothing;
            }
            catch (SecurityException)
            {
                return Maybe<FileAttributes>.Nothing;
            }
            catch (ArgumentException)
            {
                return Maybe<FileAttributes>.Nothing;
            }
        }

        public IMaybe<DateTime> CreationTime(PathSpec attributes)
        {
            try
            {
                return new Maybe<DateTime>(attributes.AsFileInfo().CreationTime);
            }
            catch (IOException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTime>.Nothing;
            }
        }

        public IMaybe<DateTime> LastAccessTime(PathSpec attributes)
        {
            try
            {
                return new Maybe<DateTime>(attributes.AsFileInfo().LastAccessTime);
            }
            catch (IOException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTime>.Nothing;
            }
        }

        public IMaybe<DateTime> LastWriteTime(PathSpec attributes)
        {
            try
            {
                return new Maybe<DateTime>(attributes.AsFileInfo().LastWriteTime);
            }
            catch (IOException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTime>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTime>.Nothing;
            }
        }

        public IMaybe<string> FullName(PathSpec attributes)
        {
            try
            {
                return new Maybe<string>(attributes.AsFileInfo().FullName);
            }
            catch (SecurityException)
            {
                return Maybe<string>.Nothing;
            }
            catch (PathTooLongException)
            {
                return Maybe<string>.Nothing;
            }
        }

        /// <summary>
        /// Includes the period character ".". For example, function would return ".exe" if the file pointed to a file named was "test.exe".
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public IMaybe<string> Extension(string pathName)
        {
            var result = Path.GetExtension(pathName);
            if (String.IsNullOrEmpty(result))
                return Maybe<string>.Nothing;
            return new Maybe<string>(result);
        }

        public bool IsImageUri(Uri uri)
        {
            if (uri == null)
                return false;
            string str = uri.ToString();
            if (!str.Contains("."))
                return false;
            string extension = str.Substring(str.LastIndexOf('.') + 1);
            return ImageFileExtensions.Any(curExtension => extension == curExtension);
        }

        public bool IsVideoUri(Uri uri)
        {
            if (uri == null)
                return false;
            string str = uri.ToString();
            if (!str.Contains("."))
                return false;
            string extension = str.Substring(str.LastIndexOf('.') + 1);
            return VideoFileExtensions.Any(curExtension => extension == curExtension);
        }

        public string StripQuotes(string str)
        {
            if (str.StartsWith("\"") && str.EndsWith("\""))
                return str.Substring(1, str.Length - 2);
            if (str.StartsWith("'") && str.EndsWith("'"))
                return str.Substring(1, str.Length - 2);
            return str;
        }


        public PathSpec Root(PathSpec path)
        {
            var ancestor = path;
            IMaybe<PathSpec> cachedParent;
            while ((cachedParent = ancestor.Parent()).HasValue)
            {
                ancestor = cachedParent.Value;
            }
            return ancestor;
        }

        public void RenameTo(PathSpec source, PathSpec target)
        {
            switch (source.GetPathType())
            {
                case PathType.File:
                    File.Move(source.ToString(), target.ToString());
                    break;
                case PathType.Folder:
                    Directory.Move(source.ToString(), target.ToString());
                    break;
            }
        }

        public bool Exists(PathSpec path)
        {
            return path.GetPathType() != PathType.None;
        }

        public PathType GetPathType(PathSpec path)
        {
            var str = path.ToString();
            if (File.Exists(str))
                return PathType.File;
            if (Directory.Exists(str))
                return PathType.Folder;
            return PathType.None;
        }

        public PathSpec ClearFolder(PathSpec path)
        {
            foreach (var item in path)
            {
                item.Delete();
            }

            return path;
        }

        public PathSpec DeleteFolder(PathSpec path, bool recursive = false)
        {
            Directory.Delete(path.ToString(), recursive);

            return path;
        }

        public bool MayCreateFile(FileMode fileMode)
        {
            return fileMode.HasFlag(FileMode.Append) || fileMode.HasFlag(FileMode.Create) ||
                   fileMode.HasFlag(FileMode.CreateNew) || fileMode.HasFlag(FileMode.OpenOrCreate);
        }
        
        public void Create(PathSpec path, PathType pathType)
        {
            switch (pathType)
            {
                case PathType.Folder:
                    Directory.CreateDirectory(path.ToString());
                    break;
                case PathType.File:
                    File.WriteAllText(path.ToString(), string.Empty);
                    break;
                default:
                    throw new ArgumentException(nameof(pathType));
            }
        }

        public IMaybe<FileStream> Open(PathSpec path, FileMode fileMode)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.Parent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(path.AsFileInfo().Open(fileMode));
            }
            catch (IOException)
            {
                return Maybe<FileStream>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<FileStream>.Nothing;
            }
        }

        public IMaybe<FileStream> Open(PathSpec path, FileMode fileMode,
                                                              FileAccess fileAccess)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.Parent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(path.AsFileInfo().Open(fileMode, fileAccess));
            }
            catch (IOException)
            {
                return Maybe<FileStream>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<FileStream>.Nothing;
            }
        }

        public IMaybe<FileStream> Open(PathSpec path, FileMode fileMode,
                                                              FileAccess fileAccess, FileShare fileShare)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.Parent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(path.AsFileInfo().Open(fileMode, fileAccess, fileShare));
            }
            catch (IOException)
            {
                return Maybe<FileStream>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<FileStream>.Nothing;
            }
        }

        public PathSpec CreateFolder(PathSpec path)
        {
            try
            {
                if (path.GetPathType() == PathType.Folder)
                    return path;
                path.Parent().IfHasValue(parent => parent.CreateFolder());
                path.AsDirectoryInfo().Create();
            }
            catch (IOException)
            {
                if (path.GetPathType() != PathType.Folder)
                    throw;
            }
            if (path.GetPathType() != PathType.Folder)
                throw new IOException("Failed to create folder " + path);
            return path;
        }

        public void WriteAllText(PathSpec path, string text)
        {
            File.WriteAllText(path.ToString(), text);
        }

        public void WriteAllLines(PathSpec path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path.ToString(), lines);
        }

        public void WriteAllLines(PathSpec path, byte[] bytes)
        {
            File.WriteAllBytes(path.ToString(), bytes);
        }

        public IEnumerable<string> ReadLines(PathSpec path)
        {
            return File.ReadLines(path.ToString());
        }

        public string ReadAllText(PathSpec path)
        {
            return File.ReadAllText(path.ToString());
        }

        #endregion

        public PathSpec ToAbsolute(PathSpec path)
        {
            if (path.IsRelative())
                return CurrentDirectory.Join(path).Value;
            return path;
        }

        public IReadOnlySet<PathSpec> Children(PathSpec path)
        {
            return path.Children("*");
        }

        public IReadOnlySet<PathSpec> Children(PathSpec path, string pattern)
        {
            return new PathSpecDescendants(path, pattern, false, this);
        }

        public IReadOnlySet<PathSpec> Descendants(PathSpec path)
        {
            return path.Descendants("*");
        }

        public IReadOnlySet<PathSpec> Descendants(PathSpec path, string pattern)
        {
            return new PathSpecDescendants(path, pattern, true, this);
        }

        public IObservable<Unit> ObserveChanges(PathSpec path)
        {
            return path.ObserveChanges(NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size);
        }

        public IObservable<Unit> ObserveChanges(PathSpec path, NotifyFilters filters)
        {
            var parent = path.Parent();
            if (!parent.HasValue) return Observable.Never<Unit>();

            return Observable.Create<Unit>(observer =>
            {
                var watcher = new FileSystemWatcher(parent.Value.ToString())
                {
                    IncludeSubdirectories = false,
                    Filter = path.Name,
                    EnableRaisingEvents = true,
                    NotifyFilter = filters
                };

                var subscription = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(handler => watcher.Changed += handler, handler => watcher.Changed -= handler)
                    .Select(_ => Unit.Default)
                    .Subscribe(observer);

                return new AnonymousDisposable(() =>
                {
                    subscription.Dispose();
                    watcher.Dispose();
                });
            });
        }


        #region FileSystemWatcher extension methods

        public IObservable<PathType> ObservePathType(PathSpec path)
        {
            var parent = path.Parent();
            if (!parent.HasValue) return Observable.Return(path.GetPathType());
            return parent.Value.Children(path.Name).ToLiveLinq().AsObservable().Select(_ => path.GetPathType()).DistinctUntilChanged();
        }

        public IObservable<PathSpec> Renamings(PathSpec path)
        {
            var parent = path.Parent();
            if (!parent.HasValue) return Observable.Return(path);

            return Observable.Create<PathSpec>(
                async (observer, token) =>
                {
                    var currentPath = path;
                    while (!token.IsCancellationRequested)
                    {
                        var watcher = new FileSystemWatcher(currentPath.Parent().Value.ToString())
                        {
                            IncludeSubdirectories = false,
                            Filter = currentPath.Name
                        };

                        var tcs = new TaskCompletionSource<PathSpec>();

                        RenamedEventHandler handler = (_, args) =>
                        {
                            tcs.SetResult(new PathSpec(path.Flags, path.DirectorySeparator, this, args.FullPath));
                        };

                        watcher.EnableRaisingEvents = true;

                        watcher.Renamed += handler;

                        currentPath = await tcs.Task;

                        observer.OnNext(currentPath);

                        watcher.Renamed -= handler;
                        watcher.Dispose();
                    }
                });
        }

        #endregion

        #region Internal extension methods

        internal static IMaybe<IEnumerable<T>> AllOrNothing<T>(IEnumerable<IMaybe<T>> source)
        {
            var source2 = source.ToList();
            if (source2.Any(opt => !opt.HasValue))
                return Nothing<IEnumerable<T>>();
            return Something(source2.Select(opt => opt.Value));
        }

        internal static IMaybe<T> Flatten<T>(IMaybe<IMaybe<T>> opt)
        {
            if (!opt.HasValue)
                return Nothing<T>();
            return opt.Value;
        }

        public StringComparison ToStringComparison(PathFlags pathFlags)
        {
            if (pathFlags.HasFlag(PathFlags.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        public StringComparison ToStringComparison(PathFlags pathFlags, PathFlags otherPathFlags)
        {
            if (pathFlags.HasFlag(PathFlags.CaseSensitive) && otherPathFlags.HasFlag(PathFlags.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        #endregion

        #region PathSpec extension methods

        public PathSpec RelativeTo(PathSpec path, PathSpec relativeTo)
        {
            var pathStr = path.Simplify().ToString();
            var relativeToStr = relativeTo.Simplify().ToString();

            var common = path.CommonWith(relativeTo);

            if (!common.HasValue)
                return path;

            var sb = new StringBuilder();

            for(var i = 0; i < relativeTo.Components.Count - common.Value.Components.Count; i++)
            {
                sb.Append("..");
                sb.Append(path.DirectorySeparator);
            }

            var restOfRelativePath = pathStr.ToString().Substring(common.Value.ToString().Length);
            while (restOfRelativePath.StartsWith(path.DirectorySeparator))
                restOfRelativePath = restOfRelativePath.Substring(path.DirectorySeparator.Length);

            sb.Append(restOfRelativePath);

            return ToPath(sb.ToString()).Value;

            //if (pathStr.StartsWith(relativeToStr))
            //{
            //    var result = pathStr.Substring(relativeToStr.Length);
            //    if (result.StartsWith(path.DirectorySeparator))
            //        return ToPathSpec(result.Substring(path.DirectorySeparator.Length)).Value;
            //}
            //throw new NotImplementedException();
        }

        public IMaybe<PathSpec> CommonWith(PathSpec path, PathSpec that)
        {
            var path1Str = path.ToString();
            var path2Str = that.ToString();

            if (!path.Flags.HasFlag(PathFlags.CaseSensitive) || !that.Flags.HasFlag(PathFlags.CaseSensitive))
            {
                path1Str = path1Str.ToUpper();
                path2Str = path2Str.ToUpper();
            }

            int i;
            for (i = 0; i < path1Str.Length && i < path2Str.Length; i++)
            {
                if (!path1Str[i].Equals(path2Str[i]))
                    break;
            }

            if (i == 0)
                return Nothing<PathSpec>();
            return ToPath(path1Str.Substring(0, i));
        }

        public bool CanBeSimplified(PathSpec path)
        {
            return path.Components.SkipWhile(str => str == "..").Any(str => str == "..");
        }

        public PathSpec Simplify(PathSpec path)
        {
            var result = new List<string>();
            var numberOfComponentsToSkip = 0;
            for (var i = path.Components.Count - 1; i >= 0; i--)
            {
                if (path.Components[i] == ".")
                    continue;
                if (path.Components[i] == "..")
                    numberOfComponentsToSkip++;
                else if (numberOfComponentsToSkip > 0)
                {
                    numberOfComponentsToSkip--;
                }
                else
                {
                    result.Insert(0, path.Components[i]);
                }
            }
            if (numberOfComponentsToSkip > 0 && !path.IsRelative())
                throw new ArgumentException("Error: the specified path points to an ancestor of the root, which means that the specified path is invalid");
            for (var i = 0; i < numberOfComponentsToSkip; i++)
            {
                result.Insert(0, "..");
            }
            var sb = new StringBuilder();
            for (var i = 0; i < result.Count; i++)
            {
                sb.Append(result[i]);
                if (result[i] != "\\" && i != result.Count - 1)
                    sb.Append(path.DirectorySeparator);
            }
            var str = sb.ToString();
            if (str.Length == 0)
                str = ".";
            return ToPath(str, path.Flags).Value;
        }

        public IMaybe<PathSpec> Parent(PathSpec path)
	    {
            return path.Components.Subset(0, -2).Select(str => TryParsePathSpec(str, path.Flags)).Join();
	    }

        public bool IsAbsolute(PathSpec path)
        {
            return ComponentsAreAbsolute(path.Components);
        }

        public bool IsRelative(PathSpec path)
        {
            return ComponentsAreRelative(path.Components);
        }

        internal bool ComponentsAreAbsolute(IReadOnlyList<string> path)
        {
            if (path[0] == "/")
                return true;
            if (char.IsLetter(path[0][0]) && path[0][1] == ':')
                return true;
            return false;
        }

        internal bool ComponentsAreRelative(IReadOnlyList<string> path)
        {
            if (ComponentsAreAbsolute(path))
                return false;
            if (path[0] == "\\")
                return false;
            return true;
        }

        #region Ways of combining PathSpecs

        #region String overloads

        public IMaybe<PathSpec> Join(IReadOnlyList<string> descendants)
        {
            return descendants.Select(str => ToPath(str)).Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<string> descendants)
        {
            return Join(descendants.ToList());
        }

        public IMaybe<PathSpec> Join(IReadOnlyList<IMaybe<string>> descendants)
        {
            if (descendants.Any(opt => !opt.HasValue))
                return Nothing<PathSpec>();
            return Join(descendants.Select(opt => opt.Value));
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<string>> descendants)
        {
            return Join(descendants.ToList());
        }

        public IMaybe<PathSpec> Join(PathSpec root, IEnumerable<string> descendants)
        {
            return root.Join(descendants.Select(str => ToPath(str)));
        }

        public IMaybe<PathSpec> Join(IMaybe<PathSpec> root, IEnumerable<string> descendants)
        {
            if (!root.HasValue)
                return Nothing<PathSpec>();
            return root.Value.Join(descendants.Select(str => ToPath(str)));
        }

        public IMaybe<PathSpec> Join(IMaybe<PathSpec> root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.SelectMany(rootVal => rootVal.Join(descendants.Select(m => m.SelectMany(str => ToPath(str)))));
        }

        public IMaybe<PathSpec> Join(PathSpec root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.Join(descendants.Select(m => m.SelectMany(str => ToPath(str))));
        }

        public IMaybe<PathSpec> Join(PathSpec root, params string[] descendants)
        {
            return root.Join(descendants.Select(str => ToPath(str)));
        }

        public IMaybe<PathSpec> Join(IMaybe<PathSpec> root, params string[] descendants)
        {
            if (!root.HasValue)
                return Nothing<PathSpec>();
            return root.Value.Join(descendants.Select(str => ToPath(str)));
        }

        public IMaybe<PathSpec> Join(IMaybe<PathSpec> root, params IMaybe<string>[] descendants)
        {
            return root.Join(descendants.Select(m => m.SelectMany(str => ToPath(str))));
        }

        public IMaybe<PathSpec> Join(PathSpec root, params IMaybe<string>[] descendants)
        {
            return root.Join(descendants.Select(m => m.SelectMany(str => ToPath(str))));
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, IEnumerable<string> descendants)
        {
            return root.Join(descendants.Select(str => ToPath(str)));
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, IEnumerable<string> descendants)
        {
            return root.Join(descendants.Select(str => ToPath(str)));
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.Concat(descendants.Select(m => m.SelectMany(str => ToPath(str)))).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, IEnumerable<IMaybe<string>> descendants)
        {
            return descendants
                .Select(m => m.SelectMany(str => ToPath(str)))
                .AllOrNothing().Select(desc => root.Concat(desc).ToList().Join()).SelectMany(x => x);
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, params string[] descendants)
        {
            return root.Join(descendants.Select(str => ToPath(str)));
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, params string[] descendants)
        {
            return root.Concat(descendants.Select(str => ToPath(str))).AllOrNothing().Select(paths => paths.Join()).SelectMany(x => x);
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, params IMaybe<string>[] descendants)
        {
            return root.Concat(descendants.Select(m => m.SelectMany(str => ToPath(str)))).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, params IMaybe<string>[] descendants)
        {
            return descendants.Select(m => m.SelectMany(str => ToPath(str))).AllOrNothing().Select(desc => root.Concat(desc).ToList().Join()).SelectMany(x => x);
        }

        #endregion

        #region PathSpec overloads

        public IMaybe<PathSpec> Join(IReadOnlyList<PathSpec> descendants)
        {
            var first = descendants[0];
            if (descendants.Skip(1).Any(c => !c.IsRelative()
                || c.DirectorySeparator != first.DirectorySeparator
                || c.Flags != first.Flags))
                return Nothing<PathSpec>();
            return Something(new PathSpec(GetDefaultFlagsForThisEnvironment(), first.DirectorySeparator, this,
                descendants.SelectMany(opt => opt.Components).Where((str, i) => i == 0 || str != ".")));
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> descendants)
        {
            return descendants.ToList().Join();
        }

        public IMaybe<PathSpec> Join(IReadOnlyList<IMaybe<PathSpec>> descendants)
        {
            if (descendants.Any(opt => !opt.HasValue))
                return Nothing<PathSpec>();
            return descendants.Select(opt => opt.Value).Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return descendants.ToList().Join();
        }

        public IMaybe<PathSpec> Join(PathSpec root, IEnumerable<PathSpec> descendants)
        {
            return root.ItemConcat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IMaybe<PathSpec> root, IEnumerable<PathSpec> descendants)
        {
            if (!root.HasValue)
                return Nothing<PathSpec>();
            return root.Value.ItemConcat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IMaybe<PathSpec> root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return root.ItemConcat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(PathSpec root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return Something(root).ItemConcat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(PathSpec root, params PathSpec[] descendants)
        {
            return root.ItemConcat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IMaybe<PathSpec> root, params PathSpec[] descendants)
        {
            if (!root.HasValue)
                return Nothing<PathSpec>();
            return root.Value.ItemConcat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IMaybe<PathSpec> root, params IMaybe<PathSpec>[] descendants)
        {
            return root.ItemConcat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(PathSpec root, params IMaybe<PathSpec>[] descendants)
        {
            return Something(root).ItemConcat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, IEnumerable<PathSpec> descendants)
        {
            return root.Concat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, IEnumerable<PathSpec> descendants)
        {
            return root.AllOrNothing().Select(enumerable => enumerable.Concat(descendants).ToList().Join()).SelectMany(x => x);
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return root.Concat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return descendants.AllOrNothing().Select(desc => root.Concat(desc).ToList().Join()).SelectMany(x => x);
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, params PathSpec[] descendants)
        {
            return root.Concat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, params PathSpec[] descendants)
        {
            return root.AllOrNothing().Select(enumerable => enumerable.Concat(descendants).ToList().Join()).SelectMany(x => x);
        }

        public IMaybe<PathSpec> Join(IEnumerable<IMaybe<PathSpec>> root, params IMaybe<PathSpec>[] descendants)
        {
            return root.Concat(descendants).ToList().Join();
        }

        public IMaybe<PathSpec> Join(IEnumerable<PathSpec> root, params IMaybe<PathSpec>[] descendants)
        {
            return descendants.AllOrNothing().Select(desc => root.Concat(desc).ToList().Join()).SelectMany(x => x);
        }

        #endregion

        #endregion

        #endregion

        #region String extension methods

        public IMaybe<PathSpec> ToPath(string path, PathFlags flags)
		{
            return TryParsePathSpec(path, flags);
		}

        public IMaybe<PathSpec> ToPath(string path)
		{
			return TryParsePathSpec(path);
		}

		public bool IsAbsoluteWindowsPath(string path)
		{
			return char.IsLetter(path[0]) && path[1] == ':';
		}

		public bool IsAbsoluteUnixPath(string path)
		{
			return path.StartsWith("/");
		}

        /// <summary>
        /// Checks for invalid relative paths, like C:\.. (Windows) or /.. (Unix)
        /// </summary>
        internal bool IsAncestorOfRoot(IReadOnlyList<string> pathComponents)
        {
            var result = new List<string>();
            var numberOfComponentsToSkip = 0;
            var isRelative = ComponentsAreRelative(pathComponents);
            for (var i = pathComponents.Count - 1; i >= 0; i--)
            {
                if (!isRelative && i == 0)
                    result.Insert(0, pathComponents[i]);
                else if (pathComponents[i] == ".")
                    continue;
                else if (pathComponents[i] == "..")
                    numberOfComponentsToSkip++;
                else if (numberOfComponentsToSkip > 0)
                    numberOfComponentsToSkip--;
                else
                    result.Insert(0, pathComponents[i]);
            }
            return numberOfComponentsToSkip > 0 && !isRelative;
        }

		#endregion
		
		
		public PathSpec CreateTemporaryPath(PathType type)
                {
                    var path = Path.GetRandomFileName();
                    var spec = ToPath(path).Value;
                    if (type == PathType.File)
                        spec.Create(PathType.File);
                    if (type == PathType.Folder)
                        spec.Create(PathType.Folder);
                    return spec;
                }
        
                private object _lock = new object();
                private PathFlags? defaultFlagsForThisEnvironment;
                private string defaultDirectorySeparatorForThisEnvironment;
        
                public PathFlags GetDefaultFlagsForThisEnvironment()
                {
                    lock(_lock)
                    {
                        if (defaultFlagsForThisEnvironment == null)
                        {
                            var file = Path.GetTempFileName();
                            var caseSensitive = File.Exists(file.ToLower()) && File.Exists(file.ToUpper());
                            File.Delete(file);
                            if (caseSensitive)
                                defaultFlagsForThisEnvironment = PathFlags.CaseSensitive;
                            else
                                defaultFlagsForThisEnvironment = PathFlags.None;
                        }
                        return defaultFlagsForThisEnvironment.Value;
                    }
                }
        
                public string GetDefaultDirectorySeparatorForThisEnvironment()
                {
                    lock(_lock)
                    {
                        if (defaultDirectorySeparatorForThisEnvironment == null)
                        {
                            var path = Path.Combine("a", "b");
                            defaultDirectorySeparatorForThisEnvironment = path.Substring(1, path.Length - 2);
                        }
                        return defaultDirectorySeparatorForThisEnvironment;
                    }
                }
        
                /// <summary>
                ///   Returns a regex that filters files the same as the specified pattern.
                ///   From here: http://www.java2s.com/Code/CSharp/Regular-Expressions/Checksifnamematchespatternwithandwildcards.htm
                /// Copyright:   Julijan ?ribar, 2004-2007
                /// 
                /// This software is provided 'as-is', without any express or implied
                /// warranty.  In no event will the author(s) be held liable for any damages
                /// arising from the use of this software.
                /// Permission is granted to anyone to use this software for any purpose,
                /// including commercial applications, and to alter it and redistribute it
                /// freely, subject to the following restrictions:
                /// 1. The origin of this software must not be misrepresented; you must not
                ///   claim that you wrote the original software. If you use this software
                ///   in a product, an acknowledgment in the product documentation would be
                ///   appreciated but is not required.
                /// 2. Altered source versions must be plainly marked as such, and must not be
                ///   misrepresented as being the original software.
                /// 3. This notice may not be removed or altered from any source distribution.
                /// </summary>
                /// <param name="filename">
                ///   Name to match.
                /// </param>
                /// <param name="pattern">
                ///   Pattern to match to.
                /// </param>
                /// <returns>
                ///   <c>true</c> if name matches pattern, otherwise <c>false</c>.
                /// </returns>
                public Regex FileNamePatternToRegex(string pattern)
                {
                    // prepare the pattern to the form appropriate for Regex class
                    var sb = new StringBuilder(pattern);
                    // remove superflous occurences of  "?*" and "*?"
                    while (sb.ToString().IndexOf("?*") != -1)
                    {
                        sb.Replace("?*", "*");
                    }
                    while (sb.ToString().IndexOf("*?") != -1)
                    {
                        sb.Replace("*?", "*");
                    }
                    // remove superflous occurences of asterisk '*'
                    while (sb.ToString().IndexOf("**") != -1)
                    {
                        sb.Replace("**", "*");
                    }
                    // if only asterisk '*' is left, the mask is ".*"
                    if (sb.ToString().Equals("*"))
                        pattern = ".*";
                    else
                    {
                        // replace '.' with "\."
                        sb.Replace(".", "\\.");
                        // replaces all occurrences of '*' with ".*" 
                        sb.Replace("*", ".*");
                        // replaces all occurrences of '?' with '.*' 
                        sb.Replace("?", ".");
                        // add "\b" to the beginning and end of the pattern
                        sb.Insert(0, "\\b");
                        sb.Append("\\b");
                        pattern = sb.ToString();
                    }
                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    return regex;
                }
        
                public PathSpec ParsePathSpec(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
                {
                    string error = string.Empty;
                    PathSpec pathSpec;
                    if (!TryParsePathSpec(path, out pathSpec, out error, flags))
                        throw new ArgumentException(error);
                    return pathSpec;
                }
        
                public IMaybe<PathSpec> TryParsePathSpec(string path, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
                {
                    string error = string.Empty;
                    PathSpec pathSpec;
                    if (!TryParsePathSpec(path, out pathSpec, out error, flags))
                        return Nothing<PathSpec>();
                    return Something(pathSpec);
                }
        
                public bool TryParsePathSpec(string path, out PathSpec pathSpec, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
                {
                    string error = string.Empty;
                    return TryParsePathSpec(path, out pathSpec, out error, flags);
                }
        
                public bool TryParsePathSpec(string path, out PathSpec pathSpec, out string error, PathFlags flags = PathFlags.UseDefaultsForGivenPath)
                {
                    if (flags.HasFlag(PathFlags.UseDefaultsFromUtility) && flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                        throw new ArgumentException("Cannot specify both PathFlags.UseDefaultsFromUtility and PathFlags.UseDefaultsForGivenPath");
                    if (flags.HasFlag(PathFlags.UseDefaultsFromUtility))
                        flags = GetDefaultFlagsForThisEnvironment();
                    error = string.Empty;
                    pathSpec = null;
                    var isWindowsStyle = path.Contains("\\") || path.Contains(":");
                    var isUnixStyle = path.Contains("/");
                    if (isWindowsStyle && isUnixStyle)
                    {
                        error = "Cannot mix slashes and backslashes in the same path";
                        return false;
                    }
                    if (isWindowsStyle)
                    {
                        if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                            flags = PathFlags.None;
                        if (path.Length > 1 && path.EndsWith("\\"))
                            path = path.Substring(0, path.Length - 1);
                        var colonIdx = path.LastIndexOf(':');
                        if (colonIdx > -1 && (colonIdx != 1 || !char.IsLetter(path[0]) || (path.Length > 2 && path[2] != '\\')))
                        {
                            error = "A Windows path may not contain a : character, except as part of the drive specifier.";
                            return false;
                        }
                        var isAbsolute = IsAbsoluteWindowsPath(path);
                        if (isAbsolute)
                        {
                            var components = path.Split('\\').ToList();
                            components.RemoveWhere((i, str) => i != 0 && str == ".");
                            if (components.Any(String.IsNullOrEmpty))
                            {
                                error = "Must not contain any directories that have empty names";
                                return false;
                            }
                            if (IsAncestorOfRoot(components))
                            {
                                error = "Must not point to an ancestor of the filesystem root";
                                return false;
                            }
                            pathSpec = new PathSpec(flags, "\\", this, components);
                        }
                        else if (path.StartsWith("."))
                        {
                            var components = path.Split('\\').ToList();
                            components.RemoveWhere((i, str) => i != 0 && str == ".");
                            if (components.Any(String.IsNullOrEmpty))
                            {
                                error = "Must not contain any directories that have empty names";
                                return false;
                            }
                            if (IsAncestorOfRoot(components))
                            {
                                error = "Must not point to an ancestor of the filesystem root";
                                return false;
                            }
                            pathSpec = new PathSpec(flags, "\\", this, components);
                        }
                        else if (path.StartsWith("\\\\"))
                        {
                            var components = "\\\\".ItemConcat(path.Substring(2).Split('\\')).ToList();
                            components.RemoveWhere((i, str) => i != 0 && str == ".");
                            if (components.Any(String.IsNullOrEmpty))
                            {
                                error = "Must not contain any directories that have empty names";
                                return false;
                            }
                            if (IsAncestorOfRoot(components))
                            {
                                error = "Must not point to an ancestor of the filesystem root";
                                return false;
                            }
                            pathSpec = new PathSpec(flags, "\\", this, components);
                        }
                        else if (path.StartsWith("\\"))
                        {
                            var components = "\\".ItemConcat(path.Substring(1).Split('\\')).ToList();
                            components.RemoveWhere((i, str) => i != 0 && str == ".");
                            if (components.Any(String.IsNullOrEmpty))
                            {
                                error = "Must not contain any directories that have empty names";
                                return false;
                            }
                            if (IsAncestorOfRoot(components))
                            {
                                error = "Must not point to an ancestor of the filesystem root";
                                return false;
                            }
                            pathSpec = new PathSpec(flags, "\\", this, components);
                        }
                        else
                        {
                            var components = ".".ItemConcat(path.Split('\\')).ToList();
                            components.RemoveWhere((i, str) => i != 0 && str == ".");
                            if (components.Any(String.IsNullOrEmpty))
                            {
                                error = "Must not contain any directories that have empty names";
                                return false;
                            }
                            if (IsAncestorOfRoot(components))
                            {
                                error = "Must not point to an ancestor of the filesystem root";
                                return false;
                            }
                            pathSpec = new PathSpec(flags, "\\", this, components);
                        }
                        return true;
                    }
                    if (isUnixStyle)
                    {
                        if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                            flags = PathFlags.CaseSensitive;
                        if (path.Length > 1 && path.EndsWith("/"))
                            path = path.Substring(0, path.Length - 1);
                        if (path.Contains(":"))
                        {
                            error = "A Unix path may not contain a : character.";
                            return false;
                        }
                        var isAbsolute = IsAbsoluteUnixPath(path);
                        if (isAbsolute)
                        {
                            var components = "/".ItemConcat(path.Substring(1).Split('/')).ToList();
                            components.RemoveWhere((i, str) => i != 0 && str == ".");
                            if (components.Any(String.IsNullOrEmpty))
                            {
                                error = "Must not contain any directories that have empty names";
                                return false;
                            }
                            if (IsAncestorOfRoot(components))
                            {
                                error = "Must not point to an ancestor of the filesystem root";
                                return false;
                            }
                            pathSpec = new PathSpec(flags, "/", this, components);
                        }
                        else if (path.StartsWith("."))
                        {
                            var components = path.Split('/').ToList();
                            components.RemoveWhere((i, str) => i != 0 && str == ".");
                            if (components.Any(String.IsNullOrEmpty))
                            {
                                error = "Must not contain any directories that have empty names";
                                return false;
                            }
                            if (IsAncestorOfRoot(components))
                            {
                                error = "Must not point to an ancestor of the filesystem root";
                                return false;
                            }
                            pathSpec = new PathSpec(flags, "/", this, components);
                        }
                        else
                        {
                            var components = ".".ItemConcat(path.Split('/')).ToList();
                            components.RemoveWhere((i, str) => i != 0 && str == ".");
                            if (components.Any(String.IsNullOrEmpty))
                            {
                                error = "Must not contain any directories that have empty names";
                                return false;
                            }
                            if (IsAncestorOfRoot(components))
                            {
                                error = "Must not point to an ancestor of the filesystem root";
                                return false;
                            }
                            pathSpec = new PathSpec(flags, "/", this, components);
                        }
                        return true;
                    }
                    // If we reach this point, there are no backslashes or slashes in the path, meaning that it's a
                    // path with one element.
                    if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                        flags = GetDefaultFlagsForThisEnvironment();
                    if (path == ".." || path == ".")
                        pathSpec = new PathSpec(flags, GetDefaultDirectorySeparatorForThisEnvironment(), this, path);
                    else
                        pathSpec = new PathSpec(flags, GetDefaultDirectorySeparatorForThisEnvironment(), this, ".", path);
                    return true;
                }
        
                public PathSpec CurrentDirectory => ToPath(Environment.CurrentDirectory).Value;
        
                public void UpdateStorage()
                {
                    var currentStorage = System.IO.Directory.GetLogicalDrives();
                    foreach (var drive in currentStorage)
                    {
                        var drivePath = ToPath(drive).Value;
                        if (!_knownStorage.Contains(drivePath))
                            _knownStorage.Add(drivePath);
                    }
        
                    var drivesThatWereRemoved = new List<PathSpec>();
        
                    foreach (var drive in _knownStorage)
                    {
                        if (!currentStorage.Contains(drive + "\\"))
                            drivesThatWereRemoved.Add(drive);
                    }
        
                    foreach (var driveThatWasRemoved in drivesThatWereRemoved)
                    {
                        _knownStorage.Remove(driveThatWasRemoved);
                    }
                }
        
                private readonly HashSet<PathSpec> _knownStorage = new HashSet<PathSpec>();
        
                public IReadOnlySet<PathSpec> Storage { get; }
                
                
                public IMaybe<StreamWriter> CreateText(PathSpec pathSpec)
                        {
                            try
                            {
                                return new Maybe<StreamWriter>(pathSpec.AsFileInfo().CreateText());
                            }
                            catch (UnauthorizedAccessException)
                            {
                                return Maybe<StreamWriter>.Nothing;
                            }
                            catch (IOException)
                            {
                                return Maybe<StreamWriter>.Nothing;
                            }
                            catch (SecurityException)
                            {
                                return Maybe<StreamWriter>.Nothing;
                            }
                        }
                
                        public IEnumerable<string> ReadLines(PathSpec pathSpec, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
                            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            var maybeFileStream = pathSpec.Open(fileMode, fileAccess, fileShare);
                            if (maybeFileStream.HasValue)
                            {
                                using (maybeFileStream.Value)
                                {
                                    return ReadLines(maybeFileStream.Value, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
                                }
                            }
                            return EnumerableUtility.EmptyArray<string>();
                        }
                
                        public IMaybe<string> ReadText(PathSpec pathSpec, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read, FileShare fileShare = FileShare.Read,
                            Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            return pathSpec.Open(fileMode, fileAccess, fileShare).Select(
                                fs =>
                                {
                                    using (fs)
                                    {
                                        return ReadText(fs, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
                                    }
                                });
                        }
                
                        public void WriteText(PathSpec pathSpec, IEnumerable<string> lines, FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
                            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            var maybeFileStream = pathSpec.Open(fileMode, fileAccess, fileShare);
                            if (maybeFileStream.HasValue)
                            {
                                using (maybeFileStream.Value)
                                {
                                    WriteLines(maybeFileStream.Value, lines, encoding, bufferSize, leaveOpen);
                                }
                            }
                        }
                
                        public void WriteText(PathSpec pathSpec, string text, FileMode fileMode = FileMode.Create, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None,
                            Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            var maybeFileStream = pathSpec.Open(fileMode, fileAccess, fileShare);
                            if (maybeFileStream.HasValue)
                            {
                                using (maybeFileStream.Value)
                                {
                                    WriteText(maybeFileStream.Value, text, encoding, bufferSize, leaveOpen);
                                }
                            }
                        }
                
                        public IEnumerable<string> ReadLines(Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            if (encoding == null)
                                encoding = Encoding.UTF8;
                            using (var sr = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    yield return line;
                                }
                            }
                        }
                
                        public IEnumerable<string> ReadLinesBackwards(Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            if (encoding == null)
                                encoding = Encoding.UTF8;
                
                            string content = string.Empty;
                            // Seek file pointer to end
                            stream.Seek(0, SeekOrigin.End);
                
                            byte[] buffer = new byte[bufferSize];
                
                            //loop now and read backwards
                            while (stream.Position > 0)
                            {
                                buffer.Initialize();
                
                                int bytesRead;
                
                                if (stream.Position - bufferSize >= 0)
                                {
                                    stream.Seek(-bufferSize, SeekOrigin.Current);
                                    bytesRead = stream.Read(buffer, 0, bufferSize);
                                    stream.Seek(-bufferSize, SeekOrigin.Current);
                                }
                                else
                                {
                                    var finalBufferSize = stream.Position;
                                    stream.Seek(0, SeekOrigin.Begin);
                                    bytesRead = stream.Read(buffer, 0, (int)finalBufferSize);
                                    stream.Seek(0, SeekOrigin.Begin);
                                }
                
                                var strBuffer = encoding.GetString(buffer, 0, bytesRead);
                
                                // lines is equal to what we just read, with the leftover content from last iteration appended to it.
                                var lines = (strBuffer + content).Split('\n');
                
                                // Loop through lines backwards, ignoring the first element, and yield each value
                                for (var i = lines.Length - 1; i > 0; i--)
                                {
                                    yield return lines[i].Trim('\r');
                                }
                
                                // Leftover content is part of a line defined on the line(s) that we'll read next iteration of while loop
                                // so we must save leftover content for later
                                content = lines[0];
                            }
                        }
                
                        public string ReadText(Stream stream, Encoding encoding = null, bool detectEncodingFromByteOrderMarks = true, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            if (encoding == null)
                                encoding = Encoding.UTF8;
                            using (var sr = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
                            {
                                return ReadText(sr);
                            }
                        }
                
                        private string ReadText(StreamReader streamReader)
                        {
                            return streamReader.ReadToEnd();
                        }
                
                        public void WriteLines(Stream stream, IEnumerable<string> lines, Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            if (encoding == null)
                                encoding = Encoding.UTF8;
                            using (var sw = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
                            {
                                WriteLines(sw, lines);
                            }
                        }
                
                        private void WriteLines(StreamWriter streamWriter, IEnumerable<string> lines)
                        {
                            foreach (var line in lines)
                            {
                                streamWriter.WriteLine(line);
                            }
                        }
                
                        public void WriteText(Stream stream, string text, Encoding encoding = null, int bufferSize = 4096, bool leaveOpen = false)
                        {
                            if (encoding == null)
                                encoding = Encoding.UTF8;
                            using (var sw = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
                            {
                                WriteText(sw, text);
                            }
                        }
                
                        private void WriteText(StreamWriter streamWriter, string text)
                        {
                            streamWriter.Write(text);
                        }
    }
}
