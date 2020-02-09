using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PowerCollections
{
    public class SelectReadOnlyList<TSource, TResult> : IReadOnlyList<TResult>
    {
        private readonly IReadOnlyList<TSource> _source;
        private readonly Func<TSource, TResult> _selector;

        public SelectReadOnlyList(IReadOnlyList<TSource> source, Func<TSource, TResult> selector)
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
            foreach (var item in _source)
            {
                yield return _selector(item);
            }
        }

        public int Count => _source.Count;

        public TResult this[int index]
        {
            get { return _selector(_source[index]); }
        }
    }
}
