using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace TreeLinq
{
	public class PreComputedTree<TNodeName, TNode> : ITree<TNodeName, TNode> where TNodeName : IComparable {
		private readonly Dictionary<TNodeName, PreComputedTree<TNodeName, TNode>> _children = new Dictionary<TNodeName, PreComputedTree<TNodeName, TNode>>();

		public bool IsLeaf => _children.Count == 0;

		public PreComputedTree( TNode value ) {
			Value = value;
		}

		public PreComputedTree() {
			Value = default;
		}

		public IReadOnlyDictionary<TNodeName, PreComputedTree<TNodeName, TNode>> AsDictionary() => _children;

		public IEnumerable<TNodeName> ChildrenNames => _children.Keys;

		public void SetNode( TNodeName name, PreComputedTree<TNodeName, TNode> child ) {
			_children[name] = child;
		}

		public bool Remove( TNodeName name ) {
			return _children.Remove( name );
		}

		public TNode Value { get; }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<TreeTraversal<TNodeName, TNode>> GetEnumerator() {
			return GetEnumerable().GetEnumerator();
		}

		public bool TryGetBranch( Path<TNodeName> path1, out ITree<TNodeName, TNode> branch1 ) {
			var currentBranch = this;
			for ( var i = 0; i < path1.Count; i++ ) {
				if (!currentBranch._children.ContainsKey( path1[i] )) {
					branch1 = default;
					return false;
				}
				currentBranch = currentBranch._children[path1[i]];
			}

			branch1 = currentBranch;
			return true;
		}

		public bool TryGetBranches( Path<TNodeName> path1, out ITree<TNodeName, TNode> branch1, Path<TNodeName> path2,
			out ITree<TNodeName, TNode> branch2 ) {
			if (!TryGetBranch( path1, out branch1 )) {
				branch2 = default;
				return false;
			}

			return TryGetBranch( path2, out branch2 );
		}

		public bool TryGetLeaf<TNode1>( Path<TNodeName> path1, out TNode1 leaf1 ) where TNode1 : TNode {
			var currentBranch = this;
			for ( var i = 0; i < path1.Count; i++ ) {
				if ( !currentBranch._children.ContainsKey( path1[i] ) ) {
					leaf1 = default;
					return false;
				}
				currentBranch = currentBranch._children[path1[i]];
			}

			if (currentBranch.IsLeaf && currentBranch.Value is TNode1 node1) {
				leaf1 = node1;
				return true;
			}

			leaf1 = default;
			return false;
		}

		public bool TryGetLeaves<TNode1, TNode2>( Path<TNodeName> path1, out TNode1 leaf1, Path<TNodeName> path2,
			out TNode2 leaf2 ) where TNode1 : TNode where TNode2 : TNode {
			if ( !TryGetLeaf( path1, out leaf1 ) ) {
				leaf2 = default;
				return false;
			}

			return TryGetLeaf( path2, out leaf2 );
		}

		private IEnumerable<TreeTraversal<TNodeName, TNode>> GetEnumerable() {
			if (IsLeaf) {
				yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.Leaf, AbsolutePath<TNodeName>.Root, Value );
				yield break;
			}

			var traversalStack = new Stack<TreeTraversalState<TNodeName, PreComputedTree<TNodeName, TNode>>>();
			traversalStack.Push( new TreeTraversalState<TNodeName, PreComputedTree<TNodeName, TNode>>( AbsolutePath<TNodeName>.Root, this, ChildrenNames ) );

			var hasYieldedInitialIteration = false;

			while ( traversalStack.Count > 0 ) {
				if ( !hasYieldedInitialIteration ) {
					yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, AbsolutePath<TNodeName>.Root );
					hasYieldedInitialIteration = true;
				}

				if ( !traversalStack.Peek().ChildNames.MoveNext() ) {
					traversalStack.Pop();
					if ( traversalStack.Count > 0 ) {
						var path = traversalStack.Peek().Path.Add(traversalStack.Peek().ChildNames.Current);
						yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, path );
					}
				} else {
					var path = traversalStack.Peek().Path.Add(traversalStack.Peek().ChildNames.Current);
					if (!traversalStack.Peek().Node._children.ContainsKey( traversalStack.Peek().ChildNames.Current ) ) {
						throw new InvalidDataException( "Attempted to access child by name and failed" );
					}

					var node = traversalStack.Peek().Node._children[traversalStack.Peek().ChildNames.Current];
					if ( node.ChildrenNames.Any() ) {
						yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, path );
						traversalStack.Push( new TreeTraversalState<TNodeName, PreComputedTree<TNodeName, TNode>>( path, node, node.ChildrenNames ) );
					} else {
						yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.Leaf, path, node.Value );
					}
				}
			}

			if ( hasYieldedInitialIteration ) {
				yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, ImmutableList<TNodeName>.Empty, Value );
			} else {
				yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.Leaf, ImmutableList<TNodeName>.Empty, Value );
			}
		}

		public override string ToString() {
			return Value.ToString();
		}
	}
}