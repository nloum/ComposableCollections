using System;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class DisposableReadOnlyDictionaryAdapter<TKey, TValue> : DelegateReadOnlyDictionaryBase<TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable) : base(source)
        {
            _disposable = disposable;
        }

        protected DisposableReadOnlyDictionaryAdapter()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable)
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