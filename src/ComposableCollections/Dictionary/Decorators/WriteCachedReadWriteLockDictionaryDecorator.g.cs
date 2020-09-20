using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using System.Collections.Immutable;
using System.Threading;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;
using UtilityDisposables;
namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedReadWriteLockDictionaryDecorator<TKey, TValue> : ReadWriteLockDictionaryDecorator<TKey, TValue>, IWriteCachedDictionary<TKey, TValue> {
private readonly IWriteCachedDictionary<TKey, TValue> _adapted;
public WriteCachedReadWriteLockDictionaryDecorator(IWriteCachedDictionary<TKey, TValue> adapted) : base(adapted) {
_adapted = adapted;}
public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
