using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace TreeLinq
{
	public class TreeTraversal<TNodeName, TNode> : IComparable<TreeTraversal<TNodeName, TNode>>, IComparable
		where TNodeName : IComparable {
		public TreeTraversal( TreeTraversalType type, IEnumerable<TNodeName> path, TNode value ) {
			Type = type;
			Value = value;
			Path = new Path<TNodeName>( path.ToImmutableList() );
		}

		public TreeTraversalType Type { get; }
		public Path<TNodeName> Path { get; }
		public TNode Value { get; }

		public int CompareTo( TreeTraversal<TNodeName, TNode> other ) {
			var pathComparison = Path.CompareTo( other.Path );
			if ( pathComparison != 0 ) {
				return pathComparison;
			}

			return ((int)Type).CompareTo( (int)other.Type );
		}

		public int CompareTo( object obj ) {
			if ( obj is TreeTraversal<TNodeName, TNode> other ) {
				return CompareTo( other );
			}

			return ToString().CompareTo( obj.ToString() );
		}

		public override string ToString() {
			string typeString = null;
			switch ( Type ) {
				case TreeTraversalType.EnterBranch:
					typeString = "enter";
					break;
				case TreeTraversalType.ExitBranch:
					typeString = "exit ";
					break;
				case TreeTraversalType.Leaf:
					typeString = "leaf ";
					break;
				default:
					throw new ArgumentException( "Unknown iteration type" );
			}

			if ( Path.IsRoot )
				return $"{typeString} <root>";
			return $"{typeString} {string.Join( ".", Path )}";
		}
	}
}