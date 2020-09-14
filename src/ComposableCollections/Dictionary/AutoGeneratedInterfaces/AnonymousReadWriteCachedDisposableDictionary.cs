using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousReadWriteCachedDisposableDictionary<TKey, TValue> : IReadWriteCachedDisposableDictionary<TKey, TValue> {
private IDisposable _disposable;
private IWriteCachedDictionary<TKey, TValue> _writeCachedDictionary;
private IReadCachedReadOnlyDictionary<TKey, TValue> _readCachedReadOnlyDictionary;
public AnonymousReadWriteCachedDisposableDictionary(IDisposable disposable, IWriteCachedDictionary<TKey, TValue> writeCachedDictionary, IReadCachedReadOnlyDictionary<TKey, TValue> readCachedReadOnlyDictionary) {
_disposable = disposable;
_writeCachedDictionary = writeCachedDictionary;
_readCachedReadOnlyDictionary = readCachedReadOnlyDictionary;
}
}
}

