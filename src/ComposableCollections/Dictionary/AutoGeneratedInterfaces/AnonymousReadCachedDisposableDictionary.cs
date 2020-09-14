using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadCachedDisposableDictionary<TKey, TValue> : IReadCachedDisposableDictionary<TKey, TValue> {
private IComposableDictionary<TKey, TValue> _composableDictionary;
private IDisposable _disposable;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadCachedDisposableDictionary(IComposableDictionary<TKey, TValue> composableDictionary, IDisposable disposable, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_composableDictionary = composableDictionary;
_disposable = disposable;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

