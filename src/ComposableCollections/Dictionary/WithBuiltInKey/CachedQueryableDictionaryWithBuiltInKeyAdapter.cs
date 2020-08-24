using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Mutations;

namespace ComposableCollections.Dictionary
{
    public class CachedQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>, ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private ICachedQueryableDictionary<TKey, TValue> _source;

        public CachedQueryableDictionaryWithBuiltInKeyAdapter(ICachedQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        protected CachedQueryableDictionaryWithBuiltInKeyAdapter()
        {
        }

        protected void Initialize(ICachedQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey)
        {
            _source = source;
            base.Initialize(source, getKey);
        }

        public ICachedDictionary<TKey, TValue> AsCachedDictionary()
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

        public ICachedQueryableDictionary<TKey, TValue> AsCachedQueryableDictionary()
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

        public new IQueryable<TValue> Values => _source.Values;
    }
}