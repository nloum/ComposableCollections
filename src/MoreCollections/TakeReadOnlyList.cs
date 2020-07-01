using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreCollections
{
    public class TakeReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _source;
        private readonly int _take;

        public TakeReadOnlyList(IReadOnlyList<T> source, int take)
        {
            _source = source;
            _take = take;

            if (_take < 0)
            {
                throw new ArgumentException($"Cannot take a negative number of items");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        private IEnumerable<T> Enumerate()
        {
            for (var i = 0; i < _source.Count && i < _take; i++)
            {
                yield return _source[i];
            }
        }

        public int Count => Math.Min(_source.Count, _take);

        public T this[int index]
        {
            get
            {
                if (index >= _take || index < 0)
                {
                    throw new IndexOutOfRangeException($"The index {index} is out of range of a list that has {_take} elements");
                }

                return _source[index];
            }
        }
    }
}