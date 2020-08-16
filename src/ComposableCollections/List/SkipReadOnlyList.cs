using System;
using System.Collections;
using System.Collections.Generic;

namespace ComposableCollections.List
{
    public class SkipReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _source;
        private readonly int _skip;

        public SkipReadOnlyList(IReadOnlyList<T> source, int skip)
        {
            _source = source;
            _skip = skip;

            if (_skip < 0)
            {
                throw new ArgumentException($"Cannot skip a negative number of items");
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
            for (var i = _skip; i < _source.Count; i++)
            {
                yield return _source[i];
            }
        }

        public int Count => _source.Count - _skip;

        public T this[int index] => _source[index + _skip];
    }
}