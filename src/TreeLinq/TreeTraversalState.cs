using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace TreeLinq
{
	internal class TreeTraversalState<TNodeName, TNode>
		where TNodeName : IComparable {
		public TreeTraversalState( AbsoluteTreePath<TNodeName> path, TNode node, IEnumerable<TNodeName> childNames ) {
			Path = path;
			Node = node;
			ChildNames = childNames.GetEnumerator();
		}

		public TNode Node { get; }
		public AbsoluteTreePath<TNodeName> Path { get; }
		public IEnumerator<TNodeName> ChildNames { get; }
	}
}