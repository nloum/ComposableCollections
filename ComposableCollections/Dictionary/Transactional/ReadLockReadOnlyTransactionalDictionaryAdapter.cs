using System.Threading;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Anonymous;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Transactional
{
    public class ReadLockReadOnlyTransactionalDictionaryAdapter<TKey, TValue> : IReadOnlyFactory<IDisposableReadOnlyDictionary<TKey, TValue>>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly IComposableReadOnlyDictionary<TKey, TValue> _source;

        public ReadLockReadOnlyTransactionalDictionaryAdapter(IComposableReadOnlyDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> CreateReader()
        {
            _lock.EnterReadLock();
            return new AnonymousDisposableReadOnlyDictionary<TKey, TValue>(_source, () => _lock.ExitReadLock());
        }
    }
}