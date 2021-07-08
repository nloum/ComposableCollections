using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humanizer;
using MoreCollections;
using SimpleMonads;
using TreeLinq;
using static SimpleMonads.Utility;

namespace IoFluently
{
    /// <summary>
    /// Represents an absolute path to a file or folder (the file or folder doesn't have to exist)
    /// </summary>
    public partial class FileOrFolderOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.Either<File, Folder, MissingPath>, IFileOrFolderOrMissingPath
    {
        private FileOrFolderOrMissingPath _path;

        /// <summary>
        /// Indicates whether or not the absolute path is case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; }
        
        /// <summary>
        /// Indicates what the directory separator is for this absolute path (e.g., '/' or '\') 
        /// </summary>
        public string DirectorySeparator { get; }
        
        /// <summary>
        /// The IIoService that is used for this absolute path
        /// </summary>
        public IIoService IoService { get; }

        /// <summary>
        /// The TreeLinq absolute path that this object represents
        /// </summary>
        public AbsoluteTreePath<string> _treePath;
        public IReadOnlyList<string> Components => _treePath.Components;

        public FileOrFolderOrMissingPath(IFileOrFolderOrMissingPath path) : this(path.IsCaseSensitive, path.DirectorySeparator, path.IoService, path.Components) {
        }
 
        internal FileOrFolderOrMissingPath(bool isCaseSensitive, string directorySeparator, IIoService ioService, IEnumerable<string> path)
        {
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
            _treePath = new AbsoluteTreePath<string>(path);
            if (!ioService.ComponentsAreAbsolute(Components))
            {
                throw new ArgumentException($"The path {_treePath} is not absolute");
            }
        }

        /// <summary>
        /// The file or directory name, a.k.a the last component in the path
        /// </summary>
        public string Name => _treePath[_treePath.Count - 1];

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            var tp = obj as FileOrFolderOrMissingPath;
            if (tp != null)
                return CompareTo(tp);
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        private IEnumerable<string> SplitComponent(string component)
        {
            // For UNC paths
            if (component.StartsWith(DirectorySeparator + DirectorySeparator))
            {
                component = component.Substring(DirectorySeparator.Length * 2);
                yield return DirectorySeparator + DirectorySeparator;
            }
            else if (component.StartsWith(DirectorySeparator))
            {
                component = component.Substring(DirectorySeparator.Length);
                yield return DirectorySeparator;
            }

            foreach (var subcomponent in component.Split(new[] {DirectorySeparator},
                StringSplitOptions.RemoveEmptyEntries)) yield return subcomponent;
        }

        /// <summary>
        /// The file extension, if there is one, including the dot.
        /// </summary>
        public IMaybe<string> Extension
        {
            get
            {
                var lastPathComponent = Name;
                var dotIndex = lastPathComponent.IndexOf('.');
                if (dotIndex < 0)
                {
                    return Nothing<string>(() => throw new InvalidOperationException($"The path {this} has no extension"));
                }

                return lastPathComponent.Substring(dotIndex).ToMaybe();
            }
        }

        private Exception ThrowWrongType(params PathType[] oneOf)
        {
            var actualType = Type;
            
            var oneOfString = oneOf.Select(x => x.ToString()).Humanize();
            
            throw new InvalidOperationException(
                $"Expected {this} to be a one of {oneOfString} but it was a {actualType} instead");
        }

        public override File? Item1 => GetFile(Type);
        public override Folder? Item2 => GetFolder(Type);
        public override MissingPath? Item3 => GetMissingPath(Type);

        private File? GetFile(PathType type)
        {
            return type == PathType.File ? new File(this) : null;
        }
        
        private Folder? GetFolder(PathType type)
        {
            return type == PathType.Folder ? new Folder(this) : null;
        }
        
        private MissingPath? GetMissingPath(PathType type)
        {
            return type == PathType.MissingPath ? new MissingPath(this) : null;
        }
        
        public override TOutput Collapse<TOutput>(Func<File, TOutput> selector1, Func<Folder, TOutput> selector2, Func<MissingPath, TOutput> selector3)
        {
            var type = Type;
            switch (type)
            {
                case PathType.File:
                    return selector1(new File(this));
                case PathType.Folder:
                    return selector2(new Folder(this));
                case PathType.MissingPath:
                    return selector3(new MissingPath(this));
                default:
                    throw new InvalidOperationException($"Unknown path type {type}");
            }
        }

        public File ExpectFile()
        {
            return Collapse(
                file => file,
                folder => throw ThrowWrongType(PathType.File),
                missingPath => throw ThrowWrongType(PathType.File));
        }

