using GenericNumbers;
using GenericNumbers.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using static GenericNumbers.NumbersUtility;

namespace MoreCollections
{
    /// <summary>
    ///     This is where the extension methods for IEnumerable types go.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region First

        /// <returns>If source contains any items, return the first item; if source does not contain any items, then return
        /// the otherwise parameter.</returns>
        public static T FirstOr<T>(this IEnumerable<T> source, T otherwise)
        {
            foreach (var item in source)
            {
                return item;
            }
            return otherwise;
        }

        /// <returns>If source contains any items, return the first item; if source does not contain any items, then return
        /// the return value of the otherwise function. The otherwise function is not called if there are items in source.</returns>
        public static T FirstOr<T>(this IEnumerable<T> source, Func<T> otherwise)
        {
            foreach (var item in source)
            {
                return item;
            }
            return otherwise();
        }

        /// <returns>Returns the first item for which predicate returns true; if predicate does not return true for any of the items,
        /// or if the source does not contain any items, then return the otherwise parameter.</returns>
        public static T FirstOr<T>(this IEnumerable<T> source, Func<T, bool> predicate, T otherwise)
        {
            foreach (var item in source.Where(predicate))
            {
                return item;
            }
            return otherwise;
        }

        /// <returns>Returns the first item for which predicate returns true; if predicate does not return true for any of the items,
        /// or if the source does not contain any items, then return the return value of the otherwise function.
        /// The otherwise function is not called if there are items in source.</returns>
        public static T FirstOr<T>(this IEnumerable<T> source, Func<T, bool> predicate, Func<T> otherwise)
        {
            foreach (var item in source.Where(predicate))
            {
                return item;
            }
            return otherwise();
        }

        /// <returns>Returns the first item for which predicate returns true; if predicate does not return true for any of the items,
        /// or if the source does not contain any items, then return the return value of the otherwise function.
        /// The otherwise function is not called if there are items in source.</returns>
        public static T2 FirstOr<T1, T2>(this IEnumerable<T1> source, Func<T1, bool> predicate, Func<T1, T2> select, Func<T2> otherwise)
        {
            foreach (var item in source.Where(predicate))
            {
                return select(item);
            }
            return otherwise();
        }

        #endregion

        #region Between/split

        /// <summary>
        /// Lazily yield items in order from haystack that are between the specified indices (splitters).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitters">Indices that split the haystack into sub-lists. Splitters indices must be in increasing order.</param>
        /// <param name="haystack">The list that will be split into sub-lists divided by the splitters.</param>
        /// <param name="options">The options that govern whether to include the items at the indexes that split the haystack</param>
        public static IEnumerable<IList<T>> Between<T>(this IEnumerable<int> splitters, IList<T> haystack, BetweenOptions options = BetweenOptions.None)
        {
            var enumerator = splitters.GetEnumerator();
            var list = new List<T>();
            var i = 0;
            enumerator.MoveNext();
            while (true)
            {
                if (i == enumerator.Current)
                {
                    if (i > 0 || options.HasFlag(BetweenOptions.YieldStartEvenIfEmpty))
                    {
                        if (options.HasFlag(BetweenOptions.YieldSplitterWithPreviousItem))
                            list.Add(haystack[i]);
                        yield return list;
                        list = new List<T>();
                        if (options.HasFlag(BetweenOptions.YieldSplitterWithNextItem))
                            list.Add(haystack[i]);
                    }
                    if (!enumerator.MoveNext())
                    {
                        if (i + 1 < haystack.Count - 1)
                        {
                            list = haystack.Subset(i + 1).ToList();
                            yield return list;
                        }
                        else if (options.HasFlag(BetweenOptions.YieldStopEvenIfEmpty))
                        {
                            yield return list;
                        }
                        yield break;
                    }
                }
                else
                    list.Add(haystack[i]);
                i++;
            }
        }

        /// <summary>
        /// Lazily yield items in order from haystack that are between the specified ranges.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="splitters">Ranges of indices that split the haystack into sub-lists. Each item in this IEnumerable must be a Tuple where the
        /// first element of the tuple is the start of the range and the second element of the tuple is the end of the range, inclusive.
        /// Indices must be in increasing order.</param>
        /// <param name="haystack">The list that will be split into sub-lists divided by the splitters.</param>
        /// <param name="options">The options that govern whether to include the items at the indexes that split the haystack. The only options that effect
        /// the return value of this function are YieldStartEvenIfEmpty and YieldStopEvenIfEmpty.</param>
        public static IEnumerable<IList<T>> Between<T>(this IEnumerable<Tuple<int, int>> splitters, IList<T> haystack, BetweenOptions options = BetweenOptions.None)
        {
            var enumerator = splitters.GetEnumerator();
            var list = new List<T>();
            var i = 0;
            var inSplitter = false;
            enumerator.MoveNext();
            while (true)
            {
                if (i == enumerator.Current.Item1 || i == enumerator.Current.Item2)
                {
                    inSplitter = !inSplitter;
                    if (inSplitter)
                    {
                        if (i > 0 || options.HasFlag(BetweenOptions.YieldStartEvenIfEmpty))
                        {
                            yield return list;
                            list = new List<T>();
                        }
                    }
                    else
                    {
                        if (!enumerator.MoveNext())
                        {
                            if (i + 1 < haystack.Count - 1)
                            {
                                list = haystack.Subset(i + 1).ToList();
                                yield return list;
                            }
                            else if (options.HasFlag(BetweenOptions.YieldStopEvenIfEmpty))
                            {
                                yield return list;
                            }
                            yield break;
                        }
                    }
                    if (enumerator.Current.Item1 == enumerator.Current.Item2)
                        inSplitter = !inSplitter;
                }
                else if (!inSplitter)
                    list.Add(haystack[i]);
                i++;
            }
            if (list.Any() || options.HasFlag(BetweenOptions.YieldStopEvenIfEmpty))
                yield return list;
        }

