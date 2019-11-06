using System;
using System.Collections.Generic;

namespace TreeLinq
{
	public interface ITree<TNodeName, TNode> : IEnumerable<TreeTraversal<TNodeName, TNode>> where TNodeName : IComparable {
		bool TryGetBranch( Path<TNodeName> path1, out ITree<TNodeName, TNode> branch1 );
		bool TryGetBranches( Path<TNodeName> path1, out ITree<TNodeName, TNode> branch1,
			Path<TNodeName> path2, out ITree<TNodeName, TNode> branch2 );
		bool TryGetLeaf<TNode1>( Path<TNodeName> path1, out TNode1 leaf1 )
			where TNode1 : TNode;
		bool TryGetLeaves<TNode1, TNode2>( Path<TNodeName> path1, out TNode1 leaf1, Path<TNodeName> path2,
			out TNode2 leaf2 )
			where TNode1 : TNode
			where TNode2 : TNode;
	}
}