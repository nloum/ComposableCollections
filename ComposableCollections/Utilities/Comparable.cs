using System;

namespace ComposableCollections.Utilities
{
    public class Comparable<T> : IComparable<Comparable<T>>
    {
        public T Item { get; }
        readonly Func<Comparable<T>, Comparable<T>, int> _compareTo;

        public Comparable(T item, Func<Comparable<T>, Comparable<T>, int> compareTo)
        {
            Item = item;
            _compareTo = compareTo;
        }

        public int CompareTo(Comparable<T> other) => _compareTo(this, other);
    }
}