using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Exceptions;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Write;
using SimpleMonads;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedConcurrentWriteCachedDisposableDictionaryAdapter<TKey, TValue> : ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>, IReadCachedWriteCachedDisposableDictionary<TKey, TValue> {
private readonly IReadCachedWriteCachedDisposableDictionary<TKey, TValue> _adapted;
public ReadCachedConcurrentWriteCachedDisposableDictionaryAdapter(IReadCachedWriteCachedDisposableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) : base(adapted, addedOrUpdated, removed) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

public virtual void ReloadCache() {
_adapted.ReloadCache();
}

public virtual void ReloadCache( TKey key) {
_adapted.ReloadCache( key);
}

public virtual void InvalidCache() {
_adapted.InvalidCache();
}

public virtual void InvalidCache( TKey key) {
_adapted.InvalidCache( key);
}

}
}
