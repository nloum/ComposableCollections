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

        private static void ValidateFlags(PathFlags flags)
        {
            if (flags.HasFlag(PathFlags.UseDefaultsForGivenPath))
                throw new ArgumentException("A path cannot have the UseDefaultsForGivenPath flag set.");
            if (flags.HasFlag(PathFlags.UseDefaultsFromUtility))
                throw new ArgumentException("A path cannot have the UseDefaultsFromUtility flag set.");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Flags.GetHashCode();
                hashCode = (hashCode * 397) ^ (Components != null ? Components.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DirectorySeparator != null ? DirectorySeparator.GetHashCode() : 0);
                return hashCode;
            }
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
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            //if (Flags != other.Flags)
            //    return false;
            if (!string.Equals(DirectorySeparator, other.DirectorySeparator))
                return false;
            var flags = IoService.ToStringComparison(Flags, other.Flags);
            if (Components.Count != other.Components.Count)
                return false;
            for (var i = 0; i < Components.Count; i++)
                if (!string.Equals(Components[i], other.Components[i], flags))
                    return false;
            return true;
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
    }
}