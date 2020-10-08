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
using System.Collections.Generic;using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections.Dictionary.Adapters {
public class ReadCachedConcurrentWriteCachedQueryableDictionaryAdapter<TKey, TValue> : ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>, IReadCachedWriteCachedQueryableDictionary<TKey, TValue> {
private readonly IReadCachedWriteCachedQueryableDictionary<TKey, TValue> _adapted;
public ReadCachedConcurrentWriteCachedQueryableDictionaryAdapter(IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) : base(adapted, addedOrUpdated, removed) {
_adapted = adapted;}
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;


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
