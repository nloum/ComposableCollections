using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadCachedDisposableReadOnlyDictionary<TKey, TValue> : IReadCachedDisposableReadOnlyDictionary<TKey, TValue> {
private IDisposable _disposable;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadCachedDisposableReadOnlyDictionary(IDisposable disposable, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_disposable = disposable;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

