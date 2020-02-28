using System;
using System.Collections.Generic;
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
using SimpleMonads;
using static SimpleMonads.Utility;

namespace MoreIO
{
    public static partial class PathExt
    {
        #region File and folder extension methods
        
        public static PathSpec CreateEmptyFile(this PathSpec pathSpec)
        {
            pathSpec.CreateFile().Dispose();
            if (pathSpec.GetPathType() != PathType.File)
                throw new IOException("Could not create file " + pathSpec);
            return pathSpec;
        }

        public static FileStream CreateFile(this PathSpec pathSpec)
        {
            if (pathSpec.Parent().Value.GetPathType() != PathType.Folder)
                pathSpec.Parent().Value.Create(PathType.Folder);
            var result = pathSpec.AsFileInfo().Create();
            if (pathSpec.GetPathType() != PathType.File)
                throw new IOException("Could not create file " + pathSpec);
            return result;
        }

        public static PathSpec DeleteFile(this PathSpec pathSpec)
        {
            if (pathSpec.GetPathType() == PathType.None)
                return pathSpec;
            try
            {
                pathSpec.AsFileInfo().Delete();
            }
            catch (IOException)
            {
                pathSpec.AsFileInfo().Delete();
            }
            catch (UnauthorizedAccessException)
            {
                pathSpec.AsFileInfo().Delete();
            }
            return pathSpec;
        }

        public static PathSpec Decrypt(this PathSpec pathSpec)
        {
            pathSpec.AsFileInfo().Decrypt();
            return pathSpec;
        }

        public static PathSpec Encrypt(this PathSpec pathSpec)
        {
            pathSpec.AsFileInfo().Encrypt();
            return pathSpec;
        }

        public static PathSpec Delete(this PathSpec pathSpec)
        {
            if (pathSpec.GetPathType() == PathType.File)
            {
                return pathSpec.DeleteFile();
            }
            if (pathSpec.GetPathType() == PathType.Folder)
            {
                return pathSpec.DeleteFolder(true);
            }
            return pathSpec;
        }

        public static string SurroundWithDoubleQuotesIfNecessary(this string str)
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

        public static bool IsAncestorOf(this PathSpec possibleAncestor, PathSpec possibleDescendant)
        {
            return IsDescendantOf(possibleDescendant, possibleAncestor);
        }

        public static bool IsDescendantOf(this PathSpec possibleDescendant, PathSpec possibleAncestor)
        {
            var possibleDescendantStr = Path.GetFullPath(possibleDescendant.ToString()).ToLower();
            var possibleAncestorStr = Path.GetFullPath(possibleAncestor.ToString()).ToLower();
            return possibleDescendantStr.StartsWith(possibleAncestorStr);
        }

        public static IEnumerable<string> Split(this PathSpec path)
        {
            return Ancestors(path, true).Select(pathName => Path.GetFileName(pathName.ToString())).Reverse();
        }

        public static string LastPathComponent(this PathSpec path)
        {
            return path.ToString().Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Last(str => !String.IsNullOrEmpty(str));
        }

        /// <summary>
        /// Returns ancestors in the order of closest (most immediate ancestors) to furthest (most distantly descended from). For example, the ancestors of the path C:\Users\myusername\Documents would be these, in this order:
        /// C:\Users\myusername
        /// C:\Users
        /// C:
        /// </summary>
        /// <param name="path"></param>
        /// <param name="includeItself"></param>
        /// <returns></returns>
        public static IEnumerable<PathSpec> Ancestors(this PathSpec path, bool includeItself = false)
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

        public static IMaybe<PathSpec> Descendant(this PathSpec path, params PathSpec[] paths)
        {
            return path.Descendant(paths.Select(p => p.ToString()).ToArray());
        }

