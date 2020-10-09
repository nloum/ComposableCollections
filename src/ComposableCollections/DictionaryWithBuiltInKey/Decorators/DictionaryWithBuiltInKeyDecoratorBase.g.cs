using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.DictionaryWithBuiltInKey.Decorators {
public class DictionaryWithBuiltInKeyDecoratorBase<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IDictionaryWithBuiltInKey<TKey, TValue> _decoratedObject;
public DictionaryWithBuiltInKeyDecoratorBase(IDictionaryWithBuiltInKey<TKey, TValue> decoratedObject) {
_decoratedObject = decoratedObject;
}
System.Collections.Generic.IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.AsComposableReadOnlyDictionary() {
return _decoratedObject.AsComposableReadOnlyDictionary();
}
TKey IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.GetKey( TValue value) {
return _decoratedObject.GetKey( value);
}
System.Collections.Generic.IEqualityComparer<TKey> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Comparer => _decoratedObject.Comparer;
TValue IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.GetValue( TKey key) {
return _decoratedObject.GetValue( key);
}
TValue IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.this[ TKey key] => _decoratedObject[ key];
System.Collections.Generic.IEnumerable<TKey> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Keys => _decoratedObject.Keys;
System.Collections.Generic.IEnumerable<TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _decoratedObject.Values;
bool IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.ContainsKey( TKey key) {
return _decoratedObject.ContainsKey( key);
}
IMaybe<TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.TryGetValue( TKey key) {
return _decoratedObject.TryGetValue( key);
}
ComposableCollections.Dictionary.Interfaces.IComposableDictionary<TKey, TValue> IDictionaryWithBuiltInKey<TKey, TValue>.AsComposableDictionary() {
return _decoratedObject.AsComposableDictionary();
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryGetValue( TKey key,  out TValue value) {
return _decoratedObject.TryGetValue( key,  out value);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.SetValue( TValue value) {
_decoratedObject.SetValue( value);
}
TValue IDictionaryWithBuiltInKey<TKey, TValue>.this[ TKey key] {
get => _decoratedObject[ key];
set => _decoratedObject[ key] = value;
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryAdd( TValue value) {
return _decoratedObject.TryAdd( value);
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryAdd( TKey key,  System.Func<TValue> value) {
return _decoratedObject.TryAdd( key,  value);
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryAdd( TKey key,  System.Func<TValue> value,  out TValue existingValue,  out TValue newValue) {
return _decoratedObject.TryAdd( key,  value,  out existingValue,  out newValue);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.TryAddRange( System.Collections.Generic.IEnumerable<TValue> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_decoratedObject.TryAddRange( newItems,  out results);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.TryAddRange( TValue[] newItems) {
_decoratedObject.TryAddRange( newItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.Add( TValue value) {
_decoratedObject.Add( value);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.AddRange( System.Collections.Generic.IEnumerable<TValue> newItems) {
_decoratedObject.AddRange( newItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.AddRange( TValue[] newItems) {
_decoratedObject.AddRange( newItems);
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryUpdate( TValue value) {
return _decoratedObject.TryUpdate( value);
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryUpdate( TValue value,  out TValue previousValue) {
return _decoratedObject.TryUpdate( value,  out previousValue);
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryUpdate( TKey key,  System.Func<TValue, TValue> value,  out TValue previousValue,  out TValue newValue) {
return _decoratedObject.TryUpdate( key,  value,  out previousValue,  out newValue);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.TryUpdateRange( TValue[] newItems) {
_decoratedObject.TryUpdateRange( newItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems) {
_decoratedObject.TryUpdateRange( newItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.TryUpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_decoratedObject.TryUpdateRange( newItems,  out results);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.Update( TValue value) {
_decoratedObject.Update( value);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.Update( TValue value,  out TValue previousValue) {
_decoratedObject.Update( value,  out previousValue);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.UpdateRange( TValue[] newItems) {
_decoratedObject.UpdateRange( newItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems) {
_decoratedObject.UpdateRange( newItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.UpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_decoratedObject.UpdateRange( newItems,  out results);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IDictionaryWithBuiltInKey<TKey, TValue>.AddOrUpdate( TValue value) {
return _decoratedObject.AddOrUpdate( value);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IDictionaryWithBuiltInKey<TKey, TValue>.AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating) {
return _decoratedObject.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating);
}
ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult IDictionaryWithBuiltInKey<TKey, TValue>.AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating,  out TValue previousValue,  out TValue newValue) {
return _decoratedObject.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating,  out previousValue,  out newValue);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.AddOrUpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_decoratedObject.AddOrUpdateRange( newItems,  out results);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.AddOrUpdateRange( TValue[] newItems) {
_decoratedObject.AddOrUpdateRange( newItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_decoratedObject.TryRemoveRange( keysToRemove);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_decoratedObject.RemoveRange( keysToRemove);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.RemoveWhere( System.Func<TKey, TValue, bool> predicate) {
_decoratedObject.RemoveWhere( predicate);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate) {
_decoratedObject.RemoveWhere( predicate);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.Clear() {
_decoratedObject.Clear();
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryRemove( TKey key) {
return _decoratedObject.TryRemove( key);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.Remove( TKey key) {
_decoratedObject.Remove( key);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems) {
_decoratedObject.TryRemoveRange( keysToRemove,  out removedItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems) {
_decoratedObject.RemoveRange( keysToRemove,  out removedItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.RemoveWhere( System.Func<TKey, TValue, bool> predicate,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems) {
_decoratedObject.RemoveWhere( predicate,  out removedItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems) {
_decoratedObject.RemoveWhere( predicate,  out removedItems);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.Clear( out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_decoratedObject.Clear( out removedItems);
}
bool IDictionaryWithBuiltInKey<TKey, TValue>.TryRemove( TKey key,  out TValue removedItem) {
return _decoratedObject.TryRemove( key,  out removedItem);
}
void IDictionaryWithBuiltInKey<TKey, TValue>.Remove( TKey key,  out TValue removedItem) {
_decoratedObject.Remove( key,  out removedItem);
}
int IReadOnlyCollection<TValue>.Count => _decoratedObject.Count;
}
}

