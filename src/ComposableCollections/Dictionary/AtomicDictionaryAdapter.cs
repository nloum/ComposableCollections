using System.Threading;
using UtilityDisposables;

namespace ComposableCollections.Dictionary
{
    public class AtomicDictionaryAdapter<TKey, TValue> : ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly IComposableDictionary<TKey, TValue> _wrapped;

        public AtomicDictionaryAdapter(IComposableDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            _lock.EnterReadLock();
            return new DisposableDictionaryDecorator<TKey, TValue>(_wrapped, new AnonymousDisposable(() => _lock.ExitReadLock()));
        }

        public IDisposableDictionary<TKey, TValue> BeginWrite()
        {
            _lock.EnterWriteLock();
            return new DisposableDictionaryDecorator<TKey, TValue>(_wrapped, new AnonymousDisposable(() => _lock.ExitWriteLock()));
        }
    }
}