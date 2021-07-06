using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class DisposableConcurrentWriteCachedDictionaryAdapter<TKey, TValue> : ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>, IWriteCachedDisposableDictionary<TKey, TValue> {
        private readonly IDisposableDictionary<TKey, TValue> _source;
        public DisposableConcurrentWriteCachedDictionaryAdapter(IDisposableDictionary<TKey, TValue> source) : base(source) {
            _source = source;
        }

        public void Dispose()
        {
            _source.Dispose();
        }
    }
}