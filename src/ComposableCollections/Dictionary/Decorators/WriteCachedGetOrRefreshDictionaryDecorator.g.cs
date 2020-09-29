using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using System;
using System.Collections;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue> : DictionaryGetOrRefreshDecorator<TKey, TValue>, IWriteCachedDictionary<TKey, TValue> {
private readonly IWriteCachedDictionary<TKey, TValue> _adapted;
public WriteCachedGetOrRefreshDictionaryDecorator(IWriteCachedDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public WriteCachedGetOrRefreshDictionaryDecorator(IWriteCachedDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public WriteCachedGetOrRefreshDictionaryDecorator(IWriteCachedDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
