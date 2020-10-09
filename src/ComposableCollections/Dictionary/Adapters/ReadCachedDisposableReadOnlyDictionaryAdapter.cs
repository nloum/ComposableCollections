using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class ReadCachedDisposableReadOnlyDictionaryAdapter<TKey, TValue> :
        ReadCachedReadOnlyDictionaryAdapter<TKey, TValue>, IReadCachedDisposableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IDisposableReadOnlyDictionary<TKey, TValue> _innerValues;

        public ReadCachedDisposableReadOnlyDictionaryAdapter(IDisposableReadOnlyDictionary<TKey, TValue> innerValues) : base(innerValues)
        {
            _innerValues = innerValues;
        }

        public void Dispose()
        {
            _innerValues.Dispose();
        }
    }
}