using System;
using System.Collections.Generic;
using ComposableCollections.Set.Base;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set.Adapters
{
    public class CachedDisposableSetAdapter<TValue> : DelegateSet<TValue>, ICachedDisposableSet<TValue>
    {
        private readonly Func<IReadOnlySet<TValue>> _asBypassCache;
        private readonly Func<IComposableSet<TValue>> _asNeverFlush;
        private readonly Action _flushCache;
        private readonly Func<IEnumerable<SetWrite<TValue>>> _getWrites;
        private readonly IDisposable _disposable;

        public CachedDisposableSetAdapter(IComposableSet<TValue> set, Func<IReadOnlySet<TValue>> asBypassCache, Func<IComposableSet<TValue>> asNeverFlush, Action flushCache, Func<IEnumerable<SetWrite<TValue>>> getWrites, IDisposable disposable) : base(set)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getWrites = getWrites;
            _disposable = disposable;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

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