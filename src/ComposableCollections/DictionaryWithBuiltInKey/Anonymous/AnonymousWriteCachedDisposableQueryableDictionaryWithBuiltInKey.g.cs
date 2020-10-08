﻿using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.DictionaryWithBuiltInKey.Anonymous {
public class AnonymousWriteCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> : IWriteCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IDictionaryWithBuiltInKey<TKey, TValue> _dictionaryWithBuiltInKey;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IDisposableDictionary<TKey, TValue>> _asDisposableDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IDisposableQueryableDictionary<TKey, TValue>> _asDisposableQueryableDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IDisposableQueryableReadOnlyDictionary<TKey, TValue>> _asDisposableQueryableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue>> _asDisposableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IQueryableDictionary<TKey, TValue>> _asQueryableDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue>> _asQueryableReadOnlyDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedDictionary<TKey, TValue>> _asWriteCachedDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedDisposableDictionary<TKey, TValue>> _asWriteCachedDisposableDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedDisposableQueryableDictionary<TKey, TValue>> _asWriteCachedDisposableQueryableDictionary;
private readonly Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedQueryableDictionary<TKey, TValue>> _asWriteCachedQueryableDictionary;
private readonly Action _dispose;
private readonly Action _flushCache;
private readonly Func<IQueryable<TValue>> _getValues;
public AnonymousWriteCachedDisposableQueryableDictionaryWithBuiltInKey(IDictionaryWithBuiltInKey<TKey, TValue> dictionaryWithBuiltInKey, Func<ComposableCollections.Dictionary.Interfaces.IDisposableDictionary<TKey, TValue>> asDisposableDictionary, Func<ComposableCollections.Dictionary.Interfaces.IDisposableQueryableDictionary<TKey, TValue>> asDisposableQueryableDictionary, Func<ComposableCollections.Dictionary.Interfaces.IDisposableQueryableReadOnlyDictionary<TKey, TValue>> asDisposableQueryableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue>> asDisposableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IQueryableDictionary<TKey, TValue>> asQueryableDictionary, Func<ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue>> asQueryableReadOnlyDictionary, Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedDictionary<TKey, TValue>> asWriteCachedDictionary, Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedDisposableDictionary<TKey, TValue>> asWriteCachedDisposableDictionary, Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedDisposableQueryableDictionary<TKey, TValue>> asWriteCachedDisposableQueryableDictionary, Func<ComposableCollections.Dictionary.Interfaces.IWriteCachedQueryableDictionary<TKey, TValue>> asWriteCachedQueryableDictionary, Action dispose, Action flushCache, Func<IQueryable<TValue>> getValues) {
_asWriteCachedDisposableQueryableDictionary = asWriteCachedDisposableQueryableDictionary;
_asWriteCachedDisposableDictionary = asWriteCachedDisposableDictionary;
_asWriteCachedDictionary = asWriteCachedDictionary;
_flushCache = flushCache;
_dictionaryWithBuiltInKey = dictionaryWithBuiltInKey;
_asDisposableDictionary = asDisposableDictionary;
_asDisposableReadOnlyDictionary = asDisposableReadOnlyDictionary;
_dispose = dispose;
_asWriteCachedQueryableDictionary = asWriteCachedQueryableDictionary;
_asQueryableDictionary = asQueryableDictionary;
_getValues = getValues;
_asQueryableReadOnlyDictionary = asQueryableReadOnlyDictionary;
_asDisposableQueryableDictionary = asDisposableQueryableDictionary;
_asDisposableQueryableReadOnlyDictionary = asDisposableQueryableReadOnlyDictionary;
}
public virtual TValue this[ TKey key] {
get => _dictionaryWithBuiltInKey[ key];
set => _dictionaryWithBuiltInKey[ key] = value;
}
public virtual System.Collections.Generic.IEqualityComparer<TKey> Comparer => _dictionaryWithBuiltInKey.Comparer;
public virtual System.Collections.Generic.IEnumerable<TKey> Keys => _dictionaryWithBuiltInKey.Keys;
System.Collections.Generic.IEnumerable<TValue> IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _dictionaryWithBuiltInKey.Values;
IQueryable<TValue> IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _getValues();
public virtual int Count => _dictionaryWithBuiltInKey.Count;
public virtual ComposableCollections.Dictionary.Interfaces.IWriteCachedDisposableQueryableDictionary<TKey, TValue> AsWriteCachedDisposableQueryableDictionary() {
return _asWriteCachedDisposableQueryableDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IWriteCachedDisposableDictionary<TKey, TValue> AsWriteCachedDisposableDictionary() {
return _asWriteCachedDisposableDictionary();
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
public virtual ComposableCollections.Dictionary.Interfaces.IDisposableDictionary<TKey, TValue> AsDisposableDictionary() {
return _asDisposableDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary() {
return _asDisposableReadOnlyDictionary();
}
public virtual void Dispose() {
_dispose();
}
public virtual ComposableCollections.Dictionary.Interfaces.IWriteCachedQueryableDictionary<TKey, TValue> AsWriteCachedQueryableDictionary() {
return _asWriteCachedQueryableDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IQueryableDictionary<TKey, TValue> AsQueryableDictionary() {
return _asQueryableDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary() {
return _asQueryableReadOnlyDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IDisposableQueryableDictionary<TKey, TValue> AsDisposableQueryableDictionary() {
return _asDisposableQueryableDictionary();
}
public virtual ComposableCollections.Dictionary.Interfaces.IDisposableQueryableReadOnlyDictionary<TKey, TValue> AsDisposableQueryableReadOnlyDictionary() {
return _asDisposableQueryableReadOnlyDictionary();
}
IEnumerator IEnumerable.GetEnumerator() {
return _dictionaryWithBuiltInKey.GetEnumerator();}
}
}

