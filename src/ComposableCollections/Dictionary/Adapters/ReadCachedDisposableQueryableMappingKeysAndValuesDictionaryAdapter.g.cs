using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedDisposableQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue> : ReadCachedQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>, IReadCachedDisposableQueryableDictionary<TKey, TValue> {
private readonly IReadCachedDisposableQueryableDictionary<TSourceKey, TSourceValue> _adapted;
public ReadCachedDisposableQueryableMappingKeysAndValuesDictionaryAdapter(IReadCachedDisposableQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) : base(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
