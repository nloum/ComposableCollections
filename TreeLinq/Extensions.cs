using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using TreeLinq.Exceptions;

namespace TreeLinq
{
	public static class Extensions {
		private static DictionaryAdapterFactory _dictionaryAdapterFactory = new DictionaryAdapterFactory();

		public static LazyTree<TNodeName, TNode> BranchChildrenToTree<TNodeName, TNode>( this IReadOnlyDictionary<TNodeName, LazyTree<TNodeName, TNode>> branchChildren )
			where TNodeName : IComparable {
			var start = new[] { new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, AbsolutePath<TNodeName>.Root ) };
			var enumerable = branchChildren.SelectMany( child =>
				child.Value.AsEnumerable().Select( x =>
					new TreeTraversal<TNodeName, TNode>( x.Type, child.Key.NameToPath().Add(new RelativePath<TNodeName>(x.Path)), x.Value ) ) );
			var finish = new[] {new TreeTraversal<TNodeName, TNode>( TreeTraversalType.ExitBranch, AbsolutePath<TNodeName>.Root )};

			return new LazyTree<TNodeName, TNode>( start.Concat(enumerable).Concat(finish) );
		}

		public static T As<TNodeName, TNode, T>( this LazyTree<TNodeName, TNode> tree ) where TNodeName : IComparable {
			return (T)tree.As( typeof( T ) );
		}

