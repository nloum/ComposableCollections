using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadWriteCachedDisposableDictionaryAdapter<TKey, TValue> :
        ReadCachedDictionaryAdapter<TKey, TValue>, IReadWriteCachedDisposableDictionary<TKey, TValue>
    {
        private readonly IWriteCachedDisposableDictionary<TKey, TValue> _innerValues;

        public ReadWriteCachedDisposableDictionaryAdapter(IWriteCachedDisposableDictionary<TKey, TValue> innerValues) : base(innerValues)
        {
            _innerValues = innerValues;
        }

        public void Dispose()
        {
            _innerValues.Dispose();
        }

        public void FlushCache()
        {
            _innerValues.FlushCache();
        }
    }
}