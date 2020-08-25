using System.Threading;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Interfaces;
using UtilityDisposables;

namespace ComposableCollections.Dictionary.Transactional
{
    public class ReadLockReadOnlyTransactionalDictionaryAdapter<TKey, TValue> : IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly IComposableReadOnlyDictionary<TKey, TValue> _source;

        public ReadLockReadOnlyTransactionalDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> BeginRead()
        {
            _lock.EnterReadLock();
            return new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(_source, new AnonymousDisposable(() => _lock.ExitReadLock()));
        }
    }
}