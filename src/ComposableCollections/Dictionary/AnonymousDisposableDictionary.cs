using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDisposableDictionary<TKey, TValue> : DelegateDictionary<TKey, TValue>,
        IDisposableDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public AnonymousDisposableDictionary(IComposableDictionary<TKey, TValue> source, IDisposable disposable) : base(source)
        {
            _disposable = disposable;
        }

        protected AnonymousDisposableDictionary()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, IDisposable disposable)
        {
            _disposable = disposable;
            base.Initialize(source);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}