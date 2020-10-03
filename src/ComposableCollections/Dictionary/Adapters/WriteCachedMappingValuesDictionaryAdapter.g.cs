using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IWriteCachedDictionary<TKey, TValue> {
private readonly IWriteCachedDictionary<TKey, TSourceValue> _adapted;
public WriteCachedMappingValuesDictionaryAdapter(IWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public WriteCachedMappingValuesDictionaryAdapter(IWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
