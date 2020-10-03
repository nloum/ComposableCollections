using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedDisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : ReadCachedMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IReadCachedDisposableReadOnlyDictionary<TKey, TValue> {
private readonly IReadCachedDisposableReadOnlyDictionary<TSourceKey, TSourceValue> _adapted;
public ReadCachedDisposableReadOnlyMappingKeysAndValuesDictionaryAdapter(IReadCachedDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertToKey2, convertToKey1) {
_adapted = adapted;}
public ReadCachedDisposableReadOnlyMappingKeysAndValuesDictionaryAdapter(IReadCachedDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
