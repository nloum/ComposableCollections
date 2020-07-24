using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MoreCollections
{
    public abstract class DictionaryBaseEx<TKey, TValue> : ReadOnlyDictionaryBaseEx<TKey, TValue>, IDictionaryEx<TKey, TValue>
    {
        #region Stuff that definitely doesn't need to be overridden for atomicity or performance reasons

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return AddOrUpdateRange(newItems.AsEnumerable());
        }

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            return AddOrUpdateRange(newItems.AsEnumerable());
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

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
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

        public void Remove(TKey key)
        {
            if (!TryRemove(key))
            {
                throw new KeyNotFoundException();
            }
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            return TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        
        
        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            return TryUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        
        
        public new TValue this[TKey key]
        {
            get => base[key];
            set => AddOrUpdate(key, value);
        }

        public void Add(TKey key, TValue value)
        {
            if (!TryAdd(key, value))
            {
                throw new InvalidOperationException("Could not add item");
            }
        }

        public void Update(TKey key, TValue value)
        {
            if (!TryUpdate(key, value))
            {
                throw new InvalidOperationException("Could not update item");
            }
        }
        
        #endregion
        
        #region Stuff that might need to be overridden for atomicity or performance reasons

        public virtual void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            RemoveWhere(kvp => predicate(kvp.Key, kvp.Value));
        }

        public virtual void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate)
        {
            RemoveRange(this.Where(predicate).Select(kvp => kvp.Key));
        }

        public virtual int TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            var count = 0;
            foreach (var keyToRemove in keysToRemove)
            {
                if (TryRemove(keyToRemove))
                {
                    count++;
                }
            }

            return count;
        }

        public virtual void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            foreach (var keyToRemove in keysToRemove)
            {
                Remove(keyToRemove);
            }
        }

        public virtual void Clear()
        {
            RemoveRange(Keys.ToImmutableHashSet());
        }

        public virtual IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            var dictionary = new DictionaryEx<TKey, AddOrUpdateResult>();

            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                var result = AddOrUpdate(newKey, value(newItem));
                if (result == AddOrUpdateResult.Add)
                {
                    dictionary[newKey] = AddOrUpdateResult.Add;
                }
                else
                {
                    dictionary[newKey] = AddOrUpdateResult.Update;
                }
            }

            return dictionary;
        }

        public virtual IReadOnlyDictionaryEx<TKey, bool> TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems,
            Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            var result = new DictionaryEx<TKey, bool>();
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                if (TryAdd(newKey, value(newItem)))
                {
                    result[newKey] = true;
                }
                else
                {
                    result[newKey] = false;
                }
            }

            return result;
        }

        public virtual void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            foreach (var newItem in newItems)
            {
                Add(key(newItem), value(newItem));
            }
        }
        
        public virtual IReadOnlyDictionaryEx<TKey, bool> TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems,
            Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            var result = new DictionaryEx<TKey, bool>();
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                if (TryUpdate(newKey, value(newItem)))
                {
                    result[newKey] = true;
                }
                else
                {
                    result[newKey] = false;
                }
            }

            return result;
        }

        public virtual void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            foreach (var newItem in newItems)
            {
                Update(key(newItem), value(newItem));
            }
        }

        #endregion

        #region Abstract methods

        public abstract bool TryAdd(TKey key, TValue value);
        public abstract bool TryUpdate(TKey key, TValue value);

        public abstract AddOrUpdateResult AddOrUpdate(TKey key, TValue value);
        
        public abstract bool TryRemove(TKey key);

        #endregion
    }
}