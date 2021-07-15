using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreCollections;
using SimpleMonads;
using TreeLinq;
using static SimpleMonads.Utility;

namespace IoFluently
{
    /// <summary>
    /// Represents a set of relative paths
    /// </summary>
    public class RelativePaths : IComparable, IComparable<RelativePaths>, IEquatable<RelativePaths>, IEnumerable<RelativePath>
    {
        /// <summary>
        /// Indicates whether or not the relative paths are case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; }
        
        /// <summary>
        /// Indicates what the directory separator is for the relative paths (e.g., '/' or '\') 
        /// </summary>
        public string DirectorySeparator { get; }
        
        /// <summary>
        /// The IIoService that is used for all these relative paths
        /// </summary>
        public IFileSystem FileSystem { get; }
        
        /// <summary>
        /// The TreeLinq relative paths that this object represents
        /// </summary>
        public RelativeTreePaths<string> Paths { get; }

        /// <summary>
        /// Constructs a new object that represents a set of relative paths
        /// </summary>
        /// <param name="isCaseSensitive">Whether the relative paths are case sensitive</param>
        /// <param name="directorySeparator">What the directory separator is for the relative paths (e.g. '/' or '\')</param>
        /// <param name="fileSystem">The IIoService used for these relative paths</param>
        /// <param name="paths">The TreeLinq paths that this object will represent</param>
        public RelativePaths(RelativeTreePaths<string> paths, bool isCaseSensitive, string directorySeparator, IFileSystem fileSystem)
        {
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            FileSystem = fileSystem;
            Paths = paths;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<RelativePath> GetEnumerator()
        {
            foreach (var path in Paths)
            {
                yield return new RelativePath(path, IsCaseSensitive, DirectorySeparator, FileSystem);
            }
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        /// <inheritdoc />
        public int CompareTo(RelativePaths other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }

        /// <inheritdoc />
        public bool Equals(RelativePaths other)
        {
            return CompareTo(other) == 0;
        }

        private static void ValidateFlags(CaseSensitivityMode flags)
        {
            if (flags.HasFlag(CaseSensitivityMode.UseDefaultsForGivenPath))
                throw new ArgumentException("A path cannot have the UseDefaultsForGivenPath flag set.");
            if (flags.HasFlag(CaseSensitivityMode.UseDefaultsFromEnvironment))
                throw new ArgumentException("A path cannot have the UseDefaultsFromUtility flag set.");
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (IsCaseSensitive)
            {
                return ToString().GetHashCode();
            }
            
            return ToString().ToLower().GetHashCode();
        }

        /// <summary>
        /// Checks this object and the specified object for equality
        /// </summary>
        /// <param name="other">The object that this object will be compared to</param>
        /// <returns>True if the objects are equal; false otherwise</returns>
        public bool Equals(FileOrFolderOrMissingPath other)
        {
            return GetHashCode().Equals(other.GetHashCode());
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FileOrFolderOrMissingPath) obj);
        }

        /// <summary>
        /// Adds a subpath to all the relative paths
        /// </summary>
        /// <param name="relPath">The relative paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the relative paths</param>
        /// <returns>A new RelativePaths object where all relative paths in it has an additional subpath appended to it</returns>
        public static RelativePaths operator / (RelativePaths relPath, string whatToAdd)
        {
            return new RelativePaths(relPath.Paths / whatToAdd, relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.FileSystem);
        }

        /// <summary>
        /// Adds a subpath to all the relative paths
        /// </summary>
        /// <param name="relPath">The relative paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the relative paths</param>
        /// <returns>A new RelativePaths object where all relative paths in it has an additional subpath appended to it</returns>
        public static RelativePaths operator / (RelativePaths relPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new RelativePaths(relPath.Paths / whatToAdd.Select(x => x.Path), relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.FileSystem);
        }

        /// <summary>
        /// Adds a subpath to all the relative paths
        /// </summary>
        /// <param name="relPath">The relative paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the relative paths</param>
        /// <returns>A new RelativePaths object where all relative paths in it has an additional subpath appended to it</returns>
        public static RelativePaths operator / (RelativePaths relPath, Func<RelativePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new RelativePaths(relPath.Paths / (rel => whatToAdd(new RelativePath(rel, relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.FileSystem)).Select(x => x.Path)),
                relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.FileSystem);
        }

        /// <summary>
        /// Adds a subpath to all the relative paths
        /// </summary>
        /// <param name="relPath">The relative paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the relative paths</param>
        /// <returns>A new RelativePaths object where all relative paths in it has an additional subpath appended to it</returns>
        public static RelativePaths operator / (RelativePaths relPath, RelativePath whatToAdd)
        {
            return new RelativePaths(relPath.Paths / whatToAdd.Path, relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.FileSystem);
        }

        /// <summary>
        /// Adds a subpath to all the relative paths
        /// </summary>
        /// <param name="relPath">The relative paths that will have a subpath added to them</param>
        /// <param name="whatToAdd">The subpath that will be added to all the relative paths</param>
        /// <returns>A new RelativePaths object where all relative paths in it has an additional subpath appended to it</returns>
        public static RelativePaths operator / (RelativePaths relPath, IEnumerable<string> whatToAdd)
        {
            return new RelativePaths(relPath.Paths / whatToAdd, relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.FileSystem);
        }
        
        /// <summary>
        /// Uses the RelativePaths.Equals method to compare equality between the two RelativePaths
        /// </summary>
        /// <param name="left">The first object to check for equality</param>
        /// <param name="right">The second object to check for equality</param>
        /// <returns>True if the two objects are equal; false otherwise</returns>
        public static bool operator ==(RelativePaths left, RelativePaths right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Uses the RelativePaths.Equals method to compare equality between the two RelativePaths
        /// </summary>
        /// <param name="left">The first object to check for inequality</param>
        /// <param name="right">The second object to check for inequality</param>
        /// <returns>False if the two objects are equal; true otherwise</returns>
        public static bool operator !=(RelativePaths left, RelativePaths right)
        {
            return !Equals(left, right);
        }
    }
}