using System;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class DisposableDictionaryAdapter<TKey, TValue> : DelegateDictionaryBase<TKey, TValue>, IDisposableDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableDictionaryAdapter(IComposableDictionary<TKey, TValue> source, IDisposable disposable) : base(source)
        {
            _disposable = disposable;
        }

        protected DisposableDictionaryAdapter()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, IDisposable disposable)
        {
            base.Initialize(source);
            _disposable = disposable;
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}