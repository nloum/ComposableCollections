using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue> : DictionaryGetOrDefaultDecorator<TKey, TValue>, IWriteCachedDisposableDictionary<TKey, TValue> {
private readonly IWriteCachedDisposableDictionary<TKey, TValue> _adapted;
public WriteCachedDisposableGetOrDefaultDictionaryDecorator(IWriteCachedDisposableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public WriteCachedDisposableGetOrDefaultDictionaryDecorator(IWriteCachedDisposableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public WriteCachedDisposableGetOrDefaultDictionaryDecorator(IWriteCachedDisposableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
