using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleMonads;

namespace MoreCollections
{
    public abstract class DictionaryWithBuiltInKeyAdapter<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDictionaryEx<TKey, TValue> _wrapped;

        public DictionaryWithBuiltInKeyAdapter(IDictionaryEx<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        protected DictionaryWithBuiltInKeyAdapter()
        {
        }

        protected void Initialize(IDictionaryEx<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public abstract TKey GetKey(TValue value);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.GetEnumerator();
        }

        public int Count => _wrapped.Count;
        public IEqualityComparer<TKey> Comparer => _wrapped.Comparer;

        public TValue this[TKey key]
        {
            get => _wrapped[key];
            set => _wrapped[key] = value;
        }

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

        public bool TryAdd(TValue value)
        {
            return _wrapped.TryAdd(GetKey(value), value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            return _wrapped.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            return _wrapped.TryAdd(key, value, out existingValue, out newValue);
        }

        public void TryAddRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _wrapped.TryAddRange(newItems, GetKey, x => x, out results);
        }

        public void TryAddRange(params TValue[] newItems)
        {
            _wrapped.TryAddRange(newItems.AsEnumerable(), GetKey, x => x);
        }

        public void Add(TValue value)
        {
            _wrapped.Add(GetKey(value), value);
        }

        public void AddRange(IEnumerable<TValue> newItems)
        {
            _wrapped.AddRange(newItems, GetKey, x => x);
        }

        public void AddRange(params TValue[] newItems)
        {
            _wrapped.AddRange(newItems.AsEnumerable(), GetKey, x => x);
        }

        public bool TryUpdate(TValue value)
        {
            return _wrapped.TryUpdate(GetKey(value), value);
        }

        public bool TryUpdate(TValue value, out TValue previousValue)
        {
            return _wrapped.TryUpdate(GetKey(value), value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            return _wrapped.TryUpdate(key, value, out previousValue, out newValue);
        }

        public void TryUpdateRange(params TValue[] newItems)
        {
            _wrapped.TryUpdateRange(newItems, GetKey, x => x);
        }

        public void TryUpdateRange(IEnumerable<TValue> newItems)
        {
            _wrapped.TryUpdateRange(newItems, GetKey, x => x);
        }

        public void TryUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _wrapped.TryUpdateRange(newItems, GetKey, x => x, out var resultsInner);
            results = new DelegateReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>>(resultsInner);
        }

        public void Update(TValue value)
        {
            _wrapped.Update(GetKey(value), value);
        }

        public void Update(TValue value, out TValue previousValue)
        {
            _wrapped.Update(GetKey(value), value, out previousValue);
        }

        public void UpdateRange(params TValue[] newItems)
        {
            _wrapped.UpdateRange(newItems, GetKey, x => x);
        }

        public void UpdateRange(IEnumerable<TValue> newItems)
        {
            _wrapped.UpdateRange(newItems, GetKey, x => x);
        }

        public void UpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _wrapped.UpdateRange(newItems, GetKey, x => x, out var innerResults);
            results = new DelegateReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>>(innerResults);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TValue value)
        {
            return _wrapped.AddOrUpdate(GetKey(value), value);
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
            _wrapped.AddOrUpdateRange(newItems, GetKey, x => x, out var innerResult);
            results = new DelegateReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemAddOrUpdate<TValue>>(innerResult);
        }

        public void AddOrUpdateRange(params TValue[] newItems)
        {
            _wrapped.AddOrUpdateRange(newItems, GetKey, x => x);
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

        public void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate)
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
            _wrapped.TryRemoveRange(keysToRemove, out var innerRemovedItems);
            removedItems = new DelegateReadOnlyDictionaryWithBuiltInKey<TKey, TValue>(innerRemovedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _wrapped.RemoveRange(keysToRemove, out var innerRemovedItems);
            removedItems = new DelegateReadOnlyDictionaryWithBuiltInKey<TKey, TValue>(innerRemovedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _wrapped.RemoveWhere(predicate, out var innerRemovedItems);
            removedItems = new DelegateReadOnlyDictionaryWithBuiltInKey<TKey, TValue>(innerRemovedItems);
        }

        public void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _wrapped.RemoveWhere(predicate, out var innerRemovedItems);
            removedItems = new DelegateReadOnlyDictionaryWithBuiltInKey<TKey, TValue>(innerRemovedItems);
        }

        public void Clear(out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
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