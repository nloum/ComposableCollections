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
    public class FileOrFolderOrMissingPath : FileOrFolderOrMissingPath<File, Folder, MissingPath>, IFileOrFolderOrMissingPath
    {
        public FileOrFolderOrMissingPath(IAbsolutePath path) : base(path)
        {
        }

        public FileOrFolderOrMissingPath(File item1) : base(item1)
        {
        }

        public FileOrFolderOrMissingPath(Folder item2) : base(item2)
        {
        }

        public FileOrFolderOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        internal FileOrFolderOrMissingPath(bool isCaseSensitive, string directorySeparator, IIoService ioService, IEnumerable<string> path) : base(isCaseSensitive, directorySeparator, ioService, path)
        {
        }
    }
    
    /// <summary>
    /// Represents an absolute path to a file or folder (the file or folder doesn't have to exist)
    /// </summary>
    public partial class FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> : Either<TFile, TFolder, TMissingPath>, IFileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>,
        IComparable<FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>>, IEquatable<FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>>
        where TFile : File
        where TFolder : Folder
        where TMissingPath : MissingPath
    {
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

        public FileOrFolderOrMissingPath(IAbsolutePath path) : this(path.IsCaseSensitive, path.DirectorySeparator, path.IoService, path.Components) {
        }

        public FileOrFolderOrMissingPath(TFile item1)
            : this(item1.IsCaseSensitive, item1.DirectorySeparator, item1.IoService, item1.Components)
        {
        }

        public FileOrFolderOrMissingPath(TFolder item2)
            : this(item2.IsCaseSensitive, item2.DirectorySeparator, item2.IoService, item2.Components)
        {
        }

        public FileOrFolderOrMissingPath(TMissingPath item3)
            : this(item3.IsCaseSensitive, item3.DirectorySeparator, item3.IoService, item3.Components)
        {
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
            var tp = obj as FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>;
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

        public override TFile? Item1 => GetFile(Type);
        public override TFolder? Item2 => GetFolder(Type);
        public override TMissingPath? Item3 => GetMissingPath(Type);

        private TFile? GetFile(PathType type)
        {
            return type == PathType.File ? IoService.CreateFileObject<File>(this) : null;
        }
        
        private TFolder? GetFolder(PathType type)
        {
            return type == PathType.Folder ? IoService.CreateFolderObject<Folder>(this) : null;
        }
        
        private TMissingPath? GetMissingPath(PathType type)
        {
            return type == PathType.MissingPath ? IoService.CreateMissingPathObject<MissingPath>(this) : null;
        }
        
        public override TOutput Collapse<TOutput>(Func<TFile, TOutput> selector1, Func<TFolder, TOutput> selector2, Func<TMissingPath, TOutput> selector3)
        {
            var type = Type;
            switch (type)
            {
                case PathType.File:
                    return selector1(GetFile(type));
                case PathType.Folder:
                    return selector2(GetFolder(type));
                case PathType.MissingPath:
                    return selector3(GetMissingPath(type));
                default:
                    throw new InvalidOperationException($"Unknown path type {type}");
            }
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (_treePath != null ? _treePath.GetHashCode() : 0);
        }

        /// <inheritdoc />
        public int CompareTo(FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> other)
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
        public bool Equals(FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> other)
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
        public static implicit operator string(FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> path)  
        {  
            return path.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>) obj);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> operator / (FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> absPath, string whatToAdd)
        {
            if (string.IsNullOrEmpty(whatToAdd))
            {
                return absPath;
            }
            
            return new FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Components.Concat(whatToAdd.Split('/', '\\')));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, (absPath / whatToAdd).Paths);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> absPath, Func<FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>, IEnumerable<RelativePath>> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath._treePath / (x => whatToAdd(new FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, x.Components)).Select(x => x.Path)));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> operator / (FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> absPath, RelativePath whatToAdd)
        {
            if (whatToAdd == null)
            {
                return absPath;
            }

            return new FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath>(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath.Components.Concat(whatToAdd.Path));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> absPath, IEnumerable<string> whatToAdd)
        {
            return new AbsolutePaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.IoService, absPath._treePath / whatToAdd.Select(x => new RelativeTreePath<string>(x.Split('/', '\\'))));
        }

        /// <summary>
        /// Uses the AbsolutePath.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for equality</param>
        /// <param name="right">The second object to check for equality</param>
        /// <returns>True if the two objects are equal; false otherwise</returns>
        public static bool operator ==(FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> left, FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Uses the AbsolutePath.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for inequality</param>
        /// <param name="right">The second object to check for inequality</param>
        /// <returns>False if the two objects are equal; true otherwise</returns>
        public static bool operator !=(FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> left, FileOrFolderOrMissingPath<TFile, TFolder, TMissingPath> right)
        {
            return !Equals(left, right);
        }
    }
}