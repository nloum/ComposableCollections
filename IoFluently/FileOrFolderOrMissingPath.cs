using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using SimpleMonads;
using TreeLinq;

namespace IoFluently
{
    public partial class FileOrFolderOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.Either<IFilePath, IFolderPath, IMissingPath>, IFileOrFolderOrMissingPath
    {
        private readonly AbsoluteTreePath<string> _treePath;
        public IReadOnlyList<string> Components => _treePath.Components;
        public bool IsCaseSensitive { get; }
        public string DirectorySeparator { get; }
        public IFileSystem FileSystem { get; }

        public FileOrFolderOrMissingPathAncestors Ancestors => new(this);
        public FolderPath Root => Ancestors.Count > 0 ? Ancestors[0].ExpectFolder() : this.ExpectFolder();

        public IMaybe<FileOrFolderOrMissingPath> Parent => Ancestors.Count > 0
            ? Ancestors[Ancestors.Count - 1].ToMaybe()
            : Maybe<FileOrFolderOrMissingPath>.Nothing();
        
        public Boolean CanBeSimplified => FileSystem.CanBeSimplified(this);
        public Boolean Exists => FileSystem.Exists(this);
        public string Extension => FileSystem.Extension(this);
        public Boolean HasExtension => FileSystem.HasExtension(this);
        public Boolean IsFile => FileSystem.IsFile(this);
        public Boolean IsFolder => FileSystem.IsFolder(this);
        public string Name => FileSystem.Name(this);
        public PathType Type => FileSystem.Type(this);
        public FileOrFolderOrMissingPath WithoutExtension => FileSystem.WithoutExtension(this);

        public FileOrFolderOrMissingPath(IReadOnlyList<string> components, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem)
        {
            _treePath = new AbsoluteTreePath<string>(components);
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            FileSystem = fileSystem;
        }
        
        public FileOrFolderOrMissingPath(IFileOrFolderOrMissingPath fileOrFolderOrMissingPath) : this(fileOrFolderOrMissingPath.Components,
            fileOrFolderOrMissingPath.IsCaseSensitive, fileOrFolderOrMissingPath.DirectorySeparator, fileOrFolderOrMissingPath.FileSystem)
        {
        }

        public FileAttributes Attributes
        {
            get => FileSystem.GetAttributes(this);
            set => FileSystem.SetAttributes(this, value);
        }

        public override IFilePath? Item1 => FileSystem.Type(this) == PathType.File
            ? new FilePath(Components, IsCaseSensitive, DirectorySeparator, FileSystem) : null;
        public override IFolderPath? Item2 => FileSystem.Type(this) == PathType.Folder
            ? new FolderPath(Components, IsCaseSensitive, DirectorySeparator, FileSystem) : null;
        public override IMissingPath? Item3 => FileSystem.Type(this) == PathType.MissingPath
            ? new MissingPath(Components, IsCaseSensitive, DirectorySeparator, FileSystem) : null;
        
        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (_treePath != null ? _treePath.GetHashCode() : 0);
        }

        /// <inheritdoc />
        public int CompareTo(FileOrFolderOrMissingPath other)
        {
            var compareCounts = Components.Count - other.Components.Count;
            if (compareCounts != 0)
                return compareCounts;
            for (var i = 0; i < Components.Count; i++)
            {
                var compareElement = Components[i].CompareTo(other.Components[i]);
                if (compareElement != 0)
                    return compareElement;
            }

            return 0;
        }

        /// <inheritdoc />
        public bool Equals(FileOrFolderOrMissingPath other)
        {
            return CompareTo(other) == 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ConvertToString();
        }

        /// <inheritdoc />
        public string FullName => this.ConvertToString();

        /// <summary>
        /// Converts this AbsolutePath to a string form of the path
        /// </summary>
        /// <param name="path">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(FileOrFolderOrMissingPath path)
        {
            return path.FullName;
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
        public static FileOrFolderOrMissingPath operator /(FileOrFolderOrMissingPath absPath, string whatToAdd)
        {
            if (string.IsNullOrEmpty(whatToAdd))
            {
                return absPath;
            }

            return new FileOrFolderOrMissingPath(absPath.Components.Concat(whatToAdd.Split('/', '\\')).ToImmutableList(), absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator /(FileOrFolderOrMissingPath absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new FilesOrFoldersOrMissingPaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem,
                new AbsoluteTreePaths<string>(absPath.Components.Select(component => 
                    new Either<string, IEnumerable<RelativeTreePath<string>>, Func<AbsoluteTreePath<string>, IEnumerable<RelativeTreePath<string>>>>(component))
                .Concat<IEither<string, IEnumerable<RelativeTreePath<string>>, Func<AbsoluteTreePath<string>, IEnumerable<RelativeTreePath<string>>>>>(whatToAdd.Select(component => 
                    new Either<string, IEnumerable<RelativeTreePath<string>>, Func<AbsoluteTreePath<string>, IEnumerable<RelativeTreePath<string>>>>(component)))));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator /(FileOrFolderOrMissingPath absPath,
            Func<FileOrFolderOrMissingPath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new FilesOrFoldersOrMissingPaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem,
                absPath._treePath / (x =>
                    whatToAdd(new FileOrFolderOrMissingPath(x.Components, absPath.IsCaseSensitive, absPath.DirectorySeparator,
                        absPath.FileSystem)).Select(x => x.Path)));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FileOrFolderOrMissingPath operator /(FileOrFolderOrMissingPath absPath, RelativePath whatToAdd)
        {
            if (whatToAdd == null)
            {
                return absPath;
            }

            return new FileOrFolderOrMissingPath(absPath.Components.Concat(whatToAdd.Path).ToImmutableList(), absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator /(FileOrFolderOrMissingPath absPath, IEnumerable<string> whatToAdd)
        {
            return new FilesOrFoldersOrMissingPaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem,
                absPath._treePath / whatToAdd.Select(x => new RelativeTreePath<string>(x.Split('/', '\\'))));
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