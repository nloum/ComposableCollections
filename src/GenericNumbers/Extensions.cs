using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMonads;
using static SimpleMonads.Utility;
using GenericNumbers.Arithmetic.Abs;
using GenericNumbers.Arithmetic.Ceiling;
using GenericNumbers.Arithmetic.DividedBy;
using GenericNumbers.Arithmetic.Floor;
using GenericNumbers.Arithmetic.Minus;
using GenericNumbers.Arithmetic.Plus;
using GenericNumbers.Arithmetic.RaisedTo;
using GenericNumbers.Arithmetic.Remainder;
using GenericNumbers.Arithmetic.Round;
using GenericNumbers.Arithmetic.SpecialNumbers;
using GenericNumbers.Arithmetic.Sqrt;
using GenericNumbers.Arithmetic.Times;
using GenericNumbers.ConvertTo;
using GenericNumbers.Relational.Equal;
using GenericNumbers.Relational.GreaterThan;
using GenericNumbers.Relational.GreaterThanOrEqual;
using GenericNumbers.Relational.LessThan;
using GenericNumbers.Relational.LessThanOrEqual;
using GenericNumbers.Relational.NotEqual;
using static GenericNumbers.NumbersUtility;

namespace GenericNumbers
{
    public static class Extensions
    {
        static Extensions()
        {
            AbsUtil<byte, byte>.Abs = (byte arg1) => arg1;
            AbsUtil<sbyte, sbyte>.Abs = Math.Abs;
            AbsUtil<int, int>.Abs = Math.Abs;
            AbsUtil<uint, uint>.Abs = (uint arg1) => arg1;
            AbsUtil<short, short>.Abs = Math.Abs;
            AbsUtil<ushort, ushort>.Abs = (ushort arg1) => arg1;
            AbsUtil<long, long>.Abs = Math.Abs;
            AbsUtil<ulong, ulong>.Abs = (ulong arg1) => arg1;
            AbsUtil<float, float>.Abs = Math.Abs;
            AbsUtil<double, double>.Abs = Math.Abs;
            AbsUtil<char, int>.Abs = (char arg1) => Math.Abs(arg1);

            SqrtUtil<double, double>.Sqrt = Math.Sqrt;
            SqrtUtil<float, float>.Sqrt = d => (float)Math.Sqrt(d);
            SqrtUtil<decimal, decimal>.Sqrt = d => (decimal)Math.Sqrt((double)d);

            CeilingUtil<byte, byte>.Ceiling = (byte arg1) => arg1;
            CeilingUtil<sbyte, sbyte>.Ceiling = arg1 => arg1;
            CeilingUtil<int, int>.Ceiling = arg1 => arg1;
            CeilingUtil<uint, uint>.Ceiling = (uint arg1) => arg1;
            CeilingUtil<short, short>.Ceiling = arg1 => arg1;
            CeilingUtil<ushort, ushort>.Ceiling = (ushort arg1) => arg1;
            CeilingUtil<long, long>.Ceiling = arg1 => arg1;
            CeilingUtil<ulong, ulong>.Ceiling = (ulong arg1) => arg1;
            CeilingUtil<float, double>.Ceiling = arg1 => Math.Ceiling(arg1);
            CeilingUtil<double, double>.Ceiling = Math.Ceiling;
            CeilingUtil<char, int>.Ceiling = (char arg1) => arg1;

            FloorUtil<byte, byte>.Floor = (byte arg1) => arg1;
            FloorUtil<sbyte, sbyte>.Floor = arg1 => arg1;
            FloorUtil<int, int>.Floor = arg1 => arg1;
            FloorUtil<uint, uint>.Floor = (uint arg1) => arg1;
            FloorUtil<short, short>.Floor = arg1 => arg1;
            FloorUtil<ushort, ushort>.Floor = (ushort arg1) => arg1;
            FloorUtil<long, long>.Floor = arg1 => arg1;
            FloorUtil<ulong, ulong>.Floor = (ulong arg1) => arg1;
            FloorUtil<float, float>.Floor = arg1 => (float) Math.Floor(arg1);
            FloorUtil<float, double>.Floor = arg1 => Math.Floor(arg1);
            FloorUtil<double, double>.Floor = Math.Floor;
            FloorUtil<char, int>.Floor = (char arg1) => arg1;

            RoundUtil<byte, byte>.Round = (byte arg1) => arg1;
            RoundUtil<sbyte, sbyte>.Round = arg1 => arg1;
            RoundUtil<int, int>.Round = arg1 => arg1;
            RoundUtil<uint, uint>.Round = (uint arg1) => arg1;
            RoundUtil<short, short>.Round = arg1 => arg1;
            RoundUtil<ushort, ushort>.Round = (ushort arg1) => arg1;
            RoundUtil<long, long>.Round = arg1 => arg1;
            RoundUtil<ulong, ulong>.Round = (ulong arg1) => arg1;
            RoundUtil<float, float>.Round = arg1 => (float) Math.Round(arg1);
            RoundUtil<float, double>.Round = arg1 => Math.Round(arg1);
            RoundUtil<double, double>.Round = Math.Round;
            RoundUtil<char, int>.Round = (char arg1) => arg1;

            SpecialNumbersUtil<double>.IsNegativeInfinite = d => d == double.NegativeInfinity;
            SpecialNumbersUtil<double>.IsPositiveInfinite = d => d == double.PositiveInfinity;
            SpecialNumbersUtil<double>.IsNumber = d => d != double.NaN;
            SpecialNumbersUtil<float>.IsNegativeInfinite = d => d == float.NegativeInfinity;
            SpecialNumbersUtil<float>.IsPositiveInfinite = d => d == float.PositiveInfinity;
            SpecialNumbersUtil<float>.IsNumber = d => d != float.NaN;
        }

