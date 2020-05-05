using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreCollections;
using SimpleMonads;
using TreeLinq;
using static SimpleMonads.Utility;

namespace MoreIO
{
    public class RelativePaths : IComparable, IEnumerable<RelativePath>
    {
        public PathFlags Flags { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }
        public RelativePaths<string> Paths { get; }

        public RelativePaths(PathFlags flags, string directorySeparator, IIoService ioService, RelativePaths<string> paths)
        {
            Flags = flags;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
            Paths = paths;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<RelativePath> GetEnumerator()
        {
            foreach (var path in Paths)
            {
                yield return new RelativePath(Flags, DirectorySeparator, IoService, path);
            }
        }

        public int CompareTo(object obj)
        {
            var tp = obj as AbsolutePath;
            if (tp != null)
                return CompareTo(tp);
            return GetHashCode().CompareTo(obj.GetHashCode());
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

        public bool Equals(AbsolutePath other)
        {
            return GetHashCode().Equals(other.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AbsolutePath) obj);
        }

        public static RelativePaths operator / (RelativePaths relPath, string whatToAdd)
        {
            return new RelativePaths(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Paths / whatToAdd);
        }

        public static RelativePaths operator / (RelativePaths relPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new RelativePaths(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Paths / whatToAdd.Select(x => x.Path));
        }

        public static RelativePaths operator / (RelativePaths relPath, Func<RelativePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new RelativePaths(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Paths / (rel => whatToAdd(new RelativePath(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, rel)).Select(x => x.Path)));
        }

        public static RelativePaths operator / (RelativePaths relPath, RelativePath whatToAdd)
        {
            return new RelativePaths(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Paths / whatToAdd.Path);
        }

        public static RelativePaths operator / (RelativePaths relPath, IEnumerable<string> whatToAdd)
        {
            return new RelativePaths(relPath.Flags, relPath.DirectorySeparator, relPath.IoService, relPath.Paths / whatToAdd);
        }
    }
}