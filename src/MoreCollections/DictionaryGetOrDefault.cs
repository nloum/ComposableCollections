using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SimpleMonads;

namespace MoreCollections
{
    public delegate void GetDefaultValue<TKey, TValue>(TKey key, out IMaybe<TValue> maybeValue, out bool persist);
    
    public class DictionaryGetOrDefault<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly GetDefaultValue<TKey, TValue> _getDefaultValue;

        public DictionaryGetOrDefault(IDictionary<TKey, TValue> dictionary, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            _dictionary = dictionary;
            _getDefaultValue = getDefaultValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Remove(item);
        }

        public int Count => _dictionary.Count;

        public bool IsReadOnly => _dictionary.IsReadOnly;

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _getDefaultValue(key, out var maybeValue, out var persist);
                if (persist && maybeValue.HasValue)
                {
                    _dictionary[key] = maybeValue.Value;
                }

                if (maybeValue.HasValue)
                {
                    value = maybeValue.Value;
                    return true;
                }

                value = default;
                return false;
            }
            else
            {
                value = _dictionary[key];
                return true;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out var value))
                {
                    throw new KeyNotFoundException();
                }

                return value;
            }
            
            set => _dictionary[key] = value;
        }

        public ICollection<TKey> Keys => _dictionary.Keys;

        public ICollection<TValue> Values => _dictionary.Values;
    }
}