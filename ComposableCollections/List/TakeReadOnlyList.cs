using System;
using System.Collections;
using System.Collections.Generic;

namespace ComposableCollections.List
{
    public class TakeReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _source;
        private int _take;

        public int Take
        {
            get => _take;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Cannot take a negative number of items");
                }
                _take = value;
            }
        }

        public TakeReadOnlyList(IReadOnlyList<T> source, int take)
        {
            _source = source;
            Take = take;
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
            for (var i = 0; i < _source.Count && i < Take; i++)
            {
                yield return _source[i];
            }
        }

        public int Count => Math.Min(_source.Count, Take);

        public T this[int index]
        {
            get
            {
                if (index >= Take || index < 0)
                {
                    throw new IndexOutOfRangeException($"The index {index} is out of range of a list that has {Take} elements");
                }

                return _source[index];
            }
        }
    }
}