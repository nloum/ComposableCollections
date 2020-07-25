using System;
using System.Collections.Generic;
using SimpleMonads;

namespace MoreCollections
{
    public interface IDictionaryEx<TKey, TValue> : IReadOnlyDictionaryEx<TKey, TValue>
    {
        bool TryGetValue(TKey key, out TValue value);
        new TValue this[TKey key] { get; set; }

        #region Add
         
        bool TryAdd(TKey key, TValue value);
        bool TryAdd(TKey key, Func<TValue> value);
        bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue);
        void TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> result);
        void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> result);
        void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> result);
        void TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value);
        void TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems);
        void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems);
        void Add(TKey key, TValue value);
        void AddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        void AddRange(params IKeyValuePair<TKey, TValue>[] newItems);
        void AddRange(params KeyValuePair<TKey, TValue>[] newItems);
        
        #endregion
        
        #region Update
        
        bool TryUpdate(TKey key, TValue value);
        bool TryUpdate(TKey key, TValue value, out TValue previousValue);
        bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue);
        void TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value);
        void TryUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems);
        void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        void TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> result);
        void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> result);
        void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> result);
        void Update(TKey key, TValue value);
        void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value);
        void Update(TKey key, TValue value, out TValue previousValue);
        void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void UpdateRange(params IKeyValuePair<TKey, TValue>[] newItems);
        void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        
        #endregion
        
        #region AddOrUpdate
        
        DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value);
        DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating);
        DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue);
        void AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> result);
        void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> result);
        void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> result);
        void AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems);
        void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value);
        void AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems);
        void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        
        #endregion
        
        #region Remove
        
        void TryRemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveWhere(Func<TKey, TValue, bool> predicate);
        void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate);
        void Clear();
        bool TryRemove(TKey key);
        void Remove(TKey key);
        void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems);
        void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems);
        void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryEx<TKey, TValue> removedItems);
        void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryEx<TKey, TValue> removedItems);
        void Clear(out IReadOnlyDictionaryEx<TKey, TValue> removedItems);
        bool TryRemove(TKey key, out TValue removedItem);
        void Remove(TKey key, out TValue removedItem);
        
        #endregion
    }
}