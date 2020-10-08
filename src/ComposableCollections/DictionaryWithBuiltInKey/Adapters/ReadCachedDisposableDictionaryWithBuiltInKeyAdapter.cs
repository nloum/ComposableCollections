using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Adapters;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public class ReadCachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IReadCachedDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IReadCachedDisposableDictionary<TKey, TValue> _source;

        public ReadCachedDisposableDictionaryWithBuiltInKeyAdapter(IReadCachedDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
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

        public void Dispose()
        {
            _source.Dispose();
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary()
        {
            return _source;
        }

        public IDisposableDictionary<TKey, TValue> AsDisposableDictionary()
        {
            return _source;
        }

        public IReadCachedDisposableReadOnlyDictionary<TKey, TValue> AsReadCachedDisposableReadOnlyDictionary()
        {
            return _source;
        }
    }
}