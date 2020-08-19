using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousTransactionalDictionary<TKey, TValue> : ITransactionalDictionary<TKey, TValue>
    {
        private Func<IDisposableDictionary<TKey, TValue>> _beginWrite;
        private Func<IDisposableReadOnlyDictionary<TKey, TValue>> _beginRead;

        public AnonymousTransactionalDictionary(Func<IDisposableDictionary<TKey, TValue>> beginWrite, Func<IDisposableReadOnlyDictionary<TKey, TValue>> beginRead)
        {
            _beginWrite = beginWrite;
            _beginRead = beginRead;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            return _beginRead();
        }

        public IDisposableDictionary<TKey, TValue> BeginWrite()
        {
            return _beginWrite();
        }
    }
}