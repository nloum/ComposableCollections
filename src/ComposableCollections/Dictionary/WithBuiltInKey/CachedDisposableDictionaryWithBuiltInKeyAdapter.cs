using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public class CachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private ICachedDisposableDictionary<TKey, TValue> _source;

        public CachedDisposableDictionaryWithBuiltInKeyAdapter(ICachedDisposableDictionary<TKey, TValue> source,
            Func<TValue, TKey> getKey)
        {
            _source = source;
        }

        public ICachedDictionary<TKey, TValue> AsCachedDictionary()
        {
            return _source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary()
        {
            return _source;
        }

        public ICachedDisposableDictionary<TKey, TValue> AsCachedDisposableDictionary()
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

        public IEnumerable<DictionaryWrite<TKey, TValue>> GetWrites(bool clear)
        {
            return _source.GetWrites(clear);
        }

        public void Dispose()
        {
            _source.Dispose();
        }

        public IDisposableDictionary<TKey, TValue> AsDisposableDictionary()
        {
            return _source;
        }
    }
}