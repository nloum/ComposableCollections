using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMonads;

namespace MoreCollections
{
    public abstract class DictionaryBaseEx<TKey, TValue> : ReadOnlyDictionaryBaseEx<TKey, TValue>, IDictionaryEx<TKey, TValue>
    {
        #region Abstract methods

        public abstract bool TryAdd(TKey key, Func<TValue> value, out TValue result, out TValue previousValue);
        public abstract bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue);
        public abstract DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue);
        public abstract bool TryRemove(TKey key, out TValue removedItem);
        public abstract void RemoveRange(IEnumerable<TKey> keysToRemove,
            out IReadOnlyDictionaryEx<TKey, TValue> removedItems);

        public virtual void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value)
        {
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                var newValue = value(newItem);
        
                Add(newKey, newValue);
            }
        }

        public virtual void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems,
            Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value,
            out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> previousValues)
        {
            var finalResult = new DictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>>();
            previousValues = finalResult;
        
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                var newValue = value(newItem);
                
                Update(newKey, newValue, out var previousValue);
                finalResult[newKey] = new DictionaryItemUpdateAttempt<TValue>(true, previousValue.ToMaybe(), newValue.ToMaybe());
            }
        }
        
        #endregion
        
        #region Stuff that may need to be overridden for atomicity or performance reasons

        #region Methods that loop through parameters and call individual add/remove/update methods on each item

        public virtual void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            var finalResults = new DictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>>();
            results = finalResults;
            
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                var newValue = value(newItem);

                var added = TryAdd(newKey, () => newValue, out TValue _,out TValue existingValue);
                finalResults[newKey] = new DictionaryItemAddAttempt<TValue>(added, added ? Maybe<TValue>.Nothing() : existingValue.ToMaybe(), newValue.ToMaybe());
            }
        }

        public virtual void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            var finalResults = new DictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>>();
            results = finalResults;
            
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                //var newValue = value(newItem);

                if (TryUpdate(newKey, _ => value(newItem), out var previousValue, out var newValue))
                {
                    finalResults[newKey] = new DictionaryItemUpdateAttempt<TValue>(true, previousValue.ToMaybe(), newValue.ToMaybe());
                }
                else
                {
                    finalResults[newKey] = new DictionaryItemUpdateAttempt<TValue>(false, Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing());
                }
            }
        }

        public virtual void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            var finalResults = new DictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>>();
            results = finalResults;
            
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                var newValue = value(newItem);

                var newResult = AddOrUpdate(newKey, () => newValue, _ => newValue, out var previousValue, out var _);
                finalResults[newKey] = new DictionaryItemAddOrUpdate<TValue>(newResult, newResult == DictionaryItemAddOrUpdateResult.Update ? previousValue.ToMaybe() : Maybe<TValue>.Nothing(), newValue);
            }
        }

        public virtual void TryRemoveRange(IEnumerable<TKey> keysToRemove,
            out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            var results = new DictionaryEx<TKey, TValue>();
            removedItems = results;
            
            foreach (var key in keysToRemove)
            {
                if (TryRemove(key, out var removedItem))
                {
                    results[key] = removedItem;
                }
            }
        }

        #endregion
        
        #region Methods that throw away bulk result objects, and hence could represent opportunities to have a more optimal implementation
        
        public virtual void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            UpdateRange(newItems, key, value, out var _);
        }

        public virtual void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            TryAddRange(newItems, key, value, out var _);
        }

        public virtual void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            TryUpdateRange(newItems, key, value, out var _);
        }

        public virtual void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            AddOrUpdateRange(newItems, key, value, out var _);
        }
        
        public virtual void Clear()
        {
            Clear(out var _);
        }

        #endregion
        
        #region Methods that throw away non-bulk result objects

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            return TryAdd(key, value, out var _, out var __);
        }

        public virtual DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return AddOrUpdate(key, valueIfAdding, valueIfUpdating, out var _, out var __);
        }
        
        public virtual bool TryRemove(TKey key)
        {
            return TryRemove(key, out var _);
        }

        public virtual void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            TryRemoveRange(keysToRemove, out var _);
        }

        public virtual void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            RemoveRange(keysToRemove, out var _);
        }

        #endregion
        
        #region Methods that enumerate this class, its Keys or its Values completely, end hence represent opportunities to optimize
        
        public virtual void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            var keysToRemove = this.Where(kvp => predicate(kvp.Key, kvp.Value)).Select(kvp => kvp.Key);
            RemoveRange(keysToRemove);
        }

        public virtual void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate)
        {
            var keysToRemove = this.Where(kvp => predicate(kvp)).Select(kvp => kvp.Key);
            RemoveRange(keysToRemove);
        }

        public virtual void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            var keysToRemove = this.Where(kvp => predicate(kvp.Key, kvp.Value)).Select(kvp => kvp.Key);
            RemoveRange(keysToRemove, out removedItems);
        }

        public virtual void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            var keysToRemove = this.Where(kvp => predicate(kvp)).Select(kvp => kvp.Key);
            RemoveRange(keysToRemove, out removedItems);
        }

        public virtual void Clear(out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            RemoveRange(Keys, out removedItems);
        }

        #endregion
        
        #region Methods that probably don't need to be optimized, but it's still possible so they're still virtual

        public virtual void Remove(TKey key)
        {
            if (!TryRemove(key))
            {
                throw new KeyNotFoundException();
            }
        }
        
        public virtual void Remove(TKey key, out TValue removedItem)
        {
            if (!TryRemove(key, out removedItem))
            {
                throw new InvalidOperationException("Cannot remove item from dictionary");
            }
        }

        #endregion
        
        #endregion
        
        #region Stuff that definitely doesn't need to be overridden for atomicity or performance reasons

        public bool TryAdd(TKey key, TValue value)
        {
            return TryAdd(key, () => value, out var _, out var __);
        }

        public void Add(TKey key, TValue value)
        {
            if (!TryAdd(key, value))
            {
                throw new InvalidOperationException("Cannot add an item to the dictionary because the key already exists");
            }
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            return TryUpdate(key, _ => value, out previousValue, out var _);
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            return TryUpdate(key, _ => value, out var _, out var __);
        }

        public void Update(TKey key, TValue value)
        {
            if (!TryUpdate(key, value))
            {
                throw new KeyNotFoundException();
            }
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            if (!TryUpdate(key, value, out previousValue))
            {
                throw new KeyNotFoundException();
            }
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            return AddOrUpdate(key, () => value, _ => value);
        }
        
        public void AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            AddOrUpdateRange(newItems.AsEnumerable());
        }
        
        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            AddOrUpdateRange(newItems.AsEnumerable());
        }
        
        public void AddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            AddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            AddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            UpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            UpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void AddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            AddRange(newItems.AsEnumerable());
        }
        
        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            AddRange(newItems.AsEnumerable());
        }
        
        public void UpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            UpdateRange(newItems.AsEnumerable());
        }
        
        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            UpdateRange(newItems.AsEnumerable());
        }
        
        public void TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public new TValue this[TKey key]
        {
            get => base[key];
            set => AddOrUpdate(key, value);
        }
        
        public void TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out results);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out results);
        }

        public void TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out results);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out results);
        }

        public void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> previousValues)
        {
            UpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out previousValues);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> previousValues)
        {
            UpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out previousValues);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out results);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out results);
        }

        #endregion
    }
}