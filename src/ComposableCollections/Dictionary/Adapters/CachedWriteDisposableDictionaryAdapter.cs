using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Adapters
{
    public class CachedWriteDisposableDictionaryAdapter<TKey, TValue> : DelegateDictionaryBase<TKey, TValue>, ICachedWriteDisposableDictionary<TKey, TValue>
    {
        private Action _flushCache;
        private IDisposable _disposable;

        public CachedWriteDisposableDictionaryAdapter(ICachedWriteDictionary<TKey, TValue> source, IDisposable disposable) : base(source)
        {
            _flushCache = source.FlushCache;
            _disposable = disposable;
        }

        public CachedWriteDisposableDictionaryAdapter(IComposableDictionary<TKey, TValue> source, Action flushCache, IDisposable disposable) : base(source)
        {
            _flushCache = flushCache;
            _disposable = disposable;
        }

        protected CachedWriteDisposableDictionaryAdapter()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryWrite<TKey, TValue>>> getWrites, IDisposable disposable)
        {
            _flushCache = flushCache;
            _disposable = disposable;
            base.Initialize(source);
        }
        
        public void FlushCache()
        { 
            _flushCache();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}