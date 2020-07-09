using System;
using System.Collections.Generic;

using GenericNumbers.Relational;

namespace GenericNumbers
{
    public class NumberBound<T> : INumberBound<T>, IComparable<INumberBound<T>>
    {
        public NumberBound(T value, bool isStrict, bool isLower)
        {
            this.Value = value;
            this.IsStrict = isStrict;
            this.IsLower = isLower;
        }

        public T Value { get; }

        public bool IsStrict { get; }

        public bool IsLower { get; }

        public bool IsUpper => !this.IsLower;

        public bool IsRelative => this.Value.LessThan(NumbersUtility<T>.Zero);

        protected bool Equals(NumberBound<T> other)
        {
            return EqualityComparer<T>.Default.Equals(this.Value, other.Value) && this.IsStrict == other.IsStrict && this.IsLower == other.IsLower && this.IsRelative == other.IsRelative;
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
            return Equals((NumberBound<T>)obj);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(INumberBound<T> other)
        {
            var comparison = IsRelative.CompareTo(other.IsRelative);
            if (comparison != 0) return comparison;
            comparison = Value.CompareTo(other.Value);
            if (comparison != 0) return comparison;
            comparison = IsLower.CompareTo(other.IsLower);
            if (comparison != 0) return comparison;
            comparison = IsStrict.CompareTo(other.IsStrict);
            return comparison;
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
                var hashCode = EqualityComparer<T>.Default.GetHashCode(this.Value);
                hashCode = (hashCode * 397) ^ this.IsStrict.GetHashCode();
                hashCode = (hashCode * 397) ^ this.IsLower.GetHashCode();
                hashCode = (hashCode * 397) ^ this.IsRelative.GetHashCode();
                return hashCode;
            }
        }

        public INumberBound<T> ChangeStrictness(bool newIsStrict)
        {
            if (IsStrict == newIsStrict)
                return this;
            if (!NumbersUtility<T>.IsWholeNumber)
                return new NumberBound<T>(Value, newIsStrict, IsLower);

            if (IsLower && newIsStrict)
                return new NumberBound<T>(Value.Minus(NumbersUtility<T>.One), newIsStrict, IsLower);
            if (IsLower && !newIsStrict)
                return new NumberBound<T>(Value.Plus(NumbersUtility<T>.One), newIsStrict, IsLower);
            if (IsUpper && newIsStrict)
                return new NumberBound<T>(Value.Plus(NumbersUtility<T>.One), newIsStrict, IsLower);
            if (IsUpper && !newIsStrict)
                return new NumberBound<T>(Value.Minus(NumbersUtility<T>.One), newIsStrict, IsLower);

            throw new NotImplementedException($"Unknown combination of IsLower and newIsStrict: {IsLower} and {newIsStrict}");
        }
    }
}
