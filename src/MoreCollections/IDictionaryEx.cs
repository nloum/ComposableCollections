using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MoreCollections
{
    public interface IDictionaryEx<TKey, TValue> : IReadOnlyDictionaryEx<TKey, TValue>
    {
        bool TryGetValue(TKey key, out TValue value);
        AddOrUpdateResult AddOrUpdate(TKey key, TValue value);
        ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems);
        ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        bool TryAdd(TKey key, TValue value);
        int TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        int TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        int TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        int TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems);
        int TryAddRange(params KeyValuePair<TKey, TValue>[] newItems);
        void Add(TKey key, TValue value);
        void AddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        void AddRange(params IKeyValuePair<TKey, TValue>[] newItems);
        void AddRange(params KeyValuePair<TKey, TValue>[] newItems);
        int TryRemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveWhere(Func<TKey, TValue, bool> predicate);
        void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate);
        void Clear();
        bool TryRemove(TKey key);
        void Remove(TKey key);
        new TValue this[TKey key] { get; set; }
    }
}