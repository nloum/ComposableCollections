using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class RelativePath<TNodeName> : Path<TNodeName> where TNodeName : IComparable
    {
        public static RelativePath<TNodeName> Empty { get; } = new RelativePath<TNodeName>( ImmutableList<TNodeName>.Empty );

        public RelativePath(params TNodeName[] components) : base(components)
        {
        }

        public RelativePath(IEnumerable<TNodeName> components) : base(components)
        {
        }

        public RelativePath(ImmutableList<TNodeName> components) : base(components)
        {
        }
		
        public override bool IsAbsolute => false;
        public override bool IsRelative => true;

        public RelativePath<TNodeName> SkipDescendants( int skipDescendants ) {
            return new RelativePath<TNodeName>( Components.Take(Count - skipDescendants ) );
        }
        
        public RelativePath<TNodeName> Add(TNodeName whatToAdd)
        {
            return new RelativePath<TNodeName>(Components.Concat(new[]{whatToAdd}));
        }

        public RelativePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return Add(whatToAdd.AsEnumerable());
        }

        public RelativePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return Add(whatToAdd.Select(x => new RelativePath<TNodeName>(x)));
        }

        public RelativePath<TNodeName> Add(RelativePath<TNodeName> whatToAdd)
        {
            return new RelativePath<TNodeName>(Components.Concat(whatToAdd.Components));
        }

        public RelativePaths<TNodeName> Add(params RelativePath<TNodeName>[] whatToAdd)
        {
            return Add(whatToAdd.AsEnumerable());
        }

        public RelativePaths<TNodeName> Add(IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return new RelativePaths<TNodeName>(
                Components.Select(c => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(c))
                    .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public RelativePaths<TNodeName> Add(Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return new RelativePaths<TNodeName>(
                Components.Select(c => new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(c))
                    .Concat(new[]{new Either<TNodeName, IEnumerable<RelativePath<TNodeName>>, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>>>(whatToAdd)}));
        }

        public static RelativePath<TNodeName> operator / (RelativePath<TNodeName> relPath, TNodeName whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativePaths<TNodeName> operator / (RelativePath<TNodeName> relPath, IEnumerable<RelativePath<TNodeName>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativePaths<TNodeName> operator / (RelativePath<TNodeName> relPath, Func<RelativePath<TNodeName>, IEnumerable<RelativePath<TNodeName>>> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativePath<TNodeName> operator / (RelativePath<TNodeName> relPath, RelativePath<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }

        public static RelativePaths<TNodeName> operator / (RelativePath<TNodeName> relPath, IEnumerable<TNodeName> whatToAdd)
        {
            return relPath.Add(whatToAdd);
        }
    }
}