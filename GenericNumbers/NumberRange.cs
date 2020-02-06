using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GenericNumbers.Relational;

namespace GenericNumbers
{
    internal class NumberRange<T> : INumberRange<T>, IComparable<INumberRange<T>>
    {
        protected NumberRange(T lowerBound, T upperBound, bool isLowerBoundStrict = false, bool isUpperBoundStrict = true)
        {
            LowerBound = new NumberBound<T>(lowerBound, isLowerBoundStrict, true);
            UpperBound = new NumberBound<T>(upperBound, isUpperBoundStrict, false);
        }

        internal static NumberRange<T> Create(T lowerBound, T upperBound, bool isLowerBoundStrict = false, bool isUpperBoundStrict = true)
        {
            return new NumberRange<T>(lowerBound, upperBound, isLowerBoundStrict, isUpperBoundStrict);
        }
        
        public INumberBound<T> LowerBound { get; protected set; }
        public INumberBound<T> UpperBound { get; protected set; }

        protected bool Equals(NumberRange<T> other)
        {
            return LowerBound.Equals(other.LowerBound) && UpperBound.Equals(other.UpperBound);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<INumberBound<T>> GetEnumerator()
        {
            return new[] { LowerBound, UpperBound }.AsEnumerable().GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NumberRange<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 397;
                hashCode = (hashCode * 397) ^ LowerBound.GetHashCode();
                hashCode = (hashCode * 397) ^ UpperBound.GetHashCode();
                return hashCode;
            }
        }

        public bool IsRelative => this.LowerBound.IsRelative || this.UpperBound.IsRelative;

        public T Size
        {
            get
            {
                if (IsRelative)
                    throw new ArgumentException("Cannot calculate the size of a relative range");

                if (NumbersUtility<T>.IsWholeNumber)
                {
                    var min = LowerBound.Value.Plus(!LowerBound.IsStrict ? 0.Convert().To<T>() : 1.Convert().To<T>());
                    var max = UpperBound.Value.Plus(UpperBound.IsStrict  ? 0.Convert().To<T>() : 1.Convert().To<T>());
                    return max.Minus(min);
                }
                return UpperBound.Value.Minus(LowerBound.Value);
            }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(INumberRange<T> other)
        {
            var comparison = Size.CompareTo(other.Size);
            if (comparison != 0) return comparison;
            comparison = LowerBound.CompareTo(other.LowerBound);
            if (comparison != 0) return comparison;
            return UpperBound.CompareTo(other.UpperBound);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (LowerBound.IsStrict)
                sb.Append("(");
            else
                sb.Append("[");

            sb.Append(LowerBound.Value);

            sb.Append(", ");

            sb.Append(UpperBound.Value);

            if (UpperBound.IsStrict)
                sb.Append(")");
            else
                sb.Append("]");

            return sb.ToString();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        /// <returns>
        /// The number of elements in the collection. 
        /// </returns>
        public int Count => 2;

        /// <summary>
        /// Gets the element at the specified index in the read-only list.
        /// </summary>
        /// <returns>
        /// The element at the specified index in the read-only list.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get. </param>
        public INumberBound<T> this[int index]
        {
            get
            {
                if (index == 0) return LowerBound;
                if (index == 1) return UpperBound;
                throw new IndexOutOfRangeException($"There are two bounds in a number range and an index of {index} was specified");
            }
        }
    }
}
