using ComposableCollections.Dictionary.Interfaces;
using System;
namespace ComposableCollections.Dictionary.Interfaces {
public class AnonymousWriteCachedDisposableDictionary<TKey, TValue> : IWriteCachedDisposableDictionary<TKey, TValue> {
private IDisposable _disposable;
private IWriteCachedDictionary<TKey, TValue> _writeCachedDictionary;
public AnonymousWriteCachedDisposableDictionary(IDisposable disposable, IWriteCachedDictionary<TKey, TValue> writeCachedDictionary) {
_disposable = disposable;
_writeCachedDictionary = writeCachedDictionary;
}
}
}

