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
    public class RelativePath : IComparable
    {
        public PathFlags Flags { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }
        public RelativePath<string> Path { get; }

        internal RelativePath(PathFlags flags, string directorySeparator, IIoService ioService, IEnumerable<string> path)
        {
            Flags = flags;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
            Path = new RelativePath<string>(path);
            if (ioService.ComponentsAreAbsolute(Path.Components))
            {
                throw new ArgumentException($"The path {Path} is not relative");
            }
        }

        public string Name => Path[Path.Count - 1];

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

        private static void ValidateFlags(PathFlags flags)
        {
            if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                throw new ArgumentException("A path cannot have the UseDefaultsForGivenPath flag set.");
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility))
                throw new ArgumentException("A path cannot have the UseDefaultsFromUtility flag set.");
        }

        public override int GetHashCode()
        {
            if (Flags.HasFlag(PathFlags.CaseSensitive))
            {
                return ToString().GetHashCode();
            }
            
            return ToString().ToLower().GetHashCode();
        }

        public int CompareTo(RelativePath other)
        {
            return Path.CompareTo(other.Path);
        }

        public bool Equals(RelativePath other)
        {
            return Path.Equals(other.Path);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Path.Count; i++)
            {
                sb.Append(Path[i]);
                if (Path[i] != DirectorySeparator && i + 1 != Path.Count)
                    sb.Append(DirectorySeparator);
            }

            return sb.ToString();
        }

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
                return Something(new RelativePath(Flags, DirectorySeparator, IoService,
                    Path.Subset(0, -1 - generations)));
            return Nothing<RelativePath>();
        }

        public IMaybe<RelativePath> Parent()
        {
            return Ancestor(1);
        }
        
        public static RelativePath operator / (RelativePath relPath, string whatToAdd)
        {
            return new RelativePath(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Path / whatToAdd);
        }

        public static RelativePaths operator / (RelativePath relPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new RelativePaths(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Path / whatToAdd.Select(x => x.Path));
        }

        public static RelativePaths operator / (RelativePath relPath, Func<RelativePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new RelativePaths(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Path / (rel => whatToAdd(new RelativePath(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, rel)).Select(x => x.Path)));
        }

        public static RelativePath operator / (RelativePath relPath, RelativePath whatToAdd)
        {
            return new RelativePath(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Path / whatToAdd.Path);
        }

        public static RelativePaths operator / (RelativePath relPath, IEnumerable<string> whatToAdd)
        {
            return new RelativePaths(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Path / whatToAdd);
        }
    }
}