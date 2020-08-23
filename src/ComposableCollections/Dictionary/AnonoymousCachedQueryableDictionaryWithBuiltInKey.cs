using System;
using System.Collections.Generic;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonoymousCachedQueryableDictionaryWithBuiltInKey<TKey, TValue> : DelegateDictionaryWithBuiltInKey<TKey, TValue>, ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private Func<IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>> _asBypassCache;
        private Func<IDictionaryWithBuiltInKey<TKey, TValue>> _asNeverFlush;
        private Action _flushCache;
        private Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> _getMutations;

        public AnonoymousCachedQueryableDictionaryWithBuiltInKey(IDictionaryWithBuiltInKey<TKey, TValue> wrapped, Func<IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>> asBypassCache, Func<IDictionaryWithBuiltInKey<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> getMutations, IQueryable<TValue> values) : base(wrapped)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getMutations = getMutations;
            Values = values;
        }

        protected AnonoymousCachedQueryableDictionaryWithBuiltInKey()
        {
        }

        protected void Initialize(IDictionaryWithBuiltInKey<TKey, TValue> wrapped, Func<IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>> asBypassCache, Func<IDictionaryWithBuiltInKey<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> getMutations, IQueryable<TValue> values)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getMutations = getMutations;
            Values = values;
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

        public new IQueryable<TValue> Values { get; private set; }
    }
}