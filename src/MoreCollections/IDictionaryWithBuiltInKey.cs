using System;
using System.Collections.Generic;
using SimpleMonads;

namespace MoreCollections
{
    public interface IDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        bool TryGetValue(TKey key, out TValue value);
        new TValue this[TKey key] { get; set; }
        TKey GetKey(TValue value);

        #region Add
        
        bool TryAdd(TValue value);
        bool TryAdd(TKey key, Func<TValue> value);
        bool TryAdd(TKey key, Func<TValue> value, out TValue result);
        void TryAddRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryEx<TKey, bool> result);
        void TryAddRange(params TValue[] newItems);
        void Add(TValue value);
        void AddRange(TValue newItems);
        void AddRange(params TValue[] newItems);
        
        #endregion
        
        #region Update
        
        bool TryUpdate(TValue value);
        bool TryUpdate(TValue value, out TValue previousValue);
        bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue);
        void TryUpdateRange(params TValue[] newItems);
        void TryUpdateRange(IEnumerable<TValue> newItems);
        void TryUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> prviousValues);
        void Update(TValue value);
        void Update(TValue value, out TValue previousValue);
        void UpdateRange(params TValue[] newItems);
        void UpdateRange(IEnumerable<TValue> newItems);
        void UpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> previousValues);

        #endregion
        
        #region AddOrUpdate
        
        IMaybe<TValue> AddOrUpdate(TValue value);
        IMaybe<TValue> AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating);
        IMaybe<TValue> AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue);
        void AddOrUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IMaybe<TValue>> result);
        void AddOrUpdateRange(params TValue[] newItems);
        
        #endregion
        
        #region Remove
        
        // Remove methods are the same as a plain IDictionaryEx
        
        void TryRemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveRange(IEnumerable<TKey> keysToRemove);
        void RemoveWhere(Func<TKey, TValue, bool> predicate);
        void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate);
        void Clear();
        bool TryRemove(TKey key);
        void Remove(TKey key);
        void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems);
        void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems);
        void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems);
        void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems);
        void Clear(out IReadOnlyDictionaryEx<TKey, TValue> removedItems);
        bool TryRemove(TKey key, out TValue removedItem);
        void Remove(TKey key, out TValue removedItem);
        
        #endregion
    }
}