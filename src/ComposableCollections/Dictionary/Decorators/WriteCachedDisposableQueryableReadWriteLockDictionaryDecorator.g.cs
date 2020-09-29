using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedDisposableQueryableReadWriteLockDictionaryDecorator<TKey, TValue> : ReadWriteLockQueryableDictionaryDecorator<TKey, TValue>, IWriteCachedDisposableQueryableDictionary<TKey, TValue> {
private readonly IWriteCachedDisposableQueryableDictionary<TKey, TValue> _adapted;
public WriteCachedDisposableQueryableReadWriteLockDictionaryDecorator(IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted) : base(adapted) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
