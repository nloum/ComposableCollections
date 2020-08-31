using System;
using System.Collections.Generic;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set
{
    public interface ISet<TValue> : ICollection<TValue>, IReadOnlySet<TValue>
    {
        #region Add
        
        bool TryAdd(TValue value);
        bool TryAdd(TValue value, out TValue existingValue, out TValue newValue);
        void TryAddRange(IEnumerable<TValue> newItems, out IReadOnlySet<ISetItemAddAttempt<TValue>> results);
        void TryAddRange(params TValue[] newItems);
        void Add(TValue value);
        void AddRange(IEnumerable<TValue> newItems);
        void AddRange(params TValue[] newItems);
        
        #endregion
        
        #region Update
        
        bool TryUpdate(TValue value);
        bool TryUpdate(TValue value, out TValue previousValue);
        void TryUpdateRange(params TValue[] newItems);
        void TryUpdateRange(IEnumerable<TValue> newItems);
        void TryUpdateRange(IEnumerable<TValue> newItems, out IReadOnlySet<ISetItemUpdateAttempt<TValue>> results);
        void Update(TValue value);
        void Update(TValue value, out TValue previousValue);
        void UpdateRange(params TValue[] newItems);
        void UpdateRange(IEnumerable<TValue> newItems);
        void UpdateRange(IEnumerable<TValue> newItems, out IReadOnlySet<ISetItemUpdateAttempt<TValue>> results);

        #endregion
        
        #region AddOrUpdate
        
        SetItemAddOrUpdateResult AddOrUpdate(TValue value);
        SetItemAddOrUpdateResult AddOrUpdate(Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating);
        SetItemAddOrUpdateResult AddOrUpdate(Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue);
        void AddOrUpdateRange(IEnumerable<TValue> newItems, out IReadOnlySet<ISetItemAddOrUpdate<TValue>> results);
        void AddOrUpdateRange(params TValue[] newItems);
        
        #endregion
        
        #region Remove
        
        // Remove methods are the same as a plain ISetEx
        
        void TryRemoveRange(IEnumerable<TValue> valuesToRemove);
        void RemoveRange(IEnumerable<TValue> keysToRemove);
        void RemoveWhere(Func<TValue, bool> predicate);
        void Clear();
        bool TryRemove(TValue value);
        void Remove(TValue value);
        void TryRemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems);
        void RemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems);
        void RemoveWhere(Func<TValue, bool> predicate, out IReadOnlySet<TValue> removedItems);
        void Clear(out IReadOnlySet<TValue> removedItems);
        
        #endregion
    }
}