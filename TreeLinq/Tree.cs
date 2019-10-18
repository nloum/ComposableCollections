using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace TreeLinq
{
	public class Tree<TNodeName, TNode> where TNodeName : IComparable {
		private Func<IEnumerable<TreeTraversal<TNodeName, TNode>>> _items;
		protected readonly Func<TNode, IEnumerable<TNodeName>> _getChildNames;
		protected readonly Extensions.TryGetChild<TNodeName, TNode> _getChild;

		public static Tree<TNodeName, TNode> Empty { get; } = new Tree<TNodeName, TNode>();

		private Tree() {
			_items = () => ImmutableList<TreeTraversal<TNodeName, TNode>>.Empty;
			Root = default(TNode);
			_getChildNames = _ => throw new NotImplementedException();
			_getChild = (TNode _, TNodeName __, out TNode ___) => throw new NotImplementedException();
		}

		public Tree( Func<IEnumerable<TreeTraversal<TNodeName, TNode>>> items, TNode root, Func<TNode, IEnumerable<TNodeName>> getChildNames,
			Extensions.TryGetChild<TNodeName, TNode> getChild ) {
			_items = items;
			Root = root;
			_getChildNames = getChildNames;
			_getChild = getChild;
		}

		public Tree(TNode root, Func<TNode, IEnumerable<TNodeName>> getChildNames, Extensions.TryGetChild<TNodeName, TNode> getChild) {
			_items = () => root.TraverseTree(getChildNames, getChild);
			Root = root;
			_getChildNames = getChildNames;
			_getChild = getChild;
		}

		public TNode Root { get; }

		public IEnumerable<TreeTraversal<TNodeName, TNode>> AsEnumerable() {
			return _items();
		}

		public IEnumerable<TNodeName> GetChildNames( TNode node ) {
			return _getChildNames( node );
		}

		public IEnumerable<TNodeName> GetChildNames() {
			return GetChildNames( Root );
		}

		public TNode Get( TNode node, TNodeName name ) {
			if ( !TryGet( node, name, out var result ) ) {
				throw new NoSuchChildException( node, name );
			}

			return result;
		}

		public TNode Get( TNodeName name ) {
			if ( !TryGet( Root, name, out var result ) ) {
				throw new NoSuchChildException( Root, name );
			}

			return result;
		}

		public TNode Get( Path<TNodeName> path ) {
			var currentNode = Root;
			foreach ( var item in path ) {
				currentNode = Get( currentNode, item );
			}

			return currentNode;
		}

		public Tree<TNodeName, TNode> GetAsTree( Path<TNodeName> path ) {
			var currentNode = Root;
			foreach ( var item in path ) {
				currentNode = Get( currentNode, item );
			}

			return new Tree<TNodeName, TNode>( currentNode, _getChildNames, _getChild );
		}

		public Tree<TNodeName, TNode> GetAsTree( TNodeName name ) {
			if ( !TryGetAsTree( Root, name, out var result ) ) {
				throw new NoSuchChildException( Root, name );
			}

			return result;
		}

		public Tree<TNodeName, TNode> GetAsTree( TNode node, TNodeName name ) {
			if ( !TryGetAsTree( node, name, out var result ) ) {
				throw new NoSuchChildException(node, name);
			}

			return result;
		}

		public bool TryGet( TNode node, TNodeName name, out TNode child ) {
			return _getChild( node, name, out child );
		}

		public bool TryGet( TNodeName name, out TNode child ) {
			return TryGet( Root, name, out child );
		}

		public bool TryGet( Path<TNodeName> path, out TNode child ) {
			var currentNode = Root;
			foreach ( var item in path ) {
				if ( !TryGet( currentNode, item, out currentNode ) ) {
					child = default(TNode);
					return false;
				}
			}

			child = currentNode;
			return true;
		}

		public bool TryGetAsTree( Path<TNodeName> path, out Tree<TNodeName, TNode> child ) {
			if ( !TryGet( path, out var childNode ) ) {
				child = null;
				return false;
			}

			child = new Tree<TNodeName, TNode>(childNode, _getChildNames, _getChild);
			return true;
		}

		public bool TryGetAsTree( TNodeName name, out Tree<TNodeName, TNode> child ) {
			return TryGetAsTree( Root, name, out child );
		}

		public bool TryGetAsTree( TNode node, TNodeName name, out Tree<TNodeName, TNode> child ) {
			if ( !TryGet( node, name, out var childNode ) ) {
				child = null;
				return false;
			}

			child = new Tree<TNodeName, TNode>(childNode, _getChildNames, _getChild);
			return true;
		}
	}
}