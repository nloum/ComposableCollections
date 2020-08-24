using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public class DelegateDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        private IComposableDictionary<TKey, TValue> _source;

        public DelegateDictionary(IComposableDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        protected DelegateDictionary()
        {
            _source = null;
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.GetEnumerator();
        }

        public int Count
        {
            get
            {
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
                }
                return _source.Count;
            }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
                }
                return _source.Comparer;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get {       
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
                }
                return _source.Keys;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
                }
                return _source.Values;
            }
        }

        public bool ContainsKey(TKey key)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.ContainsKey(key);
        }

        public void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            _source.Write(writes, out results);
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryGetValue(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
                }
                return _source[key];
            }
            set
            {
                if (_source == null)
                {
                    throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
                }
                _source[key] = value;
            }
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryAdd(key, value, out existingValue, out newValue);
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryAddRange(newItems, out results);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryAddRange(newItems, out results);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryAddRange(newItems, key, value, out results);
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryAddRange(newItems);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryAddRange(newItems);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryAddRange(newItems, key, value);
        }

        public void TryAddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryAddRange(newItems);
        }

        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryAddRange(newItems);
        }

        public void Add(TKey key, TValue value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.Add(key, value);
        }

        public void AddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddRange(newItems);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddRange(newItems);
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddRange(newItems, key, value);
        }

        public void AddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddRange(newItems);
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddRange(newItems);
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryUpdate(key, value);
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryUpdate(key, value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryUpdate(key, value, out previousValue, out newValue);
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryUpdateRange(newItems, key, value);
        }

        public void TryUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryUpdateRange(newItems, out results);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryUpdateRange(newItems, out results);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryUpdateRange(newItems, key, value, out results);
        }

        public void Update(TKey key, TValue value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.Update(key, value);
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.UpdateRange(newItems);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.UpdateRange(newItems, key, value);
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.Update(key, value, out previousValue);
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.UpdateRange(newItems, out results);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.UpdateRange(newItems, out results);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.UpdateRange(newItems, key, value, out results);
        }

        public void UpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.UpdateRange(newItems);
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.UpdateRange(newItems);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.AddOrUpdate(key, value);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddOrUpdateRange(newItems, key, value, out results);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddOrUpdateRange(newItems, key, value);
        }

        public void AddOrUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.AddOrUpdateRange(newItems);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.RemoveWhere(predicate);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.RemoveWhere(predicate);
        }

        public void Clear()
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.Clear();
        }

        public bool TryRemove(TKey key)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.Remove(key);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.TryRemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.RemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.RemoveWhere(predicate, out removedItems);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.RemoveWhere(predicate, out removedItems);
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.Clear(out removedItems);
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            return _source.TryRemove(key, out removedItem);
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            if (_source == null)
            {
                throw new InvalidOperationException("Must call SetWrapped or pass the source value in via constructor before performing any calls on the dictionary");
            }
            _source.Remove(key, out removedItem);
        }
    }
}