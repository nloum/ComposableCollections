using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class ReadCachedDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>, IReadCachedDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IReadCachedDictionary<TKey, TValue> _source;

        public ReadCachedDictionaryWithBuiltInKeyAdapter(IReadCachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        public IReadCachedReadOnlyDictionary<TKey, TValue> AsReadCachedReadOnlyDictionary()
        {
            return _source;
        }

        public void ReloadCache()
        {
            _source.ReloadCache();
        }
    }
}