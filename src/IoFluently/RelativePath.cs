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
    /// Represents a path that has no root, e.g. './test1.txt'. For absolute paths, such as '/test1.txt', see <see cref="AbsolutePath"/>.
    /// </summary>
    public partial class RelativePath : IComparable, IComparable<RelativePath>, IEquatable<RelativePath>
    {
        /// <summary>
        /// Indicates whether or not the relative path is case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; }

        /// <summary>
        /// Indicates what the directory separator is for this relative path (e.g., '/' or '\') 
        /// </summary>
        public string DirectorySeparator { get; }
        
        /// <summary>
        /// The IIoService that is used for this relative path
        /// </summary>
        public IIoService IoService { get; }
        
        /// <summary>
        /// The TreeLinq relative path that this object represents
        /// </summary>
        public RelativeTreePath<string> Path { get; }

        internal RelativePath(bool isCaseSensitive, string directorySeparator, IIoService ioService, IEnumerable<string> path)
        {
            IsCaseSensitive = isCaseSensitive;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
            Path = new RelativeTreePath<string>(path);
            if (ioService.ComponentsAreAbsolute(Path.Components))
            {
                throw new ArgumentException($"The path {Path} is not relative");
            }
        }

        /// <summary>
        /// The file or directory name, a.k.a the last component in the path
        /// </summary>
        public string Name => Path[Path.Count - 1];

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            var tp = obj as RelativePath;
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
                StringSplitOptions.RemoveEmptyEntries))
            {
                yield return subcomponent;
            }
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

        /// <inheritdoc />
        public int CompareTo(RelativePath other)
        {
            return Path.CompareTo(other.Path);
        }

        /// <inheritdoc />
        public bool Equals(RelativePath other)
        {
            return Path.Equals(other.Path);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var isUnc = Path.Components.FirstOrDefault() == @"\\";

            var sb = new StringBuilder();
            for (var i = 0; i < Path.Count; i++)
            {
                sb.Append(Path[i]);
                if (Path[i] != DirectorySeparator && i + 1 != Path.Count)
                {
                    if (!isUnc || i != 0)
                    {
                        sb.Append(DirectorySeparator);
                    }
                }
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Converts this RelativePath to a string form of the path
        /// </summary>
        /// <param name="path">The path to be converted to a string</param>
        /// <returns>The string form of this path</returns>
        public static implicit operator string(RelativePath path)  
        {  
            return path.ToString();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RelativePath) obj);
        }

        public IMaybe<RelativePath> Ancestor(int generations)
        {
            if (Path.Count > generations)
                return Something(new RelativePath(IsCaseSensitive, DirectorySeparator, IoService,
                    Path.Subset(0, -1 - generations)));
            return Nothing<RelativePath>();
        }

        public IMaybe<RelativePath> Parent()
        {
            return Ancestor(1);
        }
        
        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static RelativePath operator / (RelativePath relPath, string whatToAdd)
        {
            if (string.IsNullOrEmpty(whatToAdd))
            {
                return relPath;
            }
            
            return new RelativePath(relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.IoService, relPath.Path / whatToAdd);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static RelativePaths operator / (RelativePath relPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new RelativePaths(relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.IoService, relPath.Path / whatToAdd.Select(x => x.Path));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static RelativePaths operator / (RelativePath relPath, Func<RelativePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new RelativePaths(relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.IoService, relPath.Path / (rel => whatToAdd(new RelativePath(relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.IoService, rel)).Select(x => x.Path)));
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static RelativePath operator / (RelativePath relPath, RelativePath whatToAdd)
        {
            if (whatToAdd == null)
            {
                return relPath;
            }
            
            return new RelativePath(relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.IoService, relPath.Path / whatToAdd.Path);
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static RelativePaths operator / (RelativePath relPath, IEnumerable<string> whatToAdd)
        {
            return new RelativePaths(relPath.IsCaseSensitive, relPath.DirectorySeparator, relPath.IoService, relPath.Path / whatToAdd);
        }
        
        /// <summary>
        /// Uses the RelativePath.Equals method to compare equality between the two RelativePaths
        /// </summary>
        /// <param name="left">The first object to check for equality</param>
        /// <param name="right">The second object to check for equality</param>
        /// <returns>True if the two objects are equal; false otherwise</returns>
        public static bool operator ==(RelativePath left, RelativePath right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Uses the RelativePath.Equals method to compare equality between the two RelativePaths
        /// </summary>
        /// <param name="left">The first object to check for inequality</param>
        /// <param name="right">The second object to check for inequality</param>
        /// <returns>False if the two objects are equal; true otherwise</returns>
        public static bool operator !=(RelativePath left, RelativePath right)
        {
            return !Equals(left, right);
        }
    }
}