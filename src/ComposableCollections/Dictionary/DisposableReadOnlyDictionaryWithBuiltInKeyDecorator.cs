using System;

namespace ComposableCollections.Dictionary
{
    public class DisposableReadOnlyDictionaryWithBuiltInKeyDecorator<TKey, TValue> : DelegateReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableReadOnlyDictionaryWithBuiltInKeyDecorator(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> wrapped, IDisposable disposable) : base(wrapped)
        {
            _disposable = disposable;
        }

        protected DisposableReadOnlyDictionaryWithBuiltInKeyDecorator()
        {
        }

        protected void Initialize(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> wrapped, IDisposable disposable)
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