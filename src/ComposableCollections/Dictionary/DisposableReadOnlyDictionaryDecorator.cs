using System;

namespace ComposableCollections.Dictionary
{
    public class DisposableReadOnlyDictionaryDecorator<TKey, TValue> : DelegateReadOnlyDictionary<TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableReadOnlyDictionaryDecorator(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable) : base(source)
        {
            _disposable = disposable;
        }

        protected DisposableReadOnlyDictionaryDecorator()
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