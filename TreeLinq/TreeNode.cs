using System.Collections;
using System.Collections.Generic;

namespace TreeLinq
{
	public class TreeNode<TNodeName, TNode> : ITreeNode<TNodeName, TNode> {
		private readonly Dictionary<TNodeName, ITreeNode<TNodeName, TNode>> _children = new Dictionary<TNodeName, ITreeNode<TNodeName, TNode>>();

		public bool IsLeaf => _children.Count == 0;

		public TreeNode( TNode value ) {
			Value = value;
		}

		public IEnumerable<TNodeName> ChildrenNames => _children.Keys;

		public ITreeNode<TNodeName, TNode> GetChild( TNodeName name ) {
			return _children[name];
		}

		public void SetChild( TNodeName name, ITreeNode<TNodeName, TNode> child ) {
			_children[name] = child;
		}

		public bool Remove( TNodeName name ) {
			return _children.Remove( name );
		}

		public TNode Value { get; }

		public IEnumerator<ITreeNode<TNodeName, TNode>> GetEnumerator() {
			return _children.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public override string ToString() {
			return Value.ToString();
		}
	}
}