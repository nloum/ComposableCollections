using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Sources
{
    public class ConcurrentDictionary<TKey, TValue> : DictionaryBase<TKey, TValue>
    {
        protected ImmutableDictionary<TKey, TValue> State = ImmutableDictionary<TKey, TValue>.Empty;
        protected readonly object Lock = new object();

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return State.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return State.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
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

        public override void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            lock (Lock)
            {
                var finalResults = new List<DictionaryWriteResult<TKey, TValue>>();
                results = finalResults;
                foreach (var write in writes)
                {
                    switch (write.Type)
                    {
                        case DictionaryWriteType.Add:
                        {
                            var value = write.ValueIfAdding.Value();
                            State = State.Add(write.Key, value);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true, Maybe<TValue>.Nothing(), value.ToMaybe()));
                            break;
                        }
                        case DictionaryWriteType.TryAdd:
                        {
                            if (!State.TryGetValue(write.Key, out var existingValue))
                            {
                                var newValue = write.ValueIfAdding.Value();
                                State = State.Add(write.Key, newValue);
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateTryAdd(write.Key, true, Maybe<TValue>.Nothing(), newValue.ToMaybe()));
                            }
                            else
                            {
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, false, existingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                            }
                            break;
                        }
                        case DictionaryWriteType.Update:
                        {
                            if (State.TryGetValue(write.Key, out var previousValue))
                            {
                                var newValue = write.ValueIfUpdating.Value(previousValue);
                                State = State.Add(write.Key, newValue);
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                            }
                            else
                            {
                                throw new KeyNotFoundException();
                            }
                            break;
                        }
                        case DictionaryWriteType.TryUpdate:
                        {
                            if (State.TryGetValue(write.Key, out var previousValue))
                            {
                                var newValue = write.ValueIfUpdating.Value(previousValue);
                                State = State.Add(write.Key, newValue);
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                            }
                            else
                            {
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, false, Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing()));
                            }
                            break;
                        }
                        case DictionaryWriteType.AddOrUpdate:
                        {
                            if (State.TryGetValue(write.Key, out var previousValue))
                            {
                                var newValue = write.ValueIfUpdating.Value(previousValue);
                                State = State.SetItem(write.Key, newValue);
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(write.Key, DictionaryItemAddOrUpdateResult.Update, previousValue.ToMaybe(), newValue));
                            }
                            else
                            {
                                var newValue = write.ValueIfAdding.Value();
                                State = State.Add(write.Key, newValue);
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(write.Key, DictionaryItemAddOrUpdateResult.Add, Maybe<TValue>.Nothing(), newValue));
                            }
                            break;
                        }
                        case DictionaryWriteType.Remove:
                        {
                            if (State.TryGetValue(write.Key, out var previousValue))
                            {
                                State = State.Remove(write.Key);
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, previousValue.ToMaybe()));
                            }
                            else
                            {
                                throw new KeyNotFoundException();
                            }
                            break;
                        }
                        case DictionaryWriteType.TryRemove:
                        {
                            if (State.TryGetValue(write.Key, out var previousValue))
                            {
                                State = State.Remove(write.Key);
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, previousValue.ToMaybe()));
                            }
                            else
                            {
                                finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, Maybe<TValue>.Nothing()));
                            }
                            break;
                        }
                        default:
                            throw new ArgumentException($"Unknown mutation type: {write.Type}");
                    }
                }
            }
        }

        public override bool TryRemove(TKey key, out TValue removedItem)
        {
            lock (Lock)
            {
                if (TryGetValueInsideLock(key, out removedItem))
                {
                    State = State.Remove(key);
                    return true;
                }
            
                return false;
            }
        }

        public override void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            lock (Lock)
            {
                var results = new ComposableDictionary<TKey, TValue>();
                removedItems = results;
            
                foreach (var key in keysToRemove)
                {
                    if (!TryGetValue(key, out var previousValue))
                    {
                        results.Clear();
                        throw new KeyNotFoundException($"Key not found: {key}");
                    }

                    results[key] = previousValue;
                }

                var state = State;
                
                foreach (var key in results.Keys)
                {
                    state = state.Remove(key);
                }

                State = state;
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
        
        public override void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            lock (Lock)
            {
                var finalResult = new ComposableDictionary<TKey, IDictionaryItemAddAttempt<TValue>>();
                results = finalResult;
            
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

        public override void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            lock (Lock)
            {
                var finalResult = new ComposableDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>>();
                results = finalResult;
            
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

        public override void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> previousValues)
        {
            lock (Lock)
            {
                var finalResults = new ComposableDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>>();
                previousValues = finalResults;

                var state = State;
                
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);

                    var previousValue = state[newKey];
                    state = state.SetItem(newKey, newValue);
                    finalResults[newKey] = new DictionaryItemUpdateAttempt<TValue>(true, previousValue.ToMaybe(), newValue.ToMaybe());
                }

                State = state;
            }
        }

        public override void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            lock (Lock)
            {
                var finalResults = new ComposableDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>>();
                results = finalResults;
            
                foreach (var newItem in newItems)
                {
                    var newKey = key(newItem);
                    var newValue = value(newItem);

                    if (AddOrUpdate(newKey, () => newValue, _ => newValue, out var previousValue, out var _) ==
                        DictionaryItemAddOrUpdateResult.Update)
                    {
                        finalResults[newKey] = new DictionaryItemAddOrUpdate<TValue>(DictionaryItemAddOrUpdateResult.Update, previousValue.ToMaybe(), newValue);
                    }
                    else
                    {
                        finalResults[newKey] = new DictionaryItemAddOrUpdate<TValue>(DictionaryItemAddOrUpdateResult.Add, Maybe<TValue>.Nothing(), newValue);
                    }
                }
            }
        }

        public override void TryRemoveRange(IEnumerable<TKey> keysToRemove,
            out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            lock (Lock)
            {
                var results = new ComposableDictionary<TKey, TValue>();
                removedItems = results;
            
                foreach (var key in keysToRemove)
                {
                    if (TryRemove(key, out var removedItem))
                    {
                        results[key] = removedItem;
                    }
                }
            }
        }
        
        public override void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            lock (Lock)
            {
                removedItems = State.ToComposableDictionary();
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