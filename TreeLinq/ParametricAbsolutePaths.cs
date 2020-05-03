using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class ParametricAbsolutePaths<TNodeName> : IEnumerable<AbsolutePath<TNodeName>> where TNodeName : IComparable
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

        public ParametricAbsolutePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public ParametricAbsolutePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(new []{new RelativePath<TNodeName>(whatToAdd)})}));
        }

        public ParametricAbsolutePaths<TNodeName> Add(RelativePath<TNodeName> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(whatToAdd.Select(x => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(x))));
        }

        public ParametricAbsolutePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricAbsolutePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public ParametricAbsolutePaths<TNodeName> Add(
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<AbsolutePath<TNodeName>> GetEnumerator()
        {
            return ToAbsolutePaths().GetEnumerator();
        }

        private AbsolutePaths<TNodeName> ToAbsolutePaths()
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
                    var newAbsolutePaths = new AbsolutePaths<TNodeName>(absolutePathsSoFar)
                        .SelectMany(absolutePath => absolutePath / component.Item3.Value(absolutePath))
                        .Select(x => new RelativePath<TNodeName>(x.Components));
                    
                    absolutePathsSoFar.Clear();
                    absolutePathsSoFar.Add(new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(newAbsolutePaths));
                }
            }

            return new AbsolutePaths<TNodeName>(absolutePathsSoFar);
        }
        
        public static ParametricAbsolutePaths<TNodeName> operator / (ParametricAbsolutePaths<TNodeName> absPath, TNodeName whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static ParametricAbsolutePaths<TNodeName> operator / (ParametricAbsolutePaths<TNodeName> absPath, IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static ParametricAbsolutePaths<TNodeName> operator / (ParametricAbsolutePaths<TNodeName> absPath, RelativePath<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static ParametricAbsolutePaths<TNodeName> operator / (ParametricAbsolutePaths<TNodeName> absPath, IEnumerable<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static ParametricAbsolutePaths<TNodeName> operator / (ParametricAbsolutePaths<TNodeName> absPath,
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }
    }
}