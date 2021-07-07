using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class AbsoluteTreePaths<TNodeName> : IEnumerable<AbsoluteTreePath<TNodeName>> where TNodeName : IComparable
    {
        public ImmutableList<IEither<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>> Components { get; }

        public AbsoluteTreePaths(IEnumerable<IEither<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>> components)
        {
            Components = components.ToImmutableList();
        }

        public AbsoluteTreePaths<TNodeName> Add(TNodeName whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        public AbsoluteTreePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(new []{new RelativeTreePath<TNodeName>(whatToAdd)}.AsEnumerable())}));
        }

        public AbsoluteTreePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(new []{new RelativeTreePath<TNodeName>(whatToAdd)}.AsEnumerable())}));
        }

        public AbsoluteTreePaths<TNodeName> Add(RelativeTreePath<TNodeName> whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(Components.Concat(whatToAdd.Select(x => new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(x))));
        }

        public AbsoluteTreePaths<TNodeName> Add(params RelativeTreePath<TNodeName>[] whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd.AsEnumerable())}));
        }

        public AbsoluteTreePaths<TNodeName> Add(IEnumerable<RelativeTreePath<TNodeName>> whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        public AbsoluteTreePaths<TNodeName> Add(
            Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>> whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(Components
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<AbsoluteTreePath<TNodeName>> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }
        
        private static IEnumerable<IEnumerable<TNodeName>> GetEnumerator(TNodeName nodeName)
        {
            yield return new[] {nodeName};
        }

        private static IEnumerable<IEnumerable<TNodeName>> GetEnumerator(IEnumerable<RelativeTreePath<TNodeName>> relativePaths)
        {
            foreach (var relativePath in relativePaths)
            {
                yield return relativePath;
            }
        }

        private static IEnumerable<AbsoluteTreePath<TNodeName>> GetEnumerable(List<IEither<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>>> components)
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
                    new AbsoluteTreePath<TNodeName>(x.SelectMany(y => y)));
        }
        
        private IEnumerable<AbsoluteTreePath<TNodeName>> GetEnumerable()
        {
            var absolutePathsSoFar = new List<IEither<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>>>();

            foreach (var component in Components)
            {
                if (component.Item1 != null)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>>(component.Item1);
                    absolutePathsSoFar.Add(result);
                }

                if (component.Item2 != null)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>>(component.Item2);
                    absolutePathsSoFar.Add(result);
                }

                if (component.Item3 != null)
                {
                    var newAbsolutePaths = GetEnumerable(absolutePathsSoFar)
                        .SelectMany(absolutePath => absolutePath / component.Item3(absolutePath))
                        .Select(x => new RelativeTreePath<TNodeName>(x.Components));
                    
                    absolutePathsSoFar.Clear();
                    absolutePathsSoFar.Add(new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>>(newAbsolutePaths));
                }
            }

            return GetEnumerable(absolutePathsSoFar);
        }
        
        public static AbsoluteTreePaths<TNodeName> operator / (AbsoluteTreePaths<TNodeName> absPath, TNodeName whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsoluteTreePaths<TNodeName> operator / (AbsoluteTreePaths<TNodeName> absPath, IEnumerable<RelativeTreePath<TNodeName>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsoluteTreePaths<TNodeName> operator / (AbsoluteTreePaths<TNodeName> absPath, RelativeTreePath<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsoluteTreePaths<TNodeName> operator / (AbsoluteTreePaths<TNodeName> absPath, IEnumerable<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsoluteTreePaths<TNodeName> operator / (AbsoluteTreePaths<TNodeName> absPath,
            Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }
    }
}