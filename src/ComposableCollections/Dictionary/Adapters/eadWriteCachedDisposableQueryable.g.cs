using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Adapters {
public class eadWriteCachedDisposableQueryable<TSourceKey, TSourceValue, TKey, TValue> : ReadCachedQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, ReadWriteCachedDisposableQueryable<TKey, TValue> {
private readonly ReadWriteCachedDisposableQueryable<TSourceKey, TSourceValue> _adapted;
public eadWriteCachedDisposableQueryable(ReadWriteCachedDisposableQueryable<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
