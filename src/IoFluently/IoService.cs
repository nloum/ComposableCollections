using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Security;
using System.Threading.Tasks;
using LiveLinq.Set;
using ReactiveProcesses;
using SimpleMonads;

namespace IoFluently
{
    public class IoService : IoServiceBase
    {
        public IoService(IReactiveProcessFactory reactiveProcessFactory) : base(reactiveProcessFactory, Environment.NewLine)
        {
            
        }
        
        public override IMaybe<StreamWriter> TryCreateText(AbsolutePath pathSpec)
        {
            try
            {
                return new Maybe<StreamWriter>(AsFileInfo(pathSpec).CreateText());
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

        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.TryParent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<Stream>(AsFileInfo(path).Open(fileMode, fileAccess, fileShare));
            }
            catch (IOException)
            {
                return Maybe<Stream>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<Stream>.Nothing;
            }
        }

        public override IMaybe<FileAttributes> TryAttributes(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<FileAttributes>(AsFileInfo(attributes).Attributes);
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

        public override IMaybe<DateTimeOffset> TryCreationTime(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<DateTimeOffset>(AsFileInfo(attributes).CreationTime);
            }
            catch (IOException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
        }

        public override IMaybe<DateTimeOffset> TryLastAccessTime(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<DateTimeOffset>(AsFileInfo(attributes).LastAccessTime);
            }
            catch (IOException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
        }

        public override IMaybe<DateTimeOffset> TryLastWriteTime(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<DateTimeOffset>(AsFileInfo(attributes).LastWriteTime);
            }
            catch (IOException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
            catch (PlatformNotSupportedException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
            catch (ArgumentOutOfRangeException)
            {
                return Maybe<DateTimeOffset>.Nothing;
            }
        }

        public override IMaybe<string> TryFullName(AbsolutePath attributes)
        {
            try
            {
                return new Maybe<string>(AsFileInfo(attributes).FullName);
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
        
        public override IMaybe<long> TryLength(AbsolutePath path)
        {
            try
            {
                return new Maybe<long>(AsFileInfo(path).Length);
            }
            catch (IOException)
            {
                return Maybe<long>.Nothing;
            }
        }

        public override IMaybe<bool> TryIsReadOnly(AbsolutePath path)
        {
            try
            {
                return new Maybe<bool>(AsFileInfo(path).IsReadOnly);
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

        private FileInfo AsFileInfo(AbsolutePath path)
        {
            return new FileInfo(path.ToString());
        }

        private DirectoryInfo AsDirectoryInfo(AbsolutePath path)
        {
            return new DirectoryInfo(path.ToString());
        }

        public override AbsolutePath DeleteFile(AbsolutePath path)
        {
            if (path.GetPathType() == PathType.None)
                return path;
            try
            {
                AsFileInfo(path).Delete();
            }
            catch (IOException)
            {
                AsFileInfo(path).Delete();
            }
            catch (UnauthorizedAccessException)
            {
                AsFileInfo(path).Delete();
            }

            return path;
        }

        public override AbsolutePath Decrypt(AbsolutePath path)
        {
            AsFileInfo(path).Decrypt();
            return path;
        }

        public override AbsolutePath Encrypt(AbsolutePath path)
        {
            AsFileInfo(path).Encrypt();
            return path;
        }

        public override IReadOnlyObservableSet<AbsolutePath> Storage => _storage;

        private readonly ObservableSet<AbsolutePath> _storage = new ObservableSet<AbsolutePath>();

        public override IEnumerable<AbsolutePath> EnumerateChildren(AbsolutePath path, bool includeFolders = true, bool includeFiles = true)
        {
            if (!path.IsFolder()) return ImmutableArray<AbsolutePath>.Empty;

            var fullName = AsDirectoryInfo(path).FullName;

            if (includeFiles && includeFolders)
                return Directory.GetFileSystemEntries(fullName).Select(x => ParseAbsolutePath(x));

            if (includeFiles) return Directory.GetFiles(fullName).Select(x => ParseAbsolutePath(x));

            if (includeFolders)
                return Directory.GetDirectories(fullName).Select(x => ParseAbsolutePath(x));

            return ImmutableArray<AbsolutePath>.Empty;
        }

        public override void UpdateStorage()
        {
            var currentStorage = Directory.GetLogicalDrives();
            foreach (var drive in currentStorage)
            {
                var drivePath = TryParseAbsolutePath(drive).Value;
                if (!_storage.Contains(drivePath))
                    _storage.Add(drivePath);
            }

            var drivesThatWereRemoved = new List<AbsolutePath>();

            foreach (var drive in _storage)
                if (!currentStorage.Contains(drive + "\\"))
                    drivesThatWereRemoved.Add(drive);

            foreach (var driveThatWasRemoved in drivesThatWereRemoved) _storage.Remove(driveThatWasRemoved);
        }

        public override IObservable<AbsolutePath> Renamings(AbsolutePath path)
        {
            var parent = path.TryParent();
            if (!parent.HasValue) return Observable.Return(path);

            return Observable.Create<AbsolutePath>(
                async (observer, token) =>
                {
                    var currentPath = path;
                    while (!token.IsCancellationRequested)
                    {
                        var watcher = new FileSystemWatcher(currentPath.Parent().ToString())
                        {
                            IncludeSubdirectories = false,
                            Filter = currentPath.Name
                        };

                        var tcs = new TaskCompletionSource<AbsolutePath>();

                        RenamedEventHandler handler = (_, args) =>
                        {
                            tcs.SetResult(new AbsolutePath(path.Flags, path.DirectorySeparator, this, new[]{args.FullPath}));
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

        public override PathFlags GetDefaultFlagsForThisEnvironment()
        {
            lock (_lock)
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
        
        public override AbsolutePath CreateFolder(AbsolutePath path)
        {
            try
            {
                if (path.GetPathType() == PathType.Folder)
                    return path;
                path.TryParent().IfHasValue(parent => parent.CreateFolder());
                AsDirectoryInfo(path).Create();
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

        public override void WriteAllText(AbsolutePath path, string text)
        {
            File.WriteAllText(path.ToString(), text);
        }

        public override void WriteAllLines(AbsolutePath path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path.ToString(), lines);
        }

        public override void WriteAllBytes(AbsolutePath path, byte[] bytes)
        {
            File.WriteAllBytes(path.ToString(), bytes);
        }

        public override IEnumerable<string> ReadLines(AbsolutePath path)
        {
            return File.ReadLines(path.ToString());
        }

        public override string ReadAllText(AbsolutePath path)
        {
            return File.ReadAllText(path.ToString());
        }

        public override IMaybe<Stream> TryOpen(AbsolutePath path, FileMode fileMode,
            FileAccess fileAccess)
        {
            try
            {
                if (MayCreateFile(fileMode))
                    path.TryParent().IfHasValue(parent => parent.Create(PathType.Folder));
                return new Maybe<Stream>(AsFileInfo(path).Open(fileMode, fileAccess));
            }
            catch (IOException)
            {
                return Maybe<Stream>.Nothing;
            }
            catch (UnauthorizedAccessException)
            {
                return Maybe<Stream>.Nothing;
            }
        }

        public override void Create(AbsolutePath path, PathType pathType)
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

        public override AbsolutePath DeleteFolder(AbsolutePath path, bool recursive = false)
        {
            Directory.Delete(path.ToString(), recursive);

            return path;
        }

        public override PathType GetPathType(AbsolutePath path)
        {
            var str = path.ToString();
            if (File.Exists(str))
                return PathType.File;
            if (Directory.Exists(str))
                return PathType.Folder;
            return PathType.None;
        }

        public override IAbsolutePathTranslation CopyFile(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            if (translation.Destination.Exists())
            {
                if (!overwrite)
                {
                    throw new IOException($"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" without overwriting the destination, but the destination already exists");
                }
                else
                {
                    translation.Destination.Delete();
                }
            }
            if (translation.Source.GetPathType() != PathType.File)
                throw new IOException(string.Format(
                    "An attempt was made to copy a file from \"{translation.Source}\" to \"{translation.Destination}\" but the source path is not a file."));
            translation.Destination.TryParent().Value.Create(PathType.Folder);
            File.Copy(translation.Source.ToString(), translation.Destination.ToString());
            return translation;
        }

        public override IAbsolutePathTranslation MoveFile(IAbsolutePathTranslation translation, bool overwrite = false)
        {
            if (translation.Destination.Exists())
            {
                if (!overwrite)
                {
                    throw new IOException($"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" without overwriting the destination, but the destination already exists");
                }
                else
                {
                    translation.Destination.Delete();
                }
            }
            if (translation.Source.GetPathType() != PathType.File)
                throw new IOException(string.Format(
                    $"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" but the source path is not a file."));
            if (translation.Destination.GetPathType() != PathType.None)
                throw new IOException(string.Format(
                    $"An attempt was made to move \"{translation.Source}\" to \"{translation.Destination}\" but the destination path exists."));
            if (translation.Destination.IsDescendantOf(translation.Source))
                throw new IOException(string.Format(
                    $"An attempt was made to move a file from \"{translation.Source}\" to \"{translation.Destination}\" but the destination path is a sub-path of the source path."));
            translation.Destination.TryParent().Value.Create(PathType.Folder);
            File.Move(translation.Source.ToString(), translation.Destination.ToString());
            return translation;
        }
    }
}