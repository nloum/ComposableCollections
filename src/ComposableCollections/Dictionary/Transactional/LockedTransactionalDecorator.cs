using System.Threading;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Interfaces;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.Transactional
{
    public class LockedTransactionalDecorator<TKey, TValue> : ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> _transactionalDictionary;

        public LockedTransactionalDecorator(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> transactionalDictionary)
        {
            _transactionalDictionary = transactionalDictionary;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            _lock.EnterReadLock();
            var result = _transactionalDictionary.BeginRead();
            return new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(result, result.DisposeWith(() => _lock.ExitReadLock()));
        }

        public IDisposableDictionary<TKey, TValue> BeginWrite()
        {
            _lock.EnterWriteLock();
            var result = _transactionalDictionary.BeginWrite();
            return new DisposableDictionaryAdapter<TKey, TValue>(result, result.DisposeWith(() => _lock.ExitWriteLock()));
        }
    }
}