        public static IMaybe<PathSpec> Descendant(this PathSpec path, params string[] paths)
        {
            var pathStr = path.ToString();
            // Make sure that pathStr is treated as a directory.
            if (!pathStr.EndsWith(@"\"))
                pathStr += @"\";

            return ToPathSpec(Path.Combine(pathStr, Path.Combine(paths)));
        }

        public static IMaybe<PathSpec> Ancestor(this PathSpec path, int level)
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

        public static bool HasExtension(this PathSpec pathSpec, string extension)
        {
            var actualExtension = Path.GetExtension(pathSpec.ToString());
            if (actualExtension == extension)
                return true;
            if (actualExtension == null)
                return false;
            return actualExtension.Equals(extension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathSpec"></param>
        /// <param name="differentExtension">Must include the "." part of the extension (e.g., ".avi" not "avi")</param>
        /// <returns></returns>
        public static IMaybe<PathSpec> WithExtension(this PathSpec pathSpec, string differentExtension)
        {
            return ToPathSpec(Path.ChangeExtension(pathSpec.ToString(), differentExtension));
        }

        public static IFileUriTranslation Copy(this IFileUriTranslation sad)
        {
            switch (sad.Source.GetPathType())
            {
                case PathType.File:
                    sad.CopyFile();
                    break;
                case PathType.Folder:
                    sad.CopyFolder();
                    break;
                case PathType.None:
                    throw new IOException(
                        String.Format("An attempt was made to copy \"{0}\" to \"{1}\", but the source path doesn't exist.",
                                      sad.Source, sad.Destination));
            }
            return sad;
        }

        public static IFileUriTranslation CopyFile(this IFileUriTranslation sad)
        {
            if (sad.Source.GetPathType() != PathType.File)
                throw new IOException(String.Format("An attempt was made to copy a file from \"{0}\" to \"{1}\" but the source path is not a file.",
                    sad.Source, sad.Destination));
            if (sad.Destination.GetPathType() != PathType.None)
                throw new IOException(String.Format("An attempt was made to copy \"{0}\" to \"{1}\" but the destination path exists.",
                    sad.Source, sad.Destination));
            sad.Destination.Parent().Value.Create(PathType.Folder);
            File.Copy(sad.Source.ToString(), sad.Destination.ToString());
            return sad;
        }

        public static IFileUriTranslation CopyFolder(this IFileUriTranslation sad)
        {
            if (sad.Source.GetPathType() != PathType.Folder)
                throw new IOException(String.Format("An attempt was made to copy a folder from \"{0}\" to \"{1}\" but the source path is not a folder.",
                    sad.Source, sad.Destination));
            sad.Destination.Create(PathType.Folder);
            return sad;
        }

        public static IFileUriTranslation Move(this IFileUriTranslation sad)
        {
            switch (sad.Source.GetPathType())
            {
                case PathType.File:
                    sad.MoveFile();
                    break;
                case PathType.Folder:
                    sad.MoveFolder();
                    break;
                case PathType.None:
                    throw new IOException(
                        String.Format("An attempt was made to move \"{0}\" to \"{1}\", but the source path doesn't exist.",
                                      sad.Source, sad.Destination));
            }
            return sad;
        }

        public static IFileUriTranslation MoveFile(this IFileUriTranslation sad)
        {
            if (sad.Source.GetPathType() != PathType.File)
                throw new IOException(String.Format("An attempt was made to move a file from \"{0}\" to \"{1}\" but the source path is not a file.",
                    sad.Source, sad.Destination));
            if (sad.Destination.GetPathType() != PathType.None)
                throw new IOException(String.Format("An attempt was made to move \"{0}\" to \"{1}\" but the destination path exists.",
                    sad.Source, sad.Destination));
            if (sad.Destination.IsDescendantOf(sad.Source))
                throw new IOException(String.Format("An attempt was made to move a file from \"{0}\" to \"{1}\" but the destination path is a sub-path of the source path.",
                    sad.Source, sad.Destination));
            sad.Destination.Parent().Value.Create(PathType.Folder);
            File.Move(sad.Source.ToString(), sad.Destination.ToString());
            return sad;
        }
        
        public static IFileUriTranslation MoveFolder(this IFileUriTranslation sad)
        {
            if (sad.Source.GetPathType() != PathType.Folder)
                throw new IOException(String.Format("An attempt was made to move a folder from \"{0}\" to \"{1}\" but the source path is not a folder.",
                    sad.Source, sad.Destination));
            if (sad.Destination.GetPathType() == PathType.File)
                throw new IOException(String.Format("An attempt was made to move \"{0}\" to \"{1}\" but the destination path is a file.",
                    sad.Source, sad.Destination));
            if (sad.Destination.IsDescendantOf(sad.Source))
                throw new IOException(String.Format("An attempt was made to move a file from \"{0}\" to \"{1}\" but the destination path is a sub-path of the source path.",
                    sad.Source, sad.Destination));
            if (sad.Source.Children().Any())
                throw new IOException(String.Format("An attempt was made to move the non-empty folder \"{0}\". This is not allowed because all the files should be moved first, and only then can the folder be moved, because the move operation deletes the source folder, which would of course also delete the files and folders within the source folder.",
                    sad.Source));
            sad.Destination.Create(PathType.Folder);
            if (!sad.Source.Children().Any())
                sad.Source.DeleteFolder(false);
            return sad;
        }

        private static bool ContainsFiles(this PathSpec pathSpec)
        {
            if (pathSpec.GetPathType() == PathType.File)
                return true;
            return pathSpec.Children().All(child => child.ContainsFiles());
        }

        public static bool FolderContainsFiles(this PathSpec pathSpec)
        {
            if (pathSpec.GetPathType() == PathType.File)
                return false;
            return pathSpec.ContainsFiles();
        }

        public static IMaybe<PathSpec> GetCommonAncestry(this PathSpec fileUri1, PathSpec fileUri2)
        {
            return fileUri1.ToString().GetCommonBeginning(fileUri2.ToString()).Trim('\\').ToPathSpec();
        }

        public static IMaybe<Uri> GetCommonDescendants(this PathSpec fileUri1, PathSpec fileUri2)
        {
            try
            {
                return new Maybe<Uri>(new Uri(fileUri1.ToString().GetCommonEnding(fileUri2.ToString()).Trim('\\'), UriKind.Relative));
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

        public static IMaybe<Tuple<Uri, Uri>> GetNonCommonDescendants(this PathSpec fileUri1, PathSpec fileUri2)
        {
            try
            {
                var commonAncestry = fileUri1.ToString().GetCommonBeginning(fileUri2.ToString()).Trim('\\');
                return new Maybe<Tuple<Uri, Uri>>(new Tuple<Uri, Uri>(new Uri(fileUri1.ToString().Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative),
                                                 new Uri(fileUri2.ToString().Substring(commonAncestry.Length).Trim('\\'), UriKind.Relative)));
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

        public static IMaybe<Tuple<Uri, Uri>> GetNonCommonAncestry(this PathSpec fileUri1, PathSpec fileUri2)
        {
            try
            {
                var commonDescendants = fileUri1.ToString().GetCommonEnding(fileUri2.ToString()).Trim('\\');
                return new Maybe<Tuple<Uri, Uri>>(new Tuple<Uri, Uri>(new Uri(fileUri1.ToString().Substring(0, fileUri1.ToString().Length - commonDescendants.Length).Trim('\\')),
                                                 new Uri(fileUri2.ToString().Substring(0, fileUri2.ToString().Length - commonDescendants.Length).Trim('\\'))));
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

        public static IFileUriTranslation Translate(this PathSpec pathToBeCopied, PathSpec source, PathSpec destination)
        {
            return new CalculatedFileUriTranslation(pathToBeCopied, source, destination);
        }

        public static IFileUriTranslation Translate(this PathSpec source, PathSpec destination)
        {
            return new FileUriTranslation(source, destination);
        }

        public static Uri Child(this Uri parent, Uri child)
        {
            var parentLocalPath = parent.ToString();
            if (!parentLocalPath.EndsWith(Path.DirectorySeparatorChar.ToString())
                && !parentLocalPath.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                parentLocalPath += Path.DirectorySeparatorChar;
            return new Uri(parentLocalPath).MakeRelativeUri(child);
        }

        public static readonly List<string> VideoFileExtensions = new List<string>
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

        public static readonly List<string> ImageFileExtensions = new List<string>
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

        public static FileInfo AsFileInfo(this PathSpec pathSpec)
        {
            return new FileInfo(pathSpec.ToString());
        }

        public static DirectoryInfo AsDirectoryInfo(this PathSpec pathSpec)
        {
            return new DirectoryInfo(pathSpec.ToString());
        }

        public static IMaybe<T> As<T>(this T pathName, PathType pathType)
            where T : PathSpec
        {
            if (pathName.GetPathType() == pathType)
                return new Maybe<T>(pathName);
            return Maybe<T>.Nothing;
        }

        public static IMaybe<bool> IsReadOnly(this PathSpec pathSpec)
        {
            try
            {
                return new Maybe<bool>(pathSpec.AsFileInfo().IsReadOnly);
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

        public static IMaybe<long> Length(this PathSpec pathSpec)
        {
            try
            {
                return new Maybe<long>(pathSpec.AsFileInfo().Length);
            }
            catch (IOException)
            {
                return Maybe<long>.Nothing;
            }
        }

        public static IMaybe<FileAttributes> Attributes(this PathSpec attributes)
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

        public static IMaybe<DateTime> CreationTime(this PathSpec attributes)
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

        public static IMaybe<DateTime> LastAccessTime(this PathSpec attributes)
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

        public static IMaybe<DateTime> LastWriteTime(this PathSpec attributes)
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

        public static IMaybe<string> FullName(this PathSpec attributes)
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
        /// Includes the period character ".". For example, this function would return ".exe" if the file pointed to a file named was "test.exe".
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public static IMaybe<string> Extension(this string pathName)
        {
            var result = Path.GetExtension(pathName);
            if (String.IsNullOrEmpty(result))
                return Maybe<string>.Nothing;
            return new Maybe<string>(result);
        }

        public static bool IsImageUri(Uri uri)
        {
            if (uri == null)
                return false;
            string str = uri.ToString();
            if (!str.Contains("."))
                return false;
            string extension = str.Substring(str.LastIndexOf('.') + 1);
            return ImageFileExtensions.Any(curExtension => extension == curExtension);
        }

        public static bool IsVideoUri(Uri uri)
        {
            if (uri == null)
                return false;
            string str = uri.ToString();
            if (!str.Contains("."))
                return false;
            string extension = str.Substring(str.LastIndexOf('.') + 1);
            return VideoFileExtensions.Any(curExtension => extension == curExtension);
        }

        public static string StripQuotes(this string str)
        {
            if (str.StartsWith("\"") && str.EndsWith("\""))
                return str.Substring(1, str.Length - 2);
            if (str.StartsWith("'") && str.EndsWith("'"))
                return str.Substring(1, str.Length - 2);
            return str;
        }


        public static PathSpec Root(this PathSpec fileUri)
        {
            var ancestor = fileUri;
            IMaybe<PathSpec> cachedParent;
            while ((cachedParent = ancestor.Parent()).HasValue)
            {
                ancestor = cachedParent.Value;
            }
            return ancestor;
        }

        public static void RenameTo(this PathSpec source, PathSpec target)
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

        public static bool Exists(this PathSpec path)
        {
            return path.GetPathType() != PathType.None;
        }

        public static PathType GetPathType(this PathSpec path)
        {
            var str = path.ToString();
            if (File.Exists(str))
                return PathType.File;
            if (Directory.Exists(str))
                return PathType.Folder;
            return PathType.None;
        }

        public static PathSpec DeleteFolder(this PathSpec path, bool recursive = false)
        {
            Directory.Delete(path.ToString(), recursive);

            return path;
        }

        public static bool MayCreateFile(this FileMode fileMode)
        {
            return fileMode.HasFlag(FileMode.Append) || fileMode.HasFlag(FileMode.Create) ||
                   fileMode.HasFlag(FileMode.CreateNew) || fileMode.HasFlag(FileMode.OpenOrCreate);
        }
        
        public static void Create(this PathSpec path, PathType pathType)
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

        public static IMaybe<FileStream> Open(this PathSpec pathSpec, FileMode fileMode)
        {
            try
            {
                if (fileMode.MayCreateFile())
                    pathSpec.Parent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(pathSpec.AsFileInfo().Open(fileMode));
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

        public static IMaybe<FileStream> Open(this PathSpec pathSpec, FileMode fileMode,
                                                              FileAccess fileAccess)
        {
            try
            {
                if (fileMode.MayCreateFile())
                    pathSpec.Parent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(pathSpec.AsFileInfo().Open(fileMode, fileAccess));
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

        public static IMaybe<FileStream> Open(this PathSpec pathSpec, FileMode fileMode,
                                                              FileAccess fileAccess, FileShare fileShare)
        {
            try
            {
                if (fileMode.MayCreateFile())
                    pathSpec.Parent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<FileStream>(pathSpec.AsFileInfo().Open(fileMode, fileAccess, fileShare));
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

        public static PathSpec CreateFolder(this PathSpec fileUri)
        {
            try
            {
                if (fileUri.GetPathType() == PathType.Folder)
                    return fileUri;
                fileUri.Parent().IfHasValue(parent => parent.CreateFolder());
                fileUri.AsDirectoryInfo().Create();
            }
            catch (IOException)
            {
                if (fileUri.GetPathType() != PathType.Folder)
                    throw;
            }
            if (fileUri.GetPathType() != PathType.Folder)
                throw new IOException("Failed to create folder " + fileUri);
            return fileUri;
        }

        public static void WriteAllText(this PathSpec path, string text)
        {
            File.WriteAllText(path.ToString(), text);
        }

        public static void WriteAllLines(this PathSpec path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path.ToString(), lines);
        }

        public static void WriteAllLines(this PathSpec path, byte[] bytes)
        {
            File.WriteAllBytes(path.ToString(), bytes);
        }

        public static IEnumerable<string> ReadLines(this PathSpec path)
        {
            return File.ReadLines(path.ToString());
        }

        public static string ReadAllText(this PathSpec path)
        {
            return File.ReadAllText(path.ToString());
        }

        #endregion

        public static PathSpec ToAbsolute(this PathSpec pathSpec)
        {
            if (pathSpec.IsRelative())
                return PathUtility.CurrentDirectory.Join(pathSpec).Value;
            return pathSpec;
        }

        public static IReadOnlyObservableSet<PathSpec> Children(this PathSpec path)
        {
            return path.Children("*");
        }

        public static IReadOnlyObservableSet<PathSpec> Children(this PathSpec path, string pattern)
        {
            return new PathSpecDescendants(path, pattern, false);
        }

        public static IReadOnlyObservableSet<PathSpec> Descendants(this PathSpec path)
        {
            return path.Descendants("*");
        }

        public static IReadOnlyObservableSet<PathSpec> Descendants(this PathSpec path, string pattern)
        {
            return new PathSpecDescendants(path, pattern, true);
        }

        public static IObservable<Unit> ObserveChanges(this PathSpec path)
        {
            return path.ObserveChanges(NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size);
        }

        public static IObservable<Unit> ObserveChanges(this PathSpec path, NotifyFilters filters)
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

        public static IObservable<PathType> ObservePathType(this PathSpec path)
        {
            var parent = path.Parent();
            if (!parent.HasValue) return Observable.Return(path.GetPathType());
            return parent.Value.Children(path.Name).ToLiveLinq().AsObservable().Select(_ => path.GetPathType()).DistinctUntilChanged();
        }

        public static IObservable<bool> ObserveIsSymlink(this PathSpec path)
        {
            var pathType = path.ObservePathType();
            return pathType.Select(_ => path.IsSymlink());
        }

        public static IObservable<bool> ObserveIsJunctionPoint(this PathSpec path)
        {
            var pathType = path.ObservePathType();
            return pathType.Select(_ => path.IsJunctionPoint());
        }

        public static IObservable<IMaybe<PathSpec>> ObserveSymlinkTarget(this PathSpec path)
        {
            var pathType = path.ObservePathType();
            return pathType.Select(_ => path.GetSymlinkTarget());
        }

        public static IObservable<IMaybe<PathSpec>> ObserveJunctionPointTarget(this PathSpec path)
        {
            var pathType = path.ObservePathType();
            return pathType.Select(_ => path.GetJunctionPointTarget());
        }

        public static IObservable<PathSpec> Renamings(this PathSpec path)
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
                            tcs.SetResult(new PathSpec(path.Flags, path.DirectorySeparator, args.FullPath));
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

        internal static IMaybe<IEnumerable<T>> AllOrNothing<T>(this IEnumerable<IMaybe<T>> source)
        {
            var source2 = source.ToList();
            if (source2.Any(opt => !opt.HasValue))
                return Nothing<IEnumerable<T>>();
            return Something(source2.Select(opt => opt.Value));
        }

        internal static IMaybe<T> Flatten<T>(this IMaybe<IMaybe<T>> opt)
        {
            if (!opt.HasValue)
                return Nothing<T>();
            return opt.Value;
        }

        internal static StringComparison ToStringComparison(this PathFlags pathFlags)
        {
            if (pathFlags.HasFlag(PathFlags.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        internal static StringComparison ToStringComparison(this PathFlags pathFlags, PathFlags otherPathFlags)
        {
            if (pathFlags.HasFlag(PathFlags.CaseSensitive) && otherPathFlags.HasFlag(PathFlags.CaseSensitive))
                return StringComparison.Ordinal;
            return StringComparison.OrdinalIgnoreCase;
        }

        #endregion

        #region PathSpec extension methods

        public static PathSpec RelativeTo(this PathSpec pathSpec, PathSpec relativeTo)
        {
            var pathSpecStr = pathSpec.Simplify().ToString();
            var relativeToStr = relativeTo.Simplify().ToString();

            var common = pathSpec.CommonWith(relativeTo);

            if (!common.HasValue)
                return pathSpec;

            var sb = new StringBuilder();

            for(var i = 0; i < relativeTo.Components.Count - common.Value.Components.Count; i++)
            {
                sb.Append("..");
                sb.Append(pathSpec.DirectorySeparator);
            }

            var restOfRelativePath = pathSpecStr.ToString().Substring(common.Value.ToString().Length);
            while (restOfRelativePath.StartsWith(pathSpec.DirectorySeparator))
                restOfRelativePath = restOfRelativePath.Substring(pathSpec.DirectorySeparator.Length);

            sb.Append(restOfRelativePath);

            return sb.ToString().ToPathSpec().Value;

            //if (pathSpecStr.StartsWith(relativeToStr))
            //{
            //    var result = pathSpecStr.Substring(relativeToStr.Length);
            //    if (result.StartsWith(pathSpec.DirectorySeparator))
            //        return result.Substring(pathSpec.DirectorySeparator.Length).ToPathSpec().Value;
            //}
            //throw new NotImplementedException();
        }

        public static IMaybe<PathSpec> CommonWith(this PathSpec pathSpec, PathSpec that)
        {
            var path1Str = pathSpec.ToString();
            var path2Str = that.ToString();

            if (!pathSpec.Flags.HasFlag(PathFlags.CaseSensitive) || !that.Flags.HasFlag(PathFlags.CaseSensitive))
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
            return path1Str.Substring(0, i).ToPathSpec();
        }

        public static bool CanBeSimplified(this PathSpec pathSpec)
        {
            return pathSpec.Components.SkipWhile(str => str == "..").Any(str => str == "..");
        }

        public static PathSpec Simplify(this PathSpec pathSpec)
        {
            var result = new List<string>();
            var numberOfComponentsToSkip = 0;
            for (var i = pathSpec.Components.Count - 1; i >= 0; i--)
            {
                if (pathSpec.Components[i] == ".")
                    continue;
                if (pathSpec.Components[i] == "..")
                    numberOfComponentsToSkip++;
                else if (numberOfComponentsToSkip > 0)
                {
                    numberOfComponentsToSkip--;
                }
                else
                {
                    result.Insert(0, pathSpec.Components[i]);
                }
            }
            if (numberOfComponentsToSkip > 0 && !pathSpec.IsRelative())
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
                    sb.Append(pathSpec.DirectorySeparator);
            }
            var str = sb.ToString();
            if (str.Length == 0)
                str = ".";
            return str.ToPathSpec(pathSpec.Flags).Value;
        }

        public static IMaybe<PathSpec> Parent(this PathSpec pathSpec)
	    {
            return pathSpec.Components.Subset(0, -2).Select(str => PathUtility.TryParsePathSpec(str, pathSpec.Flags)).Join();
	    }

        public static bool IsAbsolute(this PathSpec pathSpec)
        {
            return pathSpec.Components.ComponentsAreAbsolute();
        }

        public static bool IsRelative(this PathSpec pathSpec)
        {
            return pathSpec.Components.ComponentsAreRelative();
        }

        internal static bool ComponentsAreAbsolute(this IReadOnlyList<string> pathSpec)
        {
            if (pathSpec[0] == "/")
                return true;
            if (char.IsLetter(pathSpec[0][0]) && pathSpec[0][1] == ':')
                return true;
            return false;
        }

        internal static bool ComponentsAreRelative(this IReadOnlyList<string> pathSpec)
        {
            if (pathSpec.ComponentsAreAbsolute())
                return false;
            if (pathSpec[0] == "\\")
                return false;
            return true;
        }

        #region Ways of combining PathSpecs

        #region String overloads

        public static IMaybe<PathSpec> Join(this IReadOnlyList<string> descendants)
        {
            return descendants.Select(str => str.ToPathSpec()).Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<string> descendants)
        {
            return descendants.ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IReadOnlyList<IMaybe<string>> descendants)
        {
            if (descendants.Any(opt => !opt.HasValue))
                return Nothing<PathSpec>();
            return descendants.Select(opt => opt.Value).Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<string>> descendants)
        {
            return descendants.ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, IEnumerable<string> descendants)
        {
            return root.Join(descendants.Select(str => str.ToPathSpec()));
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, IEnumerable<string> descendants)
        {
            if (!root.HasValue)
                return Nothing<PathSpec>();
            return root.Value.Join(descendants.Select(str => str.ToPathSpec()));
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.SelectMany(rootVal => rootVal.Join(descendants.Select(m => m.SelectMany(str => str.ToPathSpec()))));
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.Join(descendants.Select(m => m.SelectMany(str => str.ToPathSpec())));
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, params string[] descendants)
        {
            return root.Join(descendants.Select(str => str.ToPathSpec()));
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, params string[] descendants)
        {
            if (!root.HasValue)
                return Nothing<PathSpec>();
            return root.Value.Join(descendants.Select(str => str.ToPathSpec()));
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, params IMaybe<string>[] descendants)
        {
            return root.Join(descendants.Select(m => m.SelectMany(str => str.ToPathSpec())));
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, params IMaybe<string>[] descendants)
        {
            return root.Join(descendants.Select(m => m.SelectMany(str => str.ToPathSpec())));
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, IEnumerable<string> descendants)
        {
            return root.Join(descendants.Select(str => str.ToPathSpec()));
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, IEnumerable<string> descendants)
        {
            return root.Join(descendants.Select(str => str.ToPathSpec()));
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, IEnumerable<IMaybe<string>> descendants)
        {
            return root.Concat(descendants.Select(m => m.SelectMany(str => str.ToPathSpec()))).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, IEnumerable<IMaybe<string>> descendants)
        {
            return descendants
                .Select(m => m.SelectMany(str => str.ToPathSpec()))
                .AllOrNothing().Select(desc => root.Concat(desc).ToList().Join()).Flatten();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, params string[] descendants)
        {
            return root.Join(descendants.Select(str => str.ToPathSpec()));
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, params string[] descendants)
        {
            return root.Concat(descendants.Select(str => str.ToPathSpec())).AllOrNothing().Select(paths => paths.Join()).SelectMany(x => x);
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, params IMaybe<string>[] descendants)
        {
            return root.Concat(descendants.Select(m => m.SelectMany(str => str.ToPathSpec()))).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, params IMaybe<string>[] descendants)
        {
            return descendants.Select(m => m.SelectMany(str => str.ToPathSpec())).AllOrNothing().Select(desc => root.Concat(desc).ToList().Join()).Flatten();
        }

        #endregion

        #region PathSpec overloads

        public static IMaybe<PathSpec> Join(this IReadOnlyList<PathSpec> descendants)
        {
            var first = descendants[0];
            if (descendants.Skip(1).Any(c => !c.IsRelative()
                || c.DirectorySeparator != first.DirectorySeparator
                || c.Flags != first.Flags))
                return Nothing<PathSpec>();
            return Something(new PathSpec(PathUtility.GetDefaultFlagsForThisEnvironment(), first.DirectorySeparator,
                descendants.SelectMany(opt => opt.Components).Where((str, i) => i == 0 || str != ".")));
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> descendants)
        {
            return descendants.ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IReadOnlyList<IMaybe<PathSpec>> descendants)
        {
            if (descendants.Any(opt => !opt.HasValue))
                return Nothing<PathSpec>();
            return descendants.Select(opt => opt.Value).Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return descendants.ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, IEnumerable<PathSpec> descendants)
        {
            return root.ItemConcat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, IEnumerable<PathSpec> descendants)
        {
            if (!root.HasValue)
                return Nothing<PathSpec>();
            return root.Value.ItemConcat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return root.ItemConcat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return Something(root).ItemConcat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, params PathSpec[] descendants)
        {
            return root.ItemConcat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, params PathSpec[] descendants)
        {
            if (!root.HasValue)
                return Nothing<PathSpec>();
            return root.Value.ItemConcat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IMaybe<PathSpec> root, params IMaybe<PathSpec>[] descendants)
        {
            return root.ItemConcat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this PathSpec root, params IMaybe<PathSpec>[] descendants)
        {
            return Something(root).ItemConcat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, IEnumerable<PathSpec> descendants)
        {
            return root.Concat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, IEnumerable<PathSpec> descendants)
        {
            return root.AllOrNothing().Select(enumerable => enumerable.Concat(descendants).ToList().Join()).Flatten();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return root.Concat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, IEnumerable<IMaybe<PathSpec>> descendants)
        {
            return descendants.AllOrNothing().Select(desc => root.Concat(desc).ToList().Join()).Flatten();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, params PathSpec[] descendants)
        {
            return root.Concat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, params PathSpec[] descendants)
        {
            return root.AllOrNothing().Select(enumerable => enumerable.Concat(descendants).ToList().Join()).Flatten();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<IMaybe<PathSpec>> root, params IMaybe<PathSpec>[] descendants)
        {
            return root.Concat(descendants).ToList().Join();
        }

        public static IMaybe<PathSpec> Join(this IEnumerable<PathSpec> root, params IMaybe<PathSpec>[] descendants)
        {
            return descendants.AllOrNothing().Select(desc => root.Concat(desc).ToList().Join()).Flatten();
        }

        #endregion

        #endregion

        #endregion

        #region String extension methods

        public static IMaybe<PathSpec> ToPathSpec(this string path, PathFlags flags)
		{
            return PathUtility.TryParsePathSpec(path, flags);
		}

        public static IMaybe<PathSpec> ToPathSpec(this string path)
		{
			return PathUtility.TryParsePathSpec(path);
		}

		public static bool IsAbsoluteWindowsPath(this string path)
		{
			return char.IsLetter(path[0]) && path[1] == ':';
		}

		public static bool IsAbsoluteUnixPath(this string path)
		{
			return path.StartsWith("/");
		}

        /// <summary>
        /// Checks for invalid relative paths, like C:\.. (Windows) or /.. (Unix)
        /// </summary>
        internal static bool IsAncestorOfRoot(this IReadOnlyList<string> pathComponents)
        {
            var result = new List<string>();
            var numberOfComponentsToSkip = 0;
            var isRelative = pathComponents.ComponentsAreRelative();
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
    }
}
