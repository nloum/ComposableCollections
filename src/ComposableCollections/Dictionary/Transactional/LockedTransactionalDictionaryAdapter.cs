using System.Threading;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Interfaces;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.Transactional
{
    public class LockedTransactionalDictionaryAdapter<TKey, TValue> : ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly IComposableDictionary<TKey, TValue> _source;

        public LockedTransactionalDictionaryAdapter(IComposableDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            _lock.EnterReadLock();
            return new DisposableDictionaryAdapter<TKey, TValue>(_source, new AnonymousDisposable(() => _lock.ExitReadLock()));
        }

        public IDisposableDictionary<TKey, TValue> BeginWrite()
        {
            _lock.EnterWriteLock();
            return new DisposableDictionaryAdapter<TKey, TValue>(_source, new AnonymousDisposable(() => _lock.ExitWriteLock()));
        }
    }
}