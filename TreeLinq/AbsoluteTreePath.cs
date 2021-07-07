using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace TreeLinq
{
    public class AbsoluteTreePath<TNodeName> : Path<TNodeName> where TNodeName : IComparable
    {
        public static AbsoluteTreePath<TNodeName> Root { get; } = new AbsoluteTreePath<TNodeName>( ImmutableList<TNodeName>.Empty );

        public AbsoluteTreePath(params TNodeName[] components) : base(components)
        {
        }

        public AbsoluteTreePath(IEnumerable<TNodeName> components) : base(components)
        {
        }

        public AbsoluteTreePath(ImmutableList<TNodeName> components) : base(components)
        {
        }

        public AbsoluteTreePath<TNodeName> SkipDescendants( int skipDescendants ) {
            return new AbsoluteTreePath<TNodeName>( Components.Take(Count - skipDescendants ) );
        }
        
        public AbsoluteTreePath<TNodeName> Add(RelativeTreePath<TNodeName> whatToAdd)
        {
            return new AbsoluteTreePath<TNodeName>(Components.Concat(whatToAdd.Components));
        }

        public override bool IsAbsolute => true;
        public override bool IsRelative => false;

        public AbsoluteTreePaths<TNodeName> Add(params RelativeTreePath<TNodeName>[] whatToAdd)
        {
            return Add(whatToAdd.AsEnumerable());
        }

        public AbsoluteTreePaths<TNodeName> Add(IEnumerable<RelativeTreePath<TNodeName>> whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(
                Components.Select(c => new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(c))
                    .Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        public AbsoluteTreePaths<TNodeName> Add(Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>> whatToAdd)
        {
            return new AbsoluteTreePaths<TNodeName>(
                Components.Select(c => new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(c))
                    .Concat(new[]{new Either<TNodeName, IEnumerable<RelativeTreePath<TNodeName>>, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>>>(whatToAdd)}));
        }

        public AbsoluteTreePath<TNodeName> Add(TNodeName whatToAdd)
        {
            return new AbsoluteTreePath<TNodeName>(Components.Concat(new[]{whatToAdd}));
        }

        public AbsoluteTreePaths<TNodeName> Add(params TNodeName[] whatToAdd)
        {
            return Add(whatToAdd.AsEnumerable());
        }
        
        public AbsoluteTreePaths<TNodeName> Add(IEnumerable<TNodeName> whatToAdd)
        {
            return Add(whatToAdd.Select(x => new RelativeTreePath<TNodeName>(x)));
        }

        public static AbsoluteTreePath<TNodeName> operator / (AbsoluteTreePath<TNodeName> absPath, TNodeName whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsoluteTreePaths<TNodeName> operator / (AbsoluteTreePath<TNodeName> absPath, IEnumerable<RelativeTreePath<TNodeName>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsoluteTreePaths<TNodeName> operator / (AbsoluteTreePath<TNodeName> absPath, Func<AbsoluteTreePath<TNodeName>, IEnumerable<RelativeTreePath<TNodeName>>> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsoluteTreePath<TNodeName> operator / (AbsoluteTreePath<TNodeName> absPath, RelativeTreePath<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }

        public static AbsoluteTreePaths<TNodeName> operator / (AbsoluteTreePath<TNodeName> absPath, IEnumerable<TNodeName> whatToAdd)
        {
            return absPath.Add(whatToAdd);
        }
    }
}