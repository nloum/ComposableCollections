using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Adapters
{
    /// <summary>
    /// Exposes a system IDictionary as an IComposableDictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SystemDelegateDictionary<TKey, TValue> : DictionaryBase<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _source;

        public SystemDelegateDictionary(IDictionary<TKey, TValue> source)
        {
            _source = source;
        }

        public override void Write(IEnumerable<DictionaryWrite<TKey, TValue>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
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
                        _source.Add(write.Key, value);
                        finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true, default(TValue), value));
                    }
                    break;
                    case DictionaryWriteType.TryAdd:
                    {
                        if (!_source.TryGetValue(write.Key, out var existingValue))
                        {
                            var newValue = write.ValueIfAdding!();
                            _source.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateTryAdd(write.Key, true, default(TValue), newValue));
                        }
                        else
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateAdd(write.Key, true, existingValue, default(TValue)));
                        }
                    }
                    break;
                    case DictionaryWriteType.Update:
                    {
                        if (_source.TryGetValue(write.Key, out var previousValue))
                        {
                            var newValue = write.ValueIfUpdating!(previousValue);
                            _source.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, previousValue, newValue));
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                    break;
                    case DictionaryWriteType.TryUpdate:
                    {
                        if (_source.TryGetValue(write.Key, out var previousValue))
                        {
                            var newValue = write.ValueIfUpdating!(previousValue);
                            _source.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, previousValue, newValue));
                        }
                        else
                        {
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, false, default(TValue), default(TValue)));
                        }
                    }
                    break;
                    case DictionaryWriteType.AddOrUpdate:
                    {
                        if (_source.TryGetValue(write.Key, out var previousValue))
                        {
                            var newValue = write.ValueIfUpdating!(previousValue);
                            _source.Add(write.Key, newValue);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, true, previousValue, newValue));
                        }
                        else
                        {
                            var newValue = write.ValueIfAdding!();
                            _source[write.Key] = newValue;
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateUpdate(write.Key, false, default(TValue), newValue));
                        }
                    }
                    break;
                    case DictionaryWriteType.Remove:
                    {
                        if (_source.TryGetValue(write.Key, out var removedValue))
                        {
                            _source.Remove(write.Key);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, removedValue));
                        }
                    }
                    break;
                    case DictionaryWriteType.TryRemove:
                    {
                        if (_source.TryGetValue(write.Key, out var removedValue))
                        {
                            _source.Remove(write.Key);
                            finalResults.Add(DictionaryWriteResult<TKey, TValue>.CreateRemove(write.Key, removedValue));
                        }
                    }
                    break;
                    default:
                        throw new ArgumentException($"Unknown mutation type: {write.Type}");
                }
            }
        }
        
        public override bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            if (_source.TryGetValue(key, out existingValue))
            {
                newValue = default;
                return false;
            }

            newValue = value();
            _source.Add(key, newValue);
            return true;
        }

        public override bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            if (!_source.TryGetValue(key, out previousValue))
            {
                newValue = default;
                return false;
            }

            newValue = value(previousValue);
            _source[key] = newValue;
            return true;
        }

        
        
        public override bool TryRemove(TKey key, out TValue removedItem)
        {
            if (_source.TryGetValue(key, out removedItem))
            {
                _source.Remove(key);
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
                _source.Remove(key);
            }
        }

        public override IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _source.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)).GetEnumerator();
        }

        public override int Count => _source.Count;

        public override IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;

        public override IEnumerable<TKey> Keys => _source.Keys;

        public override IEnumerable<TValue> Values => _source.Values;

        public override bool ContainsKey(TKey key)
        {
            return _source.ContainsKey(key);
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return _source.TryGetValue(key, out value);
        }

        public override void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            foreach (var item in newItems)
            {
                _source.Add(key(item), value(item));
            }
        }

        public override void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            foreach (var key in keysToRemove)
            {
                _source.Remove(key);
            }
        }

        public override void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            var keysToRemove = new List<TKey>();
            foreach (var kvp in _source)
            {
                if (predicate(kvp.Key, kvp.Value))
                {
                    keysToRemove.Add(kvp.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                _source.Remove(key);
            }
        }

        public override void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            var keysToRemove = new List<TKey>();
            foreach (var kvp in _source)
            {
                if (predicate(new KeyValue<TKey, TValue>(kvp.Key, kvp.Value)))
                {
                    keysToRemove.Add(kvp.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                _source.Remove(key);
            }
        }

        public override void Clear()
        {
            _source.Clear();
        }

        public override bool TryRemove(TKey key)
        {
            if (_source.ContainsKey(key))
            {
                _source.Remove(key);
                return true;
            }

            return false;
        }
    }
}