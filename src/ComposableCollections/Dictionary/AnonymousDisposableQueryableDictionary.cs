using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDisposableQueryableDictionary<TKey, TValue> : AnonymousDisposableDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>
    {
        public AnonymousDisposableQueryableDictionary(IComposableDictionary<TKey, TValue> source, IDisposable disposable, IQueryable<TValue> values) : base(source, disposable)
        {
            Values = values;
        }

        protected AnonymousDisposableQueryableDictionary()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, IDisposable disposable,
            IQueryable<TValue> values)
        {
            Values = values;
            base.Initialize(source, disposable);
        }

        public IQueryable<TValue> Values { get; private set; }
    }
}