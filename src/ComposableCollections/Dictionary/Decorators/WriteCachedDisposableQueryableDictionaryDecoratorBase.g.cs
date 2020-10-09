using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedDisposableQueryableDictionaryDecoratorBase<TKey, TValue> : IWriteCachedDisposableQueryableDictionary<TKey, TValue> {
private readonly IWriteCachedDisposableQueryableDictionary<TKey, TValue> _decoratedObject;
public WriteCachedDisposableQueryableDictionaryDecoratorBase(IWriteCachedDisposableQueryableDictionary<TKey, TValue> decoratedObject) {
_decoratedObject = decoratedObject;
}
int IReadOnlyCollection<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.Count => _decoratedObject.Count;
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
void IDisposable.Dispose() {
_decoratedObject.Dispose();
}
void IComposableDictionary<TKey, TValue>.SetValue( TKey key,  TValue value) {
_decoratedObject.SetValue( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryGetValue( TKey key,  out TValue value) {
return _decoratedObject.TryGetValue( key,  out value);
}
TValue IComposableDictionary<TKey, TValue>.this[ TKey key] {
get => _decoratedObject[ key];
set => _decoratedObject[ key] = value;
}
void IComposableDictionary<TKey, TValue>.Write( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.Write.DictionaryWrite<TKey, TValue>> writes,  out System.Collections.Generic.IReadOnlyList<ComposableCollections.Dictionary.Write.DictionaryWriteResult<TKey, TValue>> results) {
_decoratedObject.Write( writes,  out results);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  TValue value) {
return _decoratedObject.TryAdd( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  System.Func<TValue> value) {
return _decoratedObject.TryAdd( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryAdd( TKey key,  System.Func<TValue> value,  out TValue existingValue,  out TValue newValue) {
return _decoratedObject.TryAdd( key,  value,  out existingValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_decoratedObject.TryAddRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_decoratedObject.TryAddRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_decoratedObject.TryAddRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_decoratedObject.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_decoratedObject.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_decoratedObject.TryAddRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_decoratedObject.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryAddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_decoratedObject.TryAddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.Add( TKey key,  TValue value) {
_decoratedObject.Add( key,  value);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_decoratedObject.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_decoratedObject.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_decoratedObject.AddRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.AddRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_decoratedObject.AddRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_decoratedObject.AddRange( newItems);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  TValue value) {
return _decoratedObject.TryUpdate( key,  value);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  TValue value,  out TValue previousValue) {
return _decoratedObject.TryUpdate( key,  value,  out previousValue);
}
bool IComposableDictionary<TKey, TValue>.TryUpdate( TKey key,  System.Func<TValue, TValue> value,  out TValue previousValue,  out TValue newValue) {
return _decoratedObject.TryUpdate( key,  value,  out previousValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_decoratedObject.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_decoratedObject.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_decoratedObject.TryUpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_decoratedObject.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_decoratedObject.TryUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_decoratedObject.TryUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_decoratedObject.TryUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.TryUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_decoratedObject.TryUpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.Update( TKey key,  TValue value) {
_decoratedObject.Update( key,  value);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_decoratedObject.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_decoratedObject.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_decoratedObject.UpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.Update( TKey key,  TValue value,  out TValue previousValue) {
_decoratedObject.Update( key,  value,  out previousValue);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_decoratedObject.UpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_decoratedObject.UpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_decoratedObject.UpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_decoratedObject.UpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.UpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_decoratedObject.UpdateRange( newItems);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  TValue value) {
return _decoratedObject.AddOrUpdate( key,  value);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating) {
return _decoratedObject.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IComposableDictionary<TKey, TValue>.AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating,  out TValue previousValue,  out TValue newValue) {
return _decoratedObject.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating,  out previousValue,  out newValue);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_decoratedObject.AddOrUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_decoratedObject.AddOrUpdateRange( newItems,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_decoratedObject.AddOrUpdateRange( newItems,  key,  value,  out results);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> newItems) {
_decoratedObject.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> newItems) {
_decoratedObject.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange<TKeyValuePair>( System.Collections.Generic.IEnumerable<TKeyValuePair> newItems,  System.Func<TKeyValuePair, TKey> key,  System.Func<TKeyValuePair, TValue> value) {
_decoratedObject.AddOrUpdateRange( newItems,  key,  value);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( ComposableCollections.Dictionary.IKeyValue<TKey, TValue>[] newItems) {
_decoratedObject.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.KeyValuePair<TKey, TValue>[] newItems) {
_decoratedObject.AddOrUpdateRange( newItems);
}
void IComposableDictionary<TKey, TValue>.TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_decoratedObject.TryRemoveRange( keysToRemove);
}
void IComposableDictionary<TKey, TValue>.RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_decoratedObject.RemoveRange( keysToRemove);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<TKey, TValue, bool> predicate) {
_decoratedObject.RemoveWhere( predicate);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate) {
_decoratedObject.RemoveWhere( predicate);
}
void IComposableDictionary<TKey, TValue>.Clear() {
_decoratedObject.Clear();
}
bool IComposableDictionary<TKey, TValue>.TryRemove( TKey key) {
return _decoratedObject.TryRemove( key);
}
void IComposableDictionary<TKey, TValue>.Remove( TKey key) {
_decoratedObject.Remove( key);
}
void IComposableDictionary<TKey, TValue>.TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_decoratedObject.TryRemoveRange( keysToRemove,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_decoratedObject.RemoveRange( keysToRemove,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<TKey, TValue, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_decoratedObject.RemoveWhere( predicate,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_decoratedObject.RemoveWhere( predicate,  out removedItems);
}
void IComposableDictionary<TKey, TValue>.Clear( out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_decoratedObject.Clear( out removedItems);
}
bool IComposableDictionary<TKey, TValue>.TryRemove( TKey key,  out TValue removedItem) {
return _decoratedObject.TryRemove( key,  out removedItem);
}
void IComposableDictionary<TKey, TValue>.Remove( TKey key,  out TValue removedItem) {
_decoratedObject.Remove( key,  out removedItem);
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
System.Collections.Generic.IEqualityComparer<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Comparer => _decoratedObject.Comparer;
TValue IComposableReadOnlyDictionary<TKey, TValue>.GetValue( TKey key) {
return _decoratedObject.GetValue( key);
}
TValue IComposableReadOnlyDictionary<TKey, TValue>.this[ TKey key] => _decoratedObject[ key];
System.Collections.Generic.IEnumerable<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Keys => _decoratedObject.Keys;
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _decoratedObject.Values;
bool IComposableReadOnlyDictionary<TKey, TValue>.ContainsKey( TKey key) {
return _decoratedObject.ContainsKey( key);
}
IMaybe<TValue> IComposableReadOnlyDictionary<TKey, TValue>.TryGetValue( TKey key) {
return _decoratedObject.TryGetValue( key);
}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _decoratedObject.Values;
void IWriteCachedDictionary<TKey, TValue>.FlushCache() {
_decoratedObject.FlushCache();
}
}
}

