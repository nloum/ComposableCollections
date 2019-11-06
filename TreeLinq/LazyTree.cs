using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TreeLinq
{
	public class LazyTree<TNodeName, TNode> : ITree<TNodeName, TNode> where TNodeName : IComparable {
		private readonly IEnumerable<TreeTraversal<TNodeName, TNode>> _items;

		public static LazyTree<TNodeName, TNode> Empty { get; } = new LazyTree<TNodeName, TNode>();

		private LazyTree() {
			_items = ImmutableList<TreeTraversal<TNodeName, TNode>>.Empty;
		}

		public LazyTree( IEnumerable<TreeTraversal<TNodeName, TNode>> items ) {
			_items = items;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<TreeTraversal<TNodeName, TNode>> GetEnumerator() => _items.GetEnumerator();

		public bool TryGetBranch( Path<TNodeName> path1, out ITree<TNodeName, TNode> branch1 ) {
			branch1 = new LazyTree<TNodeName, TNode>( this.Where( x => x.Path.StartsWith( path1 ) ) );
			return this.Any( x => x.Path.StartsWith( path1 ) );
		}

		public bool TryGetBranches( Path<TNodeName> path1, out ITree<TNodeName, TNode> branch1, Path<TNodeName> path2, out ITree<TNodeName, TNode> branch2 ) {
			branch1 = new LazyTree<TNodeName, TNode>( this.Where( x => x.Path.StartsWith( path1 ) ) );
			branch2 = new LazyTree<TNodeName, TNode>( this.Where( x => x.Path.StartsWith( path2 ) ) );
			var branch1Exists = false;
			var branch2Exists = false;
			foreach ( var item in this ) {
				if ( item.Path.StartsWith( path1 ) ) {
					branch1Exists = true;
				}

				if ( item.Path.StartsWith( path2 ) ) {
					branch2Exists = true;
				}

				if ( branch1Exists && branch2Exists ) {
					return true;
				}
			}

			return false;
		}

		public bool TryGetLeaf<TNode1>( Path<TNodeName> path1, out TNode1 leaf1 )
			where TNode1 : TNode {
			foreach ( var item in this.AsEnumerable() ) {
				if ( path1 == item.Path && item.Value is TNode1 node1 ) {
					leaf1 = node1;
					return true;
				}
			}

			leaf1 = default;
			return false;
		}

		public bool TryGetLeaves<TNode1, TNode2>( Path<TNodeName> path1, out TNode1 leaf1, Path<TNodeName> path2, out TNode2 leaf2 )
			where TNode1 : TNode
			where TNode2 : TNode {
			leaf1 = default;
			leaf2 = default;
			var hasFoundLeaf1 = false;
			var hasFoundLeaf2 = false;
			foreach ( var item in this.AsEnumerable() ) {
				if ( item.Path == path1 ) {
					if ( item.Value is TNode1 node1 ) {
						leaf1 = node1;
						hasFoundLeaf1 = true;
					}
				}
				if ( item.Path == path2 ) {
					if ( item.Value is TNode2 node2 ) {
						leaf2 = node2;
						hasFoundLeaf2 = true;
					}
				}
				if ( hasFoundLeaf1 && hasFoundLeaf2 ) {
					return true;
				}
			}

			return hasFoundLeaf1 && hasFoundLeaf2;
		}
	}
}