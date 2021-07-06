using System;
using System.Collections.Generic;

namespace ComposableCollections.List
{
    public interface IOrderedList<T> : ICollection<T>
    {
        int Compare(T item1, T item2);
        IOrderedList<T> ThenBy(Func<T, T, int> comparer);
    }
}