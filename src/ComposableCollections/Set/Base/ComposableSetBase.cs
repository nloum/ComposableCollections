using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Set.Write;

namespace ComposableCollections.Set.Base
{
    public abstract class ComposableSetBase<TValue> : IComposableSet<TValue>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<TValue> GetEnumerator();
        public abstract int Count { get; }
        public abstract bool Contains(TValue item);

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            foreach (var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
                if (arrayIndex >= array.Length)
                {
                    break;
                }
            }
        }

        public abstract void Write(IEnumerable<SetWrite<TValue>> writes, out IReadOnlyList<SetWriteResult<TValue>> results);

        public void Add(TValue value)
        {
            Write(new[]{new SetWrite<TValue>(SetWriteType.Add, value)}, out var _);
        }

        public bool TryAdd(TValue value)
        {
            Write(new[]{new SetWrite<TValue>(SetWriteType.TryAdd, value)}, out var results);
            return results[0].Successful;
        }

        public void TryAddRange(IEnumerable<TValue> newItems, out IReadOnlySet<ISetItemAddAttempt<TValue>> results)
        {
            Write(newItems.Select(newItem => new SetWrite<TValue>(SetWriteType.TryAdd, newItem)), out var tmpResults);
            results = tmpResults
                .Select(tmpResult => (ISetItemAddAttempt<TValue>)new SetItemAddAttempt<TValue>(tmpResult.Successful, tmpResult.Value))
                .ToReadOnlySet();
        }

        public void TryAddRange(IEnumerable<TValue> newItems)
        {
            Write(newItems.Select(newItem => new SetWrite<TValue>(SetWriteType.TryAdd, newItem)), out var _);
        }

        public void TryAddRange(params TValue[] newItems)
        {
            Write(newItems.Select(newItem => new SetWrite<TValue>(SetWriteType.TryAdd, newItem)), out var _);
        }

        public void AddRange(IEnumerable<TValue> newItems)
        {
            Write(newItems.Select(newItem => new SetWrite<TValue>(SetWriteType.Add, newItem)), out var _);
        }

        public void AddRange(params TValue[] newItems)
        {
            Write(newItems.Select(newItem => new SetWrite<TValue>(SetWriteType.Add, newItem)), out var _);
        }

        public void TryRemoveRange(IEnumerable<TValue> valuesToRemove)
        {
            Write(valuesToRemove.Select(newItem => new SetWrite<TValue>(SetWriteType.TryRemove, newItem)), out var _);
        }

        public void RemoveRange(IEnumerable<TValue> valuesToRemove)
        {
            Write(valuesToRemove.Select(newItem => new SetWrite<TValue>(SetWriteType.TryRemove, newItem)), out var _);
        }

        public void RemoveWhere(Func<TValue, bool> predicate)
        {
            RemoveRange(this.Where(predicate));
        }

        public void Remove(TValue value)
        {
            Write(new[]{new SetWrite<TValue>(SetWriteType.Remove, value)}, out var results);
        }

        public bool TryRemove(TValue value)
        {
            Write(new[]{new SetWrite<TValue>(SetWriteType.TryRemove, value)}, out var results);
            return results[0].Successful;
        }

        public void TryRemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems)
        {
            Write(valuesToRemove.Select(newItem => new SetWrite<TValue>(SetWriteType.TryRemove, newItem)), out var tmpResults);
            removedItems = tmpResults
                .Where(x => x.Successful)
                .Select(tmpResult => tmpResult.Value)
                .ToReadOnlySet();
        }

        public void RemoveRange(IEnumerable<TValue> valuesToRemove, out IReadOnlySet<TValue> removedItems)
        {
            Write(valuesToRemove.Select(newItem => new SetWrite<TValue>(SetWriteType.Remove, newItem)), out var tmpResults);
            removedItems = tmpResults
                .Where(x => x.Successful)
                .Select(tmpResult => tmpResult.Value)
                .ToReadOnlySet();
        }

        public void RemoveWhere(Func<TValue, bool> predicate, out IReadOnlySet<TValue> removedItems)
        {
            Write(this.Where(predicate).Select(newItem => new SetWrite<TValue>(SetWriteType.Remove, newItem)), out var tmpResults);
            removedItems = tmpResults
                .Where(x => x.Successful)
                .Select(tmpResult => tmpResult.Value)
                .ToReadOnlySet();
        }

        public void Clear(out IReadOnlySet<TValue> removedItems)
        {
            Write(this.Select(newItem => new SetWrite<TValue>(SetWriteType.Remove, newItem)), out var tmpResults);
            removedItems = tmpResults
                .Where(x => x.Successful)
                .Select(tmpResult => tmpResult.Value)
                .ToReadOnlySet();
        }

        public void Clear()
        {
            Write(this.Select(newItem => new SetWrite<TValue>(SetWriteType.Remove, newItem)), out var tmpResults);
        }
    }
}