using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Mutations;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public abstract class LockedDictionaryBase<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _wrapped;

        public LockedDictionaryBase(IComposableDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected abstract void BeginWrite();
        protected abstract void EndWrite();

        protected abstract void BeginRead();
        protected abstract void EndRead();

        public abstract IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator();

        public abstract IEnumerable<TKey> Keys { get; }

        public abstract IEnumerable<TValue> Values { get; }

        public int Count
        {
            get
            {
                BeginRead();
                try
                {
                    return _wrapped.Count;
                }
                finally
                {
                    EndRead();
                }
            }
        }

        public IEqualityComparer<TKey> Comparer => _wrapped.Comparer;

        public bool ContainsKey(TKey key)
        {
            BeginRead();
            try
            {
                return _wrapped.ContainsKey(key);
            }
            finally
            {
                EndRead();
            }
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            BeginRead();
            try
            {
                return _wrapped.TryGetValue(key);
            }
            finally
            {
                EndRead();
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            BeginRead();
            try
            {
                return _wrapped.TryGetValue(key, out value);
            }
            finally
            {
                EndRead();
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                BeginRead();
                try
                {
                    return _wrapped[key];
                }
                finally
                {
                    EndRead();
                }
            }
            set
            {
                BeginWrite();
                try
                {
                    _wrapped[key] = value;
                }
                finally
                {
                    EndWrite();
                }
            }
        }

        public void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.Mutate(mutations, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public bool TryAdd(TKey key, TValue value)
        {
            BeginWrite();
            try
            {
                return _wrapped.TryAdd(key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            BeginWrite();
            try
            {
                return _wrapped.TryAdd(key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            BeginWrite();
            try
            {
                return _wrapped.TryAdd(key, value, out existingValue, out newValue);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.TryAddRange(newItems, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.TryAddRange(newItems, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.TryAddRange(newItems, key, value, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryAddRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryAddRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            BeginWrite();
            try
            {
                _wrapped.TryAddRange(newItems, key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryAddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryAddRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryAddRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void Add(TKey key, TValue value)
        {
            BeginWrite();
            try
            {
                _wrapped.Add(key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.AddRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.AddRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            BeginWrite();
            try
            {
                _wrapped.AddRange(newItems, key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.AddRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.AddRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            BeginWrite();
            try
            {
                return _wrapped.TryUpdate(key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            BeginWrite();
            try
            {
                return _wrapped.TryUpdate(key, value, out previousValue);
            }
            finally
            {
                EndWrite();
            }
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            BeginWrite();
            try
            {
                return _wrapped.TryUpdate(key, value, out previousValue, out newValue);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryUpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryUpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            BeginWrite();
            try
            {
                _wrapped.TryUpdateRange(newItems, key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryUpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryUpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.TryUpdateRange(newItems, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.TryUpdateRange(newItems, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.TryUpdateRange(newItems, key, value, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void Update(TKey key, TValue value)
        {
            BeginWrite();
            try
            {
                _wrapped.Update(key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.UpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.UpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            BeginWrite();
            try
            {
                _wrapped.UpdateRange(newItems, key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            BeginWrite();
            try
            {
                _wrapped.Update(key, value, out previousValue);
            }
            finally
            {
                EndWrite();
            }
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.UpdateRange(newItems, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.UpdateRange(newItems, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.UpdateRange(newItems, key, value, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void UpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.UpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.UpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            BeginWrite();
            try
            {
                return _wrapped.AddOrUpdate(key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            BeginWrite();
            try
            {
                return _wrapped.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
            }
            finally
            {
                EndWrite();
            }
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            BeginWrite();
            try
            {
                return _wrapped.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.AddOrUpdateRange(newItems, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.AddOrUpdateRange(newItems, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            BeginWrite();
            try
            {
                _wrapped.AddOrUpdateRange(newItems, key, value, out results);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.AddOrUpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.AddOrUpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            BeginWrite();
            try
            {
                _wrapped.AddOrUpdateRange(newItems, key, value);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddOrUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.AddOrUpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            BeginWrite();
            try
            {
                _wrapped.AddOrUpdateRange(newItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            BeginWrite();
            try
            {
                _wrapped.TryRemoveRange(keysToRemove);
            }
            finally
            {
                EndWrite();
            }
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            BeginWrite();
            try
            {
                _wrapped.RemoveRange(keysToRemove);
            }
            finally
            {
                EndWrite();
            }
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            BeginWrite();
            try
            {
                _wrapped.RemoveWhere(predicate);
            }
            finally
            {
                EndWrite();
            }
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            BeginWrite();
            try
            {
                _wrapped.RemoveWhere(predicate);
            }
            finally
            {
                EndWrite();
            }
        }

        public void Clear()
        {
            BeginWrite();
            try
            {
                _wrapped.Clear();
            }
            finally
            {
                EndWrite();
            }
        }

        public bool TryRemove(TKey key)
        {
            BeginWrite();
            try
            {
                return _wrapped.TryRemove(key);
            }
            finally
            {
                EndWrite();
            }
        }

        public void Remove(TKey key)
        {
            BeginWrite();
            try
            {
                _wrapped.Remove(key);
            }
            finally
            {
                EndWrite();
            }
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            BeginWrite();
            try
            {
                _wrapped.TryRemoveRange(keysToRemove, out removedItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            BeginWrite();
            try
            {
                _wrapped.RemoveRange(keysToRemove, out removedItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            BeginWrite();
            try
            {
                _wrapped.RemoveWhere(predicate, out removedItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            BeginWrite();
            try
            {
                _wrapped.RemoveWhere(predicate, out removedItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            BeginWrite();
            try
            {
                _wrapped.Clear(out removedItems);
            }
            finally
            {
                EndWrite();
            }
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            BeginWrite();
            try
            {
                return _wrapped.TryRemove(key, out removedItem);
            }
            finally
            {
                EndWrite();
            }
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            BeginWrite();
            try
            {
                _wrapped.Remove(key, out removedItem);
            }
            finally
            {
                EndWrite();
            }
        }
    }
}