using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Adapters;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public class ReadCachedDisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IReadCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IReadCachedDisposableQueryableDictionary<TKey, TValue> _source;

        public ReadCachedDisposableQueryableDictionaryWithBuiltInKeyAdapter(IReadCachedDisposableQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
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

        public new IQueryable<TValue> Values => _source.Values;
        public IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary()
        {
            return _source;
        }

        public IQueryableDictionary<TKey, TValue> AsQueryableDictionary()
        {
            return _source;
        }

        public IReadCachedQueryableReadOnlyDictionary<TKey, TValue> AsReadCachedQueryableReadOnlyDictionary()
        {
            return _source;
        }

        public IReadCachedQueryableDictionary<TKey, TValue> AsReadCachedQueryableDictionary()
        {
            return _source;
        }

        public IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> AsReadCachedDisposableQueryableReadOnlyDictionary()
        {
            return _source;
        }

        public IReadCachedDisposableQueryableDictionary<TKey, TValue> AsReadCachedDisposableQueryableDictionary()
        {
            return _source;
        }
    }
}