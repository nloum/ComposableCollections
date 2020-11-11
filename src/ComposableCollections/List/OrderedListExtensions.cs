using System;
using System.Collections.Generic;
using GenericNumbers.Relational;

namespace ComposableCollections.List
{
    public static class OrderedListExtensions
    {
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
    }
}