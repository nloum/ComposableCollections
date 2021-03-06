using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.Utilities;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Transactional
{
    public class DetransactionalDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        private readonly IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> _source;

        public DetransactionalDictionary(IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> dictionary)
        {
            _source = dictionary;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            var dictionary = _source.CreateReader();
            return new Enumerator<IKeyValue<TKey, TValue>>(dictionary.GetEnumerator(), dictionary);
        }

        public int Count
        {
            get
            {
                using (var dictionary = _source.CreateReader())
                {
                    return dictionary.Count;
                }
            }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                using (var dictionary = _source.CreateReader())
                {
                    return dictionary.Comparer;
                }
            }
        }
        
        public IEnumerable<TKey> Keys
        {
            get
            {
                var dictionary = _source.CreateReader();
                return new Enumerable<TKey>(() => new Enumerator<TKey>(dictionary.Keys.GetEnumerator(), dictionary));
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                var dictionary = _source.CreateReader();
                return new Enumerable<TValue>(() => new Enumerator<TValue>(dictionary.Values.GetEnumerator(), dictionary));
            }
        }

        public bool ContainsKey(TKey key)
        {
            using (var dictionary = _source.CreateReader())
            {
                return dictionary.ContainsKey(key);
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

        public TValue? TryGetValue(TKey key)
        {
            using (var dictionary = _source.CreateReader())
            {
                return dictionary.TryGetValue(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            using (var dictionary = _source.CreateReader())
            {
                var result = dictionary.TryGetValue(key);
                if ( result != null)
                {
                    value = result!;
                    return true;
                }
                else
                {
                    value = default;
                    return false;
                }
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                using (var dictionary = _source.CreateReader())
                {
                    return dictionary[key];
                }
            }
            set
            {
                using (var dictionary = _source.CreateWriter())
                {
                    dictionary[key] = value;
                }
            }
        }

        public void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.Write(writes, out results);
            }
        }

        public bool TryAdd(TKey key, TValue value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.TryAdd(key, value);
            }
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.TryAdd(key, value);
            }
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.TryAdd(key, value, out existingValue, out newValue);
            }
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
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryAddRange(newItems, out results);
            }
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryAddRange(newItems, out results);
            }
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryAddRange(newItems, key, value, out results);
            }
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryAddRange(newItems);
            }
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryAddRange(newItems);
            }
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryAddRange(newItems, key, value);
            }
        }

        public void TryAddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryAddRange(newItems);
            }
        }

        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryAddRange(newItems);
            }
        }

        public void Add(TKey key, TValue value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.Add(key, value);
            }
        }

        public void AddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddRange(newItems);
            }
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddRange(newItems);
            }
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddRange(newItems, key, value);
            }
        }

        public void AddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddRange(newItems);
            }
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddRange(newItems);
            }
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.TryUpdate(key, value);
            }
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.TryUpdate(key, value, out previousValue);
            }
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.TryUpdate(key, value, out previousValue, out newValue);
            }
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryUpdateRange(newItems);
            }
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryUpdateRange(newItems);
            }
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryUpdateRange(newItems, key, value);
            }
        }

        public void TryUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryUpdateRange(newItems);
            }
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryUpdateRange(newItems);
            }
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryUpdateRange(newItems, out results);
            }
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryUpdateRange(newItems, out results);
            }
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryUpdateRange(newItems, key, value, out results);
            }
        }

        public void Update(TKey key, TValue value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.Update(key, value);
            }
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.UpdateRange(newItems);
            }
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.UpdateRange(newItems);
            }
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.UpdateRange(newItems, key, value);
            }
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.Update(key, value, out previousValue);
            }
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.UpdateRange(newItems, out results);
            }
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.UpdateRange(newItems, out results);
            }
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.UpdateRange(newItems, key, value, out results);
            }
        }

        public void UpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.UpdateRange(newItems);
            }
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.UpdateRange(newItems);
            }
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.AddOrUpdate(key, value);
            }
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
            }
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
            }
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddOrUpdateRange(newItems, out results);
            }
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddOrUpdateRange(newItems, out results);
            }
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddOrUpdateRange(newItems, key, value, out results);
            }
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddOrUpdateRange(newItems);
            }
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddOrUpdateRange(newItems);
            }
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddOrUpdateRange(newItems, key, value);
            }
        }

        public void AddOrUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddOrUpdateRange(newItems);
            }
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.AddOrUpdateRange(newItems);
            }
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryRemoveRange(keysToRemove);
            }
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.RemoveRange(keysToRemove);
            }
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.RemoveWhere(predicate);
            }
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.RemoveWhere(predicate);
            }
        }

        public void Clear()
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.Clear();
            }
        }

        public bool TryRemove(TKey key)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.TryRemove(key);
            }
        }

        public void Remove(TKey key)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.Remove(key);
            }
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.TryRemoveRange(keysToRemove, out removedItems);
            }
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.RemoveRange(keysToRemove, out removedItems);
            }
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.RemoveWhere(predicate, out removedItems);
            }
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.RemoveWhere(predicate, out removedItems);
            }
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.Clear(out removedItems);
            }
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            using (var dictionary = _source.CreateWriter())
            {
                return dictionary.TryRemove(key, out removedItem);
            }
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            using (var dictionary = _source.CreateWriter())
            {
                dictionary.Remove(key, out removedItem);
            }
        }
    }
}