		public static IEnumerable<TreeTraversal<TNodeName, TNode>> AsLeaf<TNodeName, TNode>(this TNode node) where TNodeName : IComparable {
			yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.Leaf, AbsolutePath<TNodeName>.Root, node );
		}

		public static object As<TNodeName, TNode>(this LazyTree<TNodeName, TNode> tree, Type type ) where TNodeName : IComparable {
			var first = tree.AsEnumerable().First();
			if ( first.Type == TreeTraversalType.Leaf ) {
				return first.Value;
			}

			var result = new Hashtable();
			var stack = new Stack<Hashtable>();
			stack.Push( result );
			foreach ( var item in tree.AsEnumerable() ) {
				if ( item.Type == TreeTraversalType.EnterBranch ) {
					stack.Peek()[item.Path.Name] = new Hashtable();
				} else if ( item.Type == TreeTraversalType.Leaf ) {
					stack.Peek()[item.Path.Name] = item.Value;
				} else if ( item.Type == TreeTraversalType.ExitBranch ) {
					stack.Pop();
				}
			}

			return _dictionaryAdapterFactory.GetAdapterRecursively(type, result);
		}

		private static object GetAdapterRecursively(this DictionaryAdapterFactory factory, Type type, Hashtable hashtable) {
			foreach(var key in hashtable.Keys) {
				if (hashtable[key] is Hashtable subHashtable) {
					hashtable[key] = factory.GetAdapterRecursively( type.GetProperty( key.ToString() ).PropertyType, subHashtable );
				}
			}
			return _dictionaryAdapterFactory.GetAdapter( type, hashtable );
		}

		public static PreComputedTree<TNodeName, TNode> CompileTree<TNodeName, TNode>( this ITree<TNodeName, TNode> tree ) where TNodeName : IComparable {
			var first = tree.AsEnumerable().First();
			if (first.Type == TreeTraversalType.Leaf) {
				return new PreComputedTree<TNodeName, TNode>( first.Value );
			}

			var result = new PreComputedTree<TNodeName, TNode>();
			var stack = new Stack<PreComputedTree<TNodeName, TNode>>();
			stack.Push( result );
			foreach(var item in tree.AsEnumerable()) {
				if (item.Type == TreeTraversalType.EnterBranch) {
					stack.Peek().SetNode(item.Path.Name, new PreComputedTree<TNodeName, TNode>() );
				} else if (item.Type == TreeTraversalType.Leaf) {
					stack.Peek().SetNode( item.Path.Name, new PreComputedTree<TNodeName, TNode>( item.Value ) );
				} else if (item.Type == TreeTraversalType.ExitBranch) {
					stack.Pop();
				}
			}

			return result;
		}

		public static TNode GetLeaf<TNodeName, TNode>( this ITree<TNodeName, TNode> tree, TNodeName name1 ) where TNodeName : IComparable {
			var path1 = name1.NameToPath();
			return tree.GetLeaf( path1 );
		}

		public static bool TryGetLeaf<TNodeName, TNode, TNode1>( this ITree<TNodeName, TNode> tree, TNodeName name1, out TNode1 leaf1 )
			where TNodeName : IComparable
			where TNode1 : TNode {
			var path1 = name1.NameToPath();
			return tree.TryGetLeaf( path1, out leaf1 );

		}

		public static bool TryGetLeaves<TNodeName, TNode, TNode1, TNode2>( this ITree<TNodeName, TNode> tree, TNodeName name1, out TNode1 leaf1, TNodeName name2, out TNode2 leaf2 )
			where TNodeName : IComparable
			where TNode1 : TNode
			where TNode2 : TNode {
			var path1 = name1.NameToPath();
			var path2 = name2.NameToPath();
			return tree.TryGetLeaves( path1, out leaf1, path2, out leaf2 );
		}

		public static ITree<TNodeName, TNode> GetBranch<TNodeName, TNode>( this ITree<TNodeName, TNode> tree, TNodeName name1 )
			where TNodeName : IComparable {
			var path1 = name1.NameToPath();
			return tree.GetBranch( path1 );
		}

		public static bool TryGetBranch<TNodeName, TNode>( this ITree<TNodeName, TNode> tree, TNodeName name1, out ITree<TNodeName, TNode> branch1 )
			where TNodeName : IComparable {
			var path1 = name1.NameToPath();
			return tree.TryGetBranch( path1, out branch1 );
		}

		public static bool TryGetBranches<TNodeName, TNode>( this ITree<TNodeName, TNode> tree, TNodeName name1, out ITree<TNodeName, TNode> branch1, TNodeName name2, out ITree<TNodeName, TNode> branch2 )
			where TNodeName : IComparable {
			var path1 = name1.NameToPath();
			var path2 = name2.NameToPath();
			return tree.TryGetBranches( path1, out branch1, path2, out branch2 );
		}

		public static TNode GetRootLeaf<TNodeName, TNode>( this ITree<TNodeName, TNode> tree )
			where TNodeName : IComparable {
			return tree.GetLeaf( AbsolutePath<TNodeName>.Root );
		}

		public static bool TryGetRootLeaf<TNodeName, TNode, TNode1>( this ITree<TNodeName, TNode> tree, out TNode1 leaf1 )
			where TNodeName : IComparable
			where TNode1 : TNode {
			return tree.TryGetLeaf( AbsolutePath<TNodeName>.Root, out leaf1 );
		}

		public static TNode GetLeaf<TNodeName, TNode>( this ITree<TNodeName, TNode> tree, AbsolutePath<TNodeName> path1 ) where TNodeName : IComparable {
			if ( !tree.TryGetLeaf( path1, out TNode result ) ) {
				throw new NoSuchNodeNameException();
			}
			return result;
		}

        public static ITree<TNodeName, TNode> GetBranch<TNodeName, TNode>( this ITree<TNodeName, TNode> tree, AbsolutePath<TNodeName> path1 )
	        where TNodeName : IComparable {
	        if ( !tree.TryGetBranch( path1, out var result ) ) {
				throw new NoSuchNodeNameException();
	        }

	        return result;
        }

		internal static AbsolutePath<TNodeName> NameToPath<TNodeName>(this TNodeName name) where TNodeName : IComparable {
            return new AbsolutePath<TNodeName>( name );
		}

		public static bool ContainsNode<TNodeName, TNode>( this LazyTree<TNodeName, TNode> tree, AbsolutePath<TNodeName> path ) where TNodeName : IComparable {
			foreach ( var item in tree.AsEnumerable() ) {
				if ( item.Path == path ) {
					return true;
				}
			}

			return false;
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

		public static LazyTree<TNodeName, TNode> AsRoot<TNodeName, TNode>( this TNode root ) where TNodeName : IComparable {
			var items = new List<TreeTraversal<TNodeName, TNode>>();
			items.Add(new TreeTraversal<TNodeName, TNode>( TreeTraversalType.Leaf, AbsolutePath<TNodeName>.Root, root ));
			return new LazyTree<TNodeName, TNode>( items );
		}

		public static LazyTree<TNodeName, TNode> SetNode<TNodeName, TNode>( this LazyTree<TNodeName, TNode> tree,
			TNodeName name, TNode newValue ) where TNodeName : IComparable {
			return tree.SetNode( new AbsolutePath<TNodeName>( name ), newValue.AsRoot<TNodeName, TNode>() );
		}

		public static LazyTree<TNodeName, TNode> SetNode<TNodeName, TNode>( this LazyTree<TNodeName, TNode> tree,
			AbsolutePath<TNodeName> path, TNode newValue ) where TNodeName : IComparable {
			return tree.SetNode( path, newValue.AsRoot<TNodeName, TNode>() );
		}

		public static LazyTree<TNodeName, TNode> SetNode<TNodeName, TNode>( this LazyTree<TNodeName, TNode> tree,
			TNodeName name, LazyTree<TNodeName, TNode> newValue ) where TNodeName : IComparable {
			return tree.SetNode( new AbsolutePath<TNodeName>( name ), newValue );
		}

		public static LazyTree<TNodeName, TNode> SetNode<TNodeName, TNode>( this LazyTree<TNodeName, TNode> tree,
			AbsolutePath<TNodeName> path, LazyTree<TNodeName, TNode> newValue ) where TNodeName : IComparable {
			if ( path.IsRoot ) {
				return newValue;
			}
			return new LazyTree<TNodeName, TNode>( SetNodeInternal( tree, path, newValue));
		}

		private static IEnumerable<TreeTraversal<TNodeName, TNode>> SetNodeInternal<TNodeName, TNode>( this LazyTree<TNodeName, TNode> tree, AbsolutePath<TNodeName> path, LazyTree<TNodeName, TNode> newValue ) where TNodeName : IComparable {
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

		public static LazyTree<TNodeName, TNode> AsTree<TNodeName, TNode>(
			this IEnumerable<TreeTraversal<TNodeName, TNode>> items ) where TNodeName : IComparable {
			return new LazyTree<TNodeName, TNode>( items );
		}

		private class SelectBranchBuilder<TNodeName, TNode1, TNode2> where TNodeName : IComparable {
			public SelectBranchBuilder( TNode1 value ) {
				Value = value;
			}

			public TNode1 Value { get; }

			public Dictionary<TNodeName, LazyTree<TNodeName, TNode2>> ChildrenToBe { get; } = new Dictionary<TNodeName, LazyTree<TNodeName, TNode2>>();
		}

		public static LazyTree<TNodeName2, TNode2> SelectNodes<TNodeName1, TNode1, TNodeName2, TNode2>(
			this LazyTree<TNodeName1, TNode1> tree,
			Func<Path<TNodeName1>, IReadOnlyDictionary<TNodeName2, LazyTree<TNodeName2, TNode2>>, IEnumerable<TreeTraversal<TNodeName2, TNode2>>> branchSelector = null,
			Func<Path<TNodeName1>, TNode1, IEnumerable<TreeTraversal<TNodeName2, TNode2>>> leafSelector = null)
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
						var leafReplacementNode = leafSelector( treeTraversalResult.Path, treeTraversalResult.Value );
						var leafReplacementNodePath = leafReplacementNode.First().Path;
						if ( leafReplacementNodePath.Count != 1 ) {
							throw new InvalidOperationException();
						}
						var newName = leafReplacementNodePath.First();
						branchBuilders.Peek().ChildrenToBe.Add( newName, leafReplacementNode.AsTree() );
						break;
					case TreeTraversalType.ExitBranch:
						var branchBuilder = branchBuilders.Pop();
						IEnumerable<TreeTraversal<TNodeName2, TNode2>> branchReplacementNode;
						if ( branchSelector != null ) {
							branchReplacementNode = branchSelector( treeTraversalResult.Path, branchBuilder.ChildrenToBe );
						} else {
							branchReplacementNode = branchBuilder.ChildrenToBe.Values.SelectMany( x => x );
						}
						if ( branchBuilders.Count == 0 ) {
							return branchReplacementNode.AsTree();
						} else {
							var branchReplacementNodePath = branchReplacementNode.First().Path;
							if ( branchReplacementNodePath.Count != 1 ) {
								throw new InvalidOperationException();
							}
							branchBuilders.Peek().ChildrenToBe.Add( branchReplacementNodePath.First(), branchReplacementNode.AsTree() );
						}
						break;
				}
			}

			if ( count != 0 ) {
				throw new Exception( "Unknown algorithmic error: failed to create tree" );
			}

			return LazyTree<TNodeName2, TNode2>.Empty;
		}

		public static LazyTree<TNodeName, TNode> WhereNodes<TNodeName, TNode>( this LazyTree<TNodeName, TNode> tree, Func<Path<TNodeName>, TNode, bool> predicate )
			where TNodeName : IComparable {
			return new LazyTree<TNodeName, TNode>( tree.WhereNodesInternal( predicate ) );
		}

		private static IEnumerable<TreeTraversal<TNodeName, TNode>> WhereNodesInternal<TNodeName, TNode>( this LazyTree<TNodeName, TNode> tree, Func<Path<TNodeName>, TNode, bool> predicate )
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

		public delegate bool TryGetChild<TNodeName, TNode>( TNode node, TNodeName childName, out TNode child ) where TNodeName : IComparable;

		public static LazyTree<TNodeName, TNode> ToDictionaryTree<TNodeName, TNode>(this IReadOnlyDictionary<TNodeName, TNode> dictionary)
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

		public static LazyTree<TNodeName, object> ToTree<TNodeName>(
			this IReadOnlyDictionary<TNodeName, object> treeSource) where TNodeName : IComparable {
			return treeSource.ToTree<TNodeName, object>( node => {
					if ( node is IReadOnlyDictionary<TNodeName, object> dictionary ) {
						return dictionary.Keys;
					}

					return ImmutableList<TNodeName>.Empty;
				},
				( object node, TNodeName childName, out object child ) => {
					if ( node is IReadOnlyDictionary<TNodeName, object> dictionary ) {
						if ( dictionary.ContainsKey( childName ) ) {
							child = dictionary[childName];
							return true;
						}
					}

					child = default;
					return false;
				}
			);
		}

		public static LazyTree<TNodeName, TNode> ToTree<TNodeName, TNode>(
			this TNode root,
			Func<TNode, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNode> getChild )
			where TNodeName : IComparable {
			return new LazyTree<TNodeName, TNode>( root.TraverseTree( getChildNames, getChild ) );
		}

		public static IEnumerable<TreeTraversal<TNodeName, TNodeName>> TraverseTree<TNodeName>(
			this TNodeName root,
			Func<TNodeName, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNodeName> getChild)
		where TNodeName : IComparable
		{
			return root.TraverseTree<TNodeName, TNodeName>(getChildNames, getChild);
		}
		
		public static IEnumerable<TreeTraversal<TNodeName, TNode>> TraverseTree<TNodeName, TNode>(
			this TNode root,
			Func<TNode, IEnumerable<TNodeName>> getChildNames,
			TryGetChild<TNodeName, TNode> getChild )
		where TNodeName : IComparable
		{
			if (!getChildNames(root).Any()) {
				yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.Leaf, AbsolutePath<TNodeName>.Root, root );
				yield break;
			}

			var traversalStack = new Stack<TreeTraversalState<TNodeName, TNode>>();
			traversalStack.Push( new TreeTraversalState<TNodeName, TNode>( AbsolutePath<TNodeName>.Root, root, getChildNames( root ) ) );

			var hasYieldedInitialIteration = false;

			while ( traversalStack.Count > 0 ) {
				if ( !hasYieldedInitialIteration ) {
					yield return new TreeTraversal<TNodeName, TNode>( TreeTraversalType.EnterBranch, ImmutableList<TNodeName>.Empty );
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