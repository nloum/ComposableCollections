using System;
using System.Collections.Generic;
using GenericNumbers;
using GenericNumbers.Relational;
using static GenericNumbers.NumbersUtility;

namespace ComposableCollections.List
{
    public static class OrderedListExtensions
    {
        #region Sorted insertions
        
        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T>(this IReadOnlyList<T> target, Func<T, int> comparer, int min = 0, int max = -1)
        {
            // Special case for when the target list is empty
            if (target.Count == 0)
                return 0;

            min = target.GetAbsoluteIndex(min);
            max = target.GetAbsoluteIndex(max);
            if (max < min)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }

            // Special case for when the insertion point
            // is off the end of the list
            if (comparer(target[max]) < 0)
                return max + 1;

            int current;
            int currentComparison;
            while(max > min)
            {
                current = (min + max) / 2;
                currentComparison = comparer(target[current]);
                var minComparison = comparer(target[min]);
                var maxComparison = comparer(target[max]);
                if (minComparison == 0)
                    return min;
                else if (currentComparison == 0)
                    return current;
                else if (maxComparison == 0)
                    return max;
                else if (currentComparison > 0)
                {
                    if (min == max)
                        return Math.Max(min, max);
                    max = current;
                }
                else
                {
                    if (min == current)
                        return Math.Max(min, max);
                    min = current;
                }
            }

