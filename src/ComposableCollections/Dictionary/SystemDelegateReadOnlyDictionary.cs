using System.Collections.Generic;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// Exposes a system IReadOnlyDictionary as an IComposableReadOnlyDictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SystemDelegateReadOnlyDictionary<TKey, TValue> : ReadOnlyDictionaryBase<TKey, TValue>
    {
        private IReadOnlyDictionary<TKey, TValue> _wrapped;

        public SystemDelegateReadOnlyDictionary(IReadOnlyDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return _wrapped.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _wrapped.Count;
        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys => _wrapped.Keys;
        public override IEnumerable<TValue> Values => _wrapped.Values;
    }
}