using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Set.Base;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set.Adapters
{
    public class CachedQueryableSetAdapter<TValue> : DelegateSet<TValue>, ICachedQueryableSet<TValue>
    {
        private readonly IQueryable<TValue> _queryable;
        private readonly Func<IReadOnlySet<TValue>> _asBypassCache;
        private readonly Func<IComposableSet<TValue>> _asNeverFlush;
        private readonly Action _flushCache;
        private readonly Func<IEnumerable<SetWrite<TValue>>> _getWrites;

        public CachedQueryableSetAdapter(IComposableSet<TValue> set, IQueryable<TValue> queryable, Func<IReadOnlySet<TValue>> asBypassCache, Func<IComposableSet<TValue>> asNeverFlush, Action flushCache, Func<IEnumerable<SetWrite<TValue>>> getWrites) : base(set)
        {
            _queryable = queryable;
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getWrites = getWrites;
        }

        public Type ElementType => _queryable.ElementType;
        public Expression Expression => _queryable.Expression;
        public IQueryProvider Provider => _queryable.Provider;
        public IReadOnlySet<TValue> AsBypassCache()
        {
            return _asBypassCache();
        }

        public IComposableSet<TValue> AsNeverFlush()
        {
            return _asNeverFlush();
        }

        public void FlushCache()
        {
            _flushCache();
        }

        public IEnumerable<SetWrite<TValue>> GetWrites(bool clear)
        {
            return _getWrites();
        }
    }
}