            return Math.Max(min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T>(this IReadOnlyList<T> target, T newElement, Func<T, T, int> compare, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert(newElement, (t1) => compare(t1, newElement), min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T, TKey>(this IReadOnlyList<T> target, T newElement, Func<T, TKey> keySelector, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert((t1) => keySelector(t1).CompareTo(keySelector(newElement)), min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T>(this IReadOnlyList<T> target, T newElement, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert(newElement, t => t, min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T>(this IList<T> target, Func<T, int> comparer, int min = 0, int max = -1)
        {
            // Special case for when the target list is empty
            if (target.Count == 0)
                return 0;

            min = target.GetAbsoluteIndex(min);
            max = target.GetAbsoluteIndex(max);
            if (max < min)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }

            // Special case for when the insertion point
            // is off the end of the list
            if (comparer(target[max]) < 0)
                return max + 1;

            int current;
            int currentComparison;
            while (max > min)
            {
                current = (min + max) / 2;
                currentComparison = comparer(target[current]);
                var minComparison = comparer(target[min]);
                var maxComparison = comparer(target[max]);
                if (minComparison == 0)
                    return min;
                else if (currentComparison == 0)
                    return current;
                else if (maxComparison == 0)
                    return max;
                else if (currentComparison > 0)
                {
                    if (min == max)
                        return Math.Max(min, max);
                    max = current;
                }
                else
                {
                    if (min == current)
                        return Math.Max(min, max);
                    min = current;
                }
            }

            return Math.Max(min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T>(this IList<T> target, T newElement, Func<T, T, int> compare, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert(newElement, (t1) => compare(t1, newElement), min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T, TKey>(this IList<T> target, T newElement, Func<T, TKey> keySelector, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert((t1) => keySelector(t1).CompareTo(keySelector(newElement)), min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T>(this IList<T> target, T newElement, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert(newElement, t => t, min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function inserts the specified newElement
        /// into the target list at a location that makes the target list remain sorted.
        /// </summary>
        public static int SortedInsert<T>(this IList<T> target, T newElement, int min = 0, int max = -1)
        {
            var index = GetIndexOfSortedInsert(target, newElement, min, max);

            target.Insert(index, newElement);

            return index;
        }

        /// <summary>
        /// Assumes that target is already sorted. This function inserts the specified newElement
        /// into the target list at a location that makes the target list remain sorted.
        /// </summary>
        public static int SortedInsert<T, TKey>(this IList<T> target, T newElement, Func<T, TKey> keySelector, int min = 0, int max = -1)
        {
            var index = GetIndexOfSortedInsert(target, newElement, keySelector, min, max);

            target.Insert(index, newElement);

            return index;
        }

        /// <summary>
        /// Assumes that target is already sorted. This function inserts the specified newElement
        /// into the target list at a location that makes the target list remain sorted.
        /// </summary>
        public static int SortedInsert<T>(this IList<T> target, T newElement, Func<T, int> comparer, int min = 0, int max = -1)
        {
            var index = GetIndexOfSortedInsert(target, newElement, comparer, min, max);

            target.Insert(index, newElement);

            return index;
        }

        /// <summary>
        /// Assumes that target is already sorted. This function inserts the specified newElement
        /// into the target list at a location that makes the target list remain sorted.
        /// </summary>
        public static int SortedInsert<T>(this IList<T> target, T newElement, Func<T, T, int> comparer, int min = 0, int max = -1)
        {
            var index = GetIndexOfSortedInsert(target, t1 => comparer(t1, newElement), min, max);

            target.Insert(index, newElement);

            return index;
        }
        
        #endregion
        
        #region Binary search

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
        public static INumberRange<int> RangeBinarySearch<T>(this IReadOnlyList<T> haystack, Func<T, int> needle, int min = 0, int max = -1)
        {
            var rangeMin = haystack.GetIndexOfSortedInsert(t =>
            {
                var result = needle(t);
                if (result == 0)
                    return 1;
                return result;
            }, min, max);
            var rangeMax = haystack.GetIndexOfSortedInsert(t =>
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
        public static INumberRange<int> RangeBinarySearch<T>(this IReadOnlyList<T> haystack, T needle, Func<T, T, int> compare, int min = 0, int max = -1)
        {
            var rangeMin = haystack.GetIndexOfSortedInsert(t =>
            {
                var result = compare(t, needle);
                if (result == 0)
                    return 1;
                return result;
            }, min, max);
            var rangeMax = haystack.GetIndexOfSortedInsert(t =>
            {
                var result = compare(t, needle);
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
        /// <param name="needle">The item you're searching for.</param>
        /// <param name="min">Specifies the minimum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <param name="max">Specifies the maximum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <returns>The absolute index of the element if found; otherwise, returns -1</returns>
        public static INumberRange<int> RangeBinarySearch<T>(this IReadOnlyList<T> haystack, T needle, int min = 0, int max = -1)
        {
            var rangeMin = haystack.GetIndexOfSortedInsert(t =>
            {
                var result = t.CompareTo(needle);
                if (result == 0)
                    return 1;
                return result;
            }, min, max);
            var rangeMax = haystack.GetIndexOfSortedInsert(t =>
            {
                var result = t.CompareTo(needle);
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
        /// <param name="needle">A function that compares the item you're searching for with the specified item.</param>
        /// <param name="keySelector">The key that is comparable. The Compare function for this type defines the sort order of this list
        /// and is used by the binary search algorithm.</param>
        /// <param name="min">Specifies the minimum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <param name="max">Specifies the maximum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <returns>The absolute index of the element if found; otherwise, returns -1</returns>
        public static INumberRange<int> RangeBinarySearch<T, TKey>(this IReadOnlyList<T> haystack, T needle, Func<T, TKey> keySelector, int min = 0, int max = -1)
        {
            var rangeMin = haystack.GetIndexOfSortedInsert(t =>
            {
                var result = keySelector(t).CompareTo(keySelector(needle));
                if (result == 0)
                    return 1;
                return result;
            }, min, max);
            var rangeMax = haystack.GetIndexOfSortedInsert(t =>
            {
                var result = keySelector(t).CompareTo(keySelector(needle));
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
        public static int BinarySearch<T>(this IReadOnlyList<T> haystack, T needle, int min = 0, int max = -1)
        {
            return haystack.BinarySearch(t => t.CompareTo(needle), min, max);
        }

        /// <summary>
        /// Performs a binary search on the given data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="needle">The item for which to search</param>
        /// <param name="keySelector">The function that, for a given item, returns the key by which the item is compared</param>
        /// <param name="min">Specifies the minimum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <param name="max">Specifies the maximum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <returns>The absolute index of the element if found; otherwise, returns -1</returns>
        public static int BinarySearch<T, TKey>(this IReadOnlyList<T> haystack, T needle, Func<T, TKey> keySelector, int min = 0, int max = -1)
        {
            return haystack.BinarySearch(t => keySelector(t).CompareTo(keySelector(needle)), min, max);
        }

        /// <summary>
        /// Performs a binary search on the given data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="needle">The item for which to search</param>
        /// <param name="keySelector">The function that, for a given item, returns the key by which the item is compared</param>
        /// <param name="min">Specifies the minimum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <param name="max">Specifies the maximum index at which the element might be found. This can be used to ignore big portions of the list, reducing the number of iterations the search must perform. This can be a relative index.</param>
        /// <returns>The absolute index of the element if found; otherwise, returns -1</returns>
        public static int BinarySearch<T>(this IReadOnlyList<T> haystack, T needle, Func<T, T, int> comparer, int min = 0, int max = -1)
        {
            return haystack.BinarySearch(t => comparer(t, needle), min, max);
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
        public static int BinarySearch<T>(this IReadOnlyList<T> haystack, Func<T, int> by, int min = 0, int max = -1)
        {
            if (haystack.Count == 0)
                return -1;
            min = haystack.GetAbsoluteIndex(min);
            max = haystack.GetAbsoluteIndex(max);
            if (max < min)
            {
                var tmp = max;
                max = min;
                min = tmp;
            }
            do
            {
                int current = (min + max) / 2;
                var comparison = by(haystack[current]);
                if (comparison == 0)
                    return current;
                if (comparison < 0)
                    min = current + 1;
                else
                    max = current - 1;
            } while (min <= max);
            return -1;
        }

        #endregion
    }
}