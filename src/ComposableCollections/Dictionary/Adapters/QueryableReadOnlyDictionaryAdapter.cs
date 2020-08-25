using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class QueryableReadOnlyDictionaryAdapter<TKey, TValue> : DelegateReadOnlyDictionaryBase<TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue>
    {
        public QueryableReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue> source, IQueryable<TValue> values) : base(source)
        {
            Values = values;
        }

        protected QueryableReadOnlyDictionaryAdapter()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> source, IQueryable<TValue> values)
        {
            Values = values;
            base.Initialize(source);
        }

        public new IQueryable<TValue> Values { get; private set; }
    }
}