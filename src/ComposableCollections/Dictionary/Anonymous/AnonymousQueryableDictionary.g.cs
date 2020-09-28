using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.Dictionary.Anonymous {
public class AnonymousQueryableDictionary<TKey, TValue> : IQueryableDictionary<TKey, TValue> {
private readonly IComposableDictionary<TKey, TValue> _composableDictionary;
private readonly Func<IQueryable<TValue>> _getValues;
public AnonymousQueryableDictionary(IComposableDictionary<TKey, TValue> composableDictionary, Func<IQueryable<TValue>> getValues) {
_composableDictionary = composableDictionary;
_getValues = getValues;
}
public virtual TValue this[ TKey key] {
get => _composableDictionary[ key];
set => _composableDictionary[ key] = value;
}
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _composableDictionary.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _composableDictionary.Keys;
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _composableDictionary.Values;
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _getValues();
public virtual int Count => _composableDictionary.Count;
public virtual void SetValue( TKey key,  TValue value) {
_composableDictionary.SetValue( key,  value);
}
public virtual bool TryGetValue( TKey key,  out TValue value) {
return _composableDictionary.TryGetValue( key,  out value);
}
public virtual void Write( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.Write.DictionaryWrite<TKey, TValue>> writes,  out System.Collections.Generic.IReadOnlyList<ComposableCollections.Dictionary.Write.DictionaryWriteResult<TKey, TValue>> results) {
_composableDictionary.Write( writes,  out results);
}
public virtual bool TryAdd( TKey key,  TValue value) {
return _composableDictionary.TryAdd( key,  value);
}
public virtual bool TryAdd( TKey key,  System.Func<TValue> value) {
return _composableDictionary.TryAdd( key,  value);
}
public virtual bool TryAdd( TKey key,  System.Func<TValue> value,  out TValue existingValue,  out TValue newValue) {
return _composableDictionary.TryAdd( key,  value,  out existingValue,  out newValue);
}
public virtual void TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_composableDictionary.TryAddRange( newItems,  out results);
}
public virtual void TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_composableDictionary.TryAddRange( newItems,  out results);
}
public virtual void TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_composableDictionary.TryAddRange( newItems,  key,  value,  out results);
}
public virtual void TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.TryAddRange( newItems);
}
public virtual void TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.TryAddRange( newItems);
}
public virtual void TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.TryAddRange( newItems,  key,  value);
}
public virtual void TryAddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.TryAddRange( newItems);
}
public virtual void TryAddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.TryAddRange( newItems);
}
public virtual void Add( TKey key,  TValue value) {
_composableDictionary.Add( key,  value);
}
public virtual void AddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.AddRange( newItems);
}
public virtual void AddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.AddRange( newItems);
}
public virtual void AddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.AddRange( newItems,  key,  value);
}
public virtual void AddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.AddRange( newItems);
}
public virtual void AddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.AddRange( newItems);
}
public virtual bool TryUpdate( TKey key,  TValue value) {
return _composableDictionary.TryUpdate( key,  value);
}
public virtual bool TryUpdate( TKey key,  TValue value,  out TValue previousValue) {
return _composableDictionary.TryUpdate( key,  value,  out previousValue);
}
public virtual bool TryUpdate( TKey key,  System.Func<TValue, TValue> value,  out TValue previousValue,  out TValue newValue) {
return _composableDictionary.TryUpdate( key,  value,  out previousValue,  out newValue);
}
public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.TryUpdateRange( newItems);
}
public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.TryUpdateRange( newItems);
}
public virtual void TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.TryUpdateRange( newItems,  key,  value);
}
public virtual void TryUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.TryUpdateRange( newItems);
}
public virtual void TryUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.TryUpdateRange( newItems);
}
public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.TryUpdateRange( newItems,  out results);
}
public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.TryUpdateRange( newItems,  out results);
}
public virtual void TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.TryUpdateRange( newItems,  key,  value,  out results);
}
public virtual void Update( TKey key,  TValue value) {
_composableDictionary.Update( key,  value);
}
public virtual void UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.UpdateRange( newItems);
}
public virtual void UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.UpdateRange( newItems);
}
public virtual void UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.UpdateRange( newItems,  key,  value);
}
public virtual void Update( TKey key,  TValue value,  out TValue previousValue) {
_composableDictionary.Update( key,  value,  out previousValue);
}
public virtual void UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.UpdateRange( newItems,  out results);
}
public virtual void UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.UpdateRange( newItems,  out results);
}
public virtual void UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.UpdateRange( newItems,  key,  value,  out results);
}
public virtual void UpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.UpdateRange( newItems);
}
public virtual void UpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.UpdateRange( newItems);
}
public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TKey key,  TValue value) {
return _composableDictionary.AddOrUpdate( key,  value);
}
public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating) {
return _composableDictionary.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating);
}
public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating,  out TValue previousValue,  out TValue newValue) {
return _composableDictionary.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating,  out previousValue,  out newValue);
}
public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_composableDictionary.AddOrUpdateRange( newItems,  out results);
}
public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_composableDictionary.AddOrUpdateRange( newItems,  out results);
}
public virtual void AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_composableDictionary.AddOrUpdateRange( newItems,  key,  value,  out results);
}
public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.AddOrUpdateRange( newItems);
}
public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.AddOrUpdateRange( newItems);
}
public virtual void AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.AddOrUpdateRange( newItems,  key,  value);
}
public virtual void AddOrUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.AddOrUpdateRange( newItems);
}
public virtual void AddOrUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.AddOrUpdateRange( newItems);
}
public virtual void TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_composableDictionary.TryRemoveRange( keysToRemove);
}
public virtual void RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_composableDictionary.RemoveRange( keysToRemove);
}
public virtual void RemoveWhere( System.Func<TKey, TValue, bool> predicate) {
_composableDictionary.RemoveWhere( predicate);
}
public virtual void RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate) {
_composableDictionary.RemoveWhere( predicate);
}
public virtual void Clear() {
_composableDictionary.Clear();
}
public virtual bool TryRemove( TKey key) {
return _composableDictionary.TryRemove( key);
}
public virtual void Remove( TKey key) {
_composableDictionary.Remove( key);
}
public virtual void TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.TryRemoveRange( keysToRemove,  out removedItems);
}
public virtual void RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.RemoveRange( keysToRemove,  out removedItems);
}
public virtual void RemoveWhere( System.Func<TKey, TValue, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.RemoveWhere( predicate,  out removedItems);
}
public virtual void RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.RemoveWhere( predicate,  out removedItems);
}
public virtual void Clear( out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.Clear( out removedItems);
}
public virtual bool TryRemove( TKey key,  out TValue removedItem) {
return _composableDictionary.TryRemove( key,  out removedItem);
}
public virtual void Remove( TKey key,  out TValue removedItem) {
_composableDictionary.Remove( key,  out removedItem);
}
public virtual TValue GetValue( TKey key) {
return _composableDictionary.GetValue( key);
}
public virtual bool ContainsKey( TKey key) {
return _composableDictionary.ContainsKey( key);
}
public virtual IMaybe<TValue> TryGetValue( TKey key) {
return _composableDictionary.TryGetValue( key);
}
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _composableDictionary.GetEnumerator();
}
IEnumerator IEnumerable.GetEnumerator() {
return _composableDictionary.GetEnumerator();}
}
}

