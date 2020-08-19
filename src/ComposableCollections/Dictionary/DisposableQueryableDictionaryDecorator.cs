using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class DisposableQueryableDictionaryDecorator<TKey, TValue> : DelegateDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>
    {
        private IQueryableDictionary<TKey, TValue> _wrapped;
        private IDisposable _disposable;

        public DisposableQueryableDictionaryDecorator(IQueryableDictionary<TKey, TValue> wrapped, IDisposable disposable) : base(wrapped)
        {
            _wrapped = wrapped;
            _disposable = disposable;
        }

        protected DisposableQueryableDictionaryDecorator()
        {
        }

        protected void Initialize(IQueryableDictionary<TKey, TValue> wrapped, IDisposable disposable)
        {
            base.Initialize(wrapped);
            _wrapped = wrapped;
            _disposable = disposable;
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }

        public new IQueryable<TValue> Values => _wrapped.Values;
    }
}