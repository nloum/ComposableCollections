using System.Collections.Generic;
using System.Linq;

namespace MoreCollections
{
    /// <summary>
    /// Using an abstract Convert method, converts an IReadOnlyDictionaryEx{TKey, TInnerValue} to an
    /// IReadOnlyDictionaryEx{TKey, TValue} instance. This will lazily convert objects in the underlying innerValues.
    /// This class works whether innerValues changes underneath it or not.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInnerValue"></typeparam>
    public abstract class MapReadOnlyDictionaryBase<TKey, TValue, TInnerValue> : ReadOnlyDictionaryBaseEx<TKey, TValue> where TValue : class {
        private readonly IReadOnlyDictionaryEx<TKey, TInnerValue> _innerValues;

        protected MapReadOnlyDictionaryBase(IReadOnlyDictionaryEx<TKey, TInnerValue> innerValues)
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

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _innerValues.Select(kvp => Utility.KeyValuePair<TKey, TValue>(kvp.Key, Convert(kvp.Key, kvp.Value))).GetEnumerator();
        }

        public override int Count => _innerValues.Count;
        public override IEqualityComparer<TKey> Comparer => _innerValues.Comparer;
        public override IEnumerable<TKey> Keys => _innerValues.Keys;
        public override IEnumerable<TValue> Values => _innerValues.Select(kvp => Convert(kvp.Key, kvp.Value));
    }
}