using System.Threading;
using UtilityDisposables;

namespace ComposableCollections.Dictionary
{
    public class AtomicTransactionalDecorator<TKey, TValue> : ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> _transactionalDictionary;

        public AtomicTransactionalDecorator(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> transactionalDictionary)
        {
            _transactionalDictionary = transactionalDictionary;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            _lock.EnterReadLock();
            var result = _transactionalDictionary.BeginRead();
            return new DisposableReadOnlyDictionaryDecorator<TKey, TValue>(result, result.DisposeWith(() => _lock.ExitReadLock()));
        }

        public IDisposableDictionary<TKey, TValue> BeginWrite()
        {
            _lock.EnterWriteLock();
            var result = _transactionalDictionary.BeginWrite();
            return new DisposableDictionaryDecorator<TKey, TValue>(result, result.DisposeWith(() => _lock.ExitWriteLock()));
        }
    }
}