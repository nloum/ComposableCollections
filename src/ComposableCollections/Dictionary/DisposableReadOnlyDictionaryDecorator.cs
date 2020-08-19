using System;

namespace ComposableCollections.Dictionary
{
    public class DisposableReadOnlyDictionaryDecorator<TKey, TValue> : DelegateReadOnlyDictionary<TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableReadOnlyDictionaryDecorator(IComposableReadOnlyDictionary<TKey, TValue> wrapped, IDisposable disposable) : base(wrapped)
        {
            _disposable = disposable;
        }

        protected DisposableReadOnlyDictionaryDecorator()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> wrapped, IDisposable disposable)
        {
            base.Initialize(wrapped);
            _disposable = disposable;
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}