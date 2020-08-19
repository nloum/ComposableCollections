using System;

namespace ComposableCollections.Dictionary
{
    public class AnonymousTransactionalDictionary<TKey, TValue> : ITransactionalDictionary<TKey, TValue>
    {
        private Func<IDisposableDictionary<TKey, TValue>> _beginWrite;
        private Func<IDisposableReadOnlyDictionary<TKey, TValue>> _beginRead;
        
        public AnonymousTransactionalDictionary(Func<IDisposableDictionary<TKey, TValue>> beginReadOrWrite)
        {
            _beginRead = beginReadOrWrite;
            _beginWrite = () => beginReadOrWrite();
        }

        public AnonymousTransactionalDictionary(Func<IDisposableReadOnlyDictionary<TKey, TValue>> beginRead, Func<IDisposableDictionary<TKey, TValue>> beginWrite)
        {
            _beginRead = beginRead;
            _beginWrite = beginWrite;
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