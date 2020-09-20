using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : ReadOnlyDictionaryBase<TKey, TValue>, IComposableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IComposableReadOnlyDictionary<TSourceKey, TSourceValue> _innerValues;
        private Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> _convertTo2;
        private Func<TKey, TSourceKey> _convertToKey1;
        private Func<TSourceKey, TKey> _convertToKey2;
        
        protected MappingKeysAndValuesReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues)
        {
            _innerValues = innerValues;
        }

        public MappingKeysAndValuesReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1)
        {
            _innerValues = innerValues;
            _convertTo2 = convertTo2;
            _convertToKey1 = convertToKey1;
            _convertToKey2 = convertToKey2;
        }

        protected virtual IKeyValue<TKey, TValue> Convert(TSourceKey key, TSourceValue value)
        {
            return _convertTo2(key, value);
        }

        protected virtual TSourceKey ConvertToKey1(TKey key)
        {
            return _convertToKey1(key);
        }

        protected virtual TKey ConvertToKey2(TSourceKey key)
        {
            return _convertToKey2(key);
        }

        public override bool ContainsKey(TKey key)
        {
            return _innerValues.ContainsKey(ConvertToKey1(key));
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            var convertedKey = ConvertToKey1(key);
            var innerValue = _innerValues.TryGetValue(convertedKey);
            if (!innerValue.HasValue)
            {
                value = default;
                return false;
            }

            var kvp = Convert(convertedKey, innerValue.Value);
            value = kvp.Value;
            return true;
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys => _innerValues.Keys.Select(ConvertToKey2);
        public override IEnumerable<TValue> Values => _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value).Value);
    }
}