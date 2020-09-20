using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousWriteCachedQueryableDictionary<TKey, TValue> : IWriteCachedQueryableDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IWriteCachedDictionary<TKey, TValue> _writeCachedDictionary;
public AnonymousWriteCachedQueryableDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IWriteCachedDictionary<TKey, TValue> writeCachedDictionary) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_writeCachedDictionary = writeCachedDictionary;
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _queryableReadOnlyDictionary.GetEnumerator();
}
int IReadOnlyCollection<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.Count => _queryableReadOnlyDictionary.Count;
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _queryableReadOnlyDictionary.GetEnumerator();
}
System.Collections.Generic.IEqualityComparer<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Comparer => _queryableReadOnlyDictionary.Comparer;
TValue IComposableReadOnlyDictionary<TKey, TValue>.GetValue( TKey key) {
return _queryableReadOnlyDictionary.GetValue( key);
}
TValue IComposableReadOnlyDictionary<TKey, TValue>.this[ TKey key] => _queryableReadOnlyDictionary[ key];
System.Collections.Generic.IEnumerable<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Keys => _queryableReadOnlyDictionary.Keys;
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _queryableReadOnlyDictionary.Values;
bool IComposableReadOnlyDictionary<TKey, TValue>.ContainsKey( TKey key) {
return _queryableReadOnlyDictionary.ContainsKey( key);
}
IMaybe<TValue> IComposableReadOnlyDictionary<TKey, TValue>.TryGetValue( TKey key) {
return _queryableReadOnlyDictionary.TryGetValue( key);
}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _queryableReadOnlyDictionary.Values;
void IComposableDictionary<TKey, TValue>.SetValue( TKey key,  TValue value) {
_writeCachedDictionary.SetValue( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryGetValue( TKey key,  out TValue value) {
return _writeCachedDictionary.TryGetValue( key,  out value);
}
TValue IComposableDictionary<TKey, TValue>.this[ TKey key] {
get => _writeCachedDictionary[ key];
set => _writeCachedDictionary[ key] = value;
}
void IComposableDictionary<TKey, TValue>.Write( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.Write.DictionaryWrite<TKey, TValue>> writes,  out System.Collections.Generic.IReadOnlyList<ComposableCollections.Dictionary.Write.DictionaryWriteResult<TKey, TValue>> results) {
_writeCachedDictionary.Write( writes,  out results);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  TValue value) {
return _writeCachedDictionary.TryAdd( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  System.Func<TValue> value) {
return _writeCachedDictionary.TryAdd( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  System.Func<TValue> value,  out TValue existingValue,  out TValue newValue) {
return _writeCachedDictionary.TryAdd( key,  value,  out existingValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_writeCachedDictionary.TryAddRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_writeCachedDictionary.TryAddRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_writeCachedDictionary.TryAddRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_writeCachedDictionary.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_writeCachedDictionary.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_writeCachedDictionary.TryAddRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_writeCachedDictionary.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_writeCachedDictionary.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.Add( TKey key,  TValue value) {
_writeCachedDictionary.Add( key,  value);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_writeCachedDictionary.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_writeCachedDictionary.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_writeCachedDictionary.AddRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.AddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_writeCachedDictionary.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_writeCachedDictionary.AddRange( newItems);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  TValue value) {
return _writeCachedDictionary.TryUpdate( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  TValue value,  out TValue previousValue) {
return _writeCachedDictionary.TryUpdate( key,  value,  out previousValue);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  System.Func<TValue, TValue> value,  out TValue previousValue,  out TValue newValue) {
return _writeCachedDictionary.TryUpdate( key,  value,  out previousValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_writeCachedDictionary.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_writeCachedDictionary.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_writeCachedDictionary.TryUpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_writeCachedDictionary.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_writeCachedDictionary.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_writeCachedDictionary.TryUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_writeCachedDictionary.TryUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_writeCachedDictionary.TryUpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.Update( TKey key,  TValue value) {
_writeCachedDictionary.Update( key,  value);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_writeCachedDictionary.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_writeCachedDictionary.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_writeCachedDictionary.UpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.Update( TKey key,  TValue value,  out TValue previousValue) {
_writeCachedDictionary.Update( key,  value,  out previousValue);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_writeCachedDictionary.UpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_writeCachedDictionary.UpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_writeCachedDictionary.UpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_writeCachedDictionary.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_writeCachedDictionary.UpdateRange( newItems);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  TValue value) {
return _writeCachedDictionary.AddOrUpdate( key,  value);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating) {
return _writeCachedDictionary.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating,  out TValue previousValue,  out TValue newValue) {
return _writeCachedDictionary.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating,  out previousValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_writeCachedDictionary.AddOrUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_writeCachedDictionary.AddOrUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_writeCachedDictionary.AddOrUpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_writeCachedDictionary.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_writeCachedDictionary.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_writeCachedDictionary.AddOrUpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_writeCachedDictionary.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_writeCachedDictionary.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_writeCachedDictionary.TryRemoveRange( keysToRemove);
}
void IComposableDictionary<TKey, TValue>.RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_writeCachedDictionary.RemoveRange( keysToRemove);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<TKey, TValue, bool> predicate) {
_writeCachedDictionary.RemoveWhere( predicate);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate) {
_writeCachedDictionary.RemoveWhere( predicate);
}
void IComposableDictionary<TKey, TValue>.Clear() {
_writeCachedDictionary.Clear();
}
bool IComposableDictionary<TKey, TValue>.TryRemove( TKey key) {
return _writeCachedDictionary.TryRemove( key);
}
void IComposableDictionary<TKey, TValue>.Remove( TKey key) {
_writeCachedDictionary.Remove( key);
}
void IComposableDictionary<TKey, TValue>.TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_writeCachedDictionary.TryRemoveRange( keysToRemove,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_writeCachedDictionary.RemoveRange( keysToRemove,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<TKey, TValue, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_writeCachedDictionary.RemoveWhere( predicate,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_writeCachedDictionary.RemoveWhere( predicate,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.Clear( out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_writeCachedDictionary.Clear( out removedItems);
}
bool IComposableDictionary<TKey, TValue>.TryRemove( TKey key,  out TValue removedItem) {
return _writeCachedDictionary.TryRemove( key,  out removedItem);
}
void IComposableDictionary<TKey, TValue>.Remove( TKey key,  out TValue removedItem) {
_writeCachedDictionary.Remove( key,  out removedItem);
}
void IWriteCachedDictionary<TKey, TValue>.FlushCache() {
_writeCachedDictionary.FlushCache();
}
}
}

