using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Exceptions;

namespace ComposableCollections.Dictionary
{
    public abstract class DictionaryBase<TKey, TValue> : ReadOnlyDictionaryBase<TKey, TValue>, IComposableDictionary<TKey, TValue>
    {
        public abstract void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations,
            out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results);
        
        #region Stuff that may need to be overridden for atomicity or performance reasons

        #region Individual mutation methods that call bulk mutation methods

        public virtual bool TryAdd(TKey key, Func<TValue> value, out TValue result, out TValue previousValue)
        {
            Mutate(new[] { DictionaryMutation<TKey, TValue>.CreateTryAdd(key, value) }, out var results);
            var firstResult = results.First();
            result = firstResult.Add.Value.NewValue.ValueOrDefault;
            previousValue = firstResult.Add.Value.ExistingValue.ValueOrDefault;
            return firstResult.Add.Value.NewValue.HasValue;
        }

        public virtual bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            Mutate(new[] {DictionaryMutation<TKey, TValue>.CreateTryUpdate(key, value)}, out var results);
            var firstResult = results.First();
            newValue = firstResult.Update.Value.NewValue.ValueOrDefault;
            previousValue = firstResult.Update.Value.ExistingValue.ValueOrDefault;
            return firstResult.Update.Value.NewValue.HasValue;
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding,
            Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue)
        {
            Mutate(new[] {DictionaryMutation<TKey, TValue>.CreateAddOrUpdate(key, valueIfAdding, valueIfUpdating)}, out var results);
            var firstResult = results.First();
            newValue = firstResult.AddOrUpdate.Value.NewValue;
            previousValue = firstResult.AddOrUpdate.Value.ExistingValue.ValueOrDefault;
            return firstResult.AddOrUpdate.Value.Result;
        }

        public virtual bool TryRemove(TKey key, out TValue removedItem)
        {
            Mutate(new[] {DictionaryMutation<TKey, TValue>.CreateTryRemove(key)}, out var results);
            var firstResult = results.First();
            removedItem = firstResult.Remove.Value.ValueOrDefault;
            return firstResult.Remove.Value.HasValue;
        }

        #endregion

        #region Bulk mutation methods of only one type that call Mutate

        public virtual void RemoveRange(IEnumerable<TKey> keysToRemove,
            out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            Mutate(keysToRemove.Select(key => DictionaryMutation<TKey, TValue>.CreateTryRemove(key)), out var results);
            removedItems = results
                .Where(x => x.Remove.Value.HasValue)
                .ToComposableDictionary(x => x.Key, x => x.Remove.Value.Value);
        }

        public virtual void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key,
            Func<TKeyValuePair, TValue> value)
        {
            Mutate(newItems.Select(x => DictionaryMutation<TKey, TValue>.CreateAdd(key(x), () => value(x))), out var _);
        }

        public virtual void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems,
            Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> previousValues)
        {
            Mutate(newItems.Select(x => DictionaryMutation<TKey, TValue>.CreateUpdate(key(x), _ => value(x))), out var results);
            previousValues = results
                .ToComposableDictionary(x => x.Key, x => x.Update.Value);
        }

        public virtual void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> result)
        {
            Mutate(newItems.Select(x => DictionaryMutation<TKey, TValue>.CreateUpdate(key(x), _ => value(x))), out var results);
            result = results
                .ToComposableDictionary(x => x.Key, x => x.Add.Value);
        }

        public virtual void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> result)
        {
            Mutate(newItems.Select(x => DictionaryMutation<TKey, TValue>.CreateUpdate(key(x), _ => value(x))), out var results);
            result = results
                .ToComposableDictionary(x => x.Key, x => x.Update.Value);
        }

        public virtual void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> result)
        {
            Mutate(newItems.Select(x => DictionaryMutation<TKey, TValue>.CreateUpdate(key(x), _ => value(x))), out var results);
            result = results
                .ToComposableDictionary(x => x.Key, x => x.AddOrUpdate.Value);
        }

        public virtual void TryRemoveRange(IEnumerable<TKey> keysToRemove,
            out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            Mutate(keysToRemove.Select(key => DictionaryMutation<TKey, TValue>.CreateTryRemove(key)), out var results);
            removedItems = results
                .Where(x => x.Remove.Value.HasValue)
                .ToComposableDictionary(x => x.Key, x => x.Remove.Value.Value);
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

        public virtual void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            var keysToRemove = this.Where(kvp => predicate(kvp)).Select(kvp => kvp.Key);
            RemoveRange(keysToRemove);
        }

        public virtual void RemoveWhere(Func<TKey, TValue, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            var keysToRemove = this.Where(kvp => predicate(kvp.Key, kvp.Value)).Select(kvp => kvp.Key);
            RemoveRange(keysToRemove, out removedItems);
        }

        public virtual void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            var keysToRemove = this.Where(kvp => predicate(kvp)).Select(kvp => kvp.Key);
            RemoveRange(keysToRemove, out removedItems);
        }

        public virtual void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
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
                throw new RemoveFailedBecauseNoSuchKeyExistsException(key);
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
                throw new AddFailedBecauseKeyAlreadyExistsException(key);
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
                throw new UpdateFailedBecauseNoSuchKeyExistsException(key);
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
        
        public void AddOrUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            AddOrUpdateRange(newItems.AsEnumerable());
        }
        
        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            AddOrUpdateRange(newItems.AsEnumerable());
        }
        
        public void AddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            AddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            AddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            UpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            UpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void AddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            AddRange(newItems.AsEnumerable());
        }
        
        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            AddRange(newItems.AsEnumerable());
        }
        
        public void UpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            UpdateRange(newItems.AsEnumerable());
        }
        
        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            UpdateRange(newItems.AsEnumerable());
        }
        
        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryAddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public void TryUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
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
        
        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> result)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out result);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> result)
        {
            TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out result);
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> result)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out result);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> result)
        {
            TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out result);
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> previousValues)
        {
            UpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out previousValues);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> previousValues)
        {
            UpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out previousValues);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> result)
        {
            AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out result);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> result)
        {
            AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value, out result);
        }

        #endregion
    }
}
