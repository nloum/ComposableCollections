using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators
{
    public class DictionaryGetOrRefreshDecorator<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        private IComposableDictionary<TKey, TValue> _source;
        private RefreshValueWithOptionalPersistence<TKey, TValue> _refreshValue;

        public DictionaryGetOrRefreshDecorator(IComposableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue)
        {
            _source = source;
            _refreshValue = refreshValue;
        }

        public DictionaryGetOrRefreshDecorator(IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue)
        {
            _source = source;
            _refreshValue = (TKey key, TValue value, out TValue refreshedValue, out bool persist) =>
            {
                persist = true;
                return refreshValue(key, value, out refreshedValue);
            };
        }

        protected DictionaryGetOrRefreshDecorator()
        {
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> getDefaultValue)
        {
            _source = source;
            _refreshValue = getDefaultValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_source.TryGetValue(key, out value))
            {
                var hasValue = _refreshValue(key, value, out value, out var persist);
                
                if (hasValue)
                {
                    if (persist)
                    {
                        _source.Update(key, value);
                    }

                    return true;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                return value.ToMaybe();
            }
            
            return Maybe<TValue>.Nothing();
        }

        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }
        
        public TValue GetValue(TKey key)
        {
            return this[key];
        }

        public void SetValue(TKey key, TValue value)
        {
            this[key] = value;
        }

        public TValue this[TKey key]
        {
            get 
            {
                if (TryGetValue(key, out var value))
                {
                    return value;
                }
                
                throw new KeyNotFoundException();
            }
            set => _source[key] = value;
        }


        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        public int Count => _source.Count;

        public IEqualityComparer<TKey> Comparer => _source.Comparer;

        public IEnumerable<TKey> Keys => _source.Keys;

        public IEnumerable<TValue> Values => _source.Values;

        public bool TryAdd(TKey key, TValue value)
        {
            return _source.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            return _source.TryAdd(key, value);
        }

        public void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            _source.Write(writes, out results);
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            return _source.TryAdd(key, value, out existingValue, out newValue);
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _source.TryAddRange(newItems, out results);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _source.TryAddRange(newItems, out results);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _source.TryAddRange(newItems, key, value, out results);
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _source.TryAddRange(newItems);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _source.TryAddRange(newItems);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _source.TryAddRange(newItems, key, value);
        }

        public void TryAddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _source.TryAddRange(newItems);
        }

        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _source.TryAddRange(newItems);
        }

        public void Add(TKey key, TValue value)
        {
            _source.Add(key, value);
        }

        public void AddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _source.AddRange(newItems);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _source.AddRange(newItems);
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _source.AddRange(newItems, key, value);
        }

        public void AddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _source.AddRange(newItems);
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _source.AddRange(newItems);
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            return _source.TryUpdate(key, value);
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            return _source.TryUpdate(key, value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            return _source.TryUpdate(key, value, out previousValue, out newValue);
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _source.TryUpdateRange(newItems, key, value);
        }

        public void TryUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.TryUpdateRange(newItems, out results);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.TryUpdateRange(newItems, out results);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.TryUpdateRange(newItems, key, value, out results);
        }

        public void Update(TKey key, TValue value)
        {
            _source.Update(key, value);
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _source.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _source.UpdateRange(newItems);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _source.UpdateRange(newItems, key, value);
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            _source.Update(key, value, out previousValue);
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.UpdateRange(newItems, out results);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.UpdateRange(newItems, out results);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.UpdateRange(newItems, key, value, out results);
        }

        public void UpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _source.UpdateRange(newItems);
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _source.UpdateRange(newItems);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            return _source.AddOrUpdate(key, value);
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

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _source.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _source.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _source.AddOrUpdateRange(newItems, key, value, out results);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _source.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _source.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _source.AddOrUpdateRange(newItems, key, value);
        }

        public void AddOrUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _source.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
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

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _source.TryRemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _source.RemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _source.RemoveWhere(predicate, out removedItems);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
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