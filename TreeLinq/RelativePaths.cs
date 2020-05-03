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
        public ImmutableList<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>> Components { get; }
    
        public RelativePaths(IEnumerable<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>> components)
        {
            Components = components.ToImmutableList();
        }
        
        public RelativePaths<TNodeName> Add(TNodeName whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(whatToAdd)}));
        }

        public RelativePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(whatToAdd)}));
        }

        public RelativePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(whatToAdd)}));
        }

        public RelativePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public RelativePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new RelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }
        
        public ParametricRelativePaths<TNodeName> Add(
            Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Select(x => x.Or<Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>())
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }
        
        public IEnumerator<RelativePath<TNodeName>> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        private IEnumerable<RelativePath<TNodeName>> GetEnumerable()
        {
            var stack = Components
                .Select(either =>
                {
                    if (either.Item1.HasValue)
                    {
                        return GetEnumerator(either.Item1.Value);
                    }

                    return GetEnumerator(either.Item2.Value);
                }).ToImmutableList();

            return InternalUtilities.CalcCombinationsOfOneFromEach(stack)
                .Select(x =>
                    new RelativePath<TNodeName>(x.SelectMany(y => y)));
        }

        private IEnumerable<IEnumerable<TNodeName>> GetEnumerator(TNodeName nodeName)
        {
            yield return new[] {nodeName};
        }

        private IEnumerable<IEnumerable<TNodeName>> GetEnumerator(IEnumerable<RelativePath<TNodeName>> relativePaths)
        {
            foreach (var relativePath in relativePaths)
            {
                yield return relativePath;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}