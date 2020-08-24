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
        private IReadOnlyDictionary<TKey, TValue> _source;

        public SystemDelegateReadOnlyDictionary(IReadOnlyDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return _source.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _source.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _source.Count;
        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys => _source.Keys;
        public override IEnumerable<TValue> Values => _source.Values;
    }
}