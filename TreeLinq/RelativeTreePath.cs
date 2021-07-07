using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class RelativeTreePath<TNodeName> : Path<TNodeName> where TNodeName : IComparable
    {
        public static RelativeTreePath<TNodeName> Empty { get; } = new RelativeTreePath<TNodeName>( ImmutableList<TNodeName>.Empty );

        public RelativeTreePath(params TNodeName[] components) : base(components)
        {
        }

        public RelativeTreePath(IEnumerable<TNodeName> components) : base(components)
        {
        }

        public RelativeTreePath(ImmutableList<TNodeName> components) : base(components)
        {
        }
		
        public override bool IsAbsolute => false;
        public override bool IsRelative => true;

        public RelativeTreePath<TNodeName> SkipDescendants( int skipDescendants ) {
            return new RelativeTreePath<TNodeName>( Components.Take(Count - skipDescendants ) );
        }
        
        public RelativeTreePath<TNodeName> Add(TNodeName whatToAdd)
        {
            return new RelativeTreePath<TNodeName>(Components.Concat(new[]{whatToAdd}));
        }

        public RelativeTreePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return Add(whatToAdd.AsEnumerable());
        }

        public RelativeTreePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return Add(whatToAdd.Select(x => new RelativeTreePath<TNodeName>(x)));
        }

        public RelativeTreePath<TNodeName> Add(RelativeTreePath<TNodeName> whatToAdd)
        {
            return new RelativeTreePath<TNodeName>(Components.Concat(whatToAdd.Components));
        }

        public RelativeTreePaths<TNodeName> Add(params RelativeTreePath<TNodeName>[] whatToAdd)
        {
            return Add(whatToAdd.AsEnumerable());
        }

        public RelativeTreePaths<TNodeName> Add(IEnumerable<RelativeTreePath<TNodeName>> whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(
                Components.Select(c => new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(c))
                    .Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        public RelativeTreePaths<TNodeName> Add(Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>> whatToAdd)
        {
            return new RelativeTreePaths<TNodeName>(
                Components.Select(c => new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(c))
                    .Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        public static RelativeTreePath<TNodeName> operator / (RelativeTreePath<TNodeName> relPath, TNodeName whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativeTreePaths<TNodeName> operator / (RelativeTreePath<TNodeName> relPath, IEnumerable<RelativeTreePath<TNodeName>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativeTreePaths<TNodeName> operator / (RelativeTreePath<TNodeName> relPath, Func<RelativeTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativeTreePath<TNodeName> operator / (RelativeTreePath<TNodeName> relPath, RelativeTreePath<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativeTreePaths<TNodeName> operator / (RelativeTreePath<TNodeName> relPath, IEnumerable<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }
    }
}