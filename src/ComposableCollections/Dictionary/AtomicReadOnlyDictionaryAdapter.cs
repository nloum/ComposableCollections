using System.Threading;
using ComposableCollections.Common;
using UtilityDisposables;

namespace ComposableCollections.Dictionary
{
    public class AtomicReadOnlyDictionaryAdapter<TKey, TValue> : IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly IComposableReadOnlyDictionary<TKey, TValue> _source;

        public AtomicReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            _lock.EnterReadLock();
            return new DisposableReadOnlyDictionaryDecorator<TKey, TValue>(_source, new AnonymousDisposable(() => _lock.ExitReadLock()));
        }
    }
}