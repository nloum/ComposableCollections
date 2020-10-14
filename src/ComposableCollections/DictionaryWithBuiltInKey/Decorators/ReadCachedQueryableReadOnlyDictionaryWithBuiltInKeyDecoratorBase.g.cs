using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace ComposableCollections.DictionaryWithBuiltInKey.Decorators {
public class ReadCachedQueryableReadOnlyDictionaryWithBuiltInKeyDecoratorBase<TKey, TValue> : IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> {
private readonly IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> _decoratedObject;
public ReadCachedQueryableReadOnlyDictionaryWithBuiltInKeyDecoratorBase(IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> decoratedObject) {
_decoratedObject = decoratedObject;
}
IQueryable<TValue> IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.Values => _decoratedObject.Values;
ComposableCollections.Dictionary.Interfaces.IQueryableReadOnlyDictionary<TKey, TValue> IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.AsQueryableReadOnlyDictionary() {
return _decoratedObject.AsQueryableReadOnlyDictionary();
}
System.Collections.Generic.IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() {
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
ComposableCollections.Dictionary.Interfaces.IReadCachedQueryableReadOnlyDictionary<TKey, TValue> IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.AsReadCachedQueryableReadOnlyDictionary() {
return _decoratedObject.AsReadCachedQueryableReadOnlyDictionary();
}
int IReadOnlyCollection<TValue>.Count => _decoratedObject.Count;
System.Collections.IEnumerator IEnumerable.GetEnumerator() {
return _decoratedObject.GetEnumerator();
}
void IReadCachedReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.ReloadCache() {
_decoratedObject.ReloadCache();
}
ComposableCollections.Dictionary.Interfaces.IReadCachedReadOnlyDictionary<TKey, TValue> IReadCachedReadOnlyDictionaryWithBuiltInKey<TKey, TValue>.AsReadCachedReadOnlyDictionary() {
return _decoratedObject.AsReadCachedReadOnlyDictionary();
}
}
}

