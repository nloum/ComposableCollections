using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class DisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>, IDisposableReadOnlyDictionary<TKey, TValue> {
private readonly IDisposableReadOnlyDictionary<TKey, TSourceValue> _adapted;
public DisposableReadOnlyMappingValuesDictionaryAdapter(IDisposableReadOnlyDictionary<TKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public DisposableReadOnlyMappingValuesDictionaryAdapter(IDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) : base(adapted, convertTo2) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
