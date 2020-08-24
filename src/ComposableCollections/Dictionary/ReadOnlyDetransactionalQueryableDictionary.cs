using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Common;

namespace ComposableCollections.Dictionary
{
    public class ReadOnlyDetransactionalQueryableDictionary<TKey, TValue> : ReadOnlyDetransactionalDictionary<TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>> _source;

        public ReadOnlyDetransactionalQueryableDictionary(IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>> dictionary) : base(dictionary)
        {
            _source = dictionary;
        }

        public IQueryable<TValue> Values
        {
            get
            {
                var result = _source.BeginRead();
                return new Queryable<TValue>(result.Values, result);
            }
        }

        private class Queryable<T> : IQueryable<T>
        {
            private readonly IQueryable<T> _queryable;
            private readonly IDisposable _disposable;

            public Queryable(IQueryable<T> queryable, IDisposable disposable)
            {
                _queryable = queryable;
                _disposable = disposable;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new Enumerator<T>(_queryable.GetEnumerator(), _disposable);
            }

            public Type ElementType => _queryable.ElementType;

            public Expression Expression => _queryable.Expression;

            public IQueryProvider Provider => _queryable.Provider;
        }
    }
}