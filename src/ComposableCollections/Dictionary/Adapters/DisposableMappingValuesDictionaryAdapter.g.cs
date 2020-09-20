using System;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue> : MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>, IDisposableDictionary<TKey, TValue> {
private readonly IDisposableDictionary<TKey, TSourceValue> _adapted;
public DisposableMappingValuesDictionaryAdapter(IDisposableDictionary<TKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public DisposableMappingValuesDictionaryAdapter(IDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public DisposableMappingValuesDictionaryAdapter(IDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) : base(adapted, convertTo2, convertTo1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
