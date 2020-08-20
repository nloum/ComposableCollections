using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ComposableCollections.Dictionary
{
    public class DetransactionalQueryableDictionary<TKey, TValue> : DetransactionalDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue>
    {
        private readonly IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>> _wrapped;

        public DetransactionalQueryableDictionary(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> dictionary) : base(dictionary)
        {
            _wrapped = dictionary;
        }

        public IQueryable<TValue> Values
        {
            get
            {
                var result = _wrapped.BeginRead();
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