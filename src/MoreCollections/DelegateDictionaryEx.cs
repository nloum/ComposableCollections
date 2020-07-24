using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using SimpleMonads;

namespace MoreCollections
{
    public class DelegateDictionaryEx<TKey, TValue> : IDictionaryEx<TKey, TValue>
    {
        private readonly IDictionaryEx<TKey, TValue> _wrapped;

        public DelegateDictionaryEx(IDictionaryEx<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            return _wrapped.ContainsKey(key);
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            return _wrapped.TryGetValue(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _wrapped.TryGetValue(key, out value);
        }

        public AddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            return _wrapped.AddOrUpdate(key, value);
        }

        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.AddOrUpdateRange(newItems);
        }

        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.AddOrUpdateRange(newItems);
        }

        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            return _wrapped.AddOrUpdateRange(newItems, key, value);
        }

        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.AddOrUpdateRange(newItems);
        }

        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.AddOrUpdateRange(newItems);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            return _wrapped.TryAdd(key, value);
        }

        public int TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.TryAddRange(newItems);
        }

        public int TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return _wrapped.TryAddRange(newItems);
        }

        public int TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            return _wrapped.TryAddRange(newItems, key, value);
        }

        public int TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return _wrapped.TryAddRange(newItems);
        }

        public int TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
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