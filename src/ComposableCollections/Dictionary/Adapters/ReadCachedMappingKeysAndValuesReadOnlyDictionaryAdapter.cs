using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class
        ReadCachedMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> :
            MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>
    {
        private readonly IReadCachedReadOnlyDictionary<TSourceKey, TSourceValue> _innerValues;
        private readonly Func<TKey, TSourceKey> _convertToKey1;

        public ReadCachedMappingKeysAndValuesReadOnlyDictionaryAdapter(IReadCachedReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceValue, TValue> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(innerValues, convertTo2, convertToKey2, convertToKey1)
        {
            _innerValues = innerValues;
            _convertToKey1 = convertToKey1;
        }

        public ReadCachedMappingKeysAndValuesReadOnlyDictionaryAdapter(IReadCachedReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(innerValues, convertTo2, convertToKey2, convertToKey1)
        {
            _innerValues = innerValues;
            _convertToKey1 = convertToKey1;
        }
        
        public void ReloadCache()
        {
            _innerValues.ReloadCache();
        }

        public void ReloadCache(TKey key)
        {
            _innerValues.ReloadCache(_convertToKey1(key));
        }

        public void InvalidCache()
        {
            _innerValues.InvalidCache();
        }

        public void InvalidCache(TKey key)
        {
            _innerValues.InvalidCache(_convertToKey1(key));
        }
    }
}