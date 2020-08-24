using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Mutations;
using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    /// <summary>
    /// Exposes a system IDictionary as an IComposableDictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SystemDelegateDictionary<TKey, TValue> : DictionaryBase<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _wrapped;

        public SystemDelegateDictionary(IDictionary<TKey, TValue> wrapped)
        {
            _wrapped = wrapped;
        }

        public override void Mutate(IEnumerable<DictionaryMutation<TKey, TValue>> mutations, out IReadOnlyList<DictionaryMutationResult<TKey, TValue>> results)
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
                        _wrapped.Add(mutation.Key, value);
                        finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAdd(mutation.Key, true, Maybe<TValue>.Nothing(), value.ToMaybe()));
                    }
                    break;
                    case DictionaryMutationType.TryAdd:
                    {
                        if (!_wrapped.TryGetValue(mutation.Key, out var existingValue))
                        {
                            var newValue = mutation.ValueIfAdding.Value();
                            _wrapped.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateTryAdd(mutation.Key, true, Maybe<TValue>.Nothing(), newValue.ToMaybe()));
                        }
                        else
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateAdd(mutation.Key, true, existingValue.ToMaybe(), Maybe<TValue>.Nothing()));
                        }
                    }
                    break;
                    case DictionaryMutationType.Update:
                    {
                        if (_wrapped.TryGetValue(mutation.Key, out var previousValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(previousValue);
                            _wrapped.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                    break;
                    case DictionaryMutationType.TryUpdate:
                    {
                        if (_wrapped.TryGetValue(mutation.Key, out var previousValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(previousValue);
                            _wrapped.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, false, Maybe<TValue>.Nothing(), Maybe<TValue>.Nothing()));
                        }
                    }
                    break;
                    case DictionaryMutationType.AddOrUpdate:
                    {
                        if (_wrapped.TryGetValue(mutation.Key, out var previousValue))
                        {
                            var newValue = mutation.ValueIfUpdating.Value(previousValue);
                            _wrapped.Add(mutation.Key, newValue);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                        }
                        else
                        {
                            var newValue = mutation.ValueIfAdding.Value();
                            _wrapped[mutation.Key] = newValue;
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateUpdate(mutation.Key, false, Maybe<TValue>.Nothing(), newValue.ToMaybe()));
                        }
                    }
                    break;
                    case DictionaryMutationType.Remove:
                    {
                        if (_wrapped.TryGetValue(mutation.Key, out var removedValue))
                        {
                            _wrapped.Remove(mutation.Key);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, removedValue.ToMaybe()));
                        }
                    }
                    break;
                    case DictionaryMutationType.TryRemove:
                    {
                        if (_wrapped.TryGetValue(mutation.Key, out var removedValue))
                        {
                            _wrapped.Remove(mutation.Key);
                            finalResults.Add(DictionaryMutationResult<TKey, TValue>.CreateRemove(mutation.Key, removedValue.ToMaybe()));
                        }
                    }
                    break;
                    default:
                        throw new ArgumentException($"Unknown mutation type: {mutation.Type}");
                }
            }
        }
        
        public override bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            if (_wrapped.TryGetValue(key, out existingValue))
            {
                newValue = default;
                return false;
            }

            newValue = value();
            _wrapped.Add(key, newValue);
            return true;
        }

        public override bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            if (!_wrapped.TryGetValue(key, out previousValue))
            {
                newValue = default;
                return false;
            }

            newValue = value(previousValue);
            _wrapped[key] = newValue;
            return true;
        }

        
        
        public override bool TryRemove(TKey key, out TValue removedItem)
        {
            if (_wrapped.TryGetValue(key, out removedItem))
            {
                _wrapped.Remove(key);
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
                _wrapped.Remove(key);
            }
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _wrapped.Count;

        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;

        public override IEnumerable<TKey> Keys => _wrapped.Keys;

        public override IEnumerable<TValue> Values => _wrapped.Values;

        public override bool ContainsKey(TKey key)
        {
            return _wrapped.ContainsKey(key);
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return _wrapped.TryGetValue(key, out value);
        }

        public override void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            foreach (var item in newItems)
            {
                _wrapped.Add(key(item), value(item));
            }
        }

        public override void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            foreach (var key in keysToRemove)
            {
                _wrapped.Remove(key);
            }
        }

        public override void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            var keysToRemove = new List<TKey>();
            foreach (var kvp in _wrapped)
            {
                if (predicate(kvp.Key, kvp.Value))
                {
                    keysToRemove.Add(kvp.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                _wrapped.Remove(key);
            }
        }

        public override void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            var keysToRemove = new List<TKey>();
            foreach (var kvp in _wrapped)
            {
                if (predicate(new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)))
                {
                    keysToRemove.Add(kvp.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                _wrapped.Remove(key);
            }
        }

        public override void Clear()
        {
            _wrapped.Clear();
        }

        public override bool TryRemove(TKey key)
        {
            if (_wrapped.ContainsKey(key))
            {
                _wrapped.Remove(key);
                return true;
            }

            return false;
        }
    }
}