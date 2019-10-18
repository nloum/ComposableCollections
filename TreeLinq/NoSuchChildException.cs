using System;

namespace TreeLinq
{
	public class NoSuchChildException : Exception {
		public object Node { get; }
		public object ChildName { get; }

		public NoSuchChildException( object node, object childName ) {
			Node = node;
			ChildName = childName;
		}
	}
}