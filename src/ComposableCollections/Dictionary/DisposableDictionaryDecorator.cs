using System;

namespace ComposableCollections.Dictionary
{
    public class DisposableDictionaryDecorator<TKey, TValue> : DelegateDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public DisposableDictionaryDecorator(IComposableDictionary<TKey, TValue> source, IDisposable disposable) : base(source)
        {
            _disposable = disposable;
        }

        protected DisposableDictionaryDecorator()
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