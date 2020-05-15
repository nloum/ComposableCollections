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
    public class AbsolutePath : IComparable, IEnumerable<AbsolutePath>
    {
        public PathFlags Flags { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }
        public AbsolutePath<string> Path { get; }

        public AbsolutePath(PathFlags flags, string directorySeparator, IIoService ioService, IEnumerable<string> path)
        {
            Flags = flags;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
            Path = new AbsolutePath<string>(path);
            if (!ComponentsAreAbsolute(Path.Components))
            {
                throw new ArgumentException($"The path {Path} is not absolute");
            }
        }
        
        private static bool ComponentsAreAbsolute(IReadOnlyList<string> path)
        {
            if (path[0] == "/")
                return true;
            if (char.IsLetter(path[0][0]) && path[0][1] == ':')
                return true;
            if (path[0] == "\\")
                return true;
            return false;
        }

        public string Name => Path[Path.Count - 1];

        public int CompareTo(object obj)
        {
            var tp = obj as AbsolutePath;
            if (tp != null)
                return CompareTo(tp);
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<AbsolutePath> GetEnumerator()
        {
            return IoService.EnumerateChildren(this).GetEnumerator();
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
                    return Maybe<string>.Nothing;
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

        public bool Equals(AbsolutePath other)
        {
            return GetHashCode().Equals(other.GetHashCode());
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
            return Equals((AbsolutePath) obj);
        }

        public IMaybe<AbsolutePath> TryAncestor(int generations)
        {
            if (Path.Count > generations)
                return Something(new AbsolutePath(Flags, DirectorySeparator, IoService,
                    Path.Subset(0, -1 - generations)));
            return Nothing<AbsolutePath>();
        }

        public AbsolutePath Ancestor(int generations)
        {
            return TryAncestor(generations).Value;
        }

        public IMaybe<AbsolutePath> TryParent()
        {
            return TryAncestor(1);
        }

        public AbsolutePath Parent()
        {
            return TryParent().Value;
        }

        public static AbsolutePath operator / (AbsolutePath absPath, string whatToAdd)
        {
            return new AbsolutePath(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Path / whatToAdd);
        }

        public static AbsolutePaths operator / (AbsolutePath absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new AbsolutePaths(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Path / whatToAdd.Select(x => x.Path));
        }

        public static AbsolutePaths operator / (AbsolutePath absPath, Func<AbsolutePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new AbsolutePaths(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Path / (x => whatToAdd(new AbsolutePath(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, x)).Select(y => y.Path)));
        }

        public static AbsolutePath operator / (AbsolutePath absPath, RelativePath whatToAdd)
        {
            return new AbsolutePath(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Path / whatToAdd.Path);
        }

        public static AbsolutePaths operator / (AbsolutePath absPath, IEnumerable<string> whatToAdd)
        {
            return new AbsolutePaths(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Path / whatToAdd);
        }
    }
}