using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public class AnonymousDisposableQueryableReadOnlyDictionary<TKey, TValue> : DelegateReadOnlyDictionary<TKey, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue>
    {
        private IDisposable _disposable;

        public AnonymousDisposableQueryableReadOnlyDictionary(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable, IQueryable<TValue> values) : base(source)
        {
            _disposable = disposable;
            Values = values;
        }

        protected AnonymousDisposableQueryableReadOnlyDictionary()
        {
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> source, IDisposable disposable,
            IQueryable<TValue> values)
        {
            _disposable = disposable;
            Values = values;
            base.Initialize(source);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public IQueryable<TValue> Values { get; private set; }
    }
}