        public static TNumber Negative<TNumber>(this TNumber number)
        {
            return number.Times(NumbersUtility<TNumber>.NegativeOne);
        }

        #region Statistics-related stuff

        public static IMaybe<TNumber> Maximum<TNumber>(this IEnumerable<TNumber> enumerable)
        {
            return Maximum(enumerable, a => a);
        }

        public static IMaybe<TNumber> Minimum<TNumber>(this IEnumerable<TNumber> enumerable)
        {
            return Minimum(enumerable, a => a);
        }

        public static IMaybe<T> Maximum<T, TNumber>(this IEnumerable<T> enumerable, Func<T, TNumber> selector)
        {
            IMaybe<T> currentMax = Nothing<T>();
            foreach (var item in enumerable)
            {
                if (!currentMax.HasValue || selector(item).GreaterThan(selector(currentMax.Value)))
                    currentMax = Something(item);
            }
            return currentMax;
        }

        public static IMaybe<T> Minimum<T, TNumber>(this IEnumerable<T> enumerable, Func<T, TNumber> selector)
        {
            IMaybe<T> currentMin = Nothing<T>();
            foreach (var item in enumerable)
            {
                if (!currentMin.HasValue || selector(item).LessThan(selector(currentMin.Value)))
                    currentMin = Something(item);
            }
            return currentMin;
        }

        public static IMaybe<TNumber> Mean<TNumber>(this IEnumerable<TNumber> enumerable)
        {
            List<TNumber> list = enumerable.ToList();
            if (!list.Any())
                return Nothing<TNumber>();
            var sum = 0.ConvertTo<int, TNumber>();
            foreach (var item in list)
            {
                sum = sum.Plus(item);
            }
            return Something(sum.DividedBy(list.Count.ConvertTo<int, TNumber>()));
        }

        public static IMaybe<TNumber> StandardDeviation<TNumber>(this IEnumerable<TNumber> enumerable)
        {
            return enumerable.Variance().Select(n => n.Sqrt());
        }

        public static IMaybe<TNumber> Variance<TNumber>(this IEnumerable<TNumber> enumerable)
        {
            List<TNumber> list = enumerable.ToList();
            if (!list.Any())
                return Nothing<TNumber>();
            var average = list.Mean().Value;
            for (int i = 0; i < list.Count; i++)
            {
                var difference = list[i].Minus(average);
                list[i] = difference.Times(difference);
            }
            return list.Mean();
        }

        private static IEnumerable<T> ItemConcat<T>(this T first, IEnumerable<T> next)
        {
            yield return first;
            foreach (var n in next)
            {
                yield return n;
            }
        }

