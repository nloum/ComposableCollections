using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Sources
{
    public class ComposableDictionary<TKey, TValue> : DictionaryBase<TKey, TValue>
    {
        protected readonly Dictionary<TKey, TValue> State = new Dictionary<TKey, TValue>();

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return State.TryGetValue(key, out value);
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return State.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => State.Count;
        public override IEqualityComparer<TKey> Comparer => State.Comparer;
        public override IEnumerable<TKey> Keys => State.Keys;
        public override IEnumerable<TValue> Values => State.Values;
        public override bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            if (State.TryGetValue(key, out existingValue))
            {
                newValue = default;
                return false;
            }

            newValue = value();
            State.Add(key, newValue);
            return true;
        }

        public override void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes,
            out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            var finalResults = new List<DictionaryWriteResult<TKey, TValue>>();
            results = finalResults;

            foreach (var write in writes)
            {
                switch (write.Type)
                {
                    case DictionaryWriteType.Add:
                    {
                        var value = write.ValueIfAdding!();
                        State.Add(write.Key, value);
                        finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true,
                            default(TValue), value));
                    }
                        break;
                    case DictionaryWriteType.TryAdd:
                    {
                        if (!State.TryGetValue(write.Key, out var existingValue))
                        {
                            var newValue = write.ValueIfAdding!();
                            State.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateTryAdd(write.Key, true,
                                default(TValue), newValue));
                        }
                        else
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true,
                                existingValue, default(TValue)));
                        }
                    }
                        break;
                    case DictionaryWriteType.Update:
                    {
                        if (State.TryGetValue(write.Key, out var previousValue))
                        {
                            var newValue = write.ValueIfUpdating!(previousValue);
                            State.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true,
                                previousValue, newValue));
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                        break;
                    case DictionaryWriteType.TryUpdate:
                    {
                        if (State.TryGetValue(write.Key, out var previousValue))
                        {
                            var newValue = write.ValueIfUpdating!(previousValue);
                            State[write.Key] = newValue;
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true,
                                previousValue, newValue));
                        }
                        else
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, false,
                                default(TValue), default(TValue)));
                        }
                    }
                        break;
                    case DictionaryWriteType.AddOrUpdate:
                    {
                        if (State.TryGetValue(write.Key, out var previousValue))
                        {
                            var newValue = write.ValueIfUpdating!(previousValue);
                            State[write.Key] = newValue;
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(write.Key, DictionaryItemAddOrUpdateResult.Update,
                                previousValue, newValue));
                        }
                        else
                        {
                            var newValue = write.ValueIfAdding!();
                            State.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAddOrUpdate(write.Key, DictionaryItemAddOrUpdateResult.Add,
                                default(TValue), newValue));
                        }
                    }
                        break;
                    case DictionaryWriteType.Remove:
                    {
                        if (State.TryGetValue(write.Key, out var removedValue))
                        {
                            State.Remove(write.Key);
                            finalResults.Add(
                                DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key,
                                    removedValue));
                        }
                    }
                        break;
                    case DictionaryWriteType.TryRemove:
                    {
                        if (State.TryGetValue(write.Key, out var removedValue))
                        {
                            State.Remove(write.Key);
                            finalResults.Add(
                                DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key,
                                    removedValue));
                        }
                        else
                        {
                            finalResults.Add(
                                DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key,
                                    default(TValue)));
                        }
                    }
                        break;
                    default:
                        throw new ArgumentException($"Unknown mutation type: {write.Type}");
                }
            }
        }

        public override bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            if (TryGetValue(key, out previousValue))
            {
                newValue = value(previousValue);
                State[key] = newValue;
                return true;
            }

            newValue = default;
            return false;
        }

        public override bool TryRemove(TKey key, out TValue removedItem)
        {
            if (TryGetValue(key, out removedItem))
            {
                State.Remove(key);
                return true;
            }

            return false;
        }

        public override void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
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

            foreach (var key in results.Keys)
            {
                State.Remove(key);
            }
        }
    }
}