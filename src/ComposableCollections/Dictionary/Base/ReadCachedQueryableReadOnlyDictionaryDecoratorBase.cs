﻿using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.Dictionary.Interfaces {
public class ReadCachedQueryableReadOnlyDictionaryDecoratorBase<TKey, TValue> : IReadCachedQueryableReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedQueryableReadOnlyDictionary<TKey, TValue> _decoratedObject;
public ReadCachedQueryableReadOnlyDictionaryDecoratorBase(IReadCachedQueryableReadOnlyDictionary<TKey, TValue> decoratedObject) {
_decoratedObject = decoratedObject;
}
IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _decoratedObject.Values;
void IReadCachedReadOnlyDictionary<TKey, TValue>.ReloadCache() {
_decoratedObject.ReloadCache();
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
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
int IReadOnlyCollection<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.Count => _decoratedObject.Count;
}
}

