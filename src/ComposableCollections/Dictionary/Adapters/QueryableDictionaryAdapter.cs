using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class QueryableDictionaryAdapter<TKey, TValue> : DelegateDictionaryBase<TKey, TValue>, IQueryableDictionary<TKey, TValue>
    {
        public QueryableDictionaryAdapter(IComposableDictionary<TKey, TValue> source, IQueryable<TValue> values) : base(source)
        {
            Values = values;
        }

        protected QueryableDictionaryAdapter()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, IQueryable<TValue> values)
        {
            Values = values;
            base.Initialize(source);
        }

        public IQueryable<TValue> Values { get; private set; }
    }
}