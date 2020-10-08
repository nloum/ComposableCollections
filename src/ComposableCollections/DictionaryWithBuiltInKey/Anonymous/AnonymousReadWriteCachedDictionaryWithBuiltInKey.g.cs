using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.DictionaryWithBuiltInKey.Anonymous {
public class AnonymousReadWriteCachedDictionaryWithBuiltInKey<TKey, TValue> : IReadWriteCachedDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IDictionaryWithBuiltInKey<TKey, TValue> _dictionaryWithBuiltInKey;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue>> _asReadCachedReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IReadWriteCachedDictionary<TKey, TValue>> _asReadWriteCachedDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedDictionary<TKey, TValue>> _asWriteCachedDictionary;
private readonly Action _flushCache;
private readonly Action _reloadCache;
public AnonymousReadWriteCachedDictionaryWithBuiltInKey(IDictionaryWithBuiltInKey<TKey, TValue> dictionaryWithBuiltInKey, Func<ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue>> asReadCachedReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IReadWriteCachedDictionary<TKey, TValue>> asReadWriteCachedDictionary, Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedDictionary<TKey, TValue>> asWriteCachedDictionary, Action flushCache, Action reloadCache) {
_asReadWriteCachedDictionary = asReadWriteCachedDictionary;
_asWriteCachedDictionary = asWriteCachedDictionary;
_flushCache = flushCache;
_dictionaryWithBuiltInKey = dictionaryWithBuiltInKey;
_reloadCache = reloadCache;
_asReadCachedReadOnlyDictionary = asReadCachedReadOnlyDictionary;
}
public virtual TValue this[ TKey key] {
get => _dictionaryWithBuiltInKey[ key];
set => _dictionaryWithBuiltInKey[ key] = value;
}
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _dictionaryWithBuiltInKey.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _dictionaryWithBuiltInKey.Keys;
public virtual System.Collections.Generic.IEnumerable<TValue> Values => _dictionaryWithBuiltInKey.Values;
public virtual int Count => _dictionaryWithBuiltInKey.Count;
public virtual ComposableCollections.Dictionary.Interfaces.IReadWriteCachedDictionary<TKey, TValue> AsReadWriteCachedDictionary() {
return _asReadWriteCachedDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IWriteCachedDictionary<TKey, TValue> AsWriteCachedDictionary() {
return _asWriteCachedDictionary();
}
public virtual void FlushCache() {
_flushCache();
}
public virtual ComposableCollections.Dictionary.Interfaces.IComposableDictionary<TKey, TValue> AsComposableDictionary() {
return _dictionaryWithBuiltInKey.AsComposableDictionary();
}
public virtual bool TryGetValue( TKey key,  out TValue value) {
return _dictionaryWithBuiltInKey.TryGetValue( key,  out value);
}
public virtual void SetValue( TValue value) {
_dictionaryWithBuiltInKey.SetValue( value);
}
public virtual bool TryAdd( TValue value) {
return _dictionaryWithBuiltInKey.TryAdd( value);
}
public virtual bool TryAdd( TKey key,  System.Func<TValue> value) {
return _dictionaryWithBuiltInKey.TryAdd( key,  value);
}
public virtual bool TryAdd( TKey key,  System.Func<TValue> value,  out TValue existingValue,  out TValue newValue) {
return _dictionaryWithBuiltInKey.TryAdd( key,  value,  out existingValue,  out newValue);
}
public virtual void TryAddRange( System.Collections.Generic.IEnumerable<TValue> newItems,  out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddAttempt<TValue>> results) {
_dictionaryWithBuiltInKey.TryAddRange( newItems,  out results);
}
public virtual void TryAddRange( TValue[] newItems) {
_dictionaryWithBuiltInKey.TryAddRange( newItems);
}
public virtual void Add( TValue value) {
_dictionaryWithBuiltInKey.Add( value);
}
public virtual void AddRange( System.Collections.Generic.IEnumerable<TValue> newItems) {
_dictionaryWithBuiltInKey.AddRange( newItems);
}
public virtual void AddRange( TValue[] newItems) {
_dictionaryWithBuiltInKey.AddRange( newItems);
}
public virtual bool TryUpdate( TValue value) {
return _dictionaryWithBuiltInKey.TryUpdate( value);
}
public virtual bool TryUpdate( TValue value,  out TValue previousValue) {
return _dictionaryWithBuiltInKey.TryUpdate( value,  out previousValue);
}
public virtual bool TryUpdate( TKey key,  System.Func<TValue, TValue> value,  out TValue previousValue,  out TValue newValue) {
return _dictionaryWithBuiltInKey.TryUpdate( key,  value,  out previousValue,  out newValue);
}
public virtual void TryUpdateRange( TValue[] newItems) {
_dictionaryWithBuiltInKey.TryUpdateRange( newItems);
}
public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems) {
_dictionaryWithBuiltInKey.TryUpdateRange( newItems);
}
public virtual void TryUpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_dictionaryWithBuiltInKey.TryUpdateRange( newItems,  out results);
}
public virtual void Update( TValue value) {
_dictionaryWithBuiltInKey.Update( value);
}
public virtual void Update( TValue value,  out TValue previousValue) {
_dictionaryWithBuiltInKey.Update( value,  out previousValue);
}
public virtual void UpdateRange( TValue[] newItems) {
_dictionaryWithBuiltInKey.UpdateRange( newItems);
}
public virtual void UpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems) {
_dictionaryWithBuiltInKey.UpdateRange( newItems);
}
public virtual void UpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemUpdateAttempt<TValue>> results) {
_dictionaryWithBuiltInKey.UpdateRange( newItems,  out results);
}
public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TValue value) {
return _dictionaryWithBuiltInKey.AddOrUpdate( value);
}
public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating) {
return _dictionaryWithBuiltInKey.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating);
}
public virtual ComposableCollections.Dictionary.Write.DictionaryItemAddOrUpdateResult AddOrUpdate( TKey key,  System.Func<TValue> valueIfAdding,  System.Func<TValue, TValue> valueIfUpdating,  out TValue previousValue,  out TValue newValue) {
return _dictionaryWithBuiltInKey.AddOrUpdate( key,  valueIfAdding,  valueIfUpdating,  out previousValue,  out newValue);
}
public virtual void AddOrUpdateRange( System.Collections.Generic.IEnumerable<TValue> newItems,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, ComposableCollections.Dictionary.Write.IDictionaryItemAddOrUpdate<TValue>> results) {
_dictionaryWithBuiltInKey.AddOrUpdateRange( newItems,  out results);
}
public virtual void AddOrUpdateRange( TValue[] newItems) {
_dictionaryWithBuiltInKey.AddOrUpdateRange( newItems);
}
public virtual void TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_dictionaryWithBuiltInKey.TryRemoveRange( keysToRemove);
}
public virtual void RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove) {
_dictionaryWithBuiltInKey.RemoveRange( keysToRemove);
}
public virtual void RemoveWhere( System.Func<TKey, TValue, bool> predicate) {
_dictionaryWithBuiltInKey.RemoveWhere( predicate);
}
public virtual void RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate) {
_dictionaryWithBuiltInKey.RemoveWhere( predicate);
}
public virtual void Clear() {
_dictionaryWithBuiltInKey.Clear();
}
public virtual bool TryRemove( TKey key) {
return _dictionaryWithBuiltInKey.TryRemove( key);
}
public virtual void Remove( TKey key) {
_dictionaryWithBuiltInKey.Remove( key);
}
public virtual void TryRemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems) {
_dictionaryWithBuiltInKey.TryRemoveRange( keysToRemove,  out removedItems);
}
public virtual void RemoveRange( System.Collections.Generic.IEnumerable<TKey> keysToRemove,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems) {
_dictionaryWithBuiltInKey.RemoveRange( keysToRemove,  out removedItems);
}
public virtual void RemoveWhere( System.Func<TKey, TValue, bool> predicate,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems) {
_dictionaryWithBuiltInKey.RemoveWhere( predicate,  out removedItems);
}
public virtual void RemoveWhere( System.Func<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>, bool> predicate,  out ComposableCollections.DictionaryWithBuiltInKey.Interfaces.IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> removedItems) {
_dictionaryWithBuiltInKey.RemoveWhere( predicate,  out removedItems);
}
public virtual void Clear( out ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> removedItems) {
_dictionaryWithBuiltInKey.Clear( out removedItems);
}
public virtual bool TryRemove( TKey key,  out TValue removedItem) {
return _dictionaryWithBuiltInKey.TryRemove( key,  out removedItem);
}
public virtual void Remove( TKey key,  out TValue removedItem) {
_dictionaryWithBuiltInKey.Remove( key,  out removedItem);
}
public virtual ComposableCollections.Dictionary.Interfaces.IComposableReadOnlyDictionary<TKey, TValue> AsComposableReadOnlyDictionary() {
return _dictionaryWithBuiltInKey.AsComposableReadOnlyDictionary();
}
public virtual TKey GetKey( TValue value) {
return _dictionaryWithBuiltInKey.GetKey( value);
}
public virtual TValue GetValue( TKey key) {
return _dictionaryWithBuiltInKey.GetValue( key);
}
public virtual bool ContainsKey( TKey key) {
return _dictionaryWithBuiltInKey.ContainsKey( key);
}
public virtual IMaybe<TValue> TryGetValue( TKey key) {
return _dictionaryWithBuiltInKey.TryGetValue( key);
}
System.Collections.Generic.IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() {
return _dictionaryWithBuiltInKey.GetEnumerator();
}
public virtual void ReloadCache() {
_reloadCache();
}
public virtual ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue> AsReadCachedReadOnlyDictionary() {
return _asReadCachedReadOnlyDictionary();
}
IEnumerator IEnumerable.GetEnumerator() {
return _dictionaryWithBuiltInKey.GetEnumerator();}
}
}

