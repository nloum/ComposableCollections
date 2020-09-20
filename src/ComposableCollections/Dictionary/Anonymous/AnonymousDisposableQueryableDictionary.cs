using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousDisposableQueryableDictionary<TKey, TValue> : IDisposableQueryableDictionary<TKey, TValue> {
private IComposableDictionary<TKey, TValue> _composableDictionary;
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IDisposable _disposable;
public AnonymousDisposableQueryableDictionary(IComposableDictionary<TKey, TValue> composableDictionary, IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IDisposable disposable) {
_composableDictionary = composableDictionary;
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_disposable = disposable;
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _composableDictionary.GetEnumerator();
}
void IComposableDictionary<TKey, TValue>.SetValue( TKey key,  TValue value) {
_composableDictionary.SetValue( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryGetValue( TKey key,  out TValue value) {
return _composableDictionary.TryGetValue( key,  out value);
}
TValue IComposableDictionary<TKey, TValue>.this[ TKey key] {
get => _composableDictionary[ key];
set => _composableDictionary[ key] = value;
}
void IComposableDictionary<TKey, TValue>.Write( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.Write.DictionaryWrite<TKey, TValue>> writes,  out System.Collections.Generic.IReadOnlyList<ComposableCollections.Dictionary.Write.DictionaryWriteResult<TKey, TValue>> results) {
_composableDictionary.Write( writes,  out results);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  TValue value) {
return _composableDictionary.TryAdd( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  System.Func<TValue> value) {
return _composableDictionary.TryAdd( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  System.Func<TValue> value,  out TValue existingValue,  out TValue newValue) {
return _composableDictionary.TryAdd( key,  value,  out existingValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_composableDictionary.TryAddRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_composableDictionary.TryAddRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_composableDictionary.TryAddRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.TryAddRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.Add( TKey key,  TValue value) {
_composableDictionary.Add( key,  value);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.AddRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.AddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.AddRange( newItems);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  TValue value) {
return _composableDictionary.TryUpdate( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  TValue value,  out TValue previousValue) {
return _composableDictionary.TryUpdate( key,  value,  out previousValue);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  System.Func<TValue, TValue> value,  out TValue previousValue,  out TValue newValue) {
return _composableDictionary.TryUpdate( key,  value,  out previousValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.TryUpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.TryUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.TryUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.TryUpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.Update( TKey key,  TValue value) {
_composableDictionary.Update( key,  value);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.UpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.Update( TKey key,  TValue value,  out TValue previousValue) {
_composableDictionary.Update( key,  value,  out previousValue);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.UpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.UpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_composableDictionary.UpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.UpdateRange( newItems);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  TValue value) {
return _composableDictionary.AddOrUpdate( key,  value);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating) {
return _composableDictionary.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating,  out TValue previousValue,  out TValue newValue) {
return _composableDictionary.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating,  out previousValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_composableDictionary.AddOrUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_composableDictionary.AddOrUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_composableDictionary.AddOrUpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_composableDictionary.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_composableDictionary.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_composableDictionary.AddOrUpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_composableDictionary.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_composableDictionary.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_composableDictionary.TryRemoveRange( keysToRemove);
}
void IComposableDictionary<TKey, TValue>.RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_composableDictionary.RemoveRange( keysToRemove);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<TKey, TValue, bool> predicate) {
_composableDictionary.RemoveWhere( predicate);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate) {
_composableDictionary.RemoveWhere( predicate);
}
void IComposableDictionary<TKey, TValue>.Clear() {
_composableDictionary.Clear();
}
bool IComposableDictionary<TKey, TValue>.TryRemove( TKey key) {
return _composableDictionary.TryRemove( key);
}
void IComposableDictionary<TKey, TValue>.Remove( TKey key) {
_composableDictionary.Remove( key);
}
void IComposableDictionary<TKey, TValue>.TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.TryRemoveRange( keysToRemove,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.RemoveRange( keysToRemove,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<TKey, TValue, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.RemoveWhere( predicate,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.RemoveWhere( predicate,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.Clear( out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_composableDictionary.Clear( out removedItems);
}
bool IComposableDictionary<TKey, TValue>.TryRemove( TKey key,  out TValue removedItem) {
return _composableDictionary.TryRemove( key,  out removedItem);
}
void IComposableDictionary<TKey, TValue>.Remove( TKey key,  out TValue removedItem) {
_composableDictionary.Remove( key,  out removedItem);
}
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _composableDictionary.GetEnumerator();
}
int IReadOnlyCollection<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.Count => _composableDictionary.Count;
System.Collections.Generic.IEqualityComparer<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Comparer => _composableDictionary.Comparer;
TValue IComposableReadOnlyDictionary<TKey, TValue>.GetValue( TKey key) {
return _composableDictionary.GetValue( key);
}
TValue IComposableReadOnlyDictionary<TKey, TValue>.this[ TKey key] => _composableDictionary[ key];
System.Collections.Generic.IEnumerable<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Keys => _composableDictionary.Keys;
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _composableDictionary.Values;
bool IComposableReadOnlyDictionary<TKey, TValue>.ContainsKey( TKey key) {
return _composableDictionary.ContainsKey( key);
}
IMaybe<TValue> IComposableReadOnlyDictionary<TKey, TValue>.TryGetValue( TKey key) {
return _composableDictionary.TryGetValue( key);
}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _queryableReadOnlyDictionary.Values;
void IDisposable.Dispose() {
_disposable.Dispose();
}
}
}