        public static T MinimumWith<T>(this T firstValue, params T[] values)
        {
            return firstValue.ItemConcat(values).Minimum().Value;
        }

        public static T MaximumWith<T>(this T firstValue, params T[] values)
        {
            return firstValue.ItemConcat(values).Maximum().Value;
        }

        public static T MeanWith<T>(this T firstValue, params T[] values)
        {
            return firstValue.ItemConcat(values).Mean().Value;
        }

        #endregion

        #region Array indexing

        public static IEnumerable<TNumber> ConvertFrom1DIndex<TNumber>(this TNumber input, params TNumber[] maximums)
        {
            TNumber result = input;
            var results = new List<TNumber>();
            foreach (TNumber t in maximums)
            {
                results.Add(result.Remainder(t));
                result = result.DividedBy(t);
            }
            results.Add(result);
            return results;
        }

        public static TNumber ConvertTo1DIndex<TNumber>(this IReadOnlyList<TNumber> input, params TNumber[] maximums)
        {
            var result = 0.ConvertTo<int, TNumber>();
            int i = 0;
            for (; i < input.Count - 1; i++)
            {
                result = result.Plus(input[i].Times(maximums[i]));
            }
            result = result.Plus(input[i]);
            return result;
        }

        #endregion Array indexing

        #region Enumerating a whole number range

        public static NumbersInANumberRange<T> Numbers<T>(this INumberRange<T> source)
        {
            return new NumbersInANumberRange<T>(source);
        }
        
        #endregion
        
        #region Changing and managing INumberRange bounds' strictness

        /// <summary>
        /// Changes the upper bounds and lower bounds so that the numbers in the number range don't change,
        /// but the strictness values of the upper bounds and lower bounds become the specified values.
        /// When changing the <see cref="INumberRange{T}.LowerBound.IsStrict"/>, the <see cref="INumberRange{T}.LowerBound"/>
        /// property will change.
        /// When changing the <see cref="INumberRange{T}.UpperBound.IsStrict"/>, the <see cref="INumberRange{T}.UpperBound"/>
        /// property will change.
        /// </summary>
        public static INumberRange<T> ChangeStrictness<T>(this INumberRange<T> source, bool isLowerBoundStrict, bool isUpperBoundStrict)
        {
            if (isLowerBoundStrict == source.LowerBound.IsStrict && isUpperBoundStrict == source.UpperBound.IsStrict)
                return source;

            var one = 1.Convert().To<T>();

            var lowerBound = source.LowerBound.Value;
            if (!isLowerBoundStrict && source.LowerBound.IsStrict)
                lowerBound = lowerBound.Plus(one);
            else if (isLowerBoundStrict && !source.LowerBound.IsStrict)
                lowerBound = lowerBound.Minus(one);

            var upperBound = source.UpperBound.Value;
            if (!isUpperBoundStrict && source.UpperBound.IsStrict)
                upperBound = upperBound.Minus(one);
            else if (isUpperBoundStrict && !source.UpperBound.IsStrict)
                upperBound = upperBound.Plus(one);

            return Range(lowerBound, upperBound, isLowerBoundStrict, isUpperBoundStrict);
        }
        
        #endregion

        #region Checking number ranges and numbers for overlap

        public static bool Includes<T>(this INumberRange<T> range, T number)
        {
            return number.IsIncluded(range);
        }

        public static bool IsIncluded<T>(this T number, INumberRange<T> numberRange)
        {
            if (numberRange.IsRelative) throw new ArgumentException("Cannot check if a number is within a relative range");

            bool withinLowerBound;
            if (numberRange.LowerBound.IsStrict)
                withinLowerBound = number.GreaterThan(numberRange.LowerBound);
            else
                withinLowerBound = number.GreaterThanOrEqual(numberRange.LowerBound);

            bool withinUpperBound;
            if (numberRange.LowerBound.IsStrict)
                withinUpperBound = number.LessThan(numberRange.LowerBound);
            else
                withinUpperBound = number.LessThanOrEqual(numberRange.LowerBound);

            return withinLowerBound && withinUpperBound;
        }

