using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class ParametricRelativePaths<TNodeName> : IEnumerable<RelativePath<TNodeName>> where TNodeName : IComparable
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

        public ParametricRelativePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public ParametricRelativePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public ParametricRelativePaths<TNodeName> Add(RelativePath<TNodeName> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(whatToAdd.Select(x => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(x))));
        }

        public ParametricRelativePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricRelativePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricRelativePaths<TNodeName> Add(
            Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<RelativePath<TNodeName>> GetEnumerator()
        {
            return ToRelativePaths().GetEnumerator();
        }

        private RelativePaths<TNodeName> ToRelativePaths()
        {
            var absolutePathsSoFar = new List<IEither<TNodeName, IEnumerable<RelativePath<TNodeName>>>>();

            foreach (var component in Components)
            {
                if (component.Item1.HasValue)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(component.Item1.Value);
                    absolutePathsSoFar.Add(result);
                }

                if (component.Item2.HasValue)
                {
                    var result = new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(component.Item2.Value);
                    absolutePathsSoFar.Add(result);
                }

                if (component.Item3.HasValue)
                {
                    var newAbsolutePaths = new RelativePaths<TNodeName>(absolutePathsSoFar)
                        .SelectMany(absolutePath => absolutePath / component.Item3.Value(absolutePath))
                        .Select(x => new RelativePath<TNodeName>(x.Components));
                    
                    absolutePathsSoFar.Clear();
                    absolutePathsSoFar.Add(new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(newAbsolutePaths));
                }
            }

            return new RelativePaths<TNodeName>(absolutePathsSoFar);
        }

        public static ParametricRelativePaths<TNodeName> operator / (ParametricRelativePaths<TNodeName> relPath, TNodeName whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static ParametricRelativePaths<TNodeName> operator / (ParametricRelativePaths<TNodeName> relPath, IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static ParametricRelativePaths<TNodeName> operator / (ParametricRelativePaths<TNodeName> relPath, RelativePath<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static ParametricRelativePaths<TNodeName> operator / (ParametricRelativePaths<TNodeName> relPath, IEnumerable<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static ParametricRelativePaths<TNodeName> operator / (ParametricRelativePaths<TNodeName> relPath,
            Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }
    }
}