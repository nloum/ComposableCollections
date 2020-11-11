using System;
using System.Collections.Generic;
using System.Linq;

namespace ComposableCollections.List
{
    public static class BoyerMooreSearchExtensions
    {
        
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
    }
}