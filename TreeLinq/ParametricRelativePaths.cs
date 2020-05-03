using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class ParametricRelativePaths<TNodeName> where TNodeName : IComparable
    {
        public ImmutableList<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>> Components { get; }

        public ParametricRelativePaths(IEnumerable<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>> components)
        {
            Components = components.ToImmutableList();
        }

        public ParametricRelativePaths<TNodeName> Add(TNodeName whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricRelativePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricRelativePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricRelativePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public ParametricRelativePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }
        
        public ParametricRelativePaths<TNodeName> Add(
            Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public RelativePaths<TNodeName> Enumerate<TNode>(ITree<TNodeName, TNode> tree)
        {
            return new RelativePaths<TNodeName>(GetEnumerable());
        }

        private IEnumerable<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>> GetEnumerable()
        {
            var relativePathsSoFar = new List<Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>>();

            foreach (var component in Components)
            {
                if (component.Item1.HasValue)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(component.Item1.Value);
                    relativePathsSoFar.Add(result);
                    yield return result;
                }

                if (component.Item2.HasValue)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(component.Item2.Value);
                    relativePathsSoFar.Add(result);
                    yield return result;
                }

                if (component.Item3.HasValue)
                {
                    var absolutePaths = new RelativePaths<TNodeName>(relativePathsSoFar);
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(
                        absolutePaths.SelectMany(absolutePath => component.Item3.Value(absolutePath)));
                    relativePathsSoFar.Add(result);
                    yield return result;
                }
            }
        }
    }
}