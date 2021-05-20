using System;
using System.Collections.Generic;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IComposableDictionary<TKey, TValue> AsComposableDictionary();
        
        bool TryGetValue(TKey key, out TValue value);
        void SetValue(TValue value);
        new TValue this[TKey key] { get; set; }

        #region Add
        
        bool TryAdd(TValue value);
        bool TryAdd(TKey key, Func<TValue> value);
        bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue);
        void TryAddRange(IEnumerable<TValue> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results);
        void TryAddRange(params TValue[] newItems);
        void Add(TValue value);
        void AddRange(IEnumerable<TValue> newItems);
        void AddRange(params TValue[] newItems);
        TValue GetOrAdd(TValue value);

        #endregion
        
        #region Update
        
        bool TryUpdate(TValue value);
        bool TryUpdate(TValue value, out TValue previousValue);
        bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue);
        void TryUpdateRange(params TValue[] newItems);
        void TryUpdateRange(IEnumerable<TValue> newItems);
        void TryUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results);
        void Update(TValue value);
        void Update(TValue value, out TValue previousValue);
        void UpdateRange(params TValue[] newItems);
        void UpdateRange(IEnumerable<TValue> newItems);
        void UpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results);

        #endregion
        
        #region AddOrUpdate
        
        DictionaryItemAddOrUpdateResult AddOrUpdate(TValue value);
        DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating);
        DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue);
        void AddOrUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemAddOrUpdate<TValue>> results);
        void AddOrUpdateRange(params TValue[] newItems);
        
        #endregion
        
        #region Remove
        
        // Remove methods are the same as a plain IDictionaryEx
        
        void TryRemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveWhere(Func<TKey, TValue, bool> predicate);
        void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate);
        void Clear();
        bool TryRemove(TKey key);
        void Remove(TKey key);
        void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems);
        void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems);
        void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems);
        void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems);
        void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems);
        bool TryRemove(TKey key, out TValue removedItem);
        void Remove(TKey key, out TValue removedItem);
        
        #endregion
    }
}