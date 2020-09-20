using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
namespace ComposableCollections.Dictionary.Adapters {
public class DisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IDisposableDictionary<TKey, TValue> {
private readonly IDisposableDictionary<TSourceKey, TSourceValue> _adapted;
public DisposableMappingKeysAndValuesDictionaryAdapter(IDisposableDictionary<TSourceKey, TSourceValue> adapted) : base(adapted) {
_adapted = adapted;}
public DisposableMappingKeysAndValuesDictionaryAdapter(IDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
