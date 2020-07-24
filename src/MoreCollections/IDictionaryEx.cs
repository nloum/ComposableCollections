using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MoreCollections
{
    public interface IDictionaryEx<TKey, TValue> : IReadOnlyDictionaryEx<TKey, TValue>
    {
        bool TryGetValue(TKey key, out TValue value);
        AddOrUpdateResult AddOrUpdate(TKey key, TValue value);
        IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems);
        IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        bool TryAdd(TKey key, TValue value);
        IReadOnlyDictionaryEx<TKey, bool> TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        IReadOnlyDictionaryEx<TKey, bool> TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        IReadOnlyDictionaryEx<TKey, bool> TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        IReadOnlyDictionaryEx<TKey, bool> TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems);
        IReadOnlyDictionaryEx<TKey, bool> TryAddRange(params KeyValuePair<TKey, TValue>[] newItems);
        bool TryUpdate(TKey key, TValue value);
        IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems);
        IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        void Add(TKey key, TValue value);
        void AddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        void AddRange(params IKeyValuePair<TKey, TValue>[] newItems);
        void AddRange(params KeyValuePair<TKey, TValue>[] newItems);
        void Update(TKey key, TValue value);
        void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        void UpdateRange(params IKeyValuePair<TKey, TValue>[] newItems);
        void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
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