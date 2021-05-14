using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class QueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> : ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>, IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IQueryableReadOnlyDictionary<TKey, TValue> _source;

        public QueryableReadOnlyDictionaryWithBuiltInKeyAdapter(IQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        public new IQueryable<TValue> Values => _source.Values;
        public IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary()
        {
            return _source;
        }
    }
}