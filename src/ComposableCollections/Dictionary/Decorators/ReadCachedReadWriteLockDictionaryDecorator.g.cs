using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadCachedReadWriteLockDictionaryDecorator<TKey, TValue> : ReadWriteLockDictionaryDecorator<TKey, TValue>, IReadCachedDictionary<TKey, TValue> {
private readonly IReadCachedDictionary<TKey, TValue> _adapted;
public ReadCachedReadWriteLockDictionaryDecorator(IReadCachedDictionary<TKey, TValue> adapted) : base(adapted) {
_adapted = adapted;}
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
