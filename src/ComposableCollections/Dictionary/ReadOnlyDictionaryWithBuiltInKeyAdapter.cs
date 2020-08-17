using System.Collections;
using System.Collections.Generic;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public abstract class ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IComposableReadOnlyDictionary<TKey, TValue> _wrapped;

        public ReadOnlyDictionaryWithBuiltInKeyAdapter(IComposableReadOnlyDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        protected ReadOnlyDictionaryWithBuiltInKeyAdapter()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public abstract TKey GetKey(TValue value);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.GetEnumerator();
        }

        public int Count => _wrapped.Count;
        public IEqualityComparer<TKey> Comparer => _wrapped.Comparer;

        public TValue this[TKey key]
        {
            get => _wrapped[key];
        }

        public IEnumerable<TKey> Keys => _wrapped.Keys;
        public IEnumerable<TValue> Values => _wrapped.Values;
        public bool ContainsKey(TKey key)
        {
            return _wrapped.ContainsKey(key);
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            return _wrapped.TryGetValue(key);
        }
    }
}