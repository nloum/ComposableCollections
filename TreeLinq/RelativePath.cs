using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class RelativePath<TNodeName> : Path<TNodeName> where TNodeName : IComparable
    {
        public RelativePath(params TNodeName[] wrapped) : base(wrapped)
        {
        }

        public RelativePath(IEnumerable<TNodeName> wrapped) : base(wrapped)
        {
        }

        public RelativePath(ImmutableList<TNodeName> components) : base(components)
        {
        }
		
        public RelativePath<TNodeName> SkipDescendants( int skipDescendants ) {
            return new RelativePath<TNodeName>( Components.Take(Count - skipDescendants ) );
        }
        
        public RelativePath<TNodeName> Add(TNodeName whatToAdd)
        {
            return new RelativePath<TNodeName>(Components.Concat(new[]{whatToAdd}));
        }

        public RelativePath<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new RelativePath<TNodeName>(Components.Concat(whatToAdd.SelectMany(x => x.Components)));
        }

        public RelativePath<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return new RelativePath<TNodeName>(Components.Concat(whatToAdd.SelectMany(x => x.Components)));
        }

        public RelativePath<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return new RelativePath<TNodeName>(Components.Concat(whatToAdd));
        }

        public RelativePath<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return new RelativePath<TNodeName>(Components.Concat(whatToAdd));
        }
        
        public ParametricRelativePaths<TNodeName> Add(
            Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new ParametricRelativePaths<TNodeName>(Components.Select(x => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(x))
                .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }
    }
}