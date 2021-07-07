using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class RelativeTreePaths<TNodeName> : IEnumerable<RelativeTreePath<TNodeName>> where TNodeName : IComparable
    {
        public ImmutableList<IEither<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>> Components { get; }
    
        public RelativeTreePaths(IEnumerable<IEither<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>> components)
        {
            Components = components.ToImmutableList();
        }
        
        public RelativeTreePaths<TNodeName> Add(TNodeName whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        public RelativeTreePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(new []{new RelativeTreePath<TNodeName>(whatToAdd)}.AsEnumerable())}));
        }

        public RelativeTreePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(new []{new RelativeTreePath<TNodeName>(whatToAdd)}.AsEnumerable())}));
        }

        public RelativeTreePaths<TNodeName> Add(RelativeTreePath<TNodeName> whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(Components.Concat(whatToAdd.Components.Select(x => new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(x))));
        }

        public RelativeTreePaths<TNodeName> Add(params RelativeTreePath<TNodeName>[] whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd.AsEnumerable())}));
        }

        public RelativeTreePaths<TNodeName> Add(Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>> whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        public RelativeTreePaths<TNodeName> Add(IEnumerable<RelativeTreePath<TNodeName>> whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<RelativeTreePath<TNodeName>> GetEnumerator()
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

        private static IEnumerable<RelativeTreePath<TNodeName>> GetEnumerable(List<IEither<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>>> components)
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
                    new RelativeTreePath<TNodeName>(x.SelectMany(y => y)));
        }
        
        private IEnumerable<RelativeTreePath<TNodeName>> GetEnumerable()
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
                    var newRelativePaths = GetEnumerable(absolutePathsSoFar)
                        .SelectMany(absolutePath => absolutePath / component.Item3(absolutePath))
                        .Select(x => new RelativeTreePath<TNodeName>(x.Components));
                    
                    absolutePathsSoFar.Clear();
                    absolutePathsSoFar.Add(new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>>(newRelativePaths));
                }
            }

            return GetEnumerable(absolutePathsSoFar);
        }
        
        public static RelativeTreePaths<TNodeName> operator / (RelativeTreePaths<TNodeName> relPath, TNodeName whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativeTreePaths<TNodeName> operator / (RelativeTreePaths<TNodeName> relPath, IEnumerable<RelativeTreePath<TNodeName>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativeTreePaths<TNodeName> operator / (RelativeTreePaths<TNodeName> relPath, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativeTreePaths<TNodeName> operator / (RelativeTreePaths<TNodeName> relPath, RelativeTreePath<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativeTreePaths<TNodeName> operator / (RelativeTreePaths<TNodeName> relPath, IEnumerable<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }
    }
}