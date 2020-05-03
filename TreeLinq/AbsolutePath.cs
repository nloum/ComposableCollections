using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class AbsolutePath<TNodeName> : Path<TNodeName> where TNodeName : IComparable
    {
        public static AbsolutePath<TNodeName> Root { get; } = new AbsolutePath<TNodeName>( ImmutableList<TNodeName>.Empty );

        public AbsolutePath(params TNodeName[] components) : base(components)
        {
        }

        public AbsolutePath(IEnumerable<TNodeName> components) : base(components)
        {
        }

        public AbsolutePath(ImmutableList<TNodeName> components) : base(components)
        {
        }

        public AbsolutePath<TNodeName> SkipDescendants( int skipDescendants ) {
            return new AbsolutePath<TNodeName>( Components.Take(Count - skipDescendants ) );
        }
        
        public AbsolutePath<TNodeName> Add(RelativePath<TNodeName> whatToAdd)
        {
            return new AbsolutePath<TNodeName>(Components.Concat(whatToAdd.Components));
        }

        public AbsolutePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return Add(whatToAdd.AsEnumerable());
        }

        public AbsolutePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new AbsolutePaths<TNodeName>(
                Components.Select(c => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(c))
                    .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>>(whatToAdd)}));
        }

        public AbsolutePath<TNodeName> Add(TNodeName whatToAdd)
        {
            return new AbsolutePath<TNodeName>(Components.Concat(new[]{whatToAdd}));
        }

        public AbsolutePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return Add(whatToAdd.AsEnumerable());
        }
        
        public AbsolutePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return Add(whatToAdd.Select(x => new RelativePath<TNodeName>(x)));
        }

        public ParametricAbsolutePaths<TNodeName> Add(
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Select(x => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(x))
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }
        
        public static AbsolutePath<TNodeName> operator / (AbsolutePath<TNodeName> absPath, TNodeName whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsolutePaths<TNodeName> operator / (AbsolutePath<TNodeName> absPath, IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsolutePath<TNodeName> operator / (AbsolutePath<TNodeName> absPath, RelativePath<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsolutePaths<TNodeName> operator / (AbsolutePath<TNodeName> absPath, IEnumerable<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static ParametricAbsolutePaths<TNodeName> operator / (AbsolutePath<TNodeName> absPath,
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }
    }
}