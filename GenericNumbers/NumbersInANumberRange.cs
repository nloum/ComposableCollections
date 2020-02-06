using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericNumbers
{
    public class NumbersInANumberRange<T> : IReadOnlyList<T>
    {
        private readonly INumberRange<T> _range;

        public NumbersInANumberRange(INumberRange<T> range)
        {
            if (!NumbersUtility<T>.IsWholeNumber)
                throw new ArgumentException("Cannot enumerate all numbers in a non-whole number range");
            _range = range;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        public int Count => _range.Size.Convert().To<int>();

        public T this[int index] => _range.LowerBound.ChangeStrictness(false).Value.Plus(index.Convert().To<T>());
    }
}