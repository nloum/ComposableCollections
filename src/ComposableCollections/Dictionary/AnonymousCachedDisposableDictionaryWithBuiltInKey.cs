using System;
using System.Collections.Generic;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedDisposableDictionaryWithBuiltInKey<TKey, TValue> : DelegateDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDisposable _disposable;
        private Func<IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>> _asBypassCache;
        private Func<IDictionaryWithBuiltInKey<TKey, TValue>> _asNeverFlush;
        private Action _flushCache;
        private Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> _getMutations;

        public AnonymousCachedDisposableDictionaryWithBuiltInKey(IDictionaryWithBuiltInKey<TKey, TValue> wrapped, IDisposable disposable, Func<IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>> asBypassCache, Func<IDictionaryWithBuiltInKey<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> getMutations) : base(wrapped)
        {
            _disposable = disposable;
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getMutations = getMutations;
        }

        protected AnonymousCachedDisposableDictionaryWithBuiltInKey()
        {
        }

        protected void Initialize(IDictionaryWithBuiltInKey<TKey, TValue> wrapped, IDisposable disposable,
            Func<IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>> asBypassCache,
            Func<IDictionaryWithBuiltInKey<TKey, TValue>> asNeverFlush, Action flushCache,
            Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> getMutations)
        {
            _disposable = disposable;
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getMutations = getMutations;
            base.Initialize(wrapped);
        }

        public IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> AsBypassCache()
        {
            return _asBypassCache();
        }

        public IDictionaryWithBuiltInKey<TKey, TValue> AsNeverFlush()
        {
            return _asNeverFlush();
        }

        public void FlushCache()
        {
            _flushCache();
        }

        public IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear)
        {
            return _getMutations(clear);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}