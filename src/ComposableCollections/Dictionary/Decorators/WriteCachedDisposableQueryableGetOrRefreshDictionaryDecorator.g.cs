using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using System.Linq;
using System.Collections.Generic;using ComposableCollections.Dictionary.Interfaces;namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue> : DictionaryGetOrRefreshDecorator<TKey, TValue>, IWriteCachedDisposableQueryableDictionary<TKey, TValue> {
private readonly IWriteCachedDisposableQueryableDictionary<TKey, TValue> _adapted;
public WriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator(IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public WriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator(IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public WriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator(IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