        public FileOrFolder ExpectFileOrFolder()
        {
            return Collapse(
                file => new FileOrFolder(file),
                folder => new FileOrFolder(folder),
                missingPath => throw ThrowWrongType(PathType.File, PathType.MissingPath));
        }

        public FileOrMissingPath ExpectFileOrMissingPath()
        {
            return Collapse(
                file => new FileOrMissingPath((IFileOrFolderOrMissingPath)file),
                folder =>
                {
                    if (IoService.CanEmptyDirectoriesExist)
                    {
                        throw ThrowWrongType(PathType.File, PathType.MissingPath);
                    }

                    return new FileOrMissingPath((IFileOrFolderOrMissingPath)new MissingPath(folder));
                },
                missingPath => new FileOrMissingPath((IFileOrFolderOrMissingPath)missingPath));
        }

        public Folder ExpectFolder()
        {
            return Collapse(
                file => throw ThrowWrongType(PathType.Folder),
                folder => folder,
                missingPath => throw ThrowWrongType(PathType.Folder));
        }

        public FolderOrMissingPath ExpectFolderOrMissingPath()
        {
            return Collapse(
                file => throw ThrowWrongType(PathType.Folder, PathType.MissingPath),
                folder => new FolderOrMissingPath(folder),
                missingPath => new FolderOrMissingPath(missingPath));
        }

        public MissingPath ExpectMissingPath()
        {
            return Collapse(
                file => throw ThrowWrongType(PathType.MissingPath),
                folder => throw ThrowWrongType(PathType.MissingPath),
                missingPath => missingPath);
        }
        
        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (_treePath != null ? _treePath.GetHashCode() : 0);
        }

        /// <inheritdoc />
        public int CompareTo(FileOrFolderOrMissingPath other)
        {
            var compareCounts = _treePath.Count - other.Components.Count;
            if (compareCounts != 0)
                return compareCounts;
            for (var i = 0; i < _treePath.Count; i++)
            {
                var compareElement = _treePath[i].CompareTo(other.Components[i]);
                if (compareElement != 0)
                    return compareElement;
            }

            return 0;
        }

        /// <inheritdoc />
        public bool Equals(FileOrFolderOrMissingPath other)
        {
            return Equals(_treePath, other);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _treePath.Count; i++)
            {
                sb.Append(_treePath[i]);
                if (_treePath[i] != DirectorySeparator && i + 1 != _treePath.Count && sb.ToString() != DirectorySeparator)
                    sb.Append(DirectorySeparator);
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Converts this AbsolutePath to a string form of the path
        /// </summary>
        /// <param name="path">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(FileOrFolderOrMissingPath path)  
        {  
            return path.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FileOrFolderOrMissingPath) obj);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FileOrFolderOrMissingPath operator / (FileOrFolderOrMissingPath absPath, string whatToAdd)
        {
            if (string.IsNullOrEmpty(whatToAdd))
            {
                return absPath;
            }
            
            return new FileOrFolderOrMissingPath(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Components.Concat(whatToAdd.Split('/', '\\')));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (FileOrFolderOrMissingPath absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, (absPath / whatToAdd).Paths);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (FileOrFolderOrMissingPath absPath, Func<FileOrFolderOrMissingPath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath._treePath / (x => whatToAdd(new FileOrFolderOrMissingPath(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, x.Components)).Select(x => x.Path)));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FileOrFolderOrMissingPath operator / (FileOrFolderOrMissingPath absPath, RelativePath whatToAdd)
        {
            if (whatToAdd == null)
            {
                return absPath;
            }

            return new FileOrFolderOrMissingPath(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Components.Concat(whatToAdd.Path));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (FileOrFolderOrMissingPath absPath, IEnumerable<string> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath._treePath / whatToAdd.Select(x => new RelativeTreePath<string>(x.Split('/', '\\'))));
        }

        /// <summary>
        /// Uses the AbsolutePath.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for equality</param>
        /// <param name="right">The second object to check for equality</param>
        /// <returns>True if the two objects are equal; false otherwise</returns>
        public static bool operator ==(FileOrFolderOrMissingPath left, FileOrFolderOrMissingPath right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Uses the AbsolutePath.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for inequality</param>
        /// <param name="right">The second object to check for inequality</param>
        /// <returns>False if the two objects are equal; true otherwise</returns>
        public static bool operator !=(FileOrFolderOrMissingPath left, FileOrFolderOrMissingPath right)
        {
            return !Equals(left, right);
        }
    }
}