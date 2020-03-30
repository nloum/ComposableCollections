using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using MoreCollections;
using SimpleMonads;
using static SimpleMonads.Utility;

namespace MoreIO
{
    public class PathSpec : IComparable, IEnumerable<PathSpec>
    {
        internal PathSpec(PathFlags flags, string directorySeparator, IIoService ioService,
            IEnumerable<string> elements)
        {
            ValidateFlags(flags);
            Flags = flags;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
            Components = elements.SelectMany(SplitComponent).ToImmutableList();
        }

        internal PathSpec(PathFlags flags, string directorySeparator, IIoService ioService, params string[] components)
            : this(flags, directorySeparator, ioService, components.AsEnumerable())
        {
        }

        internal PathSpec(PathSpec other)
            : this(other.Flags, other.DirectorySeparator, other.IoService, other.Components)
        {
        }

        public IIoService IoService { get; }
        public ImmutableList<string> Components { get; }

        public string Name => Components[Components.Count - 1];

        public PathFlags Flags { get; }
        public string DirectorySeparator { get; }

        public int CompareTo(object obj)
        {
            var tp = obj as PathSpec;
            if (tp != null)
                return CompareTo(tp);
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<PathSpec> GetEnumerator()
        {
            return IoService.GetChildren(this).GetEnumerator();
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
                var lastPathComponent = this.LastPathComponent();
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

        public int CompareTo(PathSpec other)
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

        public object Clone()
        {
            return new PathSpec(this);
        }

        public bool Equals(PathSpec other)
        {
            return GetHashCode().Equals(other.GetHashCode());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Components.Count; i++)
            {
                sb.Append(Components[i]);
                if (Components[i] != DirectorySeparator && i + 1 != Components.Count)
                    sb.Append(DirectorySeparator);
            }

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PathSpec) obj);
        }

        public IMaybe<PathSpec> Ancestor(int generations)
        {
            if (Components.Count > generations)
                return Something(new PathSpec(Flags, DirectorySeparator, IoService,
                    Components.Subset(0, -1 - generations)));
            return Nothing<PathSpec>();
        }

        public IMaybe<PathSpec> Parent()
        {
            return Ancestor(1);
        }

        public static PathSpec operator / (PathSpec start, PathSpec next)
        {
            return start.Descendant(next).Value;
        }

        public static PathSpec operator / (PathSpec start, string next)
        {
            return start.Descendant(next).Value;
        }
    }
}