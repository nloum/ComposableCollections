using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue> {
private readonly IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> _adapted;
public DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter(IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter(IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
