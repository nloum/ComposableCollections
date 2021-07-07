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
    public partial class AbsolutePath : SubTypesOf<IHasAbsolutePath>.Either<File, Folder, MissingPath>, IComparable, IComparable<AbsolutePath>, IEquatable<AbsolutePath>, IHasAbsolutePath
    {
        private AbsolutePath _path;

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
        public AbsoluteTreePath<string> Path { get; }

        AbsolutePath IHasAbsolutePath.Path => this;

        internal AbsolutePath(bool isCaseSensitive, string directorySeparator, IIoService ioService, IEnumerable<string> path)
        {
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
            Path = new AbsoluteTreePath<string>(path);
            if (!ioService.ComponentsAreAbsolute(Path.Components))
            {
                throw new ArgumentException($"The path {Path} is not absolute");
            }
        }

        /// <summary>
        /// Returns the files and folders that this folder contains, but not anything else. This will not return files or folders
        /// that are nested deeper. For example, if this folder contains a folder called Level1, and Level1 contains another
        /// folder called Level2, then this method will return only Level1, not Level2 or anything else in Level1.
        /// </summary>
        /// <param name="pattern">The string pattern that files or folders must match to be included in the return value.
        /// If this is null, then all files and folders in this folder are returned.</param>
        /// <returns>An object representing the children files and folders of this folder.</returns>
        public AbsolutePathChildren Children(string pattern = null) => new AbsolutePathChildren(this, pattern, IoService);
        
        /// <summary>
        /// Returns the files and folders that this folder contains, and the files and folders that they contain, etc.
        /// This will return ALL files and folders that are nested deeper as well. For example, if this folder contains
        /// a folder called Level1, and Level1 contains another folder called Level2, then this method will return both
        /// Level1, Level2, and anything else in Level1.
        /// </summary>
        /// <param name="pattern">The string pattern that files or folders must match to be included in the return value.
        /// If this is null, then all files and folders in this folder are returned.</param>
        /// <returns>An object representing the descendant files and folders of this folder.</returns>
        public AbsolutePathDescendants Descendants(string pattern = null) => new AbsolutePathDescendants(this, pattern, IoService);
        
        /// <summary>
        /// The file or directory name, a.k.a the last component in the path
        /// </summary>
        public string Name => Path[Path.Count - 1];

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            var tp = obj as AbsolutePath;
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
                file => new FileOrMissingPath(file),
                folder => throw ThrowWrongType(PathType.File, PathType.MissingPath),
                missingPath => new FileOrMissingPath(missingPath));
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
            return (Path != null ? Path.GetHashCode() : 0);
        }

        /// <inheritdoc />
        public int CompareTo(AbsolutePath other)
        {
            var compareCounts = Path.Count - other.Path.Count;
            if (compareCounts != 0)
                return compareCounts;
            for (var i = 0; i < Path.Count; i++)
            {
                var compareElement = Path[i].CompareTo(other.Path[i]);
                if (compareElement != 0)
                    return compareElement;
            }

            return 0;
        }

        /// <inheritdoc />
        public bool Equals(AbsolutePath other)
        {
            return Equals(Path, other.Path);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Path.Count; i++)
            {
                sb.Append(Path[i]);
                if (Path[i] != DirectorySeparator && i + 1 != Path.Count && sb.ToString() != DirectorySeparator)
                    sb.Append(DirectorySeparator);
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Converts this AbsolutePath to a string form of the path
        /// </summary>
        /// <param name="path">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(AbsolutePath path)  
        {  
            return path.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AbsolutePath) obj);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator / (AbsolutePath absPath, string whatToAdd)
        {
            if (string.IsNullOrEmpty(whatToAdd))
            {
                return absPath;
            }
            
            return new AbsolutePath(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Path / new RelativeTreePath<string>(whatToAdd.Split('/', '\\')));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (AbsolutePath absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Path / whatToAdd.Select(x => x.Path));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (AbsolutePath absPath, Func<AbsolutePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Path / (x => whatToAdd(new AbsolutePath(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, x)).Select(y => y.Path)));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator / (AbsolutePath absPath, RelativePath whatToAdd)
        {
            if (whatToAdd == null)
            {
                return absPath;
            }

            return new AbsolutePath(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Path / whatToAdd.Path);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (AbsolutePath absPath, IEnumerable<string> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Path / whatToAdd.Select(x => new RelativeTreePath<string>(x.Split('/', '\\'))));
        }

        /// <summary>
        /// Uses the AbsolutePath.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for equality</param>
        /// <param name="right">The second object to check for equality</param>
        /// <returns>True if the two objects are equal; false otherwise</returns>
        public static bool operator ==(AbsolutePath left, AbsolutePath right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Uses the AbsolutePath.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for inequality</param>
        /// <param name="right">The second object to check for inequality</param>
        /// <returns>False if the two objects are equal; true otherwise</returns>
        public static bool operator !=(AbsolutePath left, AbsolutePath right)
        {
            return !Equals(left, right);
        }
    }
}