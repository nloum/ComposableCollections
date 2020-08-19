using System.Threading;
using UtilityDisposables;

namespace ComposableCollections.Dictionary
{
    public class AtomicReadOnlyDictionaryAdapter<TKey, TValue> : IReadOnlyTransactionalDictionary<TKey, TValue>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly IComposableReadOnlyDictionary<TKey, TValue> _wrapped;

        public AtomicReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            _lock.EnterReadLock();
            return new DisposableReadOnlyDictionaryDecorator<TKey, TValue>(_wrapped, new AnonymousDisposable(() => _lock.ExitReadLock()));
        }
    }
}