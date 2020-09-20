using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadCachedDisposableReadOnlyDictionary<TKey, TValue> : IReadCachedDisposableReadOnlyDictionary<TKey, TValue> {
private IDisposable _disposable;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadCachedDisposableReadOnlyDictionary(IDisposable disposable, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_disposable = disposable;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
void IDisposable.Dispose() {
_disposable.Dispose();
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
System.Collections.Generic.IEnumerator<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>> IEnumerable<ComposableCollections.Dictionary.IKeyValue<TKey, TValue>>.GetEnumerator() {
return _readCachedReadOnlyDictionary.GetEnumerator();
}
void IReadCachedReadOnlyDictionary<TKey, TValue>.ReloadCache() {
_readCachedReadOnlyDictionary.ReloadCache();
}
void IReadCachedReadOnlyDictionary<TKey, TValue>.ReloadCache( TKey key) {
_readCachedReadOnlyDictionary.ReloadCache( key);
}
void IReadCachedReadOnlyDictionary<TKey, TValue>.InvalidCache() {
_readCachedReadOnlyDictionary.InvalidCache();
}
void IReadCachedReadOnlyDictionary<TKey, TValue>.InvalidCache( TKey key) {
_readCachedReadOnlyDictionary.InvalidCache( key);
}
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _readCachedReadOnlyDictionary.GetEnumerator();
}
}
}

