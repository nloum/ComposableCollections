using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonymousQueryableReadOnlyDictionary<TKey, TValue> : DelegateReadOnlyDictionary<TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue>
    {
        public AnonymousQueryableReadOnlyDictionary(IComposableReadOnlyDictionary<TKey, TValue> source, IQueryable<TValue> values) : base(source)
        {
            Values = values;
        }

        protected AnonymousQueryableReadOnlyDictionary()
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