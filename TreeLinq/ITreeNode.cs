using System;
using System.Collections.Generic;
using System.Text;

namespace TreeLinq {
	public interface ITreeNode<TNodeName, TNode> : IEnumerable<ITreeNode<TNodeName, TNode>> {
		bool IsLeaf { get; }
		IEnumerable<TNodeName> ChildrenNames { get; }
		ITreeNode<TNodeName, TNode> GetChild( TNodeName name );
		void SetChild( TNodeName name, ITreeNode<TNodeName, TNode> child );
		bool Remove( TNodeName name );
		TNode Value { get; }
	}
}
