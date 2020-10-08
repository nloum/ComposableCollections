﻿using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadCachedDisposableReadOnlyDictionaryDecoratorBase<TKey, TValue> : IReadCachedDisposableReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedDisposableReadOnlyDictionary<TKey, TValue> _decoratedObject;
public ReadCachedDisposableReadOnlyDictionaryDecoratorBase(IReadCachedDisposableReadOnlyDictionary<TKey, TValue> decoratedObject) {
_decoratedObject = decoratedObject;
}
void IReadCachedReadOnlyDictionary<TKey, TValue>.ReloadCache() {
_decoratedObject.ReloadCache();
}
void IReadCachedReadOnlyDictionary<TKey, TValue>.ReloadCache( TKey key) {
_decoratedObject.ReloadCache( key);
}
void IReadCachedReadOnlyDictionary<TKey, TValue>.InvalidCache() {
_decoratedObject.InvalidCache();
}
void IReadCachedReadOnlyDictionary<TKey, TValue>.InvalidCache( TKey key) {
_decoratedObject.InvalidCache( key);
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
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
void IDisposable.Dispose() {
_decoratedObject.Dispose();
}
int IReadOnlyCollection<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.Count => _decoratedObject.Count;
}
}

