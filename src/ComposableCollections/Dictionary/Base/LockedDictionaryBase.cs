using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Base
{
    public abstract class LockedDictionaryBase<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _source;

        public LockedDictionaryBase(IComposableDictionary<TKey, TValue> source)
        {
            _source = source;
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
                    return _source.Count;
                }
                finally
                {
                    EndRead();
                }
            }
        }

        public TValue GetValue(TKey key)
        {
            return this[key];
        }

        public void SetValue(TKey key, TValue value)
        {
            this[key] = value;
        }

        public IEqualityComparer<TKey> Comparer => _source.Comparer;

        public bool ContainsKey(TKey key)
        {
            BeginRead();
            try
            {
                return _source.ContainsKey(key);
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
                return _source.TryGetValue(key);
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
                return _source.TryGetValue(key, out value);
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
                    return _source[key];
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
                    _source[key] = value;
                }
                finally
                {
                    EndWrite();
                }
            }
        }

        public void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            BeginWrite();
            try
            {
                _source.Write(writes, out results);
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
                return _source.TryAdd(key, value);
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
                return _source.TryAdd(key, value);
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
                return _source.TryAdd(key, value, out existingValue, out newValue);
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
                _source.TryAddRange(newItems, out results);
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
                _source.TryAddRange(newItems, out results);
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
                _source.TryAddRange(newItems, key, value, out results);
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
                _source.TryAddRange(newItems);
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
                _source.TryAddRange(newItems);
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
                _source.TryAddRange(newItems, key, value);
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
                _source.TryAddRange(newItems);
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
                _source.TryAddRange(newItems);
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
                _source.Add(key, value);
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
                _source.AddRange(newItems);
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
                _source.AddRange(newItems);
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
                _source.AddRange(newItems, key, value);
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
                _source.AddRange(newItems);
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
                _source.AddRange(newItems);
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
                return _source.TryUpdate(key, value);
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
                return _source.TryUpdate(key, value, out previousValue);
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
                return _source.TryUpdate(key, value, out previousValue, out newValue);
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
                _source.TryUpdateRange(newItems);
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
                _source.TryUpdateRange(newItems);
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
                _source.TryUpdateRange(newItems, key, value);
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
                _source.TryUpdateRange(newItems);
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
                _source.TryUpdateRange(newItems);
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
                _source.TryUpdateRange(newItems, out results);
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
                _source.TryUpdateRange(newItems, out results);
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
                _source.TryUpdateRange(newItems, key, value, out results);
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
                _source.Update(key, value);
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
                _source.UpdateRange(newItems);
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
                _source.UpdateRange(newItems);
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
                _source.UpdateRange(newItems, key, value);
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
                _source.Update(key, value, out previousValue);
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
                _source.UpdateRange(newItems, out results);
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
                _source.UpdateRange(newItems, out results);
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
                _source.UpdateRange(newItems, key, value, out results);
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
                _source.UpdateRange(newItems);
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
                _source.UpdateRange(newItems);
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
                return _source.AddOrUpdate(key, value);
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
                return _source.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
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
                return _source.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
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
                _source.AddOrUpdateRange(newItems, out results);
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
                _source.AddOrUpdateRange(newItems, out results);
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
                _source.AddOrUpdateRange(newItems, key, value, out results);
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
                _source.AddOrUpdateRange(newItems);
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
                _source.AddOrUpdateRange(newItems);
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
                _source.AddOrUpdateRange(newItems, key, value);
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
                _source.AddOrUpdateRange(newItems);
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
                _source.AddOrUpdateRange(newItems);
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
                _source.TryRemoveRange(keysToRemove);
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
                _source.RemoveRange(keysToRemove);
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
                _source.RemoveWhere(predicate);
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
                _source.RemoveWhere(predicate);
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
                _source.Clear();
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
                return _source.TryRemove(key);
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
                _source.Remove(key);
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
                _source.TryRemoveRange(keysToRemove, out removedItems);
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
                _source.RemoveRange(keysToRemove, out removedItems);
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
                _source.RemoveWhere(predicate, out removedItems);
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
                _source.RemoveWhere(predicate, out removedItems);
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
                _source.Clear(out removedItems);
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
                return _source.TryRemove(key, out removedItem);
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
                _source.Remove(key, out removedItem);
            }
            finally
            {
                EndWrite();
            }
        }
    }
}