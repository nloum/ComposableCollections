using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Transactional
{
    public class ReadOnlyDetransactionalDictionary<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IReadOnlyFactory<IDisposableReadOnlyDictionary<TKey, TValue>> _source;

        public ReadOnlyDetransactionalDictionary(IReadOnlyFactory<IDisposableReadOnlyDictionary<TKey, TValue>> dictionary)
        {
            _source = dictionary;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            var dictionary = _source.CreateReader();
            return new Enumerator<IKeyValue<TKey, TValue>>(dictionary.GetEnumerator(), dictionary);
        }

        public int Count
        {
            get
            {
                using (var dictionary = _source.CreateReader())
                {
                    return dictionary.Count;
                }
            }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                using (var dictionary = _source.CreateReader())
                {
                    return dictionary.Comparer;
                }
            }
        }
        
        public IEnumerable<TKey> Keys
        {
            get
            {
                var dictionary = _source.CreateReader();
                return new Enumerable<TKey>(() => new Enumerator<TKey>(dictionary.Keys.GetEnumerator(), dictionary));
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                var dictionary = _source.CreateReader();
                return new Enumerable<TValue>(() => new Enumerator<TValue>(dictionary.Values.GetEnumerator(), dictionary));
            }
        }

        public bool ContainsKey(TKey key)
        {
            using (var dictionary = _source.CreateReader())
            {
                return dictionary.ContainsKey(key);
            }
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            using (var dictionary = _source.CreateReader())
            {
                return dictionary.TryGetValue(key);
            }
        }

        public TValue GetValue(TKey key)
        {
            return this[key];
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            using (var dictionary = _source.CreateReader())
            {
                var result = dictionary.TryGetValue(key);
                if (result.HasValue)
                {
                    value = result.Value;
                    return true;
                }
                else
                {
                    value = default;
                    return false;
                }
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                using (var dictionary = _source.CreateReader())
                {
                    return dictionary[key];
                }
            }
        }
    }
}