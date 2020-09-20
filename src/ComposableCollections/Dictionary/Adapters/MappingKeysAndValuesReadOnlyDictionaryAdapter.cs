using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    public class MappingKeysAndValuesReadOnlyDictionaryAdapter<TKey1, TValue1, TKey2, TValue2> : ReadOnlyDictionaryBase<TKey2, TValue2>, IComposableReadOnlyDictionary<TKey2, TValue2>
    {
        private readonly IComposableReadOnlyDictionary<TKey1, TValue1> _innerValues;
        private Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> _convertTo2;
        private Func<TKey2, TKey1> _convertToKey1;
        private Func<TKey1, TKey2> _convertToKey2;
        
        protected MappingKeysAndValuesReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey1, TValue1> innerValues)
        {
            _innerValues = innerValues;
        }

        public MappingKeysAndValuesReadOnlyDictionaryAdapter(IComposableReadOnlyDictionary<TKey1, TValue1> innerValues, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertTo2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TKey1> convertToKey1)
        {
            _innerValues = innerValues;
            _convertTo2 = convertTo2;
            _convertToKey1 = convertToKey1;
            _convertToKey2 = convertToKey2;
        }

        protected virtual IKeyValue<TKey2, TValue2> Convert(TKey1 key, TValue1 value)
        {
            return _convertTo2(key, value);
        }

        protected virtual TKey1 ConvertToKey1(TKey2 key)
        {
            return _convertToKey1(key);
        }

        protected virtual TKey2 ConvertToKey2(TKey1 key)
        {
            return _convertToKey2(key);
        }

        public override bool ContainsKey(TKey2 key)
        {
            return _innerValues.ContainsKey(ConvertToKey1(key));
        }

        public override bool TryGetValue(TKey2 key, out TValue2 value)
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

        public override IEnumerator<IKeyValue<TKey2, TValue2>> GetEnumerator()
        {
            return _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey2> Comparer => EqualityComparer<TKey2>.Default;
        public override IEnumerable<TKey2> Keys => _innerValues.Keys.Select(ConvertToKey2);
        public override IEnumerable<TValue2> Values => _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value).Value);
    }
}