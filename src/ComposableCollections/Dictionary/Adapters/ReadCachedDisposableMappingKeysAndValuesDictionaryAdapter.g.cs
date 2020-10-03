using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : ReadCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IReadCachedDisposableDictionary<TKey, TValue> {
private readonly IReadCachedDisposableDictionary<TSourceKey, TSourceValue> _adapted;
public ReadCachedDisposableMappingKeysAndValuesDictionaryAdapter(IReadCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public ReadCachedDisposableMappingKeysAndValuesDictionaryAdapter(IReadCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
