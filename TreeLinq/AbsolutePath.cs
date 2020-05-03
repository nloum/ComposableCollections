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

        public AbsolutePath(params TNodeName[] wrapped) : base(wrapped)
        {
        }

        public AbsolutePath(IEnumerable<TNodeName> wrapped) : base(wrapped)
        {
        }

        public AbsolutePath(ImmutableList<TNodeName> components) : base(components)
        {
        }

        public AbsolutePath<TNodeName> SkipDescendants( int skipDescendants ) {
            return new AbsolutePath<TNodeName>( Components.Take(Count - skipDescendants ) );
        }
        
        public AbsolutePath<TNodeName> Add(TNodeName whatToAdd)
        {
            return new AbsolutePath<TNodeName>(Components.Concat(new[]{whatToAdd}));
        }

        public AbsolutePath<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new AbsolutePath<TNodeName>(Components.Concat(whatToAdd.SelectMany(x => x.Components)));
        }

        public AbsolutePath<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new AbsolutePath<TNodeName>(Components.Concat(whatToAdd.SelectMany(x => x.Components)));
        }

        public AbsolutePath<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new AbsolutePath<TNodeName>(Components.Concat(whatToAdd));
        }

        public AbsolutePath<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new AbsolutePath<TNodeName>(Components.Concat(whatToAdd));
        }
        
        public ParametricAbsolutePaths<TNodeName> Add(
            Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricAbsolutePaths<TNodeName>(Components.Select(x => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(x))
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<AbsolutePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }
    }
}