using System;
using System.Collections.Generic;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedQueryableDictionary<TKey, TValue> : DelegateDictionary<TKey, TValue>, ICachedQueryableDictionary<TKey, TValue>
    {
        private Func<IComposableReadOnlyDictionary<TKey, TValue>> _asBypassCache;
        private Func<IComposableDictionary<TKey, TValue>> _asNeverFlush;
        private Action _flushCache;
        private Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> _getMutations;

        public AnonymousCachedQueryableDictionary(IComposableDictionary<TKey, TValue> wrapped, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> getMutations, IQueryable<TValue> values) : base(wrapped)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getMutations = getMutations;
            Values = values;
        }

        protected AnonymousCachedQueryableDictionary()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> wrapped, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> getMutations, IQueryable<TValue> values)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getMutations = getMutations;
            Values = values;
            base.Initialize(wrapped);
        }
        
        public IComposableReadOnlyDictionary<TKey, TValue> AsBypassCache()
        {
            return _asBypassCache();
        }

        public IComposableDictionary<TKey, TValue> AsNeverFlush()
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