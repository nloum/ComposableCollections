using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class DisposableQueryableDictionaryDecorator<TKey, TValue> : DelegateDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>
    {
        private IQueryableDictionary<TKey, TValue> _source;
        private IDisposable _disposable;

        public DisposableQueryableDictionaryDecorator(IQueryableDictionary<TKey, TValue> source, IDisposable disposable) : base(source)
        {
            _source = source;
            _disposable = disposable;
        }

        protected DisposableQueryableDictionaryDecorator()
        {
        }

        protected void Initialize(IQueryableDictionary<TKey, TValue> wrapped, IDisposable disposable)
        {
            base.Initialize(wrapped);
            _source = wrapped;
            _disposable = disposable;
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
        }

        public new IQueryable<TValue> Values => _source.Values;
    }
}