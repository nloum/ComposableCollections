using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using ComposableCollections.Dictionary.Exceptions;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.Set;
using ComposableCollections.Set.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Adapters
{
    public class SetToDictionaryAdapter<TKey> : ReadOnlySetToReadOnlyDictionaryAdapter<TKey>, IComposableDictionary<TKey, TKey>
    {
        private readonly Set.IComposableSet<TKey> _set;

        public SetToDictionaryAdapter(Set.IComposableSet<TKey> set) : base(set)
        {
            _set = set;
        }

        public bool TryGetValue(TKey key, out TKey value)
        {
            var result = TryGetValue(key);
            value = result.ValueOrDefault;
            return result.HasValue;
        }

        public TKey this[TKey key]
        {
            get => TryGetValue(key).Value;
            set => _set.TryAdd(key);
        }

        public void Write(IEnumerable<DictionaryWrite<TKey, TKey>> writes, out IReadOnlyList<DictionaryWriteResult<TKey, TKey>> results)
        {
            var finalResults = new List<DictionaryWriteResult<TKey, TKey>>();
            results = finalResults;
            
            foreach (var write in writes)
            {
                if (write.Type == DictionaryWriteType.Add)
                {
                    var value = write.ValueIfAdding.Value();
                    Add(write.Key, value);
                    finalResults.Add(DictionaryWriteResult<TKey, TKey>.CreateAdd(write.Key, true, Maybe<TKey>.Nothing(), value.ToMaybe()));
                }
                else if (write.Type == DictionaryWriteType.TryAdd)
                {
                    var added = TryAdd(write.Key, write.ValueIfAdding.Value, out var previousValue, out var newValue);
                    finalResults.Add(DictionaryWriteResult<TKey, TKey>.CreateTryAdd(write.Key, added, added ? Maybe<TKey>.Nothing() : previousValue.ToMaybe(), added ? newValue.ToMaybe() : Maybe<TKey>.Nothing()));
                }
                else if (write.Type == DictionaryWriteType.Remove)
                {
                    Remove(write.Key, out var result);
                    finalResults.Add(DictionaryWriteResult<TKey, TKey>.CreateRemove(write.Key, result.ToMaybe()));
                }
                else if (write.Type == DictionaryWriteType.TryRemove)
                {
                    var removed = TryRemove(write.Key, out var result);
                    if (removed)
                    {
                        finalResults.Add(DictionaryWriteResult<TKey, TKey>.CreateTryRemove(write.Key, result.ToMaybe()));
                    }
                }
                else if (write.Type == DictionaryWriteType.Update)
                {
                    var newValue = write.ValueIfUpdating.Value(write.Key);
                    Update(write.Key, newValue, out var previousValue);
                    finalResults.Add(DictionaryWriteResult<TKey, TKey>.CreateUpdate(write.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                } 
                else if (write.Type == DictionaryWriteType.TryUpdate)
                {
                    var updated = TryUpdate(write.Key, write.ValueIfUpdating.Value, out var previousValue, out var newValue);
                    if (updated)
                    {
                        finalResults.Add(DictionaryWriteResult<TKey, TKey>.CreateUpdate(write.Key, true, previousValue.ToMaybe(), newValue.ToMaybe()));
                    }
                    else
                    {
                        finalResults.Add(DictionaryWriteResult<TKey, TKey>.CreateUpdate(write.Key, false, Maybe<TKey>.Nothing(), Maybe<TKey>.Nothing()));
                    }
                }
                else if (write.Type == DictionaryWriteType.AddOrUpdate)
                {
                    var result = AddOrUpdate(write.Key, write.ValueIfAdding.Value, write.ValueIfUpdating.Value,
                        out var previousValue, out var newValue);
                    finalResults.Add(DictionaryWriteResult<TKey, TKey>.CreateAddOrUpdate(write.Key, result, previousValue.ToMaybe(), newValue));
                }
            }
        }

        public bool TryAdd(TKey key, TKey value)
        {
            return _set.TryAdd(key);
        }

        public bool TryAdd(TKey key, Func<TKey> value)
        {
            return _set.TryAdd(key);
        }

        public bool TryAdd(TKey key, Func<TKey> value, out TKey existingValue, out TKey newValue)
        {
            existingValue = key;
            newValue = key;
            return _set.TryAdd(key);
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TKey>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TKey>> results)
        {
            _set.TryAddRange(newItems.Select(kvp => kvp.Key), out var setResults);
            results = setResults.ToComposableDictionary(x =>
                x.NewValue, setResult =>
                new DictionaryItemAddAttempt<TKey>(setResult.Added, setResult.NewValue.ToMaybe(), setResult.NewValue.ToMaybe()));
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TKey>> results)
        {
            _set.TryAddRange(newItems.Select(kvp => kvp.Key), out var setResults);
            results = setResults.ToComposableDictionary(x =>
                x.NewValue, setResult =>
                new DictionaryItemAddAttempt<TKey>(setResult.Added, setResult.NewValue.ToMaybe(), setResult.NewValue.ToMaybe()));
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TKey>> results)
        {
            _set.TryAddRange(newItems.Select(key), out var setResults);
            results = setResults.ToComposableDictionary(x =>
                x.NewValue, setResult =>
                new DictionaryItemAddAttempt<TKey>(setResult.Added, setResult.NewValue.ToMaybe(), setResult.NewValue.ToMaybe()));
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TKey>> newItems)
        {
            _set.TryAddRange(newItems.Select(kvp => kvp.Key));
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems)
        {
            _set.TryAddRange(newItems.Select(kvp => kvp.Key));
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value)
        {
            _set.TryAddRange(newItems.Select(key));
        }

        public void TryAddRange(params IKeyValue<TKey, TKey>[] newItems)
        {
            TryAddRange(newItems.AsEnumerable());
        }

        public void TryAddRange(params KeyValuePair<TKey, TKey>[] newItems)
        {
            TryAddRange(newItems.AsEnumerable());
        }

        public void Add(TKey key, TKey value)
        {
            _set.Add(key);
        }

        public void AddRange(IEnumerable<IKeyValue<TKey, TKey>> newItems)
        {
            _set.AddRange(newItems.Select(x => x.Key));
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems)
        {
            _set.AddRange(newItems.Select(x => x.Key));
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value)
        {
            _set.AddRange(newItems.Select(key));
        }

        public void AddRange(params IKeyValue<TKey, TKey>[] newItems)
        {
            AddRange(newItems.AsEnumerable());
        }

        public void AddRange(params KeyValuePair<TKey, TKey>[] newItems)
        {
            AddRange(newItems.AsEnumerable());
        }

        public bool TryUpdate(TKey key, TKey value)
        {
            if (_set.Contains(key))
            {
                _set.Remove(key);
                _set.Add(key);
                return true;
            }

            return false;
        }

        public bool TryUpdate(TKey key, TKey value, out TKey previousValue)
        {
            previousValue = key;

            if (_set.Contains(key))
            {
                _set.Remove(key);
                _set.Add(key);
                return true;
            }

            return false;
        }

        public bool TryUpdate(TKey key, Func<TKey, TKey> value, out TKey previousValue, out TKey newValue)
        {
            previousValue = key;
            newValue = key;

            if (_set.Contains(key))
            {
                _set.Remove(key);
                _set.Add(value(key));
                return true;
            }

            return false;
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TKey>> newItems)
        {
            foreach (var newItem in newItems)
            {
                TryUpdate(newItem.Key, newItem.Value);
            }
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems)
        {
            foreach (var newItem in newItems)
            {
                TryUpdate(newItem.Key, newItem.Value);
            }
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value)
        {
            foreach (var newItem in newItems)
            {
                TryUpdate(key(newItem), value(newItem));
            }
        }

        public void TryUpdateRange(params IKeyValue<TKey, TKey>[] newItems)
        {
            TryUpdateRange(newItems.AsEnumerable());
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TKey>[] newItems)
        {
            TryUpdateRange(newItems.AsEnumerable());
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TKey>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TKey>> results)
        {
            TryUpdateRange(newItems, x => x.Key, x => x.Value, out results);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TKey>> results)
        {
            TryUpdateRange(newItems, x => x.Key, x => x.Value, out results);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TKey>> results)
        {
            var finalResults = new ComposableDictionary<TKey, IDictionaryItemUpdateAttempt<TKey>>();
            results = finalResults;
            
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                if (_set.Contains(newKey))
                {
                    _set.Remove(newKey);
                    _set.Add(newKey);
                    finalResults.Add(newKey, new DictionaryItemUpdateAttempt<TKey>(true, newKey.ToMaybe(), newKey.ToMaybe()));
                }
                else
                {
                    finalResults.Add(newKey, new DictionaryItemUpdateAttempt<TKey>(false, Maybe<TKey>.Nothing(), newKey.ToMaybe()));
                }
            }
        }

        public void Update(TKey key, TKey value)
        {
            if (_set.Contains(key))
            {
                _set.Remove(key);
                _set.Add(key);
            }
            else
            {
                throw new UpdateFailedBecauseNoSuchKeyExistsException(key);
            }
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TKey>> newItems)
        {
            foreach (var newItem in newItems)
            {
                Update(newItem.Key, newItem.Value);
            }
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems)
        {
            foreach (var newItem in newItems)
            {
                Update(newItem.Key, newItem.Value);
            }
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value)
        {
            foreach (var newItem in newItems)
            {
                Update(key(newItem), value(newItem));
            }
        }

        public void Update(TKey key, TKey value, out TKey previousValue)
        {
            previousValue = key;
            
            if (_set.Contains(key))
            {
                _set.Remove(key);
                _set.Add(key);
            }
            else
            {
                throw new UpdateFailedBecauseNoSuchKeyExistsException(key);
            }
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TKey>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TKey>> results)
        {
            UpdateRange(newItems, x => x.Key, x => x.Value, out results);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TKey>> results)
        {
            UpdateRange(newItems, x => x.Key, x => x.Value, out results);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TKey>> results)
        {
            var finalResults = new ComposableDictionary<TKey, IDictionaryItemUpdateAttempt<TKey>>();
            results = finalResults;
            
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                if (!_set.Contains(newKey))
                {
                    throw new UpdateFailedBecauseNoSuchKeyExistsException(newKey);
                }
                
                _set.Remove(newKey);
                _set.Add(newKey);
                finalResults.Add(newKey, new DictionaryItemUpdateAttempt<TKey>(true, newKey.ToMaybe(), newKey.ToMaybe()));
            }
        }

        public void UpdateRange(params IKeyValue<TKey, TKey>[] newItems)
        {
            UpdateRange(newItems.AsEnumerable());
        }

        public void UpdateRange(params KeyValuePair<TKey, TKey>[] newItems)
        {
            UpdateRange(newItems.AsEnumerable());
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TKey value)
        {
            return AddOrUpdate(key, () => value, _ => value, out var _, out var __);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TKey> valueIfAdding, Func<TKey, TKey> valueIfUpdating)
        {
            return AddOrUpdate(key, valueIfAdding, valueIfUpdating, out var _, out var __);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TKey> valueIfAdding, Func<TKey, TKey> valueIfUpdating, out TKey previousValue,
            out TKey newValue)
        {
            if (!_set.Contains(key))
            {
                newValue = valueIfAdding();
                _set.Add(newValue);
                previousValue = default;
                return DictionaryItemAddOrUpdateResult.Add;
            }
            else
            {
                newValue = valueIfUpdating(key);
                _set.Remove(key);
                _set.Add(newValue);
                previousValue = key;
                return DictionaryItemAddOrUpdateResult.Update;
            }
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TKey>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TKey>> results)
        {
            AddOrUpdateRange(newItems, x => x.Key, x => x.Value, out results);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TKey>> results)
        {
            AddOrUpdateRange(newItems, x => x.Key, x => x.Value, out results);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value,
            out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TKey>> results)
        {
            var finalResults = new ComposableDictionary<TKey, IDictionaryItemAddOrUpdate<TKey>>();
            results = finalResults;
            
            foreach (var newItem in newItems)
            {
                var newKey = key(newItem);
                if (!_set.Contains(newKey))
                {
                    _set.Add(newKey);
                    finalResults.Add(newKey, new DictionaryItemAddOrUpdate<TKey>(DictionaryItemAddOrUpdateResult.Add, newKey.ToMaybe(), newKey));
                }
                else
                {
                    _set.Remove(newKey);
                    _set.Add(newKey);
                    finalResults.Add(newKey, new DictionaryItemAddOrUpdate<TKey>(DictionaryItemAddOrUpdateResult.Update, newKey.ToMaybe(), newKey));
                }
            }
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TKey>> newItems)
        {
            AddOrUpdateRange(newItems, x => x.Key, x => x.Value, out var _);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TKey>> newItems)
        {
            AddOrUpdateRange(newItems, x => x.Key, x => x.Value, out var _);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TKey> value)
        {
            AddOrUpdateRange(newItems, key, value, out var _);
        }

        public void AddOrUpdateRange(params IKeyValue<TKey, TKey>[] newItems)
        {
            AddOrUpdateRange(newItems.AsEnumerable());
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TKey>[] newItems)
        {
            AddOrUpdateRange(newItems.AsEnumerable());
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _set.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _set.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TKey, bool> predicate)
        {
            _set.RemoveWhere(key => predicate(key, key));
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TKey>, bool> predicate)
        {
            _set.RemoveWhere(key => predicate(new KeyValue<TKey, TKey>(key, key)));
        }

        public void Clear()
        {
            _set.Clear();
        }

        public bool TryRemove(TKey key)
        { 
            return _set.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            _set.Remove(key);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TKey> removedItems)
        {
            _set.TryRemoveRange(keysToRemove, out var results);
            removedItems = results.ToComposableReadOnlyDictionary(x => x, x => x);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TKey> removedItems)
        {
            _set.RemoveRange(keysToRemove, out var results);
            removedItems = results.ToComposableReadOnlyDictionary(x => x, x => x);
        }

        public void RemoveWhere(Func<TKey, TKey, bool> predicate, out IComposableReadOnlyDictionary<TKey, TKey> removedItems)
        {
            _set.RemoveWhere(key => predicate(key, key), out var results);
            removedItems = results.ToComposableReadOnlyDictionary(x => x, x => x);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TKey>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TKey> removedItems)
        {
            _set.RemoveWhere(key => predicate(new KeyValue<TKey, TKey>(key, key)), out var results);
            removedItems = results.ToComposableReadOnlyDictionary(x => x, x => x);
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TKey> removedItems)
        {
            _set.Clear(out var results);
            removedItems = results.ToComposableReadOnlyDictionary(x => x, x => x);
        }

        public bool TryRemove(TKey key, out TKey removedItem)
        {
            removedItem = key;
            return _set.TryRemove(key);
        }

        public void Remove(TKey key, out TKey removedItem)
        {
            removedItem = key;
            _set.Remove(key);
        }
    }
    
    public class ReadOnlySetToReadOnlyDictionaryAdapter<TKey> : IComposableReadOnlyDictionary<TKey, TKey>
    {
        private readonly Set.IReadOnlySet<TKey> _set;
        
        public ReadOnlySetToReadOnlyDictionaryAdapter(Set.IReadOnlySet<TKey> set)
        {
            _set = set;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TKey>> GetEnumerator()
        {
            return _set.Select(key => new KeyValue<TKey, TKey>(key, key)).GetEnumerator();
        }

        public int Count => _set.Count;
        public IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;

        public TKey this[TKey key] => TryGetValue(key).Value;

        public IEnumerable<TKey> Keys => _set;
        public IEnumerable<TKey> Values => _set;
        public bool ContainsKey(TKey key)
        {
            return _set.Contains(key);
        }

        public IMaybe<TKey> TryGetValue(TKey key)
        {
            if (ContainsKey(key))
            {
                return key.ToMaybe();
            }
            
            return Maybe<TKey>.Nothing();
        }
    }
}