        /// <summary>
        /// Split the source every time an element occurs that causes predicate to return true.
        /// </summary>
        public static IEnumerable<IList<T>> SplitWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool includeSplitter)
        {
            return source.SplitWhere((i, t) => predicate(t), includeSplitter);
        }

        /// <summary>
        /// Split the source every time an element occurs that causes predicate to return true.
        /// </summary>
        public static IEnumerable<IList<T>> SplitWhere<T>(this IEnumerable<T> source, Func<int, T, bool> predicate, bool includeSplitter)
        {
            var enumerator = source.GetEnumerator();
            var list = new List<T>();
            var i = 0;
            while (enumerator.MoveNext())
            {
                if (!predicate(i, enumerator.Current))
                    list.Add(enumerator.Current);
                else
                {
                    yield return list;
                    list = new List<T>();
                    if (includeSplitter)
                        list.Add(enumerator.Current);
                }
                i++;
            }
            if (list.Any())
                yield return list;
        }

        #endregion

        #region Misc
        
        /// <summary>
        ///     Lazily enumerates the enumerable in an infinite loop.
        /// </summary>
        public static IEnumerable<T> RepeatIndefinitely<T>(this IEnumerable<T> source)
        {
            while (true)
            {
                foreach (T item in source)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        ///     Generates an ordered subset of a list based on a set of indices into the list.
        /// </summary>
        /// <param name="indices">
        ///     The indices to include in the subset. Note that the indices can be negative,
        ///     in which case they are added to the length of list to get the actual index. This allows the user
        ///     to specify indices relative to the end of the list as well as the beginning of it.
        /// </param>
        public static IList<T> SubsetByIndices<T>(this IReadOnlyList<T> list, params int[] indices)
        {
            var result = new List<T>();
            for (int i = 0; i < result.Count; i++)
            {
                if (indices[i] < 0)
                    result[i] = list[list.Count + indices[i]];
                else
                    result[i] = list[indices[i]];
            }
            return result;
        }

        /// <summary>
        ///     Generates an ordered subset of a list based on a set of indices into the list.
        /// </summary>
        /// <param name="indices">
        ///     The indices to include in the subset. Note that the indices can be negative,
        ///     in which case they are added to the length of list to get the actual index. This allows the user
        ///     to specify indices relative to the end of the list as well as the beginning of it.
        /// </param>
        public static IList<T> SubsetByIndices<T>(this IReadOnlyList<T> list, IEnumerable<int> indices)
        {
            return list.SubsetByIndices(indices.ToArray());
        }

        /// <summary>
        ///     Copies a Stack
        /// </summary>
        public static Stack<T> Copy<T>(this Stack<T> source)
        {
            var array = new T[source.Count];
            source.CopyTo(array, 0);
            var target = new Stack<T>();
            foreach (T item in array)
            {
                target.Push(item);
            }
            return target;
        }

        /// <summary>
        ///     Shuffles the list using the Fischer-Yates shuffling algorithm.
        ///     From: http://stackoverflow.com/a/1262619
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        ///     Shuffles the list until we have the specified number of shuffled items, then return the result.
        /// </summary>
        public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> source, int subsetSize)
        {
            List<T> list = source.ToList();
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
                if (list.Count - n == subsetSize)
                    break;
            }
            return list.Skip(list.Count - subsetSize);
        }

        /// <summary>
        ///     Changes numList to the next permutation.
        ///     From: http://stackoverflow.com/a/11208543
        /// </summary>
        /// <returns>Whether or not the resulting permutation is the last one</returns>
        public static bool NextPermutation(this int[] numList)
        {
            /*
             Knuths
             1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
             2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
             3. Swap a[j] with a[l].
             4. Reverse the sequence from a[j + 1] up to and including the final element a[n].
             */
            int largestIndex = -1;
            for (int i = numList.Length - 2; i >= 0; i--)
            {
                if (numList[i] < numList[i + 1])
                {
                    largestIndex = i;
                    break;
                }
            }

            if (largestIndex < 0) return false;

            int largestIndex2 = -1;
            for (int i = numList.Length - 1; i >= 0; i--)
            {
                if (numList[largestIndex] < numList[i])
                {
                    largestIndex2 = i;
                    break;
                }
            }

            int tmp = numList[largestIndex];
            numList[largestIndex] = numList[largestIndex2];
            numList[largestIndex2] = tmp;

            for (int i = largestIndex + 1, j = numList.Length - 1; i < j; i++, j--)
            {
                tmp = numList[i];
                numList[i] = numList[j];
                numList[j] = tmp;
            }

            return true;
        }

        /// <summary>
        ///     Calls action on each element in input, in order. This causes the IEnumerable to be enumerated.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> input, Action<T> action)
        {
            foreach (T element in input)
            {
                action(element);
            }
        }

        /// <summary>
        ///     This is like ForEach, except it can be chained. For instance, an enumerable named myEnum could be processed like
        ///     this:
        ///     myEnum.ForEachChain(SomeMethod).ForEach(SomeOtherMethod);
        ///     Note that ForEachChain does not cause the IEnumerable to be enumerated. So if the above line read:
        ///     myEnum.ForEachChain(SomeMethod).ForEachChain(SomeOtherMethod);
        ///     then myEnum would never be enumerated.
        /// </summary>
        public static IEnumerable<T> ForEachChain<T>(this IEnumerable<T> input, Action<T> action)
        {
            // We check for null now because otherwise ForEachChain won't immediately throw a NullReferenceException
            // when the return value of this function isn't immediately enumerated.
            if (action == null)
                throw new NullReferenceException("action");

            foreach (T element in input)
            {
                action(element);
                yield return element;
            }
        }

        /// <summary>
        ///     Switches the keys and values of a dictionary.
        /// </summary>
        public static Dictionary<TItem, IEnumerable<TKey>> Invert<TKey, TItem>(
            this IDictionary<TKey, IEnumerable<TItem>> dictionary)
        {
            return dictionary
                .SelectMany(
                    keyValuePair =>
                        keyValuePair.Value.Select(item => new KeyValuePair<TItem, TKey>(item, keyValuePair.Key)))
                .GroupBy(keyValuePair => keyValuePair.Key)
                .ToDictionary(group => group.Key, group => group.Select(keyValuePair => keyValuePair.Value));
        }

        /// <summary>
        ///     Returns the specified subset of the items IEnumerable. Analogous to Substring.
        /// </summary>
        /// <param name="firstInclusive">
        ///     The index of the first item to be included in the results.
        ///     This can be a relative index.
        ///     Note that firstInclusive is inclusive, i.e. the element corresponding to firstInclusive
        ///     *will* be contained in the return value.
        /// </param>
        /// <param name="lastInclusive">
        ///     The index of the last item to be included in the results.
        ///     This can be a relative index.
        ///     Note that lastInclusive is inclusive, i.e. the element corresponding to lastInclusive
        ///     *will* be contained in the return value.
        /// </param>
        public static IEnumerable<T> Subset<T>(this IEnumerable<T> source, INumberRange<int> range)
        {
            range = range.ChangeStrictness(false, false);
            var firstInclusive = range.LowerBound;
            var lastInclusive = range.UpperBound;
            return source.Subset(firstInclusive.Value, lastInclusive.Value);
        }

        /// <summary>
        ///     Returns the specified subset of the items IEnumerable. Analogous to Substring.
        /// </summary>
        /// <param name="firstInclusive">
        ///     The index of the first item to be included in the results.
        ///     This can be a relative index.
        ///     Note that firstInclusive is inclusive, i.e. the element corresponding to firstInclusive
        ///     *will* be contained in the return value.
        /// </param>
        /// <param name="lastInclusive">
        ///     The index of the last item to be included in the results.
        ///     This can be a relative index.
        ///     Note that lastInclusive is inclusive, i.e. the element corresponding to lastInclusive
        ///     *will* be contained in the return value.
        /// </param>
        public static IEnumerable<T> Subset<T>(this IEnumerable<T> source, int firstInclusive = 0, int lastInclusive = -1)
        {
            if (lastInclusive < 0 || firstInclusive < 0)
            {
                List<T> list = source.ToList();
                if (lastInclusive < 0)
                    lastInclusive = list.Count + lastInclusive;
                if (firstInclusive < 0)
                    firstInclusive = list.Count + firstInclusive;
                source = list;
            }

            if (firstInclusive > lastInclusive)
                throw new ArgumentException("The range is specified backwards; swap the numbers to do it correctly");
            return source.Where((t, i) => i >= firstInclusive && i <= lastInclusive);
        }
        
        /// <summary>
        /// Converts a relative index into an absolute one. As long as there is at least one element in source,
        /// the return value of this function will always be a valid index into it.
        /// Negative relative indices are relative to the end of the source. Positive relative indices are relative
        /// to the beginning of the source. Relative indices that are positive and greater than or equal to
        /// the size of source are converted to absolute indices by taking the remainder when the index
        /// is divided by the size of source. A similar process occurs for a negative relative index whose absolute value
        /// is greater than the size of source. For instance, for a source of size 8, a negative index of -12 is equal
        /// to a negative index of -3, which is equivalent to an absolute index of 6.
        /// Basically, to calculate the absolute index of a negative relative index, start counting
        /// at the last element of the list with a -1 and count down from there while going backwards in the list. If you
        /// reach the beginning of the list, wrap around and continue counting down.
        /// To calculate the absolute index of a positive relative index, simply take index%source.Count and that's your
        /// answer. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="idx">The relative index</param>
        /// <returns>The absolute index</returns>
        public static int GetAbsoluteIndex<T>(this IReadOnlyCollection<T> source, int idx)
        {
            if (idx < 0)
            {
                if (idx <= -source.Count)
                {
                    var a = -idx - 1;
                    var b = a % source.Count;
                    var c = source.Count - 1 - b;
                    return c;
                }
                return source.Count + idx;
            }
            if (idx >= source.Count)
                return idx % source.Count;
            return idx;
        }

        /// <summary>
        /// Converts a relative index into an absolute one. As long as there is at least one element in source,
        /// the return value of this function will always be a valid index into it.
        /// Negative relative indices are relative to the end of the source. Positive relative indices are relative
        /// to the beginning of the source. Relative indices that are positive and greater than or equal to
        /// the size of source are converted to absolute indices by taking the remainder when the index
        /// is divided by the size of source. A similar process occurs for a negative relative index whose absolute value
        /// is greater than the size of source. For instance, for a source of size 8, a negative index of -12 is equal
        /// to a negative index of -3, which is equivalent to an absolute index of 6.
        /// Basically, to calculate the absolute index of a negative relative index, start counting
        /// at the last element of the list with a -1 and count down from there while going backwards in the list. If you
        /// reach the beginning of the list, wrap around and continue counting down.
        /// To calculate the absolute index of a positive relative index, simply take index%source.Count and that's your
        /// answer. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="idx">The relative index</param>
        /// <returns>The absolute index</returns>
        public static int GetAbsoluteIndex<T>(this List<T> source, int idx)
        {
            if (idx < 0)
            {
                if (idx <= -source.Count)
                {
                    var a = -idx - 1;
                    var b = a % source.Count;
                    var c = source.Count - 1 - b;
                    return c;
                }
                return source.Count + idx;
            }
            if (idx >= source.Count)
                return idx % source.Count;
            return idx;
        }

        /// <summary>
        /// Returns an absolute index range for the specified list and number range. This is the same as calling GetAbsoluteIndex
        /// on the lower bounds and upper bounds.
        /// <seealso cref="GenericNumbers.Extensions.ChangeStrictness{T}(INumberRange{T}, bool, bool)"/>
        /// </summary>
        public static INumberRange<int> ConvertToAbsoluteIndices<TItem>(this INumberRange<int> numberRange, IReadOnlyList<TItem> source)
        {
            var lowerBound = source.GetAbsoluteIndex(numberRange.LowerBound.Value);
            var upperBound = source.GetAbsoluteIndex(numberRange.UpperBound.Value);

            return Range(lowerBound, upperBound, false, false);
        }

        /// <summary>
        ///     Returns the specified subset of the items IEnumerable. Similar to Substring. Analogous to Substring. Optimized for
        ///     arrays.
        /// </summary>
        /// <param name="firstInclusive">
        ///     The index of the first item to be included in the results.
        ///     Can be negative, in which case the index of the first value included in the return value
        ///     is equal to the length of items + firstInclusive.
        ///     Note that firstInclusive is inclusive, i.e. the element corresponding to firstInclusive
        ///     *will* be contained in the return value.
        /// </param>
        /// <param name="lastInclusive">
        ///     The index of the last item to be included in the results.
        ///     Can be negative, in which case the index of the last value included in the return value
        ///     is equal to the length of items + lastInclusive.
        ///     Note that lastInclusive is inclusive, i.e. the element corresponding to lastInclusive
        ///     *will* be contained in the return value.
        /// </param>
        public static T[] Subset<T>(this T[] source, int firstInclusive = 0, int lastInclusive = -1)
        {
            firstInclusive = source.GetAbsoluteIndex(firstInclusive);
            lastInclusive = source.GetAbsoluteIndex(lastInclusive);
            if (firstInclusive > lastInclusive)
                throw new ArgumentException("The range is specified backwards; swap the numbers to do it correctly");
            int length = lastInclusive - firstInclusive + 1;
            var destination = new T[length];
            Array.Copy(source, firstInclusive, destination, 0, length);
            return destination;
        }

        /// <summary>
        /// Lazily calculate the set difference between two IEnumerables.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TKey">The object type, which must have its GetHashCode method implemented, that will be used to compare
        /// T1 and T2 objects to see if they have already been yielded or if they are part of the forbidden items.</typeparam>
        /// <param name="source">Items to be yielded, as long as they don't also exist in forbiddenItems</param>
        /// <param name="forbiddenItems">Items that will not be yielded by this function, even if source contains them.</param>
        /// <param name="getKey1">The function to get the object by which comparisons will be made, for objects of type T1</param>
        /// <param name="getKey2">The function to get the object by which comparisons will be made, for objects of type T2</param>
        public static IEnumerable<T1> ExceptBy<T1, T2, TKey>(this IEnumerable<T1> source, IEnumerable<T2> forbiddenItems,
            Func<T1, TKey> getKey1, Func<T2, TKey> getKey2)
        {
            var bannedElements = new HashSet<TKey>(forbiddenItems.Select(getKey2));
            foreach (var item in source)
            {
                if (bannedElements.Add(getKey1(item)))
                {
                    yield return item;
                }
            }
        }

        #endregion

        #region Sort

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
        public static int GetIndexOfSortedInsert<T>(this List<T> target, Func<T, int> comparer, int min = 0, int max = -1)
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
        public static int GetIndexOfSortedInsert<T>(this List<T> target, T newElement, Func<T, T, int> compare, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert(newElement, (t1) => compare(t1, newElement), min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T, TKey>(this List<T> target, T newElement, Func<T, TKey> keySelector, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert((t1) => keySelector(t1).CompareTo(keySelector(newElement)), min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function finds what index the specified newElement
        /// should be inserted at into the target list such that target remains sorted.
        /// Based on this: http://stackoverflow.com/a/23682008/4995014
        /// </summary>
        public static int GetIndexOfSortedInsert<T>(this List<T> target, T newElement, int min = 0, int max = -1)
        {
            return target.GetIndexOfSortedInsert(newElement, t => t, min, max);
        }

        /// <summary>
        /// Assumes that target is already sorted. This function inserts the specified newElement
        /// into the target list at a location that makes the target list remain sorted.
        /// </summary>
        public static int SortedInsert<T>(this List<T> target, T newElement, int min = 0, int max = -1)
        {
            var index = GetIndexOfSortedInsert(target, newElement, min, max);

            target.Insert(index, newElement);

            return index;
        }

        /// <summary>
        /// Assumes that target is already sorted. This function inserts the specified newElement
        /// into the target list at a location that makes the target list remain sorted.
        /// </summary>
        public static int SortedInsert<T, TKey>(this List<T> target, T newElement, Func<T, TKey> keySelector, int min = 0, int max = -1)
        {
            var index = GetIndexOfSortedInsert(target, newElement, keySelector, min, max);

            target.Insert(index, newElement);

            return index;
        }

        /// <summary>
        /// Assumes that target is already sorted. This function inserts the specified newElement
        /// into the target list at a location that makes the target list remain sorted.
        /// </summary>
        public static int SortedInsert<T>(this List<T> target, T newElement, Func<T, int> comparer, int min = 0, int max = -1)
        {
            var index = GetIndexOfSortedInsert(target, newElement, comparer, min, max);

            target.Insert(index, newElement);

            return index;
        }

        /// <summary>
        /// Assumes that target is already sorted. This function inserts the specified newElement
        /// into the target list at a location that makes the target list remain sorted.
        /// </summary>
        public static int SortedInsert<T>(this List<T> target, T newElement, Func<T, T, int> comparer, int min = 0, int max = -1)
        {
            var index = GetIndexOfSortedInsert(target, t1 => comparer(t1, newElement), min, max);

            target.Insert(index, newElement);

            return index;
        }
        
        #endregion

        #region Search

        #region Generic Boyer-Moore search

        /// <summary>
        /// Efficiently search haystack for needle, and lazily return indices as we find them.
        /// </summary>
        public static IEnumerable<int> Searches<T>(this IReadOnlyList<T> haystack, IReadOnlyList<T> needle, int haystackStart = 0,
            int haystackStop = -1)
        {
            haystackStart = haystack.GetAbsoluteIndex(haystackStart);
            haystackStop = haystack.GetAbsoluteIndex(haystackStop);
            if (haystackStart > haystackStop)
            {
                var tmp = haystackStart;
                haystackStart = haystackStop;
                haystackStop = tmp;
            }
            while (haystackStart <= haystackStop)
            {
                var result = haystack.Search(needle, haystackStart, haystackStop);
                if (result == -1)
                    yield break;
                yield return result;
                haystackStart = result + needle.Count;
            }
        }

        /// <summary>
        /// Efficiently search haystack backwards for needle, and lazily return indices as we find them.
        /// </summary>
        public static IEnumerable<int> ReverseSearches<T>(this IReadOnlyList<T> haystack, IReadOnlyList<T> needle, int haystackStart = 0,
            int haystackStop = -1)
        {
            haystackStart = haystack.GetAbsoluteIndex(haystackStart);
            haystackStop = haystack.GetAbsoluteIndex(haystackStop);
            if (haystackStart > haystackStop)
            {
                var tmp = haystackStart;
                haystackStart = haystackStop;
                haystackStop = tmp;
            }
            while (haystackStart <= haystackStop)
            {
                var result = haystack.ReverseSearch(needle, haystackStart, haystackStop);
                if (result == -1)
                    yield break;
                yield return result;
                haystackStop = result;
            }
        }

        /// <summary>
        /// Returns the index within this list of the first occurrence of the
        /// specified alleged sublist. If the alleged sublist isn't a sublist of list, return -1.
        /// </summary>
        /// <param name="haystack">The string to be scanned</param>
        /// <param name="needle">The target string to search</param>
        /// <param name="haystackStop">The stop of the sub-list to search in, inclusive. Can be a relative index.</param>
        /// <param name="haystackStart">The start of the sub-list to search in. Can be a relative index.</param>
        /// <returns>The absolute start index of the sublist, or -1 if the needle couldn't be found.</returns>
        public static int Search<T>(this IReadOnlyList<T> haystack, IReadOnlyList<T> needle, int haystackStart = 0, int haystackStop = -1)
        {
            if (needle.Count == 0)
            {
                return -1;
            }
            haystackStart = haystack.GetAbsoluteIndex(haystackStart);
            haystackStop = haystack.GetAbsoluteIndex(haystackStop);
            if (haystackStart > haystackStop)
            {
                var tmp = haystackStart;
                haystackStart = haystackStop;
                haystackStop = tmp;
            }
            var haystackCount = haystackStop - haystackStart + 1;
            var intTable = new BoyerMooreTable<T>(needle);
            var offsetTable = MakeOffsetTable(needle);
            for (int i = needle.Count - 1, j; i < haystackCount;)
            {
                for (j = needle.Count - 1; Equals(needle[j], haystack[haystackStart + i]); --i, --j)
                {
                    if (j == 0)
                    {
                        return i + haystackStart;
                    }
                }
                // i += needle.Count - j; // For naive method
                i += Math.Max(offsetTable[needle.Count - 1 - j], intTable[haystack[haystackStart + i]]);
            }
            return -1;
        }

        /// <summary>
        /// Returns the index within this list of the last occurrence of the
        /// specified alleged sublist. If the alleged sublist isn't a sublist of list, return -1.
        /// </summary>
        /// <param name="haystack">The string to be scanned</param>
        /// <param name="needle">The target string to search</param>
        /// <returns>The start index of the sublist</returns>
        /// <param name="haystackStop">The stop of the sub-list to search in, inclusive. Can be a relative index.</param>
        /// <param name="haystackStart">The start of the sub-list to search in. Can be a relative index.</param>
        /// <returns>The absolute start index of the sublist, or -1 if the needle couldn't be found.</returns>
        public static int ReverseSearch<T>(this IReadOnlyList<T> haystack, IReadOnlyList<T> needle, int haystackStart = 0, int haystackStop = -1)
        {
            if (needle.Count == 0)
            {
                return -1;
            }
            haystackStart = haystack.GetAbsoluteIndex(haystackStart);
            haystackStop = haystack.GetAbsoluteIndex(haystackStop);
            if (haystackStart > haystackStop)
            {
                var tmp = haystackStart;
                haystackStart = haystackStop;
                haystackStop = tmp;
            }
            var haystackCount = haystackStop - haystackStart + 1;
            var intTable = new BoyerMooreTable<T>(needle);
            var offsetTable = MakeOffsetTable(needle);
            for (int i = needle.Count - 1, j; i < haystackCount;)
            {
                for (j = 0; Equals(needle[j], haystack[haystackStart + haystackCount - 1 - i]); --i, ++j)
                {
                    if (j == needle.Count - 1)
                    {
                        return haystackStart + haystackCount - 2 - i;
                    }
                }
                // i += needle.Count - j; // For naive method
                i += Math.Max(offsetTable[j], intTable[haystack[haystackStart + haystackCount - 1 - i]]);
            }
            return -1;
        }

        internal class BoyerMooreTable<T>
        {
            private readonly IReadOnlyList<T> _needle;
            private readonly Dictionary<T, int> _table;

            public BoyerMooreTable(IReadOnlyList<T> needle)
            {
                _needle = needle;
                _table = _needle.Select((t, i) => new { Item = t, Index = i })
                    .ToDictionary(anon => anon.Item, anon => _needle.Count - 1 - anon.Index);
            }

            public int this[T index]
            {
                get
                {
                    int value;
                    if (!_table.TryGetValue(index, out value))
                        return _needle.Count;
                    return value;
                }
            }
        }

        /// <summary>
        /// Makes the jump table based on the scan offset which mismatch occurs.
        /// </summary>
        private static IReadOnlyList<int> MakeOffsetTable<T>(IReadOnlyList<T> needle)
        {
            var table = new int[needle.Count];
            int lastPrefixPosition = needle.Count;
            for (int i = needle.Count - 1; i >= 0; --i)
            {
                if (IsPrefix(needle, i + 1))
                {
                    lastPrefixPosition = i + 1;
                }
                table[needle.Count - 1 - i] = lastPrefixPosition - i + needle.Count - 1;
            }
            for (int i = 0; i < needle.Count - 1; ++i)
            {
                int slen = SuffixCount(needle, i);
                table[slen] = needle.Count - 1 - i + slen;
            }
            return table;
        }

        /// <summary>
        /// Is needle[p:end] a prefix of needle?
        /// </summary>
        private static bool IsPrefix<T>(IReadOnlyList<T> needle, int p)
        {
            for (int i = p, j = 0; i < needle.Count; ++i, ++j)
            {
                if (Equals(needle[i], needle[j]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the maximum Count of the substring ends at p and is a suffix.
        /// </summary>
        private static int SuffixCount<T>(IReadOnlyList<T> needle, int p)
        {
            int len = 0;
            for (int i = p, j = needle.Count - 1;
                i >= 0 && Equals(needle[i], needle[j]);
                --i, --j)
            {
                len += 1;
            }
            return len;
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
        
        /// <summary>
        ///     Returns the longest ordered subset common between the two specified lists.
        /// </summary>
        public static IEnumerable<T> LongestCommonOrderedSubset<T>(this IReadOnlyList<T> a, IReadOnlyList<T> b)
        {
            // Ensure that b is the longer list.
            if (b.Count < a.Count)
            {
                var t = a;
                a = b;
                b = t;
            }

            for (int n = a.Count; n > 0; n--)
            {
                for (int m = a.Count - n; m <= a.Count - n; m++)
                {
                    List<T> s = a.Subset(m, n).ToList();
                    if (b.Contains(s))
                        return s;
                }
            }
            return EnumerableUtility.EmptyArray<T>();
        }

        /// <summary>
        ///     Returns whether or not haystack contains needle
        /// </summary>
        public static bool Contains<T>(this IEnumerable<T> haystack, IEnumerable<T> needle)
        {
            return haystack.Select(t => t.GetHashCode()).ToList().Search(needle.Select(t => t.GetHashCode()).ToList()) >= 0;
        }

        #endregion

        #region RemoveWhere

        /// <summary>
        ///     Remove all key/value pairs in the specified dictionary that cause the predicate to return true.
        /// </summary>
        /// <param name="predicate">
        ///     A function that, when it returns true, causes the key/value pair to be removed
        ///     from the dictionary. The first parameter is the key, the second is the value.
        /// </param>
        public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            Func<TKey, TValue, bool> predicate)
        {
            int howManyHaveBeenRemoved = 0;
            List<TKey> keys = dict.Keys.ToList();
            foreach (TKey key in keys)
            {
                if (predicate(key, dict[key]))
                {
                    dict.Remove(key);
                    howManyHaveBeenRemoved++;
                }
            }
            return howManyHaveBeenRemoved;
        }

        /// <summary>
        ///     Remove all key/value pairs in the specified dictionary that cause the predicate to return true.
        /// </summary>
        /// <param name="predicate">
        ///     A function that, when it returns true, causes the key/value pair to be removed
        ///     from the dictionary. The first parameter is the key, the second is the value.
        /// </param>
        public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            int howManyHaveBeenRemoved = 0;
            List<TKey> keys = dict.Keys.ToList();
            foreach (TKey key in keys)
            {
                if (predicate(new KeyValuePair<TKey, TValue>(key, dict[key])))
                {
                    dict.Remove(key);
                    howManyHaveBeenRemoved++;
                }
            }
            return howManyHaveBeenRemoved;
        }

		/// <summary>
		///     Remove all key/value pairs in the specified dictionary that cause the predicate to return true.
		/// </summary>
		/// <param name="predicate">
		///     A function that, when it returns true, causes the key/value pair to be removed
		///     from the dictionary. The first parameter is the key, the second is the value.
		/// </param>
		public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dict,
			Func<TKey, TValue, int, bool> predicate)
		{
			int howManyHaveBeenRemoved = 0;
			List<TKey> keys = dict.Keys.ToList();
			foreach (TKey key in keys)
			{
				if (predicate(key, dict[key], howManyHaveBeenRemoved))
				{
					dict.Remove(key);
					howManyHaveBeenRemoved++;
				}
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Remove all key/value pairs in the specified dictionary that cause the predicate to return true.
		/// </summary>
		/// <param name="predicate">
		///     A function that, when it returns true, causes the key/value pair to be removed
		///     from the dictionary. The first parameter is the key, the second is the value.
		/// </param>
		public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dict,
			Func<KeyValuePair<TKey, TValue>, int, bool> predicate)
		{
			int howManyHaveBeenRemoved = 0;
			List<TKey> keys = dict.Keys.ToList();
			foreach (TKey key in keys)
			{
				if (predicate(new KeyValuePair<TKey, TValue>(key, dict[key]), howManyHaveBeenRemoved))
				{
					dict.Remove(key);
					howManyHaveBeenRemoved++;
				}
			}
		    return howManyHaveBeenRemoved;
		}

        /// <summary>
        ///     Remove all elements in the specified list that cause the predicate to return true.
        /// </summary>
        public static int RemoveWhere<T>(this IList<T> list, Func<T, bool> predicate)
        {
            var howManyHaveBeenRemoved = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                    i--;
                    howManyHaveBeenRemoved++;
                }
            }
            return howManyHaveBeenRemoved;
        }

        /// <summary>
        ///     Remove all elements in the specified list that cause the predicate to return true.
        /// </summary>
        public static int RemoveWhere<T>(this ICollection<T> list, Func<T, bool> predicate)
        {
            var stuffToRemove = new List<T>();
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    stuffToRemove.Add(item);
                }
            }
            foreach (var item in stuffToRemove)
            {
                list.Remove(item);
            }
            return stuffToRemove.Count;
        }

        /// <summary>
        ///     Removes items from the list that cause the predicate to return true.
        /// </summary>
        /// <param name="predicate">
        ///     The first parameter is the index of the element
        ///     and second is the element itself
        /// </param>
        public static int RemoveWhere<T>(this IList<T> list, Func<int, T, bool> predicate)
        {
            var howManyHaveBeenRemoved = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(i, list[i]))
                {
                    list.RemoveAt(i);
                    i--;
                    howManyHaveBeenRemoved++;
                }
            }
            return howManyHaveBeenRemoved;
        }

        /// <summary>
        ///     Removes items from the list that cause the predicate to return true.
        /// </summary>
        /// <param name="predicate">
        ///     The first parameter is the index of the element,
        ///     the second is how many elements have been removed so far, and the third is the element itself
        /// </param>
        public static int RemoveWhere<T>(this IList<T> list, Func<int, int, T, bool> predicate)
        {
            int howManyHaveBeenRemoved = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(i, howManyHaveBeenRemoved, list[i]))
                {
                    howManyHaveBeenRemoved++;
                    list.RemoveAt(i);
                    i--;
                }
            }
            return howManyHaveBeenRemoved;
        }

        /// <summary>
        ///     Same as RemoveWhere with the same signature, except works from the end of the list to the beginning.
        /// </summary>
        public static int RemoveBackwardsWhere<T>(this IList<T> list, Func<int, T, bool> predicate)
        {
            var howManyHaveBeenRemoved = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(i, list[i]))
                {
                    list.RemoveAt(i);
                    howManyHaveBeenRemoved++;
                }
            }
            return howManyHaveBeenRemoved;
        }

        /// <summary>
        ///     Same as RemoveWhere with the same signature, except works from the end of the list to the beginning.
        /// </summary>
        public static int RemoveBackwardsWhere<T>(this IList<T> list, Func<int, int, T, bool> predicate)
        {
            int howManyHaveBeenRemoved = 0;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(i, howManyHaveBeenRemoved, list[i]))
                {
                    howManyHaveBeenRemoved++;
                    list.RemoveAt(i);
                }
            }
            return howManyHaveBeenRemoved;
        }

		/// <summary>
		///     Remove all key/value pairs in the specified dictionary that cause the predicate to return true.
		/// </summary>
		/// <param name="predicate">
		///     A function that, when it returns true, causes the key/value pair to be removed
		///     from the dictionary. The first parameter is the key, the second is the value.
		/// </param>
		public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dict,
			Func<TKey, TValue, int, bool> predicate, int howManyToRemove)
		{
			int howManyHaveBeenRemoved = 0;
			List<TKey> keys = dict.Keys.ToList();
			foreach (TKey key in keys)
			{
				if (predicate(key, dict[key], howManyHaveBeenRemoved))
				{
					dict.Remove(key);
					howManyHaveBeenRemoved++;
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Remove all key/value pairs in the specified dictionary that cause the predicate to return true.
		/// </summary>
		/// <param name="predicate">
		///     A function that, when it returns true, causes the key/value pair to be removed
		///     from the dictionary. The first parameter is the key, the second is the value.
		/// </param>
		public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dict,
			Func<KeyValuePair<TKey, TValue>, int, bool> predicate, int howManyToRemove)
		{
			int howManyHaveBeenRemoved = 0;
			List<TKey> keys = dict.Keys.ToList();
			foreach (TKey key in keys)
			{
				if (predicate(new KeyValuePair<TKey, TValue>(key, dict[key]), howManyHaveBeenRemoved))
				{
					dict.Remove(key);
					howManyHaveBeenRemoved++;
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Remove all key/value pairs in the specified dictionary that cause the predicate to return true.
		/// </summary>
		/// <param name="predicate">
		///     A function that, when it returns true, causes the key/value pair to be removed
		///     from the dictionary. The first parameter is the key, the second is the value.
		/// </param>
		public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dict,
			Func<TKey, TValue, bool> predicate, int howManyToRemove)
		{
			int howManyHaveBeenRemoved = 0;
			List<TKey> keys = dict.Keys.ToList();
			foreach (TKey key in keys)
			{
				if (predicate(key, dict[key]))
				{
					dict.Remove(key);
					howManyHaveBeenRemoved++;
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Remove all key/value pairs in the specified dictionary that cause the predicate to return true.
		/// </summary>
		/// <param name="predicate">
		///     A function that, when it returns true, causes the key/value pair to be removed
		///     from the dictionary. The first parameter is the key, the second is the value.
		/// </param>
		public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dict,
			Func<KeyValuePair<TKey, TValue>, bool> predicate, int howManyToRemove)
		{
			int howManyHaveBeenRemoved = 0;
			List<TKey> keys = dict.Keys.ToList();
			foreach (TKey key in keys)
			{
				if (predicate(new KeyValuePair<TKey, TValue>(key, dict[key])))
				{
					dict.Remove(key);
					howManyHaveBeenRemoved++;
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Remove all elements in the specified list that cause the predicate to return true.
		/// </summary>
		public static int RemoveWhere<T>(this IList<T> list, Func<T, bool> predicate, int howManyToRemove)
		{
			int howManyHaveBeenRemoved = 0;
			for (int i = 0; i < list.Count; i++)
			{
				if (predicate(list[i]))
				{
					list.RemoveAt(i);
					i--;
					howManyHaveBeenRemoved++;
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Remove all elements in the specified list that cause the predicate to return true.
		/// </summary>
		public static int RemoveWhere<T>(this ICollection<T> list, Func<T, bool> predicate, int howManyToRemove)
		{
			var howManyHaveBeenRemoved = 0;
			var stuffToRemove = new List<T>();
			foreach (var item in list)
			{
				if (predicate(item))
				{
					stuffToRemove.Add(item);
					howManyHaveBeenRemoved++;
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
			foreach (var item in stuffToRemove)
			{
				list.Remove(item);
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Removes items from the list that cause the predicate to return true.
		/// </summary>
		/// <param name="predicate">
		///     The first parameter is the index of the element
		///     and second is the element itself
		/// </param>
		public static int RemoveWhere<T>(this IList<T> list, Func<int, T, bool> predicate, int howManyToRemove)
		{
			var howManyHaveBeenRemoved = 0;
			for (int i = 0; i < list.Count; i++)
			{
				if (predicate(i, list[i]))
				{
					list.RemoveAt(i);
					i--;
					howManyHaveBeenRemoved++;
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Removes items from the list that cause the predicate to return true.
		/// </summary>
		/// <param name="predicate">
		///     The first parameter is the index of the element,
		///     the second is how many elements have been removed so far, and the third is the element itself
		/// </param>
		public static int RemoveWhere<T>(this IList<T> list, Func<int, int, T, bool> predicate, int howManyToRemove)
		{
			int howManyHaveBeenRemoved = 0;
			for (int i = 0; i < list.Count; i++)
			{
				if (predicate(i, howManyHaveBeenRemoved, list[i]))
				{
					howManyHaveBeenRemoved++;
					list.RemoveAt(i);
					i--;
				}
			}
		    return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Same as RemoveWhere with the same signature, except works from the end of the list to the beginning.
		/// </summary>
		public static int RemoveBackwardsWhere<T>(this IList<T> list, Func<int, T, bool> predicate, int howManyToRemove)
		{
			int howManyHaveBeenRemoved = 0;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (predicate(i, list[i]))
				{
					howManyHaveBeenRemoved++;
					list.RemoveAt(i);
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
            return howManyHaveBeenRemoved;
		}

		/// <summary>
		///     Same as RemoveWhere with the same signature, except works from the end of the list to the beginning.
		/// </summary>
		public static int RemoveBackwardsWhere<T>(this IList<T> list, Func<int, int, T, bool> predicate, int howManyToRemove)
		{
			int howManyHaveBeenRemoved = 0;
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (predicate(i, howManyHaveBeenRemoved, list[i]))
				{
					howManyHaveBeenRemoved++;
					list.RemoveAt(i);
					if (howManyHaveBeenRemoved == howManyToRemove)
						break;
				}
			}
		    return howManyHaveBeenRemoved;
		}

        #endregion
        
        public static IEnumerable<IReadOnlyList<T>> CalcCombination<T>(this IReadOnlyList<T> list, int sizeOfCombination, int indexInList = 0)
        {
            for (var i = indexInList; i < list.Count; i++)
            {
                var item = list[i];
                var itemArray = new[] { item };
                if (sizeOfCombination == 1)
                    yield return itemArray;
                else
                {
                    foreach (var subsequentList in CalcCombination<T>(list, sizeOfCombination - 1, i + 1))
                    {
                        yield return itemArray.Concat(subsequentList).ToList();
                    }
                }
            }
        }

        /// <summary>
        /// Inserts the specified range of values into the list.
        /// </summary>
        public static void InsertRange<T>(this IList<T> list, int startingIndex, IEnumerable<T> newElements)
        {
            var index = startingIndex;
            foreach (var element in newElements)
            {
                list.Insert(index, element);
                index++;
            }
        }

        /// <summary>
        /// Inserts the specified range of values into the list.
        /// </summary>
        public static void InsertRange<T>(this IList<T> list, int startingIndex, params T[] newElements)
        {
            list.InsertRange(startingIndex, newElements.AsEnumerable());
        }

        /// <summary>
        /// Remove the specified indices from the list.
        /// </summary>
        public static void RemoveRange<T>(this IList<T> list, INumberRange<int> range)
        {
            range = range.ChangeStrictness(false, true);

            for (var i = range.LowerBound.Value; i < range.UpperBound.Value; i++)
            {
                list.RemoveAt(range.LowerBound.Value);
            }
        }

        /// <summary>
        /// Remove the specified indices from the list.
        /// </summary>
        public static ImmutableList<T> RemoveRange<T>(this ImmutableList<T> list, INumberRange<int> range)
        {
            if (range.LowerBound.IsStrict || !range.UpperBound.IsStrict)
                range = range.ChangeStrictness(false, true);

            return list.RemoveRange(range.LowerBound.Value, range.Size);
        }

        /// <summary>
        /// Gets the items at the specified indices from the list.
        /// </summary>
        public static ImmutableList<T> GetRange<T>(this ImmutableList<T> list, INumberRange<int> range)
        {
            if (range.LowerBound.IsStrict || !range.UpperBound.IsStrict)
                range = range.ChangeStrictness(false, true);

            return list.GetRange(range.LowerBound.Value, range.Size);
        }

        #region AddRange

        /// <summary>
        ///     For each specified item, checks if the list contains it. If not, add it to the list.
        /// </summary>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                list.Add(item);
            }
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            foreach (var pair in pairs)
            {
                dictionary.Add(pair);
            }
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        public static void AddRange<TKeyValue, TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<TKeyValue> pairs, Func<TKeyValue, TKey> getKey, Func<TKeyValue, TValue> getValue)
        {
            foreach (var pair in pairs)
            {
                dictionary.Add(getKey(pair), getValue(pair));
            }
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <param name="value">The value to use when adding keys</param>
        /// <param name="keys">The keys which will exist in the dictionary after this method returns</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value,
            IEnumerable<TKey> keys)
        {
            foreach (TKey key in keys)
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        ///     For each specified item, checks if the list contains it. If not, add it to the list.
        /// </summary>
        public static void AddRange<T>(this IList<T> list, params T[] items)
        {
            foreach (T item in items)
            {
                list.Add(item);
            }
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            params KeyValuePair<TKey, TValue>[] pairs)
        {
            foreach (var pair in pairs)
            {
                dictionary.Add(pair);
            }
        }

        /// <summary>
        ///     For the specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            dictionary.Add(key, value);
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <param name="value">The value to use when adding keys</param>
        /// <param name="keys">The keys which will exist in the dictionary after this method returns</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value,
            params TKey[] keys)
        {
            foreach (TKey key in keys)
            {
                dictionary.Add(key, value);
            }
        }

        #endregion

        #region EnsureContains / EnsureContainsNot

        /// <summary>
        ///     For each specified item, checks if the list contains it. If not, add it to the list.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<T>(this ICollection<T> list, IEnumerable<T> items)
        {
            var howManyWereFixed = 0;
            foreach (T item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the list contains it. If it does, remove it from the list.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<T>(this ICollection<T> list, IEnumerable<T> items)
        {
            var howManyWereFixed = 0;
            foreach (T item in items)
            {
                if (list.Contains(item))
                {
                    list.Remove(item);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            var howManyWereFixed = 0;
            foreach (var pair in pairs)
            {
                if (!dictionary.ContainsKey(pair.Key))
                {
                    dictionary.Add(pair);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <param name="value">The value to use when adding keys</param>
        /// <param name="keys">The keys which will exist in the dictionary after this method returns</param>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value,
            IEnumerable<TKey> keys)
        {
            var howManyWereFixed = 0;
            foreach (TKey key in keys)
            {
                if (dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, value);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, remove it from the dictionary.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<TKey> keys)
        {
            var howManyWereFixed = 0;
            foreach (TKey key in keys)
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Remove(key);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, remove it from the dictionary.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            var howManyWereFixed = 0;
            foreach (var pair in pairs)
            {
                if (!dictionary.ContainsKey(pair.Key))
                {
                    dictionary.Remove(pair.Key);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the list contains it. If not, add it to the list.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<T>(this ICollection<T> list, Func<T, T, bool> compare, IEnumerable<T> items)
        {
            var howManyWereFixed = 0;
            foreach (T item in items)
            {
                if (!list.Any(item2 => compare(item, item2)))
                {
                    list.Add(item);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the list contains it. If it does, remove it from the list.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<T>(this ICollection<T> list, Func<T, T, bool> compare, IEnumerable<T> items)
        {
            var howManyWereFixed = 0;
            foreach (T item in items)
            {
                howManyWereFixed += list.RemoveWhere(item2 => compare(item, item2));
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>, bool> compare,
            IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            var howManyWereFixed = 0;
            foreach (var pair in pairs)
            {
                if (!dictionary.Any(pair2 => compare(pair, pair2)))
                {
                    dictionary.Add(pair);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <param name="value">The value to use when adding keys</param>
        /// <param name="keys">The keys which will exist in the dictionary after this method returns</param>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<TKey, TKey, bool> compare, TValue value, IEnumerable<TKey> keys)
        {
            var howManyWereFixed = 0;
            foreach (TKey key in keys)
            {
                if (!dictionary.Keys.Any(key2 => compare(key, key2)))
                {
                    dictionary.Add(key, value);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, remove it from the dictionary.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<TKey, TKey, bool> compare, IEnumerable<TKey> keys)
        {
            var howManyWereFixed = 0;
            foreach (TKey key in keys)
            {
                howManyWereFixed += dictionary.RemoveWhere((key2, value2) => compare(key, key2));
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, remove it from the dictionary.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>, bool> compare,
            IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            var howManyWereFixed = 0;
            foreach (var pair in pairs)
            {
                howManyWereFixed += dictionary.RemoveWhere(pair2 => compare(pair2, pair));
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the list contains it. If not, add it to the list.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<T>(this ICollection<T> list, params T[] items)
        {
            var howManyWereFixed = 0;
            foreach (T item in items)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the list contains it. If it does, remove it from the list.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<T>(this ICollection<T> list, params T[] items)
        {
            var howManyWereFixed = 0;
            foreach (T item in items)
            {
                if (list.Contains(item))
                {
                    list.Remove(item);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            params KeyValuePair<TKey, TValue>[] pairs)
        {
            var howManyWereFixed = 0;
            foreach (var pair in pairs)
            {
                if (!dictionary.ContainsKey(pair.Key))
                { 
                    dictionary.Add(pair);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For the specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
                return 1;
            }
            return 0;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <param name="value">The value to use when adding keys</param>
        /// <param name="keys">The keys which will exist in the dictionary after this method returns</param>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value,
            params TKey[] keys)
        {
            var howManyWereFixed = 0;
            foreach (TKey key in keys)
            {
                if (dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, value);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, remove it from the dictionary.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params TKey[] keys)
        {
            var howManyWereFixed = 0;
            foreach (TKey key in keys)
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Remove(key);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, remove it from the dictionary.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            params KeyValuePair<TKey, TValue>[] pairs)
        {
            var howManyWereFixed = 0;
            foreach (var pair in pairs)
            {
                if (!dictionary.ContainsKey(pair.Key))
                {
                    dictionary.Remove(pair.Key);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the list contains it. If not, add it to the list.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<T>(this ICollection<T> list, Func<T, T, bool> compare, params T[] items)
        {
            var howManyWereFixed = 0;
            foreach (T item in items)
            {
                if (!list.Any(item2 => compare(item, item2)))
                {
                    list.Add(item);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the list contains it. If it does, remove it from the list.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<T>(this ICollection<T> list, Func<T, T, bool> compare, params T[] items)
        {
            var howManyWereFixed = 0;
            foreach (T item in items)
            {
                howManyWereFixed += list.RemoveWhere(item2 => compare(item, item2));
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>, bool> compare,
            params KeyValuePair<TKey, TValue>[] pairs)
        {
            var howManyWereFixed = 0;
            foreach (var pair in pairs)
            {
                if (!dictionary.Any(pair2 => compare(pair, pair2)))
                {
                    dictionary.Add(pair);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For the specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>, bool> compare, TKey key, TValue value)
        {
            if (!dictionary.Any(pair2 => compare(new KeyValuePair<TKey, TValue>(key, value), pair2)))
            {
                dictionary.Add(key, value);
                return 1;
            }
            return 0;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, add it to the dictionary.
        /// </summary>
        /// <param name="value">The value to use when adding keys</param>
        /// <param name="keys">The keys which will exist in the dictionary after this method returns</param>
        /// <returns>How many were added</returns>
        public static int EnsureContains<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<TKey, TKey, bool> compare, TValue value, params TKey[] keys)
        {
            var howManyWereFixed = 0;
            foreach (TKey key in keys)
            {
                if (!dictionary.Keys.Any(key2 => compare(key, key2)))
                {
                    dictionary.Add(key, value);
                    howManyWereFixed++;
                }
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, remove it from the dictionary.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<TKey, TKey, bool> compare, params TKey[] keys)
        {
            var howManyWereFixed = 0;
            foreach (TKey key in keys)
            {
                howManyWereFixed += dictionary.RemoveWhere((key2, value2) => compare(key, key2));
            }
            return howManyWereFixed;
        }

        /// <summary>
        ///     For each specified item, checks if the dictionary contains it. If not, remove it from the dictionary.
        /// </summary>
        /// <returns>How many were removed</returns>
        public static int EnsureContainsNot<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>, bool> compare,
            params KeyValuePair<TKey, TValue>[] pairs)
        {
            var howManyWereFixed = 0;
            foreach (var pair in pairs)
            {
                howManyWereFixed += dictionary.RemoveWhere(pair2 => compare(pair2, pair));
            }
            return howManyWereFixed;
        }

        #endregion

        #region Concat

        /// <summary>
        ///     Concatenates first the items, then ts (the this parameter)
        /// </summary>
        public static IEnumerable<T> ItemsConcat<T>(this IEnumerable<T> ts, params T[] items)
        {
            return items.Concat(ts);
        }

        /// <summary>
        ///     Concatenates first ts (the this parameter), then the items
        /// </summary>
        public static IEnumerable<T> ConcatItems<T>(this IEnumerable<T> ts, params T[] items)
        {
            return ts.Concat(items);
        }

        /// <summary>
        ///     Concatenates an array with item (the this parameter) as its only element, then ts.
        /// </summary>
        public static IEnumerable<T> ItemConcat<T>(this T item, IEnumerable<T> ts)
        {
            return new[] {item}.Concat(ts);
        }

        /// <summary>
        ///     Concatenates ts with an array with item (the this parameter) as its only element.
        /// </summary>
        public static IEnumerable<T> ConcatItem<T>(this T item, IEnumerable<T> ts)
        {
            return ts.Concat(new[] {item});
        }

        #endregion

        #region Tree enumeration

        /// <summary>
        ///     A delegate that is used to show how to convert a tree node into a list of children.
        /// </summary>
        /// <param name="ancestorStack">
        ///     A stack where the first item pushed was the root node and the last item pushed was the
        ///     current node.
        /// </param>
        public delegate IEnumerable<T> EnumerateTreeNode<T>(Stack<T> ancestorStack);

        /// <summary>
        ///     Enumerates the tree recursively. Each element in the returned IEnumerable
        ///     is a list where the first element in the list is the root of the tree
        ///     and the last element in the list is the element currently being enumerated.
        ///     This enumerates non-leaf nodes as well as leaf nodes.
        ///     This method assumes that each element in the tree implements IEnumerable of type T.
        ///     If this is not the case (i.e., the tree structure is more complex) then use the other overload.
        /// </summary>
        /// <param name="order">Whether to traverse the tree in post-order or pre-order</param>
        public static IEnumerable<List<T>> EnumerateTreeRecursively<T>(this T head, TreeTraversalOrder order)
            where T : IEnumerable<T>
        {
            return EnumerateTreeRecursively(head, stack => stack.Peek(), order, new Stack<T>());
        }

        /// <summary>
        ///     Enumerates the tree recursively. Each element in the returned IEnumerable
        ///     is a list where the first element in the list is the root of the tree
        ///     and the last element in the list is the element currently being enumerated.
        ///     This enumerates non-leaf nodes as well as leaf nodes.
        /// </summary>
        /// <param name="enumerateTreeNode">Specifies how to get an IEnumerable of the children of a given node</param>
        /// <param name="order">Whether to traverse the tree in post-order or pre-order</param>
        public static IEnumerable<List<T>> EnumerateTreeRecursively<T>(
            this T head,
            EnumerateTreeNode<T> enumerateTreeNode,
            TreeTraversalOrder order)
        {
            return EnumerateTreeRecursively(head, enumerateTreeNode, order, new Stack<T>());
        }

        private static IEnumerable<List<T>> EnumerateTreeRecursively<T>(
            this T head,
            EnumerateTreeNode<T> enumerateTreeNode,
            TreeTraversalOrder order,
            Stack<T> ancestorStack)
        {
            ancestorStack.Push(head);
            try
            {
                if (order == TreeTraversalOrder.PreOrder) yield return ancestorStack.ToList();
                List<T> children = enumerateTreeNode(ancestorStack).ToList();
                foreach (T child in children)
                {
                    IEnumerable<List<T>> results = EnumerateTreeRecursively(
                        child,
                        enumerateTreeNode,
                        order,
                        ancestorStack);
                    foreach (var result in results)
                    {
                        yield return result;
                    }
                }
                if (order == TreeTraversalOrder.PostOrder) yield return ancestorStack.ToList();
            }
            finally
            {
                ancestorStack.Pop();
            }
        }

        #endregion

        #region Slice

        /// <summary>
        ///     Efficiently slice an IEnumerable. Slicing is equivalent to this:
        ///     e.Skip(count).Concat(e.Take(count));
        ///     However, this Slice implementation is much faster than that.
        ///     From: http://stackoverflow.com/a/1301347
        /// </summary>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int count)
        {
            // If the enumeration is null, throw an exception.
            if (source == null) throw new ArgumentNullException("source");

            // Validate count.
            if (count < 0)
                throw new ArgumentOutOfRangeException("count",
                    "The count property must be a non-negative number.");

            // Short circuit, if the count is 0, just return the enumeration.
            if (count == 0) return source;

            // Is this an array?  If so, then take advantage of the fact it
            // is index based.
            if (source.GetType().IsArray)
            {
                // Return the array slice.
                return SliceArray((T[]) source, count);
            }

            // Check to see if it is a list.
            if (source is IList<T>)
            {
                // Return the list slice.
                return SliceList((IList<T>) source, count);
            }

            // Slice everything else.
            return SliceEverything(source, count);
        }

        private static IEnumerable<T> SliceArray<T>(T[] arr, int count)
        {
            // Error checking has been done, but use diagnostics or code
            // contract checking here.
            Debug.Assert(arr != null);
            Debug.Assert(count > 0);

            // Return from the count to the end of the array.
            for (int index = count; index < arr.Length; index++)
            {
                // Return the items at the end.
                yield return arr[index];
            }

            // Get the items at the beginning.
            for (int index = 0; index < count; index++)
            {
                // Return the items from the beginning.
                yield return arr[index];
            }
        }

        private static IEnumerable<T> SliceList<T>(IList<T> list, int count)
        {
            // Error checking has been done, but use diagnostics or code
            // contract checking here.
            Debug.Assert(list != null);
            Debug.Assert(count > 0);

            // Return from the count to the end of the list.
            for (int index = count; index < list.Count; index++)
            {
                // Return the items at the end.
                yield return list[index];
            }

            // Get the items at the beginning.
            for (int index = 0; index < list.Count; index++)
            {
                // Return the items from the beginning.
                yield return list[index];
            }
        }

        /// <summary>
        /// Helps with storing the sliced items.
        /// </summary>
        private static IEnumerable<T> SliceEverything<T>(
            this IEnumerable<T> source, int count)
        {
            // Test assertions.
            Debug.Assert(source != null);
            Debug.Assert(count > 0);

            // Create the helper.
            var helper = new SliceHelper<T>(
                source, count);

            // Return the helper concatenated with the skipped
            // items.
            return helper.Concat(helper.SkippedItems);
        }

        internal class SliceHelper<T> : IEnumerable<T>
        {
            // Creates a

            // The count of items to slice.
            private readonly int count;

            // The list of items that were skipped.
            private readonly IList<T> skippedItems;
            private readonly IEnumerable<T> source;

            internal SliceHelper(IEnumerable<T> source, int count)
            {
                // Test assertions.
                Debug.Assert(source != null);
                Debug.Assert(count > 0);

                // Set up the backing store for the list of items
                // that are skipped.
                skippedItems = new List<T>(count);

                // Set the count and the items.
                this.count = count;
                this.source = source;
            }

            // Expose the accessor for the skipped items.
            public IEnumerable<T> SkippedItems
            {
                get { return skippedItems; }
            }

            // Needed to implement IEnumerable<T>.
            // This is not supported.
            IEnumerator
                IEnumerable.GetEnumerator()
            {
                throw new InvalidOperationException(
                    "This operation is not supported.");
            }

            // Skips the items, but stores what is skipped in a list
            // which has capacity already set.
            public IEnumerator<T> GetEnumerator()
            {
                // The number of skipped items.  Set to the count.
                int skipped = count;

                // Cycle through the items.
                foreach (T item in source)
                {
                    // If there are items left, store.
                    if (skipped > 0)
                    {
                        // Store the item.
                        skippedItems.Add(item);

                        // Subtract one.
                        skipped--;
                    }
                    else
                    {
                        // Yield the item.
                        yield return item;
                    }
                }
            }
        }

        #endregion
    }
}
