using System.Collections.Generic;

namespace GenericNumbers
{
    /// <summary>
    /// Represents a range of numbers.
    /// </summary>
    public interface INumberRange<out T> : IReadOnlyList<INumberBound<T>>
    {
        /// <summary>
        /// The lower bound of the number range.
        /// If <see cref="INumberBound{T}.IsStrict"/> is false, then this property is the lowest number
        /// in the number range. If <see cref="INumberBound{T}.IsStrict"/> is true, then the number range
        /// starts just after this value.
        /// 
        /// For instance, with a LowerBound of 3 and an UpperBound of 5, when <see cref="INumberBound{T}.IsStrict"/>
        /// is false, the number range will include the number 3. If <see cref="INumberBound{T}.IsStrict"/> is true
        /// however, the number range will not include the number 3.
        /// </summary>
        INumberBound<T> LowerBound { get; }

        /// <summary>
        /// The upper bound of the number range.
        /// If <see cref="INumberBound{T}.IsStrict"/> is false, then this property is the largest number
        /// in the number range. If <see cref="INumberBound{T}.IsStrict"/> is true, then the number range
        /// ends just before this value.
        /// 
        /// For instance, with a LowerBound of 3 and an UpperBound of 5, when <see cref="INumberBound{T}.IsStrict"/>
        /// is false, the number range will include the number 5. If <see cref="INumberBound{T}.IsStrict"/> is true
        /// however, the number range will not include the number 5.
        /// </summary>
        INumberBound<T> UpperBound { get; }
        
        bool IsRelative { get; }

        T Size { get; }

        bool Equals(object obj);
    }
}