        public static NumberRangeSubset IsIncluded<T>(this INumberRange<T> possibleSubset, INumberRange<T> possibleSuperset)
        {
            possibleSubset = possibleSubset.ChangeStrictness(false, false);

            var isLowerBoundWithin = possibleSubset.LowerBound.Value.IsIncluded(possibleSuperset);
            var isUpperBoundWithin = possibleSubset.UpperBound.Value.IsIncluded(possibleSuperset);

            if (isLowerBoundWithin && isUpperBoundWithin)
                return NumberRangeSubset.Entirely;
            if (isLowerBoundWithin || isUpperBoundWithin)
                return NumberRangeSubset.Partially;
            return NumberRangeSubset.None;
        }

        public static NumberRangeSubset Includes<T>(
            this INumberRange<T> possibleSuperset,
            INumberRange<T> possibleSubset)
        {
            return possibleSubset.IsIncluded(possibleSuperset);
        }

        #endregion

        #region Combining/splitting number ranges

        public static IEnumerable<INumberRange<T>> Split<T>(this INumberRange<T> superset, T splitAt, NumberRangeSplitBehavior behavior)
        {
            if (!superset.Includes(splitAt))
            {
                yield return superset;
                yield break;
            }

            yield return Range(superset.LowerBound.Value, splitAt, superset.LowerBound.IsStrict, behavior.HasFlag(NumberRangeSplitBehavior.IncludeSplitterInFirstRange));
            yield return Range(splitAt, superset.UpperBound.Value, behavior.HasFlag(NumberRangeSplitBehavior.IncludeSplitterInSecondRange), superset.UpperBound.IsStrict);
        }

        public static IEnumerable<INumberRange<T>> Split<T>(this INumberRange<T> superset, INumberRange<T> subset)
        {
            return new[] { superset, subset }.IntersectionCount().Select(intersection => intersection.Range);
        }

        public static IEnumerable<INumberRange<T>> SplitOffset<T>(this INumberRange<T> superset, INumberRange<T> subset)
        {
            var result = new[] { superset, subset }.IntersectionCount().Select(intersection => intersection.Range).ToList();
            result[result.Count - 1] = Range(result[result.Count - 1].LowerBound.Value, result[result.Count - 1].UpperBound.Value.Plus(subset.Size), result[result.Count - 1].LowerBound.IsStrict, result[result.Count - 1].UpperBound.IsStrict);
            return result;
        }

        public static IEnumerable<INumberRange<T>> Intersection<T>(this IReadOnlyList<INumberRange<T>> ranges)
        {
            return
                ranges.IntersectionCount()
                    .Where(intersection => intersection.RangesThatIncludeThis.Count == ranges.Count)
                    .Select(intersection => intersection.Range);
        }

        public static IEnumerable<INumberRangeIntersectionCount<T>> IntersectionCount<T>(this IReadOnlyList<INumberRange<T>> ranges)
        {
            var bounds = ranges.SelectMany(range => range.Select(bound => new {bound, range}))
                .OrderBy(bound => bound.bound.Value).ToList();
            var currentRanges = new List<INumberRange<T>>();
            currentRanges.Add(bounds[0].range);
            for (var i = 1; i < bounds.Count; i++)
            {
                var prevBound = bounds[i - 1];
                var bound = bounds[i];

                var lowerBoundIsStrict = prevBound.bound.IsLower ? prevBound.bound.IsStrict : !prevBound.bound.IsStrict;
                var upperBoundIsStrict = bound.bound.IsLower     ?    !bound.bound.IsStrict :      bound.bound.IsStrict;

                yield return new NumberRangeIntersectionCount<T>(currentRanges.ToList(), Range(prevBound.bound.Value, bound.bound.Value, lowerBoundIsStrict, upperBoundIsStrict));

                if (bound.bound.IsLower) currentRanges.Add(bound.range);
                else currentRanges.Remove(bound.range);
            }
        }

        public static IEnumerable<INumberRange<T>> Union<T>(this IEnumerable<INumberRange<T>> ranges, IComparer<T> comparer = null)
        {
            if (ranges == null || !ranges.Any())
                yield break;

            if (comparer == null)
                comparer = Comparer<T>.Default;

            var ordered = ranges.OrderBy(r => r.LowerBound.Value);
            var firstRange = ordered.First();

            T min = firstRange.LowerBound.Value;
            T max = firstRange.UpperBound.Value;

            foreach (var current in ordered.Skip(1))
            {
                if (comparer.Compare(current.UpperBound.Value, max) > 0 && comparer.Compare(current.LowerBound.Value, max) > 0)
                {
                    yield return Range(min, max);
                    min = current.LowerBound.Value;
                }
                max = comparer.Compare(max, current.UpperBound.Value) > 0 ? max : current.UpperBound.Value;
            }
            yield return Range(min, max);
        }

