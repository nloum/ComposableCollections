using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Adapters;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public class ReadCachedQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IReadCachedQueryableReadOnlyDictionary<TKey, TValue> _source;

        public ReadCachedQueryableReadOnlyDictionaryWithBuiltInKeyAdapter(IReadCachedQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
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

        public new IQueryable<TValue> Values => _source.Values;
        public IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary()
        {
            return _source;
        }

        public IReadCachedQueryableReadOnlyDictionary<TKey, TValue> AsReadCachedQueryableReadOnlyDictionary()
        {
            return _source;
        }
    }
}