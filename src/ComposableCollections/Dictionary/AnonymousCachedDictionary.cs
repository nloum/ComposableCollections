using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedDictionary<TKey, TValue> : DelegateDictionary<TKey, TValue>, ICachedDictionary<TKey, TValue>
    {
        private Func<IComposableReadOnlyDictionary<TKey, TValue>> _asBypassCache;
        private Func<IComposableDictionary<TKey, TValue>> _asNeverFlush;
        private Action _flushCache;
        private Func<bool, IEnumerable<DictionaryWrite<TKey, TValue>>> _getWrites;

        public AnonymousCachedDictionary(IComposableDictionary<TKey, TValue> source, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryWrite<TKey, TValue>>> getWrites) : base(source)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getWrites = getWrites;
        }

        protected AnonymousCachedDictionary()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryWrite<TKey, TValue>>> getWrites)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getWrites = getWrites;
            base.Initialize(source);
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

        public IEnumerable<DictionaryWrite<TKey, TValue>> GetWrites(bool clear)
        {
            return _getWrites(clear);
        }
    }
}