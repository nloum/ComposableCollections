using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace TreeLinq
{
	public static class Extensions {
		public static bool Contains<TNodeName, TNode>( this Tree<TNodeName, TNode> tree, TNodeName name ) where TNodeName : IComparable {
			return tree.TryGet( name, out var child );
		}

		public static bool Contains<TNodeName, TNode>( this Tree<TNodeName, TNode> tree, Path<TNodeName> path ) where TNodeName : IComparable {
			return tree.TryGet( path, out var child );
		}

		private static IEnumerable<ImmutableList<T>> GroupByEquality<T>( this IEnumerable<T> source ) where T : IComparable {
            var equalItems = new List<T>();
			foreach ( var item in source ) {
				if ( equalItems.Count == 0 || !equalItems[0].Equals( item ) ) {
					yield return equalItems.ToImmutableList();
					equalItems.Clear();
				}
				equalItems.Add(item);
			}

			if ( equalItems.Count > 0 ) {
				yield return equalItems.ToImmutableList();
			}
		}

        public static IEnumerable<ImmutableList<ImmutableList<T>>> SortedZip<T>(this IEnumerable<T> first, IEnumerable<T> second, params IEnumerable<T>[] otherEnumerables) where T : IComparable {
			var enumerables = new List<IEnumerable<T>>() {
				first, second
			};
			enumerables.AddRange( otherEnumerables );
	        var enumerators = enumerables.Select( enumerable => enumerable.GroupByEquality().GetEnumerator() ).ToList();

	        foreach ( var enumerator in enumerators ) {
		        if ( !enumerator.MoveNext() ) {
			        enumerators.Remove( enumerator );
		        }
	        }

			while(true) {
				var equalItems = new List<IEnumerator<ImmutableList<T>>>();
				foreach ( var item in enumerators ) {
					if ( equalItems.Count == 0 || equalItems[0].Current[0].CompareTo( item.Current[0] ) > 0 ) {
						equalItems.Clear();
					}
					equalItems.Add( item );
				}

				yield return equalItems.Select(x => x.Current).ToImmutableList();

				foreach ( var item in equalItems ) {
					if ( !item.MoveNext() ) {
						enumerators.Remove( item );
					}
				}
			}
		} 

		//private static IEnumerable<TreeTraversal<TNodeName, TNode>> MergeInternal<TNodeName, TNode>( this Tree<TNodeName, TNode> firstTree, IEnumerable<Tree<TNodeName, TNode>> otherTrees, Func<Path<TNodeName>, ImmutableList<TNode>, TNode> conflictSelector )
		//	where TNodeName : IComparable {
		//	var treeEnumerators = new List<IEnumerator<TreeTraversal<TNodeName, TNode>>>();

		//	var firstTreeEnumerator = firstTree.AsEnumerable().GetEnumerator();
		//	if ( firstTreeEnumerator.MoveNext() ) {
  //              treeEnumerators.Add(firstTreeEnumerator);
		//	}

		//	foreach ( var otherTree in otherTrees ) {
		//		var enumerator = otherTree.AsEnumerable().GetEnumerator();
		//		if ( enumerator.MoveNext() ) {
  //                  treeEnumerators.Add(enumerator);
		//		}
		//	}

		//	while ( true ) {
		//		var treesToProcess = new SortedSet<IEnumerator<TreeTraversal<TNodeName, TNode>>>();

		//		foreach ( var enumerator in treeEnumerators ) {
		//			if ( treesToProcess.Count == 0 ||
		//			     treesToProcess.First().Current.Path > enumerator.Current.Path ) {
		//				treesToProcess.Clear();
  //                      treesToProcess.Add(enumerator);
		//			} else if ( treesToProcess.First().Current.Path == enumerator.Current.Path ) {
		//				treesToProcess.Add(enumerator);
		//			}
		//		}

		//		if ( !enumeratorToMoveForward.MoveNext() ) {
		//			treeEnumerators.Remove( enumeratorToMoveForward );
		//			continue;
		//		}


		//	}

		//	foreach ( var enumerator in treeEnumerators ) {
		//		enumerator.Dispose();
		//	}

		//	using ( var tree1Enumerator = firstTree.AsEnumerable().OrderBy( x => x.Path ).GetEnumerator() )
		//	using ( var tree2Enumerator = tree2.AsEnumerable().OrderBy( x => x.Path ).GetEnumerator() ) {
		//		while ( true ) {
		//			if ( !tree1Enumerator.MoveNext() ) {
		//				while ( tree2Enumerator.MoveNext() ) {
		//					yield return tree2Enumerator.Current;
		//				}

		//				break;
		//			}

		//			if ( !tree2Enumerator.MoveNext() ) {
		//				while ( tree1Enumerator.MoveNext() ) {
		//					yield return tree1Enumerator.Current;
		//				}

		//				break;
		//			}

		//			if ( tree1Enumerator.Current.Type == TreeTraversalType.Leaf &&
		//			     tree2Enumerator.Current.Type == TreeTraversalType.Leaf ) {

		//			}

  //                  if ( tree1Enumerator.Current.Path == tree2Enumerator.Current.Path ) {
                        
		//			}
		//		}
		//	}

		//	var index1 = 0;
		//	var index2 = 0;

		//	while ( true ) {
  //              if (tree1Iteration[index1])
		//	}

		//	foreach ( var traversal in tree.AsEnumerable() ) {
		//		if ( traversal.Type == TreeTraversalType.Leaf && traversal.Path.StartsWith( path ) ) {
		//			yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, traversal.Path, traversal.Value );

		//			for ( var i = traversal.Path.Count; i < path.Count - 1; i++ ) {
		//				yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, path.SkipDescendants( path.Count - i ), default( TNode ) );
		//			}

		//			foreach ( var item in newValue.AsEnumerable() ) {
		//				yield return item;
		//			}

		//			for ( var i = path.Count - 1; i >= traversal.Path.Count; i-- ) {
		//				yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, path.SkipDescendants( path.Count - i ), default( TNode ) );
		//			}

		//			yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, traversal.Path, traversal.Value );

		//			if ( traversal.Type == TreeTraversalType.ExitBranch ) {
		//				yield return traversal;
		//			}
		//		} else {
		//			yield return traversal;
		//		}
		//	}
		//}

		public static Tree<TNodeName, TNode> Set<TNodeName, TNode>( this Tree<TNodeName, TNode> tree,
			TNodeName name, TNode newValue ) where TNodeName : IComparable {
			var newValueTree = new Tree<TNodeName, TNode>( newValue, tree.GetChildNames, tree.TryGet );
            var path = new Path<TNodeName>(name);
			return new Tree<TNodeName, TNode>( () => SetInternal<TNodeName, TNode>( tree, path, newValueTree ), tree.Root, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName, TNode> Set<TNodeName, TNode>( this Tree<TNodeName, TNode> tree,
			Path<TNodeName> path, TNode newValue ) where TNodeName : IComparable {
			var newValueTree = new Tree<TNodeName, TNode>( newValue, tree.GetChildNames, tree.TryGet );
			return new Tree<TNodeName, TNode>( () => SetInternal<TNodeName, TNode>( tree, path, newValueTree ), tree.Root, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName, TNode> Set<TNodeName, TNode>( this Tree<TNodeName, TNode> tree,
			TNodeName name, Tree<TNodeName, TNode> newValue ) where TNodeName : IComparable {
			var path = new Path<TNodeName>( name );
			return new Tree<TNodeName, TNode>( () => SetInternal<TNodeName, TNode>( tree, path, newValue ), tree.Root, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName, TNode> Set<TNodeName, TNode>( this Tree<TNodeName, TNode> tree,
			Path<TNodeName> path, Tree<TNodeName, TNode> newValue ) where TNodeName : IComparable {
			var newRoot = path.IsRoot ? newValue.Root : tree.Root;
			return new Tree<TNodeName, TNode>( () => SetInternal<TNodeName, TNode>( tree, path, newValue), newRoot, tree.GetChildNames, tree.TryGet);
		}

		private static IEnumerable<TreeTraversal<TNodeName, TNode>> SetInternal<TNodeName, TNode>( this Tree<TNodeName, TNode> tree, Path<TNodeName> path, Tree<TNodeName, TNode> newValue ) where TNodeName : IComparable {
			foreach ( var traversal in tree.AsEnumerable() ) {
				if ( traversal.Type == TreeTraversalType.Leaf && traversal.Path.StartsWith( path ) ) {
					yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, traversal.Path, traversal.Value );

					for ( var i = traversal.Path.Count; i < path.Count - 1; i++ ) {
                        yield return new TreeTraversal<TNodeName, TNode>(TreeTraversalType.EnterBranch, path.SkipDescendants(path.Count - i), default(TNode) );
					}

					foreach ( var item in newValue.AsEnumerable() ) {
						yield return item;
					}

					for ( var i = path.Count - 1; i >= traversal.Path.Count; i-- ) {
						yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, path.SkipDescendants( path.Count - i ), default( TNode ) );
					}

					yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, traversal.Path, traversal.Value );

					if ( traversal.Type == TreeTraversalType.ExitBranch ) {
						yield return traversal;
					}
				} else {
					yield return traversal;
				}
			}
		}

		public static TNode Set<TNodeName, TNode>( this Tree<TNodeName, TNode> tree,
			Path<TNodeName> path ) where TNodeName : IComparable {
			var currentNode = tree.Root;
			foreach ( var item in path ) {
				currentNode = tree.Get( currentNode, item );
			}

			return currentNode;
		}
        
		private class SelectBranchBuilder<TNodeName, TNode1, TNode2> where TNodeName : IComparable {
			public SelectBranchBuilder( TNode1 value ) {
				Value = value;
			}

			public TNode1 Value { get; }

			public Dictionary<TNodeName, TNode2> ChildrenToBe { get; } = new Dictionary<TNodeName, TNode2>();
		}

		public delegate bool TryGetChild<TNodeName, TNode>( TNode node, TNodeName childName, out TNode child ) where TNodeName : IComparable;

		public delegate void Selector<TNodeName1, TNode1, TNodeName2, TNode2>
			( Path<TNodeName1> path, TNode1 node, out TNodeName2 newName, out TNode2 newNode, IReadOnlyDictionary<TNodeName2, TNode2> children )
			where TNodeName1 : IComparable where TNodeName2 : IComparable;

		public delegate void LeafSelector<TNodeName1, TNode1, TNodeName2, TNode2>
			( Path<TNodeName1> path, TNode1 node, out TNodeName2 newName, out TNode2 newNode )
			where TNodeName1 : IComparable where TNodeName2 : IComparable;

		public static Tree<TNodeName, TNode> SelectLeaves<TNodeName, TNode>(
			this Tree<TNodeName, TNode> tree,
			Func<TNode, TNode> leafSelector )
			where TNodeName : IComparable {
			return tree.SelectLeaves( ( Path<TNodeName> path, TNode node, out TNodeName newName, out TNode newNode ) => {
					newName = path.Name;
					newNode = leafSelector( node );
				}, x => x, x => x, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName, TNode2> SelectLeaves<TNodeName, TNode1, TNode2>(
			this Tree<TNodeName, TNode1> tree,
			Func<TNode1, TNode2> leafSelector,
			Func<TNode1, TNode2> branchSelector,
			Func<TNode2, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNode2> getChild )
			where TNodeName : IComparable {
			return tree.SelectLeaves( ( Path<TNodeName> path, TNode1 node, out TNodeName newName, out TNode2 newNode ) => {
					newName = path.Name;
					newNode = leafSelector( node );
				}, x=> x, branchSelector, getChildNames, getChild );
		}

		public static Tree<TNodeName, TNode> SelectLeaves<TNodeName, TNode>(
			this Tree<TNodeName, TNode> tree,
			Func<Path<TNodeName>, TNode, TNode> leafSelector )
			where TNodeName : IComparable {
			return tree.SelectLeaves( ( Path<TNodeName> path, TNode node, out TNodeName newName, out TNode newNode ) => {
					newName = path.Name;
					newNode = leafSelector( path, node );
				}, x => x, x => x, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName, TNode2> SelectLeaves<TNodeName, TNode1, TNode2>(
			this Tree<TNodeName, TNode1> tree,
			Func<Path<TNodeName>, TNode1, TNode2> leafSelector,
			Func<TNode1, TNode2> branchSelector,
			Func<TNode2, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNode2> getChild )
			where TNodeName : IComparable {
			return tree.SelectLeaves( ( Path<TNodeName> path, TNode1 node, out TNodeName newName, out TNode2 newNode ) => {
					newName = path.Name;
					newNode = leafSelector( path, node );
				}, x => x, branchSelector, getChildNames, getChild );
		}

		public static Tree<TNodeName, TNode> SelectLeaves<TNodeName, TNode>(
			this Tree<TNodeName, TNode> tree,
			LeafSelector<TNodeName, TNode, TNodeName, TNode> leafSelector )
			where TNodeName : IComparable {
			return tree.SelectLeaves( leafSelector, x => x, x => x, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName2, TNode2> SelectLeaves<TNodeName1, TNode1, TNodeName2, TNode2>(
			this Tree<TNodeName1, TNode1> tree,
			LeafSelector<TNodeName1, TNode1, TNodeName2, TNode2> leafSelector,
            Func<TNodeName1, TNodeName2> branchNameSelector,
            Func<TNode1, TNode2> branchSelector,
			Func<TNode2, IEnumerable<TNodeName2>> getChildNames,
			TryGetChild<TNodeName2, TNode2> getChild )
			where TNodeName1 : IComparable
			where TNodeName2 : IComparable {
			return tree.Select( ( Path<TNodeName1> path, TNode1 node, out TNodeName2 newName, out TNode2 newNode,
				IReadOnlyDictionary<TNodeName2, TNode2> children ) => {
				if ( children.Count > 0 ) {
					newName = branchNameSelector( path.Name);
					newNode = branchSelector( node);
				} else {
					leafSelector( path, node, out newName, out newNode );
				}
			}, getChildNames, getChild );
		}

		public static Tree<TNodeName, TNode> Select<TNodeName, TNode>(
			this Tree<TNodeName, TNode> tree,
			Func<TNode, IReadOnlyDictionary<TNodeName, TNode>, TNode> selector )
			where TNodeName : IComparable {
			return tree.Select( ( Path<TNodeName> path, TNode node, out TNodeName newName, out TNode newNode,
				IReadOnlyDictionary<TNodeName, TNode> children ) => {
				newName = path.Name;
				newNode = selector( node, children );
			}, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName, TNode2> Select<TNodeName, TNode1, TNode2>(
			this Tree<TNodeName, TNode1> tree,
			Func<TNode1, IReadOnlyDictionary<TNodeName, TNode2>, TNode2> selector,
			Func<TNode2, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNode2> getChild )
			where TNodeName : IComparable {
			return tree.Select( ( Path<TNodeName> path, TNode1 node, out TNodeName newName, out TNode2 newNode,
				IReadOnlyDictionary<TNodeName, TNode2> children ) => {
				newName = path.Name;
				newNode = selector( node, children );
			}, getChildNames, getChild );
		}

		public static Tree<TNodeName, TNode> Select<TNodeName, TNode>(
			this Tree<TNodeName, TNode> tree,
			Func<Path<TNodeName>, TNode, IReadOnlyDictionary<TNodeName, TNode>, TNode> selector )
			where TNodeName : IComparable {
			return tree.Select( ( Path<TNodeName> path, TNode node, out TNodeName newName, out TNode newNode,
				IReadOnlyDictionary<TNodeName, TNode> children ) => {
				newName = path.Name;
				newNode = selector( path, node, children );
			}, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName, TNode2> Select<TNodeName, TNode1, TNode2>(
			this Tree<TNodeName, TNode1> tree,
			Func<Path<TNodeName>, TNode1, IReadOnlyDictionary<TNodeName, TNode2>, TNode2> selector,
			Func<TNode2, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNode2> getChild )
			where TNodeName : IComparable {
			return tree.Select( ( Path<TNodeName> path, TNode1 node, out TNodeName newName, out TNode2 newNode,
				IReadOnlyDictionary<TNodeName, TNode2> children ) => {
				newName = path.Name;
				newNode = selector( path, node, children );
			}, getChildNames, getChild );
		}
        
		public static Tree<TNodeName, TNode> Select<TNodeName, TNode>(
			this Tree<TNodeName, TNode> tree,
			Selector<TNodeName, TNode, TNodeName, TNode> selector )
			where TNodeName : IComparable {
			return tree.Select( selector, tree.GetChildNames, tree.TryGet );
		}

		public static Tree<TNodeName2, TNode2> Select<TNodeName1, TNode1, TNodeName2, TNode2>(
			this Tree<TNodeName1, TNode1> tree,
			Selector<TNodeName1, TNode1, TNodeName2, TNode2> selector,
			Func<TNode2, IEnumerable<TNodeName2>> getChildNames,
			TryGetChild<TNodeName2, TNode2> getChild )
			where TNodeName1 : IComparable
			where TNodeName2 : IComparable {
			var branchBuilders = new Stack<SelectBranchBuilder<TNodeName2, TNode1, TNode2>>();
			var count = 0;

			foreach ( var treeTraversalResult in tree.AsEnumerable() ) {
				count++;
				switch ( treeTraversalResult.Type ) {
					case TreeTraversalType.EnterBranch:
						branchBuilders.Push( new SelectBranchBuilder<TNodeName2, TNode1, TNode2>( treeTraversalResult.Value ) );
						break;
					case TreeTraversalType.Leaf:
						selector( treeTraversalResult.Path, treeTraversalResult.Value, out var newName,
							out var newNode, ImmutableDictionary<TNodeName2, TNode2>.Empty );
						branchBuilders.Peek().ChildrenToBe.Add( newName, newNode );
						break;
					case TreeTraversalType.ExitBranch:
						var branchBuilder = branchBuilders.Pop();
						selector( treeTraversalResult.Path, branchBuilder.Value, out newName,
							out newNode, branchBuilder.ChildrenToBe );
						if ( branchBuilders.Count == 0 ) {
							return newNode.ToTree( getChildNames, getChild );
						} else {
							branchBuilders.Peek().ChildrenToBe.Add( newName, newNode );
						}
						break;
				}
			}

			if ( count != 0 ) {
				throw new Exception( "Unknown algorithmic error: failed to create tree" );
			}

			return Tree<TNodeName2, TNode2>.Empty;
		}

		private class TreeTraversalState<TNodeName, TNode>
			where TNodeName : IComparable {
			public TreeTraversalState( Path<TNodeName> path, TNode node, IEnumerable<TNodeName> childNames ) {
				Path = path;
				Node = node;
				ChildNames = childNames.GetEnumerator();
			}

			public Path<TNodeName> Path { get; }
			public TNode Node { get; }
			public IEnumerator<TNodeName> ChildNames { get; }
		}

		public static Tree<TNodeName, TNode> Where<TNodeName, TNode>( this Tree<TNodeName, TNode> tree, Func<Path<TNodeName>, TNode, bool> predicate )
			where TNodeName : IComparable {
			return new Tree<TNodeName, TNode>( () => tree.WhereInternal( predicate ), tree.Root, tree.GetChildNames, tree.TryGet );
		}

		private static IEnumerable<TreeTraversal<TNodeName, TNode>> WhereInternal<TNodeName, TNode>( this Tree<TNodeName, TNode> tree, Func<Path<TNodeName>, TNode, bool> predicate )
			where TNodeName : IComparable {
			Path<TNodeName> pathBeingRemoved = null;

			foreach ( var traversal in tree.AsEnumerable() ) {
				if ( pathBeingRemoved != null ) {
					if ( pathBeingRemoved.Equals( traversal.Path ) || traversal.Path.StartsWith( pathBeingRemoved ) ) {
						continue;
					}

					pathBeingRemoved = null;
				}

				if ( pathBeingRemoved == null ) {
					if ( !predicate( traversal.Path, traversal.Value ) ) {
						pathBeingRemoved = traversal.Path;
						continue;
					}
				}

				yield return traversal;
			}
		}

		public static Tree<TNodeName, TNode> ToDictionaryTree<TNodeName, TNode>(this IReadOnlyDictionary<TNodeName, TNode> dictionary)
		where TNodeName : IComparable {
			return ((TNode)dictionary).ToTree( x => ((IReadOnlyDictionary<TNodeName, TNode>)x).Keys,
				( TNode node, TNodeName childname,
					out TNode child ) => {
					if ( node is IReadOnlyDictionary<TNodeName, TNode> nodeAsDictionary ) {
						child = nodeAsDictionary[childname];
						return true;
					}

					child = default( TNode );
					return false;
				} );
		}

		public static Tree<TNodeName, TNode> ToTree<TNodeName, TNode>(
			this TNode root,
			Func<TNode, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNode> getChild )
			where TNodeName : IComparable {
			return new Tree<TNodeName, TNode>( root, getChildNames, getChild );
		}

		public static IEnumerable<TreeTraversal<TNodeName, TNode>> TraverseTree<TNodeName, TNode>(
			this TNode root,
			Func<TNode, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNode> getChild )
		where TNodeName : IComparable
		{
			var traversalStack = new Stack<TreeTraversalState<TNodeName, TNode>>();
			traversalStack.Push( new TreeTraversalState<TNodeName, TNode>( Path<TNodeName>.Empty, root, getChildNames( root ) ) );

			var hasYieldedInitialIteration = false;

			while ( traversalStack.Count > 0 ) {
				if ( !hasYieldedInitialIteration ) {
					yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, ImmutableList<TNodeName>.Empty, root );
					hasYieldedInitialIteration = true;
				}

				if ( !traversalStack.Peek().ChildNames.MoveNext() ) {
					traversalStack.Pop();
					if ( traversalStack.Count > 0 ) {
						var path = traversalStack.Peek().Path + traversalStack.Peek().ChildNames.Current;
						yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, path, traversalStack.Peek().Node );
					}
				} else {
					var path = traversalStack.Peek().Path + traversalStack.Peek().ChildNames.Current;
					if ( !getChild( traversalStack.Peek().Node, traversalStack.Peek().ChildNames.Current,
						out var node ) ) {
						throw new InvalidDataException("Attempted to access child by name and failed");
					}
					if ( getChildNames( node ).Any() ) {
						yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, path, node );
						traversalStack.Push( new TreeTraversalState<TNodeName, TNode>( path, node, getChildNames( node ) ) );
					} else {
						yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.Leaf, path, node );
					}
				}
			}

			if ( hasYieldedInitialIteration ) {
				yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, ImmutableList<TNodeName>.Empty, root );
			} else {
				yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.Leaf, ImmutableList<TNodeName>.Empty, root );
			}
		}
	}
}