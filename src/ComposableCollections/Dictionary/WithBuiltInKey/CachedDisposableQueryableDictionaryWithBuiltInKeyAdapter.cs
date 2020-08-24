using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Mutations;

namespace ComposableCollections.Dictionary
{
    public class
        CachedDisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
            DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
            ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private ICachedDisposableQueryableDictionary<TKey, TValue> _source;

        public CachedDisposableQueryableDictionaryWithBuiltInKeyAdapter(
            ICachedDisposableQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        protected CachedDisposableQueryableDictionaryWithBuiltInKeyAdapter()
        {
        }

        protected void Initialize(ICachedDisposableQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey)
        {
            _source = source;
            base.Initialize(source, getKey);
        }

        public ICachedDictionary<TKey, TValue> AsCachedDictionary()
        {
            return _source;
        }

        public IDisposableQueryableReadOnlyDictionary<TKey, TValue> AsDisposableQueryableReadOnlyDictionary()
        {
            return _source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary()
        {
            return _source;
        }

        public IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary()
        {
            return _source;
        }

        public IQueryableDictionary<TKey, TValue> AsQueryableDictionary()
        {
            return _source;
        }

        public ICachedDisposableQueryableDictionary<TKey, TValue> AsCachedDisposableQueryableDictionary()
        {
            return _source;
        }

        public IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> AsBypassCache()
        {
            return new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(_source.AsBypassCache(), GetKey);
        }

        public IDictionaryWithBuiltInKey<TKey, TValue> AsNeverFlush()
        {
            return new DictionaryWithBuiltInKeyAdapter<TKey, TValue>(_source.AsNeverFlush(), GetKey);
        }

        public void FlushCache()
        {
            _source.FlushCache();
        }

        public IEnumerable<DictionaryMutation<TKey, TValue>> GetMutations(bool clear)
        {
            return _source.GetMutations(clear);
        }

        public void Dispose()
        {
            _source.Dispose();
        }

        public IDisposableDictionary<TKey, TValue> AsDisposableDictionary()
        {
            return _source;
        }

        public new IQueryable<TValue> Values => _source.Values;
    }
}