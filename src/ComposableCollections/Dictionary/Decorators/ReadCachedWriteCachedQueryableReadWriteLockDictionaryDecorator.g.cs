using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Decorators {
public class ReadCachedWriteCachedQueryableReadWriteLockDictionaryDecorator<TKey, TValue> : ReadWriteLockQueryableDictionaryDecorator<TKey, TValue>, IReadCachedWriteCachedQueryableDictionary<TKey, TValue> {
private readonly IReadCachedWriteCachedQueryableDictionary<TKey, TValue> _adapted;
public ReadCachedWriteCachedQueryableReadWriteLockDictionaryDecorator(IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted) : base(adapted) {
_adapted = adapted;}
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
