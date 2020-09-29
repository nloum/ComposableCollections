using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public class DelegateDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDictionaryWithBuiltInKey<TKey, TValue> _source;

        public DelegateDictionaryWithBuiltInKey(IDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            _source = source;
        }

        protected DelegateDictionaryWithBuiltInKey()
        {
        }

        protected void Initialize(IDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            _source = source;
        }

        public IComposableReadOnlyDictionary<TKey, TValue> AsComposableReadOnlyDictionary()
        {
            return _source.AsComposableReadOnlyDictionary();
        }

        public IComposableDictionary<TKey, TValue> AsComposableDictionary()
        {
            return _source.AsComposableDictionary();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        public int Count => _source.Count;

        public IEqualityComparer<TKey> Comparer => _source.Comparer;

        public IEnumerable<TKey> Keys => _source.Keys;

        public IEnumerable<TValue> Values => _source.Values;

        public bool ContainsKey(TKey key)
        {
            return _source.ContainsKey(key);
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            return _source.TryGetValue(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _source.TryGetValue(key, out value);
        }

        public TValue GetValue(TKey key)
        {
            return this[key];
        }

        public void SetValue(TValue value)
        {
            this[GetKey(value)] = value;
        }

        public TValue this[TKey key]
        {
            get => _source[key];
            set => _source[key] = value;
        }

        public TKey GetKey(TValue value)
        {
            return _source.GetKey(value);
        }

        public bool TryAdd(TValue value)
        {
            return _source.TryAdd(value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            return _source.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            return _source.TryAdd(key, value, out existingValue, out newValue);
        }

        public void TryAddRange(IEnumerable<TValue> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _source.TryAddRange(newItems, out results);
        }

        public void TryAddRange(params TValue[] newItems)
        {
            _source.TryAddRange(newItems);
        }

        public void Add(TValue value)
        {
            _source.Add(value);
        }

        public void AddRange(IEnumerable<TValue> newItems)
        {
            _source.AddRange(newItems);
        }

        public void AddRange(params TValue[] newItems)
        {
            _source.AddRange(newItems);
        }

        public bool TryUpdate(TValue value)
        {
            return _source.TryUpdate(value);
        }

        public bool TryUpdate(TValue value, out TValue previousValue)
        {
            return _source.TryUpdate(value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            return _source.TryUpdate(key, value, out previousValue, out newValue);
        }

        public void TryUpdateRange(params TValue[] newItems)
        {
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<TValue> newItems)
        {
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.TryUpdateRange(newItems, out results);
        }

        public void Update(TValue value)
        {
            _source.Update(value);
        }

        public void Update(TValue value, out TValue previousValue)
        {
            _source.Update(value, out previousValue);
        }

        public void UpdateRange(params TValue[] newItems)
        {
            _source.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<TValue> newItems)
        {
            _source.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.UpdateRange(newItems, out results);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TValue value)
        {
            return _source.AddOrUpdate(value);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return _source.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            return _source.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
        }

        public void AddOrUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _source.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange(params TValue[] newItems)
        {
            _source.AddOrUpdateRange(newItems);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _source.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _source.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            _source.RemoveWhere(predicate);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            _source.RemoveWhere(predicate);
        }

        public void Clear()
        {
            _source.Clear();
        }

        public bool TryRemove(TKey key)
        {
            return _source.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            _source.Remove(key);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _source.TryRemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _source.RemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _source.RemoveWhere(predicate, out removedItems);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _source.RemoveWhere(predicate, out removedItems);
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _source.Clear(out removedItems);
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            return _source.TryRemove(key, out removedItem);
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            _source.Remove(key, out removedItem);
        }
    }
}