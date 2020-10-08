using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Adapters;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public class ReadWriteCachedDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IReadWriteCachedDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IReadWriteCachedDictionary<TKey, TValue> _source;

        public ReadWriteCachedDictionaryWithBuiltInKeyAdapter(IReadWriteCachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
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

        public void ReloadCache()
        {
            _source.ReloadCache();
        }

        public IReadCachedReadOnlyDictionary<TKey, TValue> AsReadCachedReadOnlyDictionary()
        {
            return _source;
        }

        public IReadWriteCachedDictionary<TKey, TValue> AsReadWriteCachedDictionary()
        {
            return _source;
        }
    }
}