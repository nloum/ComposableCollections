using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MoreCollections
{
    public class SelectReadOnlyList<TSource, TResult> : IReadOnlyList<TResult>
    {
        private readonly IReadOnlyList<TSource> _source;
        private readonly Func<TSource, int, TResult> _selector;

        public SelectReadOnlyList(IReadOnlyList<TSource> source, Func<TSource, int, TResult> selector)
        {
            _source = source;
            _selector = selector;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TResult> GetEnumerator()
        {
            for (var i = 0; i < _source.Count; i++)
            {
                yield return _selector(_source[i], i);
            }
        }

        public int Count => _source.Count;

        public TResult this[int index] => _selector(_source[index], index);
    }
}
