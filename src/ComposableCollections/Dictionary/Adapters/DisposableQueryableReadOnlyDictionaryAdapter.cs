using System;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue> : DelegateReadOnlyDictionaryBase<TKey, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableQueryableReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable, IQueryable<TValue> values) : base(source)
        {
            _disposable = disposable;
            Values = values;
        }

        protected DisposableQueryableReadOnlyDictionaryAdapter()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable,
            IQueryable<TValue> values)
        {
            _disposable = disposable;
            Values = values;
            base.Initialize(source);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public IQueryable<TValue> Values { get; private set; }
    }
}