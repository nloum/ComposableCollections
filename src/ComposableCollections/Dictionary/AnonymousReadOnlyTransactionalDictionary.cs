using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousReadOnlyTransactionalDictionary<TKey, TValue> : IReadOnlyTransactionalDictionary<TKey, TValue>
    {
        private Func<IDisposableReadOnlyDictionary<TKey, TValue>> _beginRead;

        public AnonymousReadOnlyTransactionalDictionary(Func<IDisposableReadOnlyDictionary<TKey, TValue>> beginRead)
        {
            _beginRead = beginRead;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            return _beginRead();
        }
    }
}