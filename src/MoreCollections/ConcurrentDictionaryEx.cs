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
        
        public override bool TryAdd(TKey key, Func<TValue> value, out TValue previousValue, out TValue newValue)
        {
            lock (Lock)
            {
                if (TryGetValueInsideLock(key, out previousValue))
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

        public override DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating, out TValue previousValue, out TValue newValue)
        {
            lock (Lock)
            {
                if (TryGetValueInsideLock(key, out previousValue))
                {
                    newValue = valueIfUpdating(previousValue);
                    State = State.SetItem(key, newValue);
                    return DictionaryItemAddOrUpdateResult.Update;
                }

                newValue = valueIfAdding();
                State = State.Add(key, newValue);
                return DictionaryItemAddOrUpdateResult.Add;
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
        
        public override void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>> result)
        {
            lock (Lock)
            {
                var finalResult = new DictionaryEx<TKey, IDictionaryItemAddAttempt<TValue>>();
                result = finalResult;
            
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);

                    if (!TryGetValueInsideLock(newKey, out var previousValue))
                    {
                        finalResult[newKey] = new DictionaryItemAddAttempt<TValue>(true, Maybe<TValue>.Nothing(), newValue.ToMaybe());
                        State = State.Add(newKey, newValue);
                    }
                    else
                    {
                        finalResult[newKey] = new DictionaryItemAddAttempt<TValue>(false, previousValue.ToMaybe(), Maybe<TValue>.Nothing());
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

        public override void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> result)
        {
            lock (Lock)
            {
                var finalResult = new DictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>>();
                result = finalResult;
            
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);

                    if (TryGetValueInsideLock(newKey, out var previousValue))
                    {
                        var newValue = value(newItem);
                        finalResult[newKey] = new DictionaryItemUpdateAttempt<TValue>(true, previousValue.ToMaybe(), newValue.ToMaybe());
                        State = State.SetItem(newKey, newValue);
                    }
                    else
                    {
                        finalResult[newKey] = new DictionaryItemUpdateAttempt<TValue>(false, Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing());
                    }
                }
            }
        }

        public override void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>> previousValues)
        {
            lock (Lock)
            {
                var finalResult = new DictionaryEx<TKey, IDictionaryItemUpdateAttempt<TValue>>();
                previousValues = finalResult;

                var state = State;
                
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);

                    var previousValue = state[newKey];
                    state = state.SetItem(newKey, newValue);
                    finalResult[newKey] = new DictionaryItemUpdateAttempt<TValue>(true, previousValue.ToMaybe(), newValue.ToMaybe());
                }

                State = state;
            }
        }

        public override void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>> result)
        {
            lock (Lock)
            {
                var finalResult = new DictionaryEx<TKey, IDictionaryItemAddOrUpdate<TValue>>();
                result = finalResult;
            
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);

                    if (AddOrUpdate(newKey, () => newValue, _ => newValue, out var previousValue, out var _) ==
                        DictionaryItemAddOrUpdateResult.Update)
                    {
                        finalResult[newKey] = new DictionaryItemAddOrUpdate<TValue>(DictionaryItemAddOrUpdateResult.Update, previousValue.ToMaybe(), newValue);
                    }
                    else
                    {
                        finalResult[newKey] = new DictionaryItemAddOrUpdate<TValue>(DictionaryItemAddOrUpdateResult.Add, Maybe<TValue>.Nothing(), newValue);
                    }
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