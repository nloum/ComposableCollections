using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenericNumbers.Relational;
using TreeLinq;

namespace IoFluently
{
    public class FilesOrFoldersOrMissingPaths : IComparable, IComparable<FilesOrFoldersOrMissingPaths>, IEquatable<FilesOrFoldersOrMissingPaths>, IEnumerable<FileOrFolderOrMissingPath>
    {
        /// <summary>
        /// Indicates whether or not the absolute paths are case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; }
        
        /// <summary>
        /// Indicates what the directory separator is for the absolute paths (e.g., '/' or '\') 
        /// </summary>
        public string DirectorySeparator { get; }
        
        /// <summary>
        /// The IIoService that is used for all these absolute paths
        /// </summary>
        public IFileSystem FileSystem { get; }
        
        /// <summary>
        /// The TreeLinq absolute paths that this object represents
        /// </summary>
        public AbsoluteTreePaths<string> Paths { get; }

        internal FilesOrFoldersOrMissingPaths(bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem, AbsoluteTreePaths<string> paths)
        {
            IsCaseSensitive = isCaseSensitive;
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            FileSystem = fileSystem;
            Paths = paths;
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is FilesOrFoldersOrMissingPaths absolutePaths)
            {
                return Paths.CompareTo(absolutePaths.Paths);
            }

            return 0;
        }

        /// <inheritdoc />
        public int CompareTo(FilesOrFoldersOrMissingPaths other)
        {
            return Paths.CompareTo(other.Paths);
        }

        public bool Equals(FilesOrFoldersOrMissingPaths other)
        {
            return CompareTo(other) == 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<FileOrFolderOrMissingPath> GetEnumerator()
        {
            foreach (var path in Paths)
            {
                yield return new FileOrFolderOrMissingPath(path, IsCaseSensitive, DirectorySeparator, FileSystem);
            }
        }

        /// <summary>
        /// Adds a subpath to all the absolute paths
        /// </summary>
        /// <param name="relPath">The absolute paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the absolute paths</param>
        /// <returns>A new AbsolutePaths object where all absolute paths in it has an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator / (FilesOrFoldersOrMissingPaths absPath, string whatToAdd)
        {
            return new FilesOrFoldersOrMissingPaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem, absPath.Paths / whatToAdd);
        }

        /// <summary>
        /// Adds a subpath to all the absolute paths
        /// </summary>
        /// <param name="relPath">The absolute paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the absolute paths</param>
        /// <returns>A new AbsolutePaths object where all absolute paths in it has an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator / (FilesOrFoldersOrMissingPaths absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new FilesOrFoldersOrMissingPaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem, absPath.Paths / whatToAdd.Select(x => x.Path));
        }

        /// <summary>
        /// Adds a subpath to all the absolute paths
        /// </summary>
        /// <param name="relPath">The absolute paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the absolute paths</param>
        /// <returns>A new AbsolutePaths object where all absolute paths in it has an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator / (FilesOrFoldersOrMissingPaths absPath, RelativePath whatToAdd)
        {
            return new FilesOrFoldersOrMissingPaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem, absPath.Paths / whatToAdd.Path);
        }

        /// <summary>
        /// Adds a subpath to all the absolute paths
        /// </summary>
        /// <param name="relPath">The absolute paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the absolute paths</param>
        /// <returns>A new AbsolutePaths object where all absolute paths in it has an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator / (FilesOrFoldersOrMissingPaths absPath, IEnumerable<string> whatToAdd)
        {
            return new FilesOrFoldersOrMissingPaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem, absPath.Paths / whatToAdd);
        }

        /// <summary>
        /// Adds a subpath to all the absolute paths
        /// </summary>
        /// <param name="relPath">The absolute paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the absolute paths</param>
        /// <returns>A new AbsolutePaths object where all absolute paths in it has an additional subpath appended to it</returns>
        public static FilesOrFoldersOrMissingPaths operator / (FilesOrFoldersOrMissingPaths absPath,
            Func<FileOrFolderOrMissingPath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new FilesOrFoldersOrMissingPaths(absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem, absPath.Paths / (abs => whatToAdd(new FileOrFolderOrMissingPath(abs, absPath.IsCaseSensitive, absPath.DirectorySeparator, absPath.FileSystem)).Select(x => x.Path)));
        }
        
        /// <summary>
        /// Uses the AbsolutePaths.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for equality</param>
        /// <param name="right">The second object to check for equality</param>
        /// <returns>True if the two objects are equal; false otherwise</returns>
        public static bool operator ==(FilesOrFoldersOrMissingPaths left, FilesOrFoldersOrMissingPaths right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Uses the AbsolutePaths.Equals method to compare equality between the two AbsolutePaths
        /// </summary>
        /// <param name="left">The first object to check for inequality</param>
        /// <param name="right">The second object to check for inequality</param>
        /// <returns>False if the two objects are equal; true otherwise</returns>
        public static bool operator !=(FilesOrFoldersOrMissingPaths left, FilesOrFoldersOrMissingPaths right)
        {
            return !Equals(left, right);
        }
    }
}