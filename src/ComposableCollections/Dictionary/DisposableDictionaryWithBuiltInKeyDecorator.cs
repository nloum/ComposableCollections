using System;

namespace ComposableCollections.Dictionary
{
    public class DisposableDictionaryWithBuiltInKeyDecorator<TKey, TValue> : DelegateDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableDictionaryWithBuiltInKeyDecorator(IDictionaryWithBuiltInKey<TKey, TValue> wrapped, IDisposable disposable) : base(wrapped)
        {
            _disposable = disposable;
        }

        protected DisposableDictionaryWithBuiltInKeyDecorator()
        {
        }

        protected void Initialize(IDictionaryWithBuiltInKey<TKey, TValue> wrapped, IDisposable disposable)
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