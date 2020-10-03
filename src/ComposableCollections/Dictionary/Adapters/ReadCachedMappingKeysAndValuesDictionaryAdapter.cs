using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class
        ReadCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> :
            MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>,
            IReadCachedDictionary<TKey, TValue>
    {
        private readonly IReadCachedDictionary<TSourceKey, TSourceValue> _innerValues;
        private readonly Func<TKey, TSourceKey> _convertToKey1;

        public ReadCachedMappingKeysAndValuesDictionaryAdapter(IReadCachedDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1)
        {
            _innerValues = innerValues;
            _convertToKey1 = convertToKey1;
        }

        public ReadCachedMappingKeysAndValuesDictionaryAdapter(IReadCachedDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1)
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