using System;
using System.Collections;
using System.Collections.Generic;
using SimpleMonads;

namespace MoreCollections
{
    public class DelegateDictionaryEx<TKey, TValue> : IDictionaryEx<TKey, TValue>
    {
        private IDictionaryEx<TKey, TValue>? _wrapped;

        public DelegateDictionaryEx(IDictionaryEx<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        protected DelegateDictionaryEx()
        {
            _wrapped = null;
        }

        protected virtual void Initialize(IDictionaryEx<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.GetEnumerator();
        }

        public int Count
        {
            get
            {
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _wrapped.Count;
            }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _wrapped.Comparer;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get {       
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _wrapped.Keys;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _wrapped.Values;
            }
        }

        public bool ContainsKey(TKey key)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.ContainsKey(key);
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryGetValue(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                return _wrapped[key];
            }
            set
            {
                if (_wrapped == null)
                {
                    throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
                }
                _wrapped[key] = value;
            }
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryAdd(key, value, out existingValue, out newValue);
        }

        public void TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryAddRange(newItems, out results);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryAddRange(newItems, out results);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryAddRange(newItems, key, value, out results);
        }

        public void TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryAddRange(newItems);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryAddRange(newItems);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryAddRange(newItems, key, value);
        }

        public void TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryAddRange(newItems);
        }

        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryAddRange(newItems);
        }

        public void Add(TKey key, TValue value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.Add(key, value);
        }

        public void AddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddRange(newItems);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddRange(newItems);
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddRange(newItems, key, value);
        }

        public void AddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddRange(newItems);
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddRange(newItems);
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryUpdate(key, value);
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryUpdate(key, value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryUpdate(key, value, out previousValue, out newValue);
        }

        public void TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryUpdateRange(newItems, key, value);
        }

        public void TryUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryUpdateRange(newItems, out results);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryUpdateRange(newItems, out results);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryUpdateRange(newItems, key, value, out results);
        }

        public void Update(TKey key, TValue value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.Update(key, value);
        }

        public void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.UpdateRange(newItems, key, value);
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.Update(key, value, out previousValue);
        }

        public void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.UpdateRange(newItems, out results);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.UpdateRange(newItems, out results);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.UpdateRange(newItems, key, value, out results);
        }

        public void UpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.UpdateRange(newItems);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.AddOrUpdate(key, value);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddOrUpdateRange(newItems, out results);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddOrUpdateRange(newItems, key, value, out results);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddOrUpdateRange(newItems, key, value);
        }

        public void AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.RemoveWhere(predicate);
        }

        public void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.RemoveWhere(predicate);
        }

        public void Clear()
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.Clear();
        }

        public bool TryRemove(TKey key)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.Remove(key);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.TryRemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.RemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.RemoveWhere(predicate, out removedItems);
        }

        public void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.RemoveWhere(predicate, out removedItems);
        }

        public void Clear(out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.Clear(out removedItems);
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            return _wrapped.TryRemove(key, out removedItem);
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            if (_wrapped == null)
            {
                throw new InvalidOperationException("Must called SetWrapped or pass the wrapped value in via constructor before performing any calls on the dictionary");
            }
            _wrapped.Remove(key, out removedItem);
        }
    }
}