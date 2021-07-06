using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set.Base
{
    public class DelegateSet<TValue> : DelegateReadOnlySet<TValue>, IComposableSet<TValue>
    {
        private readonly IComposableSet<TValue> _set;

        public DelegateSet(IComposableSet<TValue> set) : base(set)
        {
            _set = set;
        }

        public void Write(IEnumerable<SetWrite<TValue>> writes, out IReadOnlyList<SetWriteResult<TValue>> results)
        {
            _set.Write(writes, out results);
        }

        public void Add(TValue value)
        {
            _set.Add(value);
        }

        public bool TryAdd(TValue value)
        {
            return _set.TryAdd(value);
        }

        public void TryAddRange(IEnumerable<TValue> newItems, out IReadOnlySet<ISetItemAddAttempt<TValue>> results)
        {
            _set.TryAddRange(newItems, out results);
        }

        public void TryAddRange(IEnumerable<TValue> newItems)
        {
            _set.TryAddRange(newItems);
        }

        public void TryAddRange(params TValue[] newItems)
        {
            _set.TryAddRange(newItems);
        }

        public void AddRange(IEnumerable<TValue> newItems)
        {
            _set.AddRange(newItems);
        }

        public void AddRange(params TValue[] newItems)
        {
            _set.AddRange(newItems);
        }

        public void TryRemoveRange(IEnumerable<TValue> valuesToRemove)
        {
            _set.TryRemoveRange(valuesToRemove);
        }

        public void RemoveRange(IEnumerable<TValue> valuesToRemove)
        {
            _set.RemoveRange(valuesToRemove);
        }

        public void RemoveWhere(Func<TValue, bool> predicate)
        {
            _set.RemoveWhere(predicate);
        }

        public void Remove(TValue value)
        {
            _set.Remove(value);
        }

        public bool TryRemove(TValue value)
        {
            return _set.TryRemove(value);
        }

        public void TryRemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems)
        {
            _set.TryRemoveRange(valuesToRemove, out removedItems);
        }

        public void RemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems)
        {
            _set.RemoveRange(valuesToRemove, out removedItems);
        }

        public void RemoveWhere(Func<TValue, bool> predicate, out IReadOnlySet<TValue> removedItems)
        {
            _set.RemoveWhere(predicate, out removedItems);
        }

        public void Clear(out IReadOnlySet<TValue> removedItems)
        {
            _set.Clear(out removedItems);
        }

        public void Clear()
        {
            _set.Clear();
        }
    }
}