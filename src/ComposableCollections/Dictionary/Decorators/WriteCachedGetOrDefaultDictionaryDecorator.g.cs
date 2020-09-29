using System.Collections.Generic;
using ComposableCollections.Dictionary.Write;
using System;
using System.Collections;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Decorators {
public class WriteCachedGetOrDefaultDictionaryDecorator<TKey, TValue> : DictionaryGetOrDefaultDecorator<TKey, TValue>, IWriteCachedDictionary<TKey, TValue> {
private readonly IWriteCachedDictionary<TKey, TValue> _adapted;
public WriteCachedGetOrDefaultDictionaryDecorator(IWriteCachedDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public WriteCachedGetOrDefaultDictionaryDecorator(IWriteCachedDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public WriteCachedGetOrDefaultDictionaryDecorator(IWriteCachedDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) : base(adapted, getDefaultValue) {
_adapted = adapted;}
public virtual void FlushCache() {
_adapted.FlushCache();
}

}
}
