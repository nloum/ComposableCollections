using System;
using System.Collections;
using System.Collections.Generic;

namespace ComposableCollections.List
{
    public class SkipReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _source;
        private int _skip;

        public int Skip
        {
            get => _skip;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Cannot skip a negative number of items");
                }
                _skip = value;
            }
        }

        public SkipReadOnlyList(IReadOnlyList<T> source, int skip)
        {
            _source = source;
            Skip = skip;
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
            for (var i = Skip; i < _source.Count; i++)
            {
                yield return _source[i];
            }
        }

        public int Count => _source.Count - Skip;

        public T this[int index] => _source[index + Skip];
    }
}