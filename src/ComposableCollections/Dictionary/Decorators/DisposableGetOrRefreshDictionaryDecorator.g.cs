using System;
using System.Collections;
using System.Collections.Generic;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
namespace ComposableCollections.Dictionary.Decorators {
public class DisposableGetOrRefreshDictionaryDecorator<TKey, TValue> : DictionaryGetOrRefreshDecorator<TKey, TValue>, IDisposableDictionary<TKey, TValue> {
private readonly IDisposableDictionary<TKey, TValue> _adapted;
public DisposableGetOrRefreshDictionaryDecorator(IDisposableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public DisposableGetOrRefreshDictionaryDecorator(IDisposableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public DisposableGetOrRefreshDictionaryDecorator(IDisposableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) : base(adapted, refreshValue) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
