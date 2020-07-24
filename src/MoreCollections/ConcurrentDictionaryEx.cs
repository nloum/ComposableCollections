using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SimpleMonads;

namespace MoreCollections
{
    public class ConcurrentDictionaryEx<TKey, TValue> : DictionaryBaseEx<TKey, TValue>
    {
        protected ImmutableDictionary<TKey, TValue> State = ImmutableDictionary<TKey, TValue>.Empty;
        protected readonly object Lock = new object();

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
        
        public override bool TryAdd(TKey key, Func<TValue> value, out TValue newValue)
        {
            lock (Lock)
            {
                if (TryGetValueInsideLock(key, out var _))
                {
                    newValue = default;
                    return false;
                }

                newValue = value();
                State = State.Add(key, newValue);
                return true;
            }
        }

        public override bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            lock (Lock)
            {
                if (TryGetValueInsideLock(key, out previousValue))
                {
                    newValue = value(previousValue);
                    State = State.SetItem(key, newValue);
                    return true;
                }

                newValue = default;
                previousValue = default;
                return false;
            }
        }

        public override IMaybe<TValue> AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue)
        {
            lock (Lock)
            {
                if (TryGetValueInsideLock(key, out previousValue))
                {
                    newValue = valueIfUpdating(previousValue);
                    State = State.SetItem(key, newValue);
                    return previousValue.ToMaybe();
                }

                newValue = valueIfAdding();
                State = State.Add(key, newValue);
                return Maybe<TValue>.Nothing();
            }
        }

        public override bool TryRemove(TKey key, out TValue removedItem)
        {
            lock (Lock)
            {
                if (TryGetValueInsideLock(key, out removedItem))
                {
                    State.Remove(key);
                    return true;
                }
            
                return false;
            }
        }

        public override void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            lock (Lock)
            {
                var result = new DictionaryEx<TKey, TValue>();
                removedItems = result;
            
                foreach (var key in keysToRemove)
                {
                    if (!TryGetValue(key, out var previousValue))
                    {
                        result.Clear();
                        throw new KeyNotFoundException($"Key not found: {key}");
                    }

                    result[key] = previousValue;
                }

                foreach (var key in result.Keys)
                {
                    State.Remove(key);
                }
            }
        }
        
        protected virtual bool TryGetValueInsideLock(TKey key, out TValue value)
        {
            return State.TryGetValue(key, out value);
        }
        
        protected virtual bool TryGetValueOutsideLock(TKey key, out TValue value)
        {
            return State.TryGetValue(key, out value);
        }
        
        public override void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, bool> result)
        {
            lock (Lock)
            {
                var finalResult = new DictionaryEx<TKey, bool>();
                result = finalResult;
            
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);

                    if (!TryGetValueInsideLock(newKey, out var previousValue))
                    {
                        finalResult[newKey] = true;
                        State = State.Add(newKey, newValue);
                    }
                    else
                    {
                        finalResult[newKey] = false;
                    }
                }
            }
        }

        public override void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            lock (Lock)
            {
                State = State.AddRange(newItems.Select(kvp => new KeyValuePair<TKey, TValue>(key(kvp), value(kvp))));
            }
        }

        public override void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, TValue> result)
        {
            lock (Lock)
            {
                var finalResult = new DictionaryEx<TKey, TValue>();
                result = finalResult;
            
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);

                    if (TryGetValueInsideLock(newKey, out var previousValue))
                    {
                        finalResult[newKey] = previousValue;
                        State = State.SetItem(newKey, newValue);
                    }
                }
            }
        }

        public override void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, TValue> previousValues)
        {
            lock (Lock)
            {
                var finalResult = new DictionaryEx<TKey, TValue>();
                previousValues = finalResult;

                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);
                
                    Update(newKey, newValue, out var previousValue);
                    finalResult[newKey] = previousValue;
                }
            }
        }

        public override void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IMaybe<TValue>> result)
        {
            lock (Lock)
            {
                var finalResult = new DictionaryEx<TKey, IMaybe<TValue>>();
                result = finalResult;
            
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);

                    finalResult[newKey] = AddOrUpdate(newKey, newValue);
                }
            }
        }

        public override void TryRemoveRange(IEnumerable<TKey> keysToRemove,
            out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            lock (Lock)
            {
                var result = new DictionaryEx<TKey, TValue>();
                removedItems = result;
            
                foreach (var key in keysToRemove)
                {
                    if (TryRemove(key, out var removedItem))
                    {
                        result[key] = removedItem;
                    }
                }
            }
        }
        
        public override void Clear(out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            lock (Lock)
            {
                removedItems = State.ToDictionaryEx();
                State = ImmutableDictionary<TKey, TValue>.Empty;
            }
        }
        
        public override void Clear()
        {
            lock (Lock)
            {
                State = ImmutableDictionary<TKey, TValue>.Empty;
            }
        }
    }
}