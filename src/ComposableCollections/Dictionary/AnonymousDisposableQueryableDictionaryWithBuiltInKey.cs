using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> : DelegateDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDisposable _disposable;

        public AnonymousDisposableQueryableDictionaryWithBuiltInKey(IDictionaryWithBuiltInKey<TKey, TValue> wrapped, IDisposable disposable, IQueryable<TValue> values) : base(wrapped)
        {
            _disposable = disposable;
            Values = values;
        }

        protected AnonymousDisposableQueryableDictionaryWithBuiltInKey()
        {
        }

        protected void Initialize(IDictionaryWithBuiltInKey<TKey, TValue> wrapped, IDisposable disposable,
            IQueryable<TValue> values)
        {
            _disposable = disposable;
            Values = values;
            base.Initialize(wrapped);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public new IQueryable<TValue> Values { get; private set; }
    }
}