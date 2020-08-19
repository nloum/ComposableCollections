using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class DisposableQueryableReadOnlyDictionaryDecorator<TKey, TValue> : DelegateReadOnlyDictionary<TKey, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue>
    {
        private IQueryableReadOnlyDictionary<TKey, TValue> _wrapped;
        private IDisposable _disposable;

        public DisposableQueryableReadOnlyDictionaryDecorator(IQueryableReadOnlyDictionary<TKey, TValue> wrapped, IDisposable disposable) : base(wrapped)
        {
            _wrapped = wrapped;
            _disposable = disposable;
        }

        protected DisposableQueryableReadOnlyDictionaryDecorator()
        {
        }

        protected void Initialize(IQueryableReadOnlyDictionary<TKey, TValue> wrapped, IDisposable disposable)
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