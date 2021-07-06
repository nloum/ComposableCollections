using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class QueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IQueryableDictionary<TKey, TValue> _source;

        public QueryableDictionaryWithBuiltInKeyAdapter(IQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
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
    }
}