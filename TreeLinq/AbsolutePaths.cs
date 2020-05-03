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
        public ImmutableList<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>> Components { get; }

        public AbsolutePaths(IEnumerable<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>> components)
        {
            Components = components.ToImmutableList();
        }

        public AbsolutePaths<TNodeName> Add(TNodeName whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(whatToAdd)}));
        }

        public AbsolutePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(whatToAdd)}));
        }

        public AbsolutePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(whatToAdd)}));
        }

        public AbsolutePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public AbsolutePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }
        
        public ParametricAbsolutePaths<TNodeName> Add(
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Select(x => x.Or<Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>())
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public virtual TNodeName Root()
        {
            return default(TNodeName);
        }
        
        public IEnumerator<AbsolutePath<TNodeName>> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        private IEnumerable<AbsolutePath<TNodeName>> GetEnumerable()
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
                    new AbsolutePath<TNodeName>(x.SelectMany(y => y)));
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