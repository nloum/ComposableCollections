using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Adapters {
public class WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IWriteCachedDisposableDictionary<TKey, TValue> {
private readonly IWriteCachedDisposableDictionary<TKey, TSourceValue> _adapted;
public WriteCachedDisposableMappingValuesDictionaryAdapter(IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public WriteCachedDisposableMappingValuesDictionaryAdapter(IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
