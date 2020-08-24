using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonymousQueryableDictionary<TKey, TValue> : DelegateDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue>
    {
        public AnonymousQueryableDictionary(IComposableDictionary<TKey, TValue> source, IQueryable<TValue> values) : base(source)
        {
            Values = values;
        }

        protected AnonymousQueryableDictionary()
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