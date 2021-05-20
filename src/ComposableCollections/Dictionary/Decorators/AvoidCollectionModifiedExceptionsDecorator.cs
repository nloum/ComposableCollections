using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators
{
    /// <summary>
    /// This is for cases where you want to enumerate a dictionary and modify it at the same time, and you only want
    /// to enumerate the values that were in the dictionary when you *started* enumerating it. In other words, if items
    /// are added or removed during the enumeration process, you don't want those changes to affect what you're enumerating.
    /// </summary>
    /// <typeparam name="TKey">The dictionary key type</typeparam>
    /// <typeparam name="TValue">The dictionary value type</typeparam>
    public class AvoidCollectionModifiedExceptionsDecorator<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        private readonly IComposableDictionary<TKey, TValue> _innerValues;

        public AvoidCollectionModifiedExceptionsDecorator(IComposableDictionary<TKey, TValue> innerValues)
        {
            _innerValues = innerValues;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _innerValues.ToImmutableList().GetEnumerator();
        }

        public int Count => _innerValues.Count;

        public IEqualityComparer<TKey> Comparer => _innerValues.Comparer;

        public TValue GetValue(TKey key)
        {
            return _innerValues.GetValue(key);
        }

        public IEnumerable<TKey> Keys => _innerValues.Keys.ToImmutableList();

        public IEnumerable<TValue> Values => _innerValues.Values.ToImmutableList();

        public bool ContainsKey(TKey key)
        {
            return _innerValues.ContainsKey(key);
        }

        public TValue? TryGetValue(TKey key)
        {
            return _innerValues.TryGetValue(key);
        }

        public void SetValue(TKey key, TValue value)
        {
            _innerValues.SetValue(key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _innerValues.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get => _innerValues[key];
            set => _innerValues[key] = value;
        }

        public void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            _innerValues.Write(writes, out results);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            return _innerValues.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            return _innerValues.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            return _innerValues.TryAdd(key, value, out existingValue, out newValue);
        }

        public bool TryAdd(TKey key, TValue value, out TValue result, out TValue previousValue)
        {
            return TryAdd(key, () => value, out result, out previousValue);
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            return GetOrAdd(key, () => value);
        }

        public TValue GetOrAdd(TKey key, Func<TValue> value)
        {
            TryAdd(key, value, out var result, out var previousValue);
            return result;
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _innerValues.TryAddRange(newItems, out results);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _innerValues.TryAddRange(newItems, out results);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _innerValues.TryAddRange(newItems, key, value, out results);
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _innerValues.TryAddRange(newItems);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _innerValues.TryAddRange(newItems);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _innerValues.TryAddRange(newItems, key, value);
        }

        public void TryAddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _innerValues.TryAddRange(newItems);
        }

        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _innerValues.TryAddRange(newItems);
        }

        public void Add(TKey key, TValue value)
        {
            _innerValues.Add(key, value);
        }

        public void AddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _innerValues.AddRange(newItems);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _innerValues.AddRange(newItems);
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _innerValues.AddRange(newItems, key, value);
        }

        public void AddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _innerValues.AddRange(newItems);
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _innerValues.AddRange(newItems);
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            return _innerValues.TryUpdate(key, value);
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            return _innerValues.TryUpdate(key, value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            return _innerValues.TryUpdate(key, value, out previousValue, out newValue);
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _innerValues.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _innerValues.TryUpdateRange(newItems);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _innerValues.TryUpdateRange(newItems, key, value);
        }

        public void TryUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _innerValues.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _innerValues.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _innerValues.TryUpdateRange(newItems, out results);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _innerValues.TryUpdateRange(newItems, out results);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _innerValues.TryUpdateRange(newItems, key, value, out results);
        }

        public void Update(TKey key, TValue value)
        {
            _innerValues.Update(key, value);
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _innerValues.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _innerValues.UpdateRange(newItems);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _innerValues.UpdateRange(newItems, key, value);
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            _innerValues.Update(key, value, out previousValue);
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _innerValues.UpdateRange(newItems, out results);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _innerValues.UpdateRange(newItems, out results);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _innerValues.UpdateRange(newItems, key, value, out results);
        }

        public void UpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _innerValues.UpdateRange(newItems);
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _innerValues.UpdateRange(newItems);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            return _innerValues.AddOrUpdate(key, value);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return _innerValues.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            return _innerValues.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _innerValues.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _innerValues.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _innerValues.AddOrUpdateRange(newItems, key, value, out results);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _innerValues.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _innerValues.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _innerValues.AddOrUpdateRange(newItems, key, value);
        }

        public void AddOrUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _innerValues.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _innerValues.AddOrUpdateRange(newItems);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _innerValues.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _innerValues.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            _innerValues.RemoveWhere(predicate);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            _innerValues.RemoveWhere(predicate);
        }

        public void Clear()
        {
            _innerValues.Clear();
        }

        public bool TryRemove(TKey key)
        {
            return _innerValues.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            _innerValues.Remove(key);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _innerValues.TryRemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _innerValues.RemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _innerValues.RemoveWhere(predicate, out removedItems);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _innerValues.RemoveWhere(predicate, out removedItems);
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _innerValues.Clear(out removedItems);
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            return _innerValues.TryRemove(key, out removedItem);
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            _innerValues.Remove(key, out removedItem);
        }
    }
}