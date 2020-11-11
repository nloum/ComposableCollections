using System.Collections.Generic;
using GenericNumbers;
using static GenericNumbers.NumbersUtility;

namespace ComposableCollections.List
{
    internal static class InternalExtensions
    {
        
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
    }
}