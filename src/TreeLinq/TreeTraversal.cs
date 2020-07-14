using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;

namespace TreeLinq
{
	public class TreeTraversal<TNodeName, TNode> : IComparable<TreeTraversal<TNodeName, TNode>>, IComparable
		where TNodeName : IComparable {

		public TreeTraversal( TreeTraversalType type, IEnumerable<TNodeName> path, TNode value ) : this( type, new AbsolutePath<TNodeName>( path.ToImmutableList() ), value ) {
		}

		public TreeTraversal( TreeTraversalType type, IEnumerable<TNodeName> path ) : this(type, new AbsolutePath<TNodeName>( path.ToImmutableList() ) ) {
		}

		public TreeTraversal( TreeTraversalType type, AbsolutePath<TNodeName> path, TNode value ) {
			Type = type;
			Value = value;
			Path = path;
		}

		public TreeTraversal( TreeTraversalType type, AbsolutePath<TNodeName> path ) {
			if ( type == TreeTraversalType.Leaf ) {
				throw new InvalidEnumArgumentException( "Leaf tree traversals need to specify a value" );
			}
			Type = type;
			Value = default;
			Path = path;
		}

		public TreeTraversalType Type { get; }
		public AbsolutePath<TNodeName> Path { get; }
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