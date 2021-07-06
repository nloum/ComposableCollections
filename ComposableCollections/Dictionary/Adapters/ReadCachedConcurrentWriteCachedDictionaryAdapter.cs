using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadCachedConcurrentWriteCachedDictionaryAdapter<TKey, TValue> : ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>, IReadWriteCachedDictionary<TKey, TValue> {
        private readonly IReadCachedDictionary<TKey, TValue> _source;
        public ReadCachedConcurrentWriteCachedDictionaryAdapter(IReadCachedDictionary<TKey, TValue> source) : base(source) {
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
    }
}