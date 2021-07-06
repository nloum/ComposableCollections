using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set.Adapters
{
    public class CachedDisposableQueryableSetAdapter<TValue> : CachedDisposableSetAdapter<TValue>, ICachedDisposableQueryableSet<TValue>
    {
        private readonly IQueryable<TValue> _queryable;

        public CachedDisposableQueryableSetAdapter(IComposableSet<TValue> set, Func<IReadOnlySet<TValue>> asBypassCache, Func<IComposableSet<TValue>> asNeverFlush, Action flushCache, Func<IEnumerable<SetWrite<TValue>>> getWrites, IDisposable disposable, IQueryable<TValue> queryable) : base(set, asBypassCache, asNeverFlush, flushCache, getWrites, disposable)
        {
            _queryable = queryable;
        }

        public Type ElementType => _queryable.ElementType;
        public Expression Expression => _queryable.Expression;
        public IQueryProvider Provider => _queryable.Provider;
    }
}