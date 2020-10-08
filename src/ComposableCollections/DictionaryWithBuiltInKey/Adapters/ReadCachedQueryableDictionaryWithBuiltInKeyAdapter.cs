using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Adapters;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public class ReadCachedQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IReadCachedQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IReadCachedQueryableDictionary<TKey, TValue> _source;

        public ReadCachedQueryableDictionaryWithBuiltInKeyAdapter(IReadCachedQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
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

        public IQueryable<TValue> Values => _source.Values;
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
            throw new NotImplementedException();
        }

        public IReadCachedQueryableDictionary<TKey, TValue> AsReadCachedQueryableDictionary()
        {
            return _source;
        }
    }
}