using System.Collections.Generic;
using System.Linq;

namespace MoreCollections
{
    public class DelegateReadOnlyDictionaryEx<TKey, TValue> : ReadOnlyDictionaryBaseEx<TKey, TValue>
    {
        private IReadOnlyDictionaryEx<TKey, TValue> _wrapped;

        public DelegateReadOnlyDictionaryEx(IReadOnlyDictionaryEx<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            var result = _wrapped.TryGetValue(key);
            value = result.ValueOrDefault;
            return result.HasValue;
        }

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.Select(kvp => Utility.KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _wrapped.Count;
        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys => _wrapped.Keys;
        public override IEnumerable<TValue> Values => _wrapped.Values;
    }
}