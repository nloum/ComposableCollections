using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenericNumbers.Relational;

namespace GenericNumbers
{
    public class NumberRangeIntersectionCount<T> : INumberRangeIntersectionCount<T>, IComparable<INumberRangeIntersectionCount<T>>
    {
        public NumberRangeIntersectionCount(IReadOnlyList<INumberRange<T>> rangesThatIncludeThis, INumberRange<T> range)
        {
            if (range == null)
                throw new ArgumentNullException(nameof(range));
            RangesThatIncludeThis = rangesThatIncludeThis;
            this.Range = range;
        }

        public IReadOnlyList<INumberRange<T>> RangesThatIncludeThis { get; }

        public INumberRange<T> Range { get; }

        protected bool Equals(NumberRangeIntersectionCount<T> other)
        {
            return CompareTo(other) == 0;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((NumberRangeIntersectionCount<T>)obj);
        }

        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 31;

                foreach (var element in RangesThatIncludeThis)
                {
                    hashCode = hashCode ^ element.GetHashCode();
                }

                hashCode = hashCode ^ Range.GetHashCode();

                return hashCode;
            }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(INumberRangeIntersectionCount<T> other)
        {
            var orderedRanges = RangesThatIncludeThis
                .OrderBy(range => range.LowerBound.Value)
                .ThenBy(range => range.LowerBound.IsStrict)
                .ToList();
            var otherOrderedRanges = other.RangesThatIncludeThis
                .OrderBy(range => range.LowerBound.Value)
                .ThenBy(range => range.LowerBound.IsStrict)
                .ToList();
            foreach (var comparison in orderedRanges.Zip(otherOrderedRanges, (a, b) => a.CompareTo(b)))
            {
                if (comparison != 0) return comparison;
            }
            {
                var comparison = Range.CompareTo(other.Range);
                return comparison;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"{RangesThatIncludeThis.Count} ranges include {Range}";
        }
    }
}
