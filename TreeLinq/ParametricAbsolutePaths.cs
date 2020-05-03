using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class ParametricAbsolutePaths<TNodeName> where TNodeName : IComparable
    {
        public ImmutableList<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>> Components { get; }

        public ParametricAbsolutePaths(IEnumerable<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>> components)
        {
            Components = components.ToImmutableList();
        }

        public ParametricAbsolutePaths<TNodeName> Add(TNodeName whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricAbsolutePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricAbsolutePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricAbsolutePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public ParametricAbsolutePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }
        
        public ParametricAbsolutePaths<TNodeName> Add(
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public AbsolutePaths<TNodeName> Enumerate<TNode>(ITree<TNodeName, TNode> tree)
        {
            return new AbsolutePaths<TNodeName>(GetEnumerable());
        }

        private IEnumerable<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>> GetEnumerable()
        {
            var absolutePathsSoFar = new List<Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>>();

            foreach (var component in Components)
            {
                if (component.Item1.HasValue)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(component.Item1.Value);
                    absolutePathsSoFar.Add(result);
                    yield return result;
                }

                if (component.Item2.HasValue)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(component.Item2.Value);
                    absolutePathsSoFar.Add(result);
                    yield return result;
                }

                if (component.Item3.HasValue)
                {
                    var absolutePaths = new AbsolutePaths<TNodeName>(absolutePathsSoFar);
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(
                        absolutePaths.SelectMany(absolutePath => component.Item3.Value(absolutePath)));
                    absolutePathsSoFar.Add(result);
                    yield return result;
                }
            }
        }
    }
}