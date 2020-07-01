using System;
using System.Collections;
using System.Collections.Generic;

namespace MoreCollections
{
    public class DictionaryGetOrDefault<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly Func<TKey, TValue> _defaultValue;
        private readonly bool _persist;

        public DictionaryGetOrDefault(IDictionary<TKey, TValue> dictionary, Func<TKey, TValue> defaultValue, bool persist)
        {
            _dictionary = dictionary;
            _defaultValue = defaultValue;
            _persist = persist;
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
                if (_persist)
                {
                    _dictionary[key] = _defaultValue(key);
                    value = _dictionary[key];
                    return true;
                }
                else
                {
                    value = _defaultValue(key);
                    return true;
                }
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
                if (!_dictionary.ContainsKey(key))
                {
                    if (_persist)
                    {
                        _dictionary[key] = _defaultValue(key);
                        return _dictionary[key];
                    }
                    else
                    {
                        return _defaultValue(key);
                    }
                }
                else
                {
                    return _dictionary[key];
                }
            }
            
            set => _dictionary[key] = value;
        }

        public ICollection<TKey> Keys => _dictionary.Keys;

        public ICollection<TValue> Values => _dictionary.Values;
    }
}