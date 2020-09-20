using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue> : DictionaryGetOrRefreshDecorator<TKey, TValue>, IWriteCachedDisposableDictionary<TKey, TValue> {
private readonly IWriteCachedDisposableDictionary<TKey, TValue> _adapted;
public WriteCachedDisposableGetOrRefreshDictionaryDecorator(IWriteCachedDisposableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public WriteCachedDisposableGetOrRefreshDictionaryDecorator(IWriteCachedDisposableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public WriteCachedDisposableGetOrRefreshDictionaryDecorator(IWriteCachedDisposableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
