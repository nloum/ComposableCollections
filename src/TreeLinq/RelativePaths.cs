using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class RelativePaths<TNodeName> : IEnumerable<RelativePath<TNodeName>> where TNodeName : IComparable
    {
        public ImmutableList<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>> Components { get; }
    
        public RelativePaths(IEnumerable<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>> components)
        {
            Components = components.ToImmutableList();
        }
        
        public RelativePaths<TNodeName> Add(TNodeName whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public RelativePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public RelativePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public RelativePaths<TNodeName> Add(RelativePath<TNodeName> whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(whatToAdd.Components.Select(x => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(x))));
        }

        public RelativePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public RelativePaths<TNodeName> Add(Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public RelativePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<RelativePath<TNodeName>> GetEnumerator()
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

        private static IEnumerable<RelativePath<TNodeName>> GetEnumerable(List<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>> components)
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
                    new RelativePath<TNodeName>(x.SelectMany(y => y)));
        }
        
        private IEnumerable<RelativePath<TNodeName>> GetEnumerable()
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
                    var newRelativePaths = GetEnumerable(absolutePathsSoFar)
                        .SelectMany(absolutePath => absolutePath / component.Item3(absolutePath))
                        .Select(x => new RelativePath<TNodeName>(x.Components));
                    
                    absolutePathsSoFar.Clear();
                    absolutePathsSoFar.Add(new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(newRelativePaths));
                }
            }

            return GetEnumerable(absolutePathsSoFar);
        }
        
        public static RelativePaths<TNodeName> operator / (RelativePaths<TNodeName> relPath, TNodeName whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativePaths<TNodeName> operator / (RelativePaths<TNodeName> relPath, IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativePaths<TNodeName> operator / (RelativePaths<TNodeName> relPath, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativePaths<TNodeName> operator / (RelativePaths<TNodeName> relPath, RelativePath<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativePaths<TNodeName> operator / (RelativePaths<TNodeName> relPath, IEnumerable<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }
    }
}