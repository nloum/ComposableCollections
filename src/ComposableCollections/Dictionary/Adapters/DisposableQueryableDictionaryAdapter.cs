using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class DisposableQueryableDictionaryAdapter<TKey, TValue> : DisposableDictionaryAdapter<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>
    {
        public DisposableQueryableDictionaryAdapter(IQueryableDictionary<TKey, TValue> source, IDisposable disposable) : base(source, disposable)
        {
            Values = source.Values;
        }

        public DisposableQueryableDictionaryAdapter(IComposableDictionary<TKey, TValue> source, IDisposable disposable, IQueryable<TValue> values) : base(source, disposable)
        {
            Values = values;
        }

        protected DisposableQueryableDictionaryAdapter()
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