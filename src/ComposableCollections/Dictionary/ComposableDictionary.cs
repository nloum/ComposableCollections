using System;
using System.Collections.Generic;
using System.Linq;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public class ComposableDictionary<TKey, TValue> : ComposableDictionaryBase<TKey, TValue>
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

        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations,
            out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
        {
            var finalResults = new List<DictionaryMutationResult<TKey, TValue>>();
            results = finalResults;

            foreach (var mutation in mutations)
            {
                switch (mutation.Type)
                {
                    case DictionaryMutationType.Add:
                    {
                        var value = mutation.ValueIfAdding.Value();
                        State.Add(mutation.Key, value);
                        finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAdd(mutation.Key, true,
                            Maybe<TValue>.Nothing(), value.ToMaybe()));
                    }
                        break;
                    case DictionaryMutationType.TryAdd:
                    {
                        if (!State.TryGetValue(mutation.Key, out var existingValue))
                        {
                            var newValue = mutation.ValueIfAdding.Value();
                            State.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateTryAdd(mutation.Key, true,
                                Maybe<TValue>.Nothing(), newValue.ToMaybe()));
                        }
                        else
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAdd(mutation.Key, true,
                                existingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                        }
                    }
                        break;
                    case DictionaryMutationType.Update:
                    {
                        if (State.TryGetValue(mutation.Key, out var previousValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(previousValue);
                            State.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true,
                                previousValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                        break;
                    case DictionaryMutationType.TryUpdate:
                    {
                        if (State.TryGetValue(mutation.Key, out var previousValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(previousValue);
                            State.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true,
                                previousValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, false,
                                Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing()));
                        }
                    }
                        break;
                    case DictionaryMutationType.AddOrUpdate:
                    {
                        if (State.TryGetValue(mutation.Key, out var previousValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(previousValue);
                            State.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true,
                                previousValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            var newValue = mutation.ValueIfAdding.Value();
                            State[mutation.Key] = newValue;
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, false,
                                Maybe<TValue>.Nothing(), newValue.ToMaybe()));
                        }
                    }
                        break;
                    case DictionaryMutationType.Remove:
                    {
                        if (State.TryGetValue(mutation.Key, out var removedValue))
                        {
                            State.Remove(mutation.Key);
                            finalResults.Add(
                                DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key,
                                    removedValue.ToMaybe()));
                        }
                    }
                        break;
                    case DictionaryMutationType.TryRemove:
                    {
                        if (State.TryGetValue(mutation.Key, out var removedValue))
                        {
                            State.Remove(mutation.Key);
                            finalResults.Add(
                                DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key,
                                    removedValue.ToMaybe()));
                        }
                    }
                        break;
                    default:
                        throw new ArgumentException($"Unknown mutation type: {mutation.Type}");
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

        public override void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
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