using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MoreCollections
{
    public class ConcurrentDictionaryEx<TKey, TValue> : DictionaryBaseEx<TKey, TValue>
    {
        protected ImmutableDictionary<TKey, TValue> State = ImmutableDictionary<TKey, TValue>.Empty;
        protected readonly object Lock = new object();

        public override bool TryAdd(TKey key, TValue value)
        {
            lock (Lock)
            {
                if (ContainsKeyInsideLock(key))
                {
                    return false;
                }
            
                State = State.Add(key, value);
                return true;
            }
        }

        public override bool TryUpdate(TKey key, TValue value)
        {
            lock (Lock)
            {
                if (ContainsKeyInsideLock(key))
                {
                    State = State.SetItem(key, value);
                    return true;
                }
            
                return false;
            }
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return State.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return State.Select(kvp => Utility.KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => State.Count;

        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;
        public override IEnumerable<TKey> Keys => State.Keys;
        public override IEnumerable<TValue> Values => State.Values;
        public override AddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            lock (Lock)
            {
                if (ContainsKeyInsideLock(key))
                {
                    State = State.SetItem(key, value);
                    return AddOrUpdateResult.Update;
                }
            
                State = State.Add(key, value);
                return AddOrUpdateResult.Add;
            }
        }

        public override bool TryRemove(TKey key)
        {
            lock (Lock)
            {
                if (ContainsKeyInsideLock(key))
                {
                    State.Remove(key);
                    return true;
                }

                return false;
            }
        }

        public override bool ContainsKey(TKey key)
        {
            return ContainsKeyOutsideLock(key);
        }

        protected virtual bool ContainsKeyInsideLock(TKey key)
        {
            return State.ContainsKey(key);
        }

        protected virtual bool ContainsKeyOutsideLock(TKey key)
        {
            return State.ContainsKey(key);
        }

        public override void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            lock (Lock)
            {
                State = State.RemoveRange(State.Where(kvp => predicate(kvp.Key, kvp.Value)).Select(kvp => kvp.Key));
            }
        }

        public override void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate)
        {
            lock (Lock)
            {
                State = State.RemoveRange(this.Where(kvp => predicate(kvp)).Select(kvp => kvp.Key));
            }
        }

        public override int TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            lock (Lock)
            {
                var beforeCount = State.Count;
                State = State.RemoveRange(keysToRemove);
                var afterCount = State.Count;
                return beforeCount - afterCount;
            }
        }

        public override void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            lock (Lock)
            {
                var keysToRemoveList = keysToRemove.ToImmutableList();
                var beforeCount = State.Count;
                var tmpWrapped = State.RemoveRange(keysToRemoveList);
                var afterCount = tmpWrapped.Count;
                if (beforeCount - afterCount != keysToRemoveList.Count)
                {
                    throw new KeyNotFoundException("Some of the keys specified to be removed did not exist in the dictionary");
                }

                State = tmpWrapped;
            }
        }

        public override void Clear()
        {
            lock (Lock)
            {
                State = ImmutableDictionary<TKey, TValue>.Empty;
            }
        }

        public override IReadOnlyDictionaryEx<TKey, bool> TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems,
            Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            lock (Lock)
            {
                var result = new DictionaryEx<TKey, bool>();
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    if (!ContainsKeyInsideLock(newKey))
                    {
                        State = State.Add(newKey, value(newItem));
                        result[newKey] = true;
                    }
                    else
                    {
                        result[newKey] = false;
                    }
                }

                return result;
            }
        }
        
        public override IReadOnlyDictionaryEx<TKey, AddOrUpdateResult> AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            lock (Lock)
            {
                var dictionary = new DictionaryEx<TKey, AddOrUpdateResult>();

                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);
                    if (ContainsKeyInsideLock(newKey))
                    {
                        State = State.SetItem(newKey, newValue);
                        dictionary[newKey] = AddOrUpdateResult.Update;
                    }
                    else
                    {
                        State = State.Add(newKey, newValue);
                        dictionary[newKey] = AddOrUpdateResult.Add;
                    }
                }

                return dictionary;
            }
        }

        public override void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            lock (Lock)
            {
                var newItemsNonLazy = newItems
                    .Select(newItem => new KeyValuePair<TKey, TValue>(key(newItem), value(newItem))).ToImmutableList();
                var beforeCount = State.Count;
                var tmpWrapped = State.AddRange(newItemsNonLazy);
                var afterCount = tmpWrapped.Count;
                if (beforeCount - afterCount != newItemsNonLazy.Count)
                {
                    throw new InvalidOperationException("Some of the specified keys already existed, and therefore could not be added");
                }

                State = tmpWrapped;
            }
        }
    }
}