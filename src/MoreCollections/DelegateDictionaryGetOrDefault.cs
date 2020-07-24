using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using SimpleMonads;

namespace MoreCollections
{
    public class DelegateDictionaryGetOrDefault<TKey, TValue> : IDictionaryEx<TKey, TValue>
    {
        private readonly IDictionaryEx<TKey, TValue> _wrapped;
        private readonly GetDefaultValue<TKey, TValue> _getDefaultValue;

        public DelegateDictionaryGetOrDefault(IDictionaryEx<TKey, TValue> wrapped, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            _wrapped = wrapped;
            _getDefaultValue = getDefaultValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!_wrapped.TryGetValue(key, out value))
            {
                _getDefaultValue(key, out var maybeValue, out var persist);
                
                if (maybeValue.HasValue)
                {
                    if (persist)
                    {
                        _wrapped.Add(key, maybeValue.Value);
                    }

                    value = maybeValue.Value;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.GetEnumerator();
        }

        public int Count => _wrapped.Count;

        public IEqualityComparer<TKey> Comparer => _wrapped.Comparer;

        public IEnumerable<TKey> Keys => _wrapped.Keys;

        public IEnumerable<TValue> Values => _wrapped.Values;

        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                return value.ToMaybe();
            }
            
            return Maybe<TValue>.Nothing();
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            return _wrapped.TryUpdate(key, value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.TryUpdateRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.TryUpdateRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            return _wrapped.TryUpdateRange(newItems, key, value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.TryUpdateRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.TryUpdateRange(newItems);
        }

        public void Update(TKey key, TValue value)
        {
            _wrapped.Update(key, value);
        }

        public void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _wrapped.UpdateRange(newItems, key, value);
        }

        public void UpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public AddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            return _wrapped.AddOrUpdate(key, value);
        }

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.AddOrUpdateRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.AddOrUpdateRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            return _wrapped.AddOrUpdateRange(newItems, key, value);
        }

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.AddOrUpdateRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.AddOrUpdateRange(newItems);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            return _wrapped.TryAdd(key, value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.TryAddRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.TryAddRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            return _wrapped.TryAddRange(newItems, key, value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.TryAddRange(newItems);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.TryAddRange(newItems);
        }

        public void Add(TKey key, TValue value)
        {
            _wrapped.Add(key, value);
        }

        public void AddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _wrapped.AddRange(newItems, key, value);
        }

        public void AddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public int TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            return _wrapped.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _wrapped.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            _wrapped.RemoveWhere(predicate);
        }

        public void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate)
        {
            _wrapped.RemoveWhere(predicate);
        }

        public void Clear()
        {
            _wrapped.Clear();
        }

        public bool TryRemove(TKey key)
        {
            return _wrapped.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            _wrapped.Remove(key);
        }

        public TValue this[TKey key]
        {
            get => _wrapped[key];
            set => _wrapped[key] = value;
        }
    }
}