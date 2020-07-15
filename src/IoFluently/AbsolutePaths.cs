using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenericNumbers.Relational;
using TreeLinq;

namespace IoFluently
{
    public class AbsolutePaths : IComparable, IEnumerable<AbsolutePath>
    {
        public PathFlags Flags { get; }
        public string DirectorySeparator { get; }
        public IIoService IoService { get; }
        public AbsolutePaths<string> Paths { get; }

        internal AbsolutePaths(PathFlags flags, string directorySeparator, IIoService ioService, AbsolutePaths<string> paths)
        {
            Flags = flags;
            DirectorySeparator = directorySeparator;
            IoService = ioService;
            Paths = paths;
        }

        public int CompareTo(object obj)
        {
            if (obj is AbsolutePaths absolutePaths)
            {
                return Paths.CompareTo(absolutePaths.Paths);
            }

            return 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<AbsolutePath> GetEnumerator()
        {
            foreach (var path in Paths)
            {
                yield return new AbsolutePath(Flags, DirectorySeparator, IoService, path);
            }
        }

        public static AbsolutePaths operator / (AbsolutePaths absPath, string whatToAdd)
        {
            return new AbsolutePaths(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Paths / whatToAdd);
        }

        public static AbsolutePaths operator / (AbsolutePaths absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return new AbsolutePaths(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Paths / whatToAdd.Select(x => x.Path));
        }

        public static AbsolutePaths operator / (AbsolutePaths absPath, RelativePath whatToAdd)
        {
            return new AbsolutePaths(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Paths / whatToAdd.Path);
        }

        public static AbsolutePaths operator / (AbsolutePaths absPath, IEnumerable<string> whatToAdd)
        {
            return new AbsolutePaths(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Paths / whatToAdd);
        }

        public static AbsolutePaths operator / (AbsolutePaths absPath,
            Func<AbsolutePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return new AbsolutePaths(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, absPath.Paths / (abs => whatToAdd(new AbsolutePath(absPath.Flags, absPath.DirectorySeparator, absPath.IoService, abs)).Select(x => x.Path)));
        }
    }
}