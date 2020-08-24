using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary
{
    public class AnonymousCachedDisposableQueryableDictionary<TKey, TValue> : DelegateDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>
    {
        private Func<IComposableReadOnlyDictionary<TKey, TValue>> _asBypassCache;
        private Func<IComposableDictionary<TKey, TValue>> _asNeverFlush;
        private Action _flushCache;
        private Func<bool, IEnumerable<DictionaryWrite<TKey, TValue>>> _getWrites;
        private IDisposable _disposable;

        public AnonymousCachedDisposableQueryableDictionary(IComposableDictionary<TKey, TValue> source, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryWrite<TKey, TValue>>> getWrites, IDisposable disposable, IQueryable<TValue> values) : base(source)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getWrites = getWrites;
            _disposable = disposable;
            Values = values;
        }

        protected AnonymousCachedDisposableQueryableDictionary()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, Func<IComposableReadOnlyDictionary<TKey, TValue>> asBypassCache, Func<IComposableDictionary<TKey, TValue>> asNeverFlush, Action flushCache, Func<bool, IEnumerable<DictionaryWrite<TKey, TValue>>> getWrites, IDisposable disposable, IQueryable<TValue> values)
        {
            _asBypassCache = asBypassCache;
            _asNeverFlush = asNeverFlush;
            _flushCache = flushCache;
            _getWrites = getWrites;
            _disposable = disposable;
            Values = values;
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

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public new IQueryable<TValue> Values { get; private set; }
    }
}