        #endregion

        public static double RoundToNearestMultipleOf(this double d, double factor)
        {
            return Math.Round(d / factor) * factor;
        }

        public static double FloorToMultipleOf(this double d, double factor)
        {
            return Math.Floor(d / factor) * factor;
        }

        public static double CeilingToMultipleOf(this double d, double factor)
        {
            return Math.Ceiling(d / factor) * factor;
        }

        public static float RoundToNearestMultipleOf(this float d, float factor)
        {
            return (float)(Math.Round(d / factor) * factor);
        }

        public static float FloorToMultipleOf(this float d, float factor)
        {
            return (float)(Math.Floor(d / factor) * factor);
        }

        public static float CeilingToMultipleOf(this float d, float factor)
        {
            return (float)(Math.Ceiling(d / factor) * factor);
        }

        public static bool IsNegativeInfinite<T>(this T t)
        {
            return SpecialNumbersUtil<T>.IsNegativeInfinite(t);
        }

        public static bool IsPositiveInfinite<T>(this T t)
        {
            return SpecialNumbersUtil<T>.IsPositiveInfinite(t);
        }

        public static bool IsInfinite<T>(this T t)
        {
            return t.IsNegativeInfinite() || t.IsPositiveInfinite();
        }

        public static bool IsNumber<T>(this T t)
        {
            return SpecialNumbersUtil<T>.IsNumber(t);
        }

        public static TOutput DividedBy<T, TInput, TOutput>(this T t, TInput input)
        {
            return DividedByUtil<T, TInput, TOutput>.DividedBy(t, input);
        }

        public static T DividedBy<T>(this T t, T input)
        {
            return DividedByUtil<T, T, T>.DividedBy(t, input);
        }

        public static TOutput Ceiling<T, TOutput>(this T t)
        {
            return CeilingUtil<T, TOutput>.Ceiling(t);
        }

        public static T Ceiling<T>(this T t)
        {
            return CeilingUtil<T, T>.Ceiling(t);
        }

        public static TOutput Sqrt<T, TOutput>(this T t)
        {
            return SqrtUtil<T, TOutput>.Sqrt(t);
        }

        public static T Sqrt<T>(this T t)
        {
            return SqrtUtil<T, T>.Sqrt(t);
        }

        public static TOutput Abs<T, TOutput>(this T t)
        {
            return AbsUtil<T, TOutput>.Abs(t);
        }

        public static T Abs<T>(this T t)
        {
            return AbsUtil<T, T>.Abs(t);
        }

        public static TOutput Floor<T, TOutput>(this T t)
        {
            return FloorUtil<T, TOutput>.Floor(t);
        }

        public static T Floor<T>(this T t)
        {
            return FloorUtil<T, T>.Floor(t);
        }

        public static TOutput Minus<T, TInput, TOutput>(this T t, TInput input)
        {
            return MinusUtil<T, TInput, TOutput>.Minus(t, input);
        }

        public static T Minus<T>(this T t, T input)
        {
            return MinusUtil<T, T, T>.Minus(t, input);
        }

        public static TOutput Plus<T, TInput, TOutput>(this T t, TInput input)
        {
            return PlusUtil<T, TInput, TOutput>.Plus(t, input);
        }

        public static T Plus<T>(this T t1, T t2)
        {
            return PlusUtil<T, T, T>.Plus(t1, t2);
        }

        public static TOutput RaisedTo<T, TInput, TOutput>(this T t, TInput input)
        {
            return RaisedToUtil<T, TInput, TOutput>.RaisedTo(t, input);
        }

        public static T RaisedTo<T>(this T t, T input)
        {
            return RaisedToUtil<T, T, T>.RaisedTo(t, input);
        }

