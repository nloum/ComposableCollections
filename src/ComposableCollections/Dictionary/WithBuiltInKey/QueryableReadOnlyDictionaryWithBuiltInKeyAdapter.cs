using System;
using System.Linq;
using System.Reflection;

namespace ComposableCollections.Dictionary
{
    public class QueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> : ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>, IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IQueryableReadOnlyDictionary<TKey, TValue> _source;

        public QueryableReadOnlyDictionaryWithBuiltInKeyAdapter(IQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        protected QueryableReadOnlyDictionaryWithBuiltInKeyAdapter()
        {
        }

        protected void Initialize(IQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey)
        {
            _source = source;
            base.Initialize(source, getKey);
        }

        public IQueryable<TValue> Values => _source.Values;
        public IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary()
        {
            return _source;
        }
    }
}