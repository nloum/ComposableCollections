using System;
using System.Collections;
using System.Collections.Generic;
using GenericNumbers;
using GenericNumbers.Relational;
using static GenericNumbers.NumbersUtility;

namespace ComposableCollections.List
{
    public class OrderedList<T> : ICollection<T>, IReadOnlyList<T>
    {
        private readonly IList<T> _wrapped;
        private Func<T, T, int> _compare;

        public OrderedList(List<T> wrapped, Func<T, T, int> compare)
        {
            _wrapped = wrapped;
            _compare = compare;
        }

        public OrderedList(List<T> wrapped)
        {
            _wrapped = wrapped;
            _compare = (x, y) => x.CompareTo(y);
        }

        public OrderedList(Func<T, T, int> compare)
        {
            _wrapped = new List<T>();
            _compare = compare;
        }

        public OrderedList()
        {
            _wrapped = new List<T>();
            _compare = (x, y) => x.CompareTo(y);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _wrapped.GetEnumerator();
        }

        public T this[int index] => _wrapped[index];

        public void Add(T item)
        {
            var indexToInsertAt = _wrapped.GetIndexOfSortedInsert(item);
            _wrapped.Insert(indexToInsertAt, item);
        }

        public void Clear()
        {
            _wrapped.Clear();
        }

        public bool Contains(T item)
        {
            return _wrapped.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _wrapped.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _wrapped.Remove(item);
        }

        public int Count => _wrapped.Count;
        public bool IsReadOnly => false;

        public int Compare(T item1, T item2)
        {
            return _compare(item1, item2);
        }
        
        /// <summary>
        /// Uses a binary search to find the minimum and maximum indices at which the needle occurs.
        /// For instance, in the list [0, 1, 1, 2, 3, 3, 3, 3, 4], when this function is called with
        /// 3 as the needle, the return value will be 4 and 7, because that's the first and last indices
        /// of the item 3 in the list.
        /// </summary>
        /// <param name="haystack">The list in which to search</param>
        /// <param name="needle">A function that compares the item you're searching for with the specified item.
        /// If the parameter to this function precedes the item you're looking for in the sort order, then this function should return -1.
        /// If the parameter to this function follows the item you're looking for in the sort order, then this function should return 1.
        /// Otherwise, this function should return 0.
        /// The return value of this function is the same as the <see cref="IComparable.CompareTo(object)"/> function.</param>
        /// <param name="min">Specifies the minimum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <param name="max">Specifies the maximum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <returns>The absolute index of the element if found; otherwise, returns -1</returns>
        public INumberRange<int> RangeBinarySearch(Func<T, int> needle, int min = 0, int max = -1)
        {
            var rangeMin = _wrapped.GetIndexOfSortedInsert(t =>
            {
                var result = needle(t);
                if (result == 0)
                    return 1;
                return result;
            }, min, max);
            var rangeMax = _wrapped.GetIndexOfSortedInsert(t =>
            {
                var result = needle(t);
                if (result == 0)
                    return -1;
                return result;
            }, min, max) - 1;
            return Range(rangeMin, rangeMax, false, false);
        }

        /// <summary>
        /// Uses a binary search to find the minimum and maximum indices at which the needle occurs.
        /// For instance, in the list [0, 1, 1, 2, 3, 3, 3, 3, 4], when this function is called with
        /// 3 as the needle, the return value will be 4 and 7, because that's the first and last indices
        /// of the item 3 in the list.
        /// </summary>
        /// <param name="haystack">The list in which to search</param>
        /// <param name="needle">The item you're looking for.</param>
        /// <param name="compare">The function that compares two elements and determines which comes first in the sort order.
        /// The return value of this function is the same as the <see cref="IComparable.CompareTo(object)"/> function.</param>
        /// <param name="min">Specifies the minimum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <param name="max">Specifies the maximum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <returns>The absolute index of the element if found; otherwise, returns -1</returns>
        public INumberRange<int> RangeBinarySearch(T needle, int min = 0, int max = -1)
        {
            var rangeMin = _wrapped.GetIndexOfSortedInsert(t =>
            {
                var result = Compare(t, needle);
                if (result == 0)
                    return 1;
                return result;
            }, min, max);
            var rangeMax = _wrapped.GetIndexOfSortedInsert(t =>
            {
                var result = Compare(t, needle);
                if (result == 0)
                    return -1;
                return result;
            }, min, max) - 1;
            return Range(rangeMin, rangeMax, false, false);
        }

        /// <summary>
        /// Performs a binary search on the given data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="needle">The item for which to search</param>
        /// <param name="min">Specifies the minimum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <param name="max">Specifies the maximum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <returns>The absolute index of the element if found; otherwise, returns -1</returns>
        public int BinarySearch(T needle, int min = 0, int max = -1)
        {
            return this.BinarySearch(t => Compare(t, needle), min, max);
        }

        /// <summary>
        /// Performs a binary search on the given data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="haystack"></param>
        /// <param name="by">A function that returns the comparison between the specified element (the parameter to this function) and the desired element. For instance, if search for the element 10, this function might look like this: element => element.CompareTo(10)</param>
        /// <param name="min">Specifies the minimum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <param name="max">Specifies the maximum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <returns>The absolute index of the element if found; otherwise, returns -1</returns>
        public int BinarySearch(Func<T, int> by, int min = 0, int max = -1)
        {
            if (Count == 0)
                return -1;
            min = _wrapped.GetAbsoluteIndex(min);
            max = _wrapped.GetAbsoluteIndex(max);
            if (max < min)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }
            do
            {
                int current = (min + max) / 2;
                var comparison = by(_wrapped[current]);
                if (comparison == 0)
                    return current;
                if (comparison < 0)
                    min = current + 1;
                else
                    max = current - 1;
            } while (min <= max);
            return -1;
        }
    }
}