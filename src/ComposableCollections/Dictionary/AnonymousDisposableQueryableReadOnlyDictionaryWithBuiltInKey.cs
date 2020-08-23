using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : DelegateReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDisposable _disposable;

        public AnonymousDisposableQueryableReadOnlyDictionaryWithBuiltInKey(IComposableReadOnlyDictionary<TKey, TValue> wrapped, IDisposable disposable, IQueryable<TValue> values) : base(wrapped)
        {
            _disposable = disposable;
            Values = values;
        }

        protected AnonymousDisposableQueryableReadOnlyDictionaryWithBuiltInKey()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> wrapped, IDisposable disposable, IQueryable<TValue> values)
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