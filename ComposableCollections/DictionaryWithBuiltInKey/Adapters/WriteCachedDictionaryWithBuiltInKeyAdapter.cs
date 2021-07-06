using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class WriteCachedDictionaryWithBuiltInKeyAdapter<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IWriteCachedDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IWriteCachedDictionary<TKey, TValue> _source;

        public WriteCachedDictionaryWithBuiltInKeyAdapter(IWriteCachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        public IWriteCachedDictionary<TKey, TValue> AsWriteCachedDictionary()
        {
            return _source;
        }

        public void FlushCache()
        {
            _source.FlushCache();
        }
    }
}