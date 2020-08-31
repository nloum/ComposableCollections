using System;
using System.Collections.Generic;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set
{
    public interface ISet<TValue> : IReadOnlySet<TValue>
    {
        void Write(IEnumerable<SetWrite<TValue>> writes, out IReadOnlyList<SetWriteResult<TValue>> results);

        #region Add
        
        void Add(TValue value);
        bool TryAdd(TValue value);
        void TryAddRange(IEnumerable<TValue> newItems, out IReadOnlySet<ISetItemAddAttempt<TValue>> results);
        void TryAddRange(params TValue[] newItems);
        void AddRange(IEnumerable<TValue> newItems);
        void AddRange(params TValue[] newItems);
        
        #endregion
        
        #region Remove
        
        void TryRemoveRange(IEnumerable<TValue> valuesToRemove);
        void RemoveRange(IEnumerable<TValue> valuesToRemove);
        void RemoveWhere(Func<TValue, bool> predicate);
        void Remove(TValue value);
        bool TryRemove(TValue value);
        void TryRemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems);
        void RemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems);
        void RemoveWhere(Func<TValue, bool> predicate, out IReadOnlySet<TValue> removedItems);
        void Clear(out IReadOnlySet<TValue> removedItems);
        void Clear();
        
        #endregion
    }
}