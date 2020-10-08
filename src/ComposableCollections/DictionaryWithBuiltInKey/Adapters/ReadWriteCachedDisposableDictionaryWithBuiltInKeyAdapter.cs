using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Adapters;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public class ReadWriteCachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IReadWriteCachedDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IReadWriteCachedDisposableDictionary<TKey, TValue> _source;

        public ReadWriteCachedDisposableDictionaryWithBuiltInKeyAdapter(IReadWriteCachedDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
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

        public IWriteCachedDictionary<TKey, TValue> AsWriteCachedDictionary()
        {
            return _source;
        }

        public void FlushCache()
        {
            _source.FlushCache();
        }

        public IWriteCachedDisposableDictionary<TKey, TValue> AsWriteCachedDisposableDictionary()
        {
            return _source;
        }

        public IReadWriteCachedDisposableDictionary<TKey, TValue> AsReadWriteCachedDisposableDictionary()
        {
            return _source;
        }
    }
}