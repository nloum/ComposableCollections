using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Adapters;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public class ReadCachedReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IReadCachedReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IReadCachedReadOnlyDictionary<TKey, TValue> _source;

        public ReadCachedReadOnlyDictionaryWithBuiltInKeyAdapter(IReadCachedReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        public void ReloadCache()
        {
            _source.ReloadCache();
        }

        public IReadCachedReadOnlyDictionary<TKey, TValue> AsReadCachedReadOnlyDictionary()
        {
            return _source;
        }
    }
}