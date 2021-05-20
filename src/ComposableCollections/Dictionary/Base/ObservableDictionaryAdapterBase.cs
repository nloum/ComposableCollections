using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Base
{
    /// <summary>
    /// This base class provides abstract methods that are called whenever a change is made to the dictionary
    /// </summary>
    public abstract class ObservableDictionaryAdapterBase<TKey, TValue> : IComposableDictionary<TKey, TValue>
    {
        private IComposableDictionary<TKey, TValue> _state;

        protected ObservableDictionaryAdapterBase(IComposableDictionary<TKey, TValue> state)
        {
            _state = state;
        }

        public void Write(IEnumerable<DictionaryWrite<TKey, TValue>> mutations, out IReadOnlyList<DictionaryWriteResult<TKey, TValue>> results)
        {
            _state.Write(mutations, out results);
            
            foreach (var result in results)
            {
                if (result.Add != default && result.Add!.Added)
                {
                    OnAdd(new KeyValue<TKey, TValue>(result.Key, result.Add!.NewValue!));
                }
                else if (result.Update != default && result.Update!.Updated)
                {
                    OnRemove(new KeyValue<TKey, TValue>(result.Key, result.Update!.ExistingValue!));
                    OnAdd(new KeyValue<TKey, TValue>(result.Key, result.Update!.NewValue!));
                }
                else if (result.Remove != null)
                {
                    OnRemove(new KeyValue<TKey, TValue>(result.Key, result.Remove!));
                }
                else if (result.AddOrUpdate != default)
                {
                    if (result.AddOrUpdate!.Result == DictionaryItemAddOrUpdateResult.Update)
                    {
                        OnRemove(new KeyValue<TKey, TValue>(result.Key, result.AddOrUpdate!.ExistingValue!));
                    }
                    OnAdd(new KeyValue<TKey, TValue>(result.Key, result.AddOrUpdate!.NewValue));
                }
            }
        }

        protected abstract void OnRemove(IKeyValue<TKey,TValue> keyValue);

        protected virtual void OnRemove(IEnumerable<IKeyValue<TKey, TValue>> keyValues)
        {
            foreach (var keyValue in keyValues)
            {
                OnRemove(keyValue);
            }
        }

        protected virtual void OnRemove(IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            OnRemove(removedItems.AsEnumerable());
        }
        
        protected abstract void OnAdd(IKeyValue<TKey, TValue> added);

        protected virtual void OnAdd(IEnumerable<IKeyValue<TKey, TValue>> keyValues)
        {
            foreach (var keyValue in keyValues)
            {
                OnAdd(keyValue);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _state.GetEnumerator();
        }

        public int Count => _state.Count;

        public bool ContainsKey(TKey key)
        {
            return _state.ContainsKey(key);
        }

        public TValue GetValue(TKey key)
        {
            return this[key];
        }

        public void SetValue(TKey key, TValue value)
        {
            this[key] = value;
        }

        public TValue? TryGetValue(TKey key)
        {
            return _state.TryGetValue(key);
        }

        public IEqualityComparer<TKey> Comparer => _state.Comparer;

        public IEnumerable<TKey> Keys => _state.Keys;

        public IEnumerable<TValue> Values => _state.Values;

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _state.TryGetValue(key, out value);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (_state.TryAdd(key, value))
            {
                OnAdd(new KeyValue<TKey, TValue>(key, value));
                return true;
            }
            
            return false;
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            if (_state.TryAdd(key, value, out var existingValue, out var newValue))
            {
                OnAdd(new KeyValue<TKey, TValue>(key, newValue));
                return true;
            }
            
            return false;
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            if (_state.TryAdd(key, value, out existingValue, out newValue))
            {
                OnAdd(new KeyValue<TKey, TValue>(key, newValue));
                return true;
            }

            return false;
        }

        public bool TryAdd(TKey key, TValue value, out TValue result, out TValue previousValue)
        {
            return TryAdd(key, () => value, out result, out previousValue);
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            return GetOrAdd(key, () => value);
        }

        public TValue GetOrAdd(TKey key, Func<TValue> value)
        {
            TryAdd(key, value, out var result, out var previousValue);
            return result;
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> result)
        {
            _state.TryAddRange(newItems, out result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> result)
        {
            _state.TryAddRange(newItems, out result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> result)
        {
            _state.TryAddRange(newItems, key, value, out result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryAddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _state.TryAddRange(newItems, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _state.TryAddRange(newItems, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _state.TryAddRange(newItems, key, value, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryAddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _state.TryAddRange(newItems, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _state.TryAddRange(newItems, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void Add(TKey key, TValue value)
        {
            _state.Add(key, value);
            OnAdd(new KeyValue<TKey, TValue>(key, value));
        }

        public void AddRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _state.TryAddRange(newItems, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _state.TryAddRange(newItems, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _state.TryAddRange(newItems, key, value, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void AddRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _state.TryAddRange(newItems, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _state.TryAddRange(newItems, out var result);
            OnAdd(result.Where(x => x.Value.Added).Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            if (_state.TryUpdate(key, value, out var previousValue))
            {
                OnRemove(new KeyValue<TKey, TValue>(key, previousValue));
                OnAdd(new KeyValue<TKey, TValue>(key, value));
                return true;
            }

            return false;
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            if (_state.TryUpdate(key, value, out previousValue))
            {
                OnRemove(new KeyValue<TKey, TValue>(key, previousValue));
                OnAdd(new KeyValue<TKey, TValue>(key, value));
                return true;
            }

            return false;
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            if (_state.TryUpdate(key, value, out previousValue, out newValue))
            {
                OnRemove(new KeyValue<TKey, TValue>(key, previousValue));
                OnAdd(new KeyValue<TKey, TValue>(key, newValue));
                return true;
            }

            return false;
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _state.TryUpdateRange(newItems, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _state.TryUpdateRange(newItems, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _state.TryUpdateRange(newItems, key, value, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _state.TryUpdateRange(newItems, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _state.TryUpdateRange(newItems, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _state.TryUpdateRange(newItems, out results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _state.TryUpdateRange(newItems, out results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _state.TryUpdateRange(newItems, key, value, out results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void Update(TKey key, TValue value)
        {
            _state.Update(key, value, out var previousValue);
            OnRemove(new KeyValue<TKey, TValue>(key, previousValue));
            OnAdd(new KeyValue<TKey, TValue>(key, value));
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _state.UpdateRange(newItems, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _state.UpdateRange(newItems, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _state.UpdateRange(newItems, key, value, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            _state.Update(key, value, out previousValue);
            OnRemove(new KeyValue<TKey, TValue>(key, previousValue));
            OnAdd(new KeyValue<TKey, TValue>(key, value));
        }

        public void UpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _state.UpdateRange(newItems, out results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _state.UpdateRange(newItems, out results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _state.UpdateRange(newItems, key, value, out results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void UpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _state.UpdateRange(newItems, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _state.UpdateRange(newItems, out var results);
            var resultsList = results.Where(x => x.Value.Updated).ToImmutableList();
            OnRemove(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            OnAdd(resultsList.Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue!)));
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, TValue value)
        {
            var result = _state.AddOrUpdate(key, value);
            if (result == DictionaryItemAddOrUpdateResult.Update)
            {
                OnRemove(new KeyValue<TKey, TValue>(key, value));
            }

            OnAdd(new KeyValue<TKey, TValue>(key, value));

            return result;
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            var result = _state.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out var previousValue, out var newValue);
            if (result == DictionaryItemAddOrUpdateResult.Update)
            {
                OnRemove(new KeyValue<TKey, TValue>(key, newValue));
            }

            OnAdd(new KeyValue<TKey, TValue>(key, newValue));

            return result;
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            var result = _state.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
            if (result == DictionaryItemAddOrUpdateResult.Update)
            {
                OnRemove(new KeyValue<TKey, TValue>(key, newValue));
            }

            OnAdd(new KeyValue<TKey, TValue>(key, newValue));

            return result;
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _state.AddOrUpdateRange(newItems, out results);
            SendLiveLinqEventForAddOrUpdate(results);
        }

        private void SendLiveLinqEventForAddOrUpdate(IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            var resultsGrouped = results.GroupBy(x => x.Value.Result)
                .ToImmutableDictionary(x => x.Key, x => x.ToImmutableList());
            OnRemove(resultsGrouped[DictionaryItemAddOrUpdateResult.Update]
                .Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.ExistingValue!)));
            var addedByUpdate = resultsGrouped[DictionaryItemAddOrUpdateResult.Update]
                .Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue));
            var addedBySimpleAdd = resultsGrouped[DictionaryItemAddOrUpdateResult.Add]
                .Select(kvp => new KeyValue<TKey, TValue>(kvp.Key, kvp.Value.NewValue));
            OnAdd(addedByUpdate.Concat(addedBySimpleAdd));
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _state.AddOrUpdateRange(newItems, out results);
            SendLiveLinqEventForAddOrUpdate(results);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _state.AddOrUpdateRange(newItems, key, value, out results);
            SendLiveLinqEventForAddOrUpdate(results);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValue<TKey, TValue>> newItems)
        {
            _state.AddOrUpdateRange(newItems, out var results);
            SendLiveLinqEventForAddOrUpdate(results);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _state.AddOrUpdateRange(newItems, out var results);
            SendLiveLinqEventForAddOrUpdate(results);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _state.AddOrUpdateRange(newItems, key, value, out var results);
            SendLiveLinqEventForAddOrUpdate(results);
        }

        public void AddOrUpdateRange(params IKeyValue<TKey, TValue>[] newItems)
        {
            _state.AddOrUpdateRange(newItems, out var results);
            SendLiveLinqEventForAddOrUpdate(results);
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _state.AddOrUpdateRange(newItems, out var results);
            SendLiveLinqEventForAddOrUpdate(results);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _state.TryRemoveRange(keysToRemove, out var results);
            OnRemove(results);
        }
        
        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _state.RemoveRange(keysToRemove, out var results);
            OnRemove(results);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            _state.RemoveWhere(predicate, out var results);
            OnRemove(results);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            _state.RemoveWhere(predicate, out var results);
            OnRemove(results);
        }

        public void Clear()
        {
            _state.Clear(out var results);
            OnRemove(results);
        }

        public bool TryRemove(TKey key)
        {
            if (_state.TryRemove(key, out var removedItem))
            {
                OnRemove(new KeyValue<TKey, TValue>(key, removedItem));
                return true;
            }

            return false;
        }

        public void Remove(TKey key)
        {
            _state.Remove(key, out var removedItem);
            OnRemove(new KeyValue<TKey, TValue>(key, removedItem));
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _state.TryRemoveRange(keysToRemove, out removedItems);
            OnRemove(removedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _state.RemoveRange(keysToRemove, out removedItems);
            OnRemove(removedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _state.RemoveWhere(predicate, out removedItems);
            OnRemove(removedItems);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _state.RemoveWhere(predicate, out removedItems);
            OnRemove(removedItems);
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _state.Clear(out removedItems);
            OnRemove(removedItems);
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            if (_state.TryRemove(key, out removedItem))
            {
                OnRemove(new KeyValue<TKey, TValue>(key, removedItem));
                
                return true;
            }

            return false;
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            _state.Remove(key, out removedItem);
            OnRemove(new KeyValue<TKey, TValue>(key, removedItem));
        }

        public TValue this[TKey key]
        {
            get => _state[key];
            set
            {
                AddOrUpdate(key, value);
            }
        }
    }
}