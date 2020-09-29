using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadWriteCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue> : ReadWriteLockDictionaryDecorator<TKey, TValue>, IReadWriteCachedDisposableDictionary<TKey, TValue> {
private readonly IReadWriteCachedDisposableDictionary<TKey, TValue> _adapted;
public ReadWriteCachedDisposableReadWriteLockDictionaryDecorator(IReadWriteCachedDisposableDictionary<TKey, TValue> adapted) : base(adapted) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

public virtual void ReloadCache() {
_adapted.ReloadCache();
}

public virtual void ReloadCache( TKey key) {
_adapted.ReloadCache( key);
}

public virtual void InvalidCache() {
_adapted.InvalidCache();
}

public virtual void InvalidCache( TKey key) {
_adapted.InvalidCache( key);
}

}
}
