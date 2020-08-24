using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Mutations;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedDisposableDictionary<TKey, TValue> : DelegateDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>
    {
        private Func<IComposableReadOnlyDictionary<TKey, TValue>> _asBypassCache;
        private Func<IComposableDictionary<TKey, TValue>> _asNeverFlush;
        private Action _flushCache;
        private Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> _getMutations;
        private IDisposable _disposable;

        public AnonymousCachedDisposableDictionary(IComposableDictionary<TKey, TValue> source, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> getMutations, IDisposable disposable) : base(source)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getMutations = getMutations;
            _disposable = disposable;
        }

        protected AnonymousCachedDisposableDictionary()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> wrapped, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryMutation<TKey, TValue>>> getMutations, IDisposable disposable)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getMutations = getMutations;
            _disposable = disposable;
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

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}