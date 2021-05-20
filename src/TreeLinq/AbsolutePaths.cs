using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class AbsolutePaths<TNodeName> : IEnumerable<AbsolutePath<TNodeName>> where TNodeName : IComparable
    {
        public ImmutableList<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>> Components { get; }

        public AbsolutePaths(IEnumerable<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>> components)
        {
            Components = components.ToImmutableList();
        }

        public AbsolutePaths<TNodeName> Add(TNodeName whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public AbsolutePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public AbsolutePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public AbsolutePaths<TNodeName> Add(RelativePath<TNodeName> whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(whatToAdd.Select(x => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(x))));
        }

        public AbsolutePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public AbsolutePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public AbsolutePaths<TNodeName> Add(
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<AbsolutePath<TNodeName>> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }
        
        private static IEnumerable<IEnumerable<TNodeName>> GetEnumerator(TNodeName nodeName)
        {
            yield return new[] {nodeName};
        }

        private static IEnumerable<IEnumerable<TNodeName>> GetEnumerator(IEnumerable<RelativePath<TNodeName>> relativePaths)
        {
            foreach (var relativePath in relativePaths)
            {
                yield return relativePath;
            }
        }

        private static IEnumerable<AbsolutePath<TNodeName>> GetEnumerable(List<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>> components)
        {
            var stack = components
                .Select(either =>
                {
                    if (either.Item1 != null)
                    {
                        return GetEnumerator(either.Item1);
                    }

                    return GetEnumerator(either.Item2);
                }).ToImmutableList();

            return InternalUtilities.CalcCombinationsOfOneFromEach(stack)
                .Select(x =>
                    new AbsolutePath<TNodeName>(x.SelectMany(y => y)));
        }
        
        private IEnumerable<AbsolutePath<TNodeName>> GetEnumerable()
        {
            var absolutePathsSoFar = new List<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>>();

            foreach (var component in Components)
            {
                if (component.Item1 != null)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(component.Item1);
                    absolutePathsSoFar.Add(result);
                }

                if (component.Item2 != null)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(component.Item2);
                    absolutePathsSoFar.Add(result);
                }

                if (component.Item3 != null)
                {
                    var newAbsolutePaths = GetEnumerable(absolutePathsSoFar)
                        .SelectMany(absolutePath => absolutePath / component.Item3(absolutePath))
                        .Select(x => new RelativePath<TNodeName>(x.Components));
                    
                    absolutePathsSoFar.Clear();
                    absolutePathsSoFar.Add(new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(newAbsolutePaths));
                }
            }

            return GetEnumerable(absolutePathsSoFar);
        }
        
        public static AbsolutePaths<TNodeName> operator / (AbsolutePaths<TNodeName> absPath, TNodeName whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsolutePaths<TNodeName> operator / (AbsolutePaths<TNodeName> absPath, IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsolutePaths<TNodeName> operator / (AbsolutePaths<TNodeName> absPath, RelativePath<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsolutePaths<TNodeName> operator / (AbsolutePaths<TNodeName> absPath, IEnumerable<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsolutePaths<TNodeName> operator / (AbsolutePaths<TNodeName> absPath,
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }
    }
}