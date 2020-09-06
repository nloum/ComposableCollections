using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Interfaces
{
    /// <summary>
    /// An IDictionary equivalent that has lots of extension methods that let you add facades to it for various additional
    /// behaviors. 
    /// </summary>
    public interface IComposableDictionary<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        void SetValue(TKey key, TValue value);
        bool TryGetValue(TKey key, out TValue value);
        new TValue this[TKey key] { get; set; }
        void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results);
        
        #region Add
        
        bool TryAdd(TKey key, TValue value);
        bool TryAdd(TKey key, Func<TValue> value);
        bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue);
        void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results);
        void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results);
        void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results);
        void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems);
        void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value);
        void TryAddRange(params IKeyValue<TKey, TValue>[] newItems);
        void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems);
        void Add(TKey key, TValue value);
        void AddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems);
        void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value);
        void AddRange(params IKeyValue<TKey, TValue>[] newItems);
        void AddRange(params KeyValuePair<TKey, TValue>[] newItems);
        
        #endregion
        
        #region Update
        
        bool TryUpdate(TKey key, TValue value);
        bool TryUpdate(TKey key, TValue value, out TValue previousValue);
        bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue);
        void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems);
        void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value);
        void TryUpdateRange(params IKeyValue<TKey, TValue>[] newItems);
        void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void Update(TKey key, TValue value);
        void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems);
        void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value);
        void Update(TKey key, TValue value, out TValue previousValue);
        void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void UpdateRange(params IKeyValue<TKey, TValue>[] newItems);
        void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        
        #endregion
        
        #region AddOrUpdate
        
        DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value);
        DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating);
        DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue);
        void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results);
        void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results);
        void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results);
        void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems);
        void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems);
        void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value);
        void AddOrUpdateRange(params IKeyValue<TKey, TValue>[] newItems);
        void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems);
        
        #endregion
        
        #region Remove
        
        void TryRemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveWhere(Func<TKey, TValue, bool> predicate);
        void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate);
        void Clear();
        bool TryRemove(TKey key);
        void Remove(TKey key);
        void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems);
        void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems);
        void RemoveWhere(Func<TKey, TValue, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems);
        void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems);
        void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems);
        bool TryRemove(TKey key, out TValue removedItem);
        void Remove(TKey key, out TValue removedItem);
        
        #endregion
    }
}