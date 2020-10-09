using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadCachedDisposableDictionaryAdapter<TKey, TValue> :
        ReadCachedDictionaryAdapter<TKey, TValue>, IReadCachedDisposableDictionary<TKey, TValue>
    {
        private readonly IDisposableDictionary<TKey, TValue> _innerValues;

        public ReadCachedDisposableDictionaryAdapter(IDisposableDictionary<TKey, TValue> innerValues) : base(innerValues)
        {
            _innerValues = innerValues;
        }

        public void Dispose()
        {
            _innerValues.Dispose();
        }
    }
}