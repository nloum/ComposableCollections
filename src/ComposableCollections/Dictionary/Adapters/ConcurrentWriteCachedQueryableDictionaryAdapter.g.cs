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
using System.Collections.Generic;using ComposableCollections.Dictionary.Interfaces;using ComposableCollections.Dictionary.Interfaces;namespace ComposableCollections.Dictionary.Adapters {
public class ConcurrentWriteCachedQueryableDictionaryAdapter<TKey, TValue> : ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>, IWriteCachedQueryableDictionary<TKey, TValue> {
private readonly IWriteCachedQueryableDictionary<TKey, TValue> _adapted;
public ConcurrentWriteCachedQueryableDictionaryAdapter(IWriteCachedQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) : base(adapted, addedOrUpdated, removed) {
_adapted = adapted;}
System.Collections.Generic.IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;

IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _adapted.Values;





}
}
