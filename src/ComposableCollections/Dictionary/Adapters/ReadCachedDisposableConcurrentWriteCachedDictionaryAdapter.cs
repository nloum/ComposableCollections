using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadCachedDisposableConcurrentWriteCachedDictionaryAdapter<TKey, TValue> : ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>, IReadWriteCachedDisposableDictionary<TKey, TValue> {
        private readonly IReadCachedDisposableDictionary<TKey, TValue> _source;
        public ReadCachedDisposableConcurrentWriteCachedDictionaryAdapter(IReadCachedDisposableDictionary<TKey, TValue> source) : base(source) {
            _source = source;
        }

        public void ReloadCache()
        {
            _source.ReloadCache();
        }

        public void ReloadCache(TKey key)
        {
            _source.ReloadCache(key);
        }

        public void InvalidCache()
        {
            _source.InvalidCache();
        }

        public void InvalidCache(TKey key)
        {
            _source.InvalidCache(key);
        }

        public void Dispose()
        {
            _source.Dispose();
        }
    }
}