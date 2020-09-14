﻿using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadCachedQueryableReadOnlyDictionary<TKey, TValue> : IReadCachedQueryableReadOnlyDictionary<TKey, TValue> {
private IQueryableReadOnlyDictionary<TKey, TValue> _queryableReadOnlyDictionary;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadCachedQueryableReadOnlyDictionary(IQueryableReadOnlyDictionary<TKey, TValue> queryableReadOnlyDictionary, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_queryableReadOnlyDictionary = queryableReadOnlyDictionary;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _queryableReadOnlyDictionary.GetEnumerator();
}
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _queryableReadOnlyDictionary.GetEnumerator();
}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _queryableReadOnlyDictionary.Values;
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
int IReadOnlyCollection<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.Count => _queryableReadOnlyDictionary.Count;
void IReadCachedReadOnlyDictionary<TKey, TValue>.ReloadCache() {
_readCachedReadOnlyDictionary.ReloadCache();
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _readCachedReadOnlyDictionary.GetEnumerator();
}
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _readCachedReadOnlyDictionary.GetEnumerator();
}
System.Collections.Generic.IEqualityComparer<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Comparer => _readCachedReadOnlyDictionary.Comparer;
TValue IComposableReadOnlyDictionary<TKey, TValue>.GetValue( TKey key) {
return _readCachedReadOnlyDictionary.GetValue( key);
}
TValue IComposableReadOnlyDictionary<TKey, TValue>.this[ TKey key] => _readCachedReadOnlyDictionary[ key];
System.Collections.Generic.IEnumerable<TKey> IComposableReadOnlyDictionary<TKey, TValue>.Keys => _readCachedReadOnlyDictionary.Keys;
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _readCachedReadOnlyDictionary.Values;
bool IComposableReadOnlyDictionary<TKey, TValue>.ContainsKey( TKey key) {
return _readCachedReadOnlyDictionary.ContainsKey( key);
}
IMaybe<TValue> IComposableReadOnlyDictionary<TKey, TValue>.TryGetValue( TKey key) {
return _readCachedReadOnlyDictionary.TryGetValue( key);
}
int IReadOnlyCollection<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.Count => _readCachedReadOnlyDictionary.Count;
}
}

