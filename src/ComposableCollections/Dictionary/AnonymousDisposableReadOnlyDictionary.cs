using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDisposableReadOnlyDictionary<TKey, TValue> : DelegateReadOnlyDictionary<TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public AnonymousDisposableReadOnlyDictionary(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable) : base(source)
        {
            _disposable = disposable;
        }

        protected AnonymousDisposableReadOnlyDictionary()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable)
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