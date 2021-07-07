using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IoFluently
{
    public class QueryableWithDisposable<T> : IQueryable<T>
    {
        private readonly IQueryable<T> _queryable;
        private readonly IDisposable _disposable;

        public QueryableWithDisposable(IQueryable<T> queryable, IDisposable disposable)
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
        
        private class Enumerator<T> : IEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;
            private readonly IDisposable _disposable;

            public Enumerator(IEnumerator<T> enumerator, IDisposable disposable)
            {
                _enumerator = enumerator;
                _disposable = disposable;
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
            }

            public void Dispose()
            {
                _enumerator.Dispose();
                _disposable.Dispose();
            }

            public T Current => _enumerator.Current;
        }
    }
}