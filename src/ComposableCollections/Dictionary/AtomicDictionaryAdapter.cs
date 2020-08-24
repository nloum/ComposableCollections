using System.Threading;
using ComposableCollections.Common;
using UtilityDisposables;

namespace ComposableCollections.Dictionary
{
    public class AtomicDictionaryAdapter<TKey, TValue> : ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly IComposableDictionary<TKey, TValue> _source;

        public AtomicDictionaryAdapter(IComposableDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            _lock.EnterReadLock();
            return new DisposableDictionaryDecorator<TKey, TValue>(_source, new AnonymousDisposable(() => _lock.ExitReadLock()));
        }

        public IDisposableDictionary<TKey, TValue> BeginWrite()
        {
            _lock.EnterWriteLock();
            return new DisposableDictionaryDecorator<TKey, TValue>(_source, new AnonymousDisposable(() => _lock.ExitWriteLock()));
        }
    }
}