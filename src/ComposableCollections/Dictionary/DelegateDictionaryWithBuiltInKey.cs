using System;
using System.Collections;
using System.Collections.Generic;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public class DelegateDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDictionaryWithBuiltInKey<TKey, TValue> _wrapped;

        public DelegateDictionaryWithBuiltInKey(IDictionaryWithBuiltInKey<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        protected DelegateDictionaryWithBuiltInKey()
        {
        }

        protected void Initialize(IDictionaryWithBuiltInKey<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.GetEnumerator();
        }

        public int Count => _wrapped.Count;

        public IEqualityComparer<TKey> Comparer => _wrapped.Comparer;

        public IEnumerable<TKey> Keys => _wrapped.Keys;

        public IEnumerable<TValue> Values => _wrapped.Values;

        public bool ContainsKey(TKey key)
        {
            return _wrapped.ContainsKey(key);
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            return _wrapped.TryGetValue(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _wrapped.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get => _wrapped[key];
            set => _wrapped[key] = value;
        }

        public TKey GetKey(TValue value)
        {
            return _wrapped.GetKey(value);
        }

        public bool TryAdd(TValue value)
        {
            return _wrapped.TryAdd(value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            return _wrapped.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            return _wrapped.TryAdd(key, value, out existingValue, out newValue);
        }

        public void TryAddRange(IEnumerable<TValue> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _wrapped.TryAddRange(newItems, out results);
        }

        public void TryAddRange(params TValue[] newItems)
        {
            _wrapped.TryAddRange(newItems);
        }

        public void Add(TValue value)
        {
            _wrapped.Add(value);
        }

        public void AddRange(IEnumerable<TValue> newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public void AddRange(params TValue[] newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public bool TryUpdate(TValue value)
        {
            return _wrapped.TryUpdate(value);
        }

        public bool TryUpdate(TValue value, out TValue previousValue)
        {
            return _wrapped.TryUpdate(value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            return _wrapped.TryUpdate(key, value, out previousValue, out newValue);
        }

        public void TryUpdateRange(params TValue[] newItems)
        {
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<TValue> newItems)
        {
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _wrapped.TryUpdateRange(newItems, out results);
        }

        public void Update(TValue value)
        {
            _wrapped.Update(value);
        }

        public void Update(TValue value, out TValue previousValue)
        {
            _wrapped.Update(value, out previousValue);
        }

        public void UpdateRange(params TValue[] newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<TValue> newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _wrapped.UpdateRange(newItems, out results);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TValue value)
        {
            return _wrapped.AddOrUpdate(value);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return _wrapped.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            return _wrapped.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
        }

        public void AddOrUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _wrapped.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange(params TValue[] newItems)
        {
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _wrapped.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _wrapped.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            _wrapped.RemoveWhere(predicate);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            _wrapped.RemoveWhere(predicate);
        }

        public void Clear()
        {
            _wrapped.Clear();
        }

        public bool TryRemove(TKey key)
        {
            return _wrapped.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            _wrapped.Remove(key);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _wrapped.TryRemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _wrapped.RemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _wrapped.RemoveWhere(predicate, out removedItems);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _wrapped.RemoveWhere(predicate, out removedItems);
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _wrapped.Clear(out removedItems);
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            return _wrapped.TryRemove(key, out removedItem);
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            _wrapped.Remove(key, out removedItem);
        }
    }
}