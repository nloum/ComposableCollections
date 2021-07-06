using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Set.Base;

namespace ComposableCollections.Set.Adapters
{
    public class DisposableQueryableReadOnlySetAdapter<TValue> : DelegateReadOnlySet<TValue>, IDisposableQueryableReadOnlySet<TValue>
    {
        private readonly IDisposable _disposable;
        private readonly IQueryable<TValue> _queryable;

        public DisposableQueryableReadOnlySetAdapter(IReadOnlySet<TValue> state, IDisposable disposable, IQueryable<TValue> queryable) : base(state)
        {
            _disposable = disposable;
            _queryable = queryable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public Type ElementType => _queryable.ElementType;
        public Expression Expression => _queryable.Expression;
        public IQueryProvider Provider => _queryable.Provider;
    }
}