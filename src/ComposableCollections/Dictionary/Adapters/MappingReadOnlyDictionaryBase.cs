using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters
{
    /// <summary>
    /// Using an abstract Convert method, converts an IComposableReadOnlyDictionary{TKey, TInnerValue} to an
    /// IComposableReadOnlyDictionary{TKey, TValue} instance. This will lazily convert objects in the underlying innerValues.
    /// This class works whether innerValues changes underneath it or not.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInnerValue"></typeparam>
    public abstract class MappingReadOnlyDictionaryBase<TKey, TValue, TInnerValue> : ReadOnlyDictionaryBase<TKey, TValue> {
        private readonly IComposableReadOnlyDictionary<TKey, TInnerValue> _innerValues;

        protected MappingReadOnlyDictionaryBase(IComposableReadOnlyDictionary<TKey, TInnerValue> innerValues)
        {
            _innerValues = innerValues;
        }

        protected abstract TValue Convert(TKey key, TInnerValue innerValue);

        public override bool ContainsKey(TKey key)
        {
            return _innerValues.ContainsKey(key);
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            var innerValue = _innerValues.TryGetValue(key);
            if (!innerValue.HasValue)
            {
                value = default;
                return false;
            }

            value = Convert(key, innerValue.Value);
            return true;
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _innerValues.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, Convert(kvp.Key, kvp.Value))).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey> Comparer => _innerValues.Comparer;
        public override IEnumerable<TKey> Keys => _innerValues.Keys;
        public override IEnumerable<TValue> Values => _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value));
    }
}