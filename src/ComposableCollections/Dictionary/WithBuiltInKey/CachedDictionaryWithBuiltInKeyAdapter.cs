using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Mutations;

namespace ComposableCollections.Dictionary
{
    public class CachedDictionaryWithBuiltInKeyAdapter<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        ICachedDictionaryWithBuiltInKey<TKey, TValue>
    {
        private ICachedDictionary<TKey, TValue> _source;

        public CachedDictionaryWithBuiltInKeyAdapter(ICachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        protected CachedDictionaryWithBuiltInKeyAdapter()
        {
        }

        protected void Initialize(ICachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey)
        {
            _source = source;
            base.Initialize(source, getKey);
        }

        public ICachedDictionary<TKey, TValue> AsCachedDictionary()
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
    }
}