        public static TOutput Remainder<T, TInput, TOutput>(this T t, TInput input)
        {
            return RemainderUtil<T, TInput, TOutput>.Remainder(t, input);
        }

        public static T Remainder<T>(this T t, T input)
        {
            return RemainderUtil<T, T, T>.Remainder(t, input);
        }

        public static TOutput Round<T, TOutput>(this T t)
        {
            return RoundUtil<T, TOutput>.Round(t);
        }

        public static T Round<T>(this T t)
        {
            return RoundUtil<T, T>.Round(t);
        }

        public static TOutput Times<T, TInput, TOutput>(this T t, TInput input)
        {
            return TimesUtil<T, TInput, TOutput>.Times(t, input);
        }

        public static T Times<T>(this T t, T input)
        {
            return TimesUtil<T, T, T>.Times(t, input);
        }

        public static TOutput ConvertTo<T, TOutput>(this T t)
        {
            return ConvertToUtil<T, TOutput>.ConvertTo(t);
        }

        public static ConverterTo<T> Convert<T>(this T t)
        {
            return new ConverterTo<T>(t);
        }

        public static bool Equal<T, TInput>(this T t, TInput input)
        {
            return EqualUtil<T, TInput>.Equal(t, input);
        }

        public static bool GreaterThan<T, TInput>(this T t, TInput input)
        {
            return GreaterThanUtil<T, TInput>.GreaterThan(t, input);
        }

        public static bool GreaterThanOrEqual<T, TInput>(this T t, TInput input)
        {
            return GreaterThanOrEqualUtil<T, TInput>.GreaterThanOrEqual(t, input);
        }

        public static bool LessThan<T, TInput>(this T t, TInput input)
        {
            return LessThanUtil<T, TInput>.LessThan(t, input);
        }

        public static bool LessThanOrEqual<T, TInput>(this T t, TInput input)
        {
            return LessThanOrEqualUtil<T, TInput>.LessThanOrEqual(t, input);
        }

        public static bool NotEqual<T, TInput>(this T t, TInput input)
        {
            return NotEqualUtil<T, TInput>.NotEqual(t, input);
        }

        #region Methods that use the above, base-level methods

        /// <summary>
        /// Returns whether or not two inputs are "close" to each other with respect to a given delta.
        /// </summary>
        /// <remarks>
        /// This implementation currently does no overflow checking - if (input1-input2) overflows, it
        /// could yield the wrong result.
        /// </remarks>
        /// <typeparam name="T">Type to calculate with</typeparam>
        /// <param name="input1">First input value</param>
        /// <param name="input2">Second input value</param>
        /// <param name="delta">Permitted range (exclusive)</param>
        /// <returns>True if AbsoluteValue(input1-input2) is less than or equal to delta; false otherwise.</returns>
        public static bool WithinDelta<T>(this T input1, T input2, T delta)
        {
            return input1.Minus(input2).Abs().LessThanOrEqual(delta);
        }

        public static T Constrain<T>(this T value, T inclusiveMin, T inclusiveMax)
        {
            if (value.LessThan(inclusiveMin))
                return inclusiveMin;
            if (value.GreaterThan(inclusiveMax))
                return inclusiveMax;
            return value;
        }

        public static bool HasFractionalPart<T>(this T value)
        {
            return value.Round().Equal(value);
        }

        public static T NextPowerOf<T>(this T n, T exponent)
        {
            T i;
            var one = 1.ConvertTo<int, T>();
            var two = 2.ConvertTo<int, T>();
            for (i = one; i.LessThan(n); i = i.Times(two)) ;
            return i;
        }

        public static bool ApproxEqual<T>(this T t1, T t2)
        {
            return t1.WithinDelta(t2, NumbersUtility<T>.Epsilon);
        }

        public static T Gcd<T>(T[] numbers)
        {
            return numbers.Aggregate(Gcd);
        }

        public static T Gcd<T>(T a, T b)
        {
            return b.Equal(0.ConvertTo<int, T>()) ? a : Gcd(b, a.Remainder(b));
        }

        public static IEnumerable<T> Fibonacci<T>()
        {
            var prev = 0.Convert().To<T>();
            var next = 1.Convert().To<T>();
            while (true)
            {
                yield return next;
                next = prev.Plus(next);
            }
        }

        #endregion
    }
}
