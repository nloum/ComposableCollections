using System;

namespace ComposableCollections.Dictionary
{
    public class DisposableDictionaryDecorator<TKey, TValue> : DelegateDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableDictionaryDecorator(IComposableDictionary<TKey, TValue> wrapped, IDisposable disposable) : base(wrapped)
        {
            _disposable = disposable;
        }

        protected DisposableDictionaryDecorator()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> wrapped, IDisposable disposable)
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