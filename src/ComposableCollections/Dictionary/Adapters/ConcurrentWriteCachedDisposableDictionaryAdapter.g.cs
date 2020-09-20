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
namespace ComposableCollections.Dictionary.Adapters {
public class ConcurrentWriteCachedDisposableDictionaryAdapter<TKey, TValue> : ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>, IWriteCachedDisposableDictionary<TKey, TValue> {
private readonly IWriteCachedDisposableDictionary<TKey, TValue> _adapted;
public ConcurrentWriteCachedDisposableDictionaryAdapter(IWriteCachedDisposableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) : base(adapted, addedOrUpdated, removed) {
_adapted = adapted;}
public virtual void Dispose() {
_adapted.Dispose();
}

}
}
