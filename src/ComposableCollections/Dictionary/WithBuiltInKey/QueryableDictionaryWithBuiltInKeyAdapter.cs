using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class QueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IQueryableDictionary<TKey, TValue> _source;

        public QueryableDictionaryWithBuiltInKeyAdapter(IQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        protected QueryableDictionaryWithBuiltInKeyAdapter()
        {
        }

        protected void Initialize(IQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey)
        {
            _source = source;
            base.Initialize(source, getKey);
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