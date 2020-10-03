using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using System;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Adapters {
public class WriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IWriteCachedDictionary<TKey, TValue> {
private readonly IWriteCachedDictionary<TSourceKey, TSourceValue> _adapted;
public WriteCachedMappingKeysAndValuesDictionaryAdapter(IWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public WriteCachedMappingKeysAndValuesDictionaryAdapter(IWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
