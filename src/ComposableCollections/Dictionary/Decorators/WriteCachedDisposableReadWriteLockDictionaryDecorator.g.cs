using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;
using ComposableCollections.Dictionary.Write;
namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue> : ReadWriteLockDictionaryDecorator<TKey, TValue>, IWriteCachedDisposableDictionary<TKey, TValue> {
private readonly IWriteCachedDisposableDictionary<TKey, TValue> _adapted;
public WriteCachedDisposableReadWriteLockDictionaryDecorator(IWriteCachedDisposableDictionary<TKey, TValue> adapted) : base(adapted) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
