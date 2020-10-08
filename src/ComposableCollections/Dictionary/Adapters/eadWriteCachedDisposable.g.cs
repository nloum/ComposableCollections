using System;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Adapters {
public class eadWriteCachedDisposable<TSourceKey, TSourceValue, TKey, TValue> : ReadCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, ReadWriteCachedDisposable<TKey, TValue> {
private readonly ReadWriteCachedDisposable<TSourceKey, TSourceValue> _adapted;
public eadWriteCachedDisposable(ReadWriteCachedDisposable<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public eadWriteCachedDisposable(ReadWriteCachedDisposable<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
