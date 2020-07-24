using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MoreCollections
{
    public abstract class DictionaryBaseEx<TKey, TValue> : ReadOnlyDictionaryBaseEx<TKey, TValue>, IDictionaryEx<TKey, TValue>
    {
        #region Stuff that definitely doesn't need to be overridden for atomicity or performance reasons

        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return AddOrUpdateRange(newItems.AsEnumerable());
        }

        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
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
        
        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return AddOrUpdateRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
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

        public void Remove(TKey key)
        {
            if (!TryRemove(key))
            {
                throw new KeyNotFoundException();
            }
        }

        public int TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            return TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public int TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            return TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public int TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            return TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public int TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            return TryAddRange(newItems, kvp => kvp.Key, kvp => kvp.Value);
        }

        public new TValue this[TKey key]
        {
            get => base[key];
            set => AddOrUpdate(key, value);
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

        public virtual int TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems,
            Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            var count = 0;
            foreach (var newItem in newItems)
            {
                if (TryAdd(key(newItem), value(newItem)))
                {
                    count++;
                }
            }

            return count;
        }
        
        public virtual ImmutableDictionary<AddOrUpdateResult, int> AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            var dictionary = new Dictionary<AddOrUpdateResult, int>();
            dictionary[AddOrUpdateResult.Add] = 0;
            dictionary[AddOrUpdateResult.Update] = 0;

            foreach (var newItem in newItems)
            {
                var result = AddOrUpdate(key(newItem), value(newItem));
                if (result == AddOrUpdateResult.Add)
                {
                    dictionary[AddOrUpdateResult.Add]++;
                }
                else
                {
                    dictionary[AddOrUpdateResult.Update]++;
                }
            }

            return dictionary.ToImmutableDictionary();
        }

        public virtual void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            foreach (var newItem in newItems)
            {
                Add(key(newItem), value(newItem));
            }
        }

        #endregion

        #region Abstract methods

        public abstract bool TryAdd(TKey key, TValue value);
        
        public abstract AddOrUpdateResult AddOrUpdate(TKey key, TValue value);

        public abstract void Add(TKey key, TValue value);

        public abstract bool TryRemove(TKey key);

        #endregion
    }
}