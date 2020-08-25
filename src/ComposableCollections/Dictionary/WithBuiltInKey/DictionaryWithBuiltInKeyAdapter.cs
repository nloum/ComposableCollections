using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;

namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public class DictionaryWithBuiltInKeyAdapter<TKey, TValue> : ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>, IDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IComposableDictionary<TKey, TValue> _source;

        public DictionaryWithBuiltInKeyAdapter(IComposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        protected DictionaryWithBuiltInKeyAdapter()
        {
        }

        public IComposableDictionary<TKey, TValue> AsComposableDictionary()
        {
            return _source;
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey)
        {
            _source = source;
            base.Initialize(source, getKey);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public new TValue this[TKey key]
        {
            get => _source[key];
            set => _source[key] = value;
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _source.TryGetValue(key, out value);
        }
        
        public bool TryAdd(TValue value)
        {
            return _source.TryAdd(GetKey(value), value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            return _source.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value, out TValue existingValue, out TValue newValue)
        {
            return _source.TryAdd(key, value, out existingValue, out newValue);
        }

        public void TryAddRange(IEnumerable<TValue> newItems, out IComposableReadOnlyDictionary<TKey, IDictionaryItemAddAttempt<TValue>> results)
        {
            _source.TryAddRange(newItems, GetKey, x => x, out results);
        }

        public void TryAddRange(params TValue[] newItems)
        {
            _source.TryAddRange(newItems.AsEnumerable(), GetKey, x => x);
        }

        public void Add(TValue value)
        {
            _source.Add(GetKey(value), value);
        }

        public void AddRange(IEnumerable<TValue> newItems)
        {
            _source.AddRange(newItems, GetKey, x => x);
        }

        public void AddRange(params TValue[] newItems)
        {
            _source.AddRange(newItems.AsEnumerable(), GetKey, x => x);
        }

        public bool TryUpdate(TValue value)
        {
            return _source.TryUpdate(GetKey(value), value);
        }

        public bool TryUpdate(TValue value, out TValue previousValue)
        {
            return _source.TryUpdate(GetKey(value), value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue, out TValue newValue)
        {
            return _source.TryUpdate(key, value, out previousValue, out newValue);
        }

        public void TryUpdateRange(params TValue[] newItems)
        {
            _source.TryUpdateRange(newItems, GetKey, x => x);
        }

        public void TryUpdateRange(IEnumerable<TValue> newItems)
        {
            _source.TryUpdateRange(newItems, GetKey, x => x);
        }

        public void TryUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.TryUpdateRange(newItems, GetKey, x => x, out var resultsInner);
            results = new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, IDictionaryItemUpdateAttempt<TValue>>(resultsInner, x => x.NewValue.Select(GetKey).Otherwise(() => GetKey(x.ExistingValue.Value)));
        }

        public void Update(TValue value)
        {
            _source.Update(GetKey(value), value);
        }

        public void Update(TValue value, out TValue previousValue)
        {
            _source.Update(GetKey(value), value, out previousValue);
        }

        public void UpdateRange(params TValue[] newItems)
        {
            _source.UpdateRange(newItems, GetKey, x => x);
        }

        public void UpdateRange(IEnumerable<TValue> newItems)
        {
            _source.UpdateRange(newItems, GetKey, x => x);
        }

        public void UpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemUpdateAttempt<TValue>> results)
        {
            _source.UpdateRange(newItems, GetKey, x => x, out var innerResults);
            results = new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, IDictionaryItemUpdateAttempt<TValue>>(innerResults, x => x.NewValue.Select(GetKey).Otherwise(() => GetKey(x.ExistingValue.Value)));
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TValue value)
        {
            return _source.AddOrUpdate(GetKey(value), value);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return _source.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating,
            out TValue previousValue, out TValue newValue)
        {
            return _source.AddOrUpdate(key, valueIfAdding, valueIfUpdating, out previousValue, out newValue);
        }

        public void AddOrUpdateRange(IEnumerable<TValue> newItems, out IReadOnlyDictionaryWithBuiltInKey<TKey, IDictionaryItemAddOrUpdate<TValue>> results)
        {
            _source.AddOrUpdateRange(newItems, GetKey, x => x, out var innerResult);
            results = new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, IDictionaryItemAddOrUpdate<TValue>>(innerResult, x => GetKey(x.NewValue));
        }

        public void AddOrUpdateRange(params TValue[] newItems)
        {
            _source.AddOrUpdateRange(newItems, GetKey, x => x);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _source.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _source.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            _source.RemoveWhere(predicate);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate)
        {
            _source.RemoveWhere(predicate);
        }

        public void Clear()
        {
            _source.Clear();
        }

        public bool TryRemove(TKey key)
        {
            return _source.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            _source.Remove(key);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _source.TryRemoveRange(keysToRemove, out var innerRemovedItems);
            removedItems = new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(innerRemovedItems, GetKey);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _source.RemoveRange(keysToRemove, out var innerRemovedItems);
            removedItems = new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(innerRemovedItems, GetKey);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _source.RemoveWhere(predicate, out var innerRemovedItems);
            removedItems = new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(innerRemovedItems, GetKey);
        }

        public void RemoveWhere(Func<IKeyValue<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems)
        {
            _source.RemoveWhere(predicate, out var innerRemovedItems);
            removedItems = new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(innerRemovedItems, GetKey);
        }

        public void Clear(out IComposableReadOnlyDictionary<TKey, TValue> removedItems)
        {
            _source.Clear(out removedItems);
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            return _source.TryRemove(key, out removedItem);
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            _source.Remove(key, out removedItem);
        }
    }
}