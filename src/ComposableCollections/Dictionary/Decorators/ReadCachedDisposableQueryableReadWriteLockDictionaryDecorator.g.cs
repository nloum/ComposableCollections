using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadCachedDisposableQueryableReadWriteLockDictionaryDecorator<TKey, TValue> : ReadWriteLockQueryableDictionaryDecorator<TKey, TValue>, IReadCachedDisposableQueryableDictionary<TKey, TValue> {
private readonly IReadCachedDisposableQueryableDictionary<TKey, TValue> _adapted;
public ReadCachedDisposableQueryableReadWriteLockDictionaryDecorator(IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted) : base(adapted) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
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
