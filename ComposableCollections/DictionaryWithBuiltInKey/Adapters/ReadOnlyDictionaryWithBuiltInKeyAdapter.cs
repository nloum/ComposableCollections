using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IComposableReadOnlyDictionary<TKey, TValue> _source;
        private readonly Func<TValue, TKey> _getKey;

        public ReadOnlyDictionaryWithBuiltInKeyAdapter(IComposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey)
        {
            _source = source;
            _getKey = getKey;
        }

        public IComposableReadOnlyDictionary<TKey, TValue> AsComposableReadOnlyDictionary()
        {
            return _source;
        }

        public TValue GetValue(TKey key)
        {
            return _source[key];
        }

        public virtual TKey GetKey(TValue value)
        {
            return _getKey(value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _source.Values.GetEnumerator();
        }

        public int Count => _source.Count;
        public IEqualityComparer<TKey> Comparer => _source.Comparer;

        public TValue this[TKey key]
        {
            get => _source[key];
        }

        public IEnumerable<TKey> Keys => _source.Keys;
        public IEnumerable<TValue> Values => _source.Values;
        public bool ContainsKey(TKey key)
        {
            return _source.ContainsKey(key);
        }

        public TValue? TryGetValue(TKey key)
        {
            return _source.TryGetValue(key);
        }
    }
}