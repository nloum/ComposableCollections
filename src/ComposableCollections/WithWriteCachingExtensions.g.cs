using System;
 		        using System.Collections.Generic;
 		        using System.Linq;
 		        using System.Linq.Expressions;
 		        using ComposableCollections.Common;
 		        using ComposableCollections.Dictionary;
 		        using ComposableCollections.Dictionary.Adapters;
 		        using ComposableCollections.Dictionary.Decorators;
 		        using ComposableCollections.Dictionary.Interfaces;
 		        using ComposableCollections.Dictionary.Sources;
 		        using ComposableCollections.Dictionary.Transactional;
 		        using ComposableCollections.Dictionary.WithBuiltInKey;
 		        using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
 		        using UtilityDisposables;

 			        namespace ComposableCollections
 		        {
         public static class WithWriteCachingExtensions
         {
#region WithWriteCaching
public static ICachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> source) {
var adapter = new ConcurrentCachedWriteDictionaryAdapter<TKey, TValue>(source);
    return adapter;
}
public static ICachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source) {
var adapter = new ConcurrentCachedWriteDictionaryAdapter<TKey, TValue>(source);
    return new CachedDisposableDictionaryAdapter<TKey, TValue>(adapter, adapter.AsBypassCache, adapter.AsNeverFlush, adapter.FlushCache, adapter.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source) {
var adapter = new ConcurrentCachedWriteDictionaryAdapter<TKey, TValue>(source);
    return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(adapter, adapter.AsBypassCache, adapter.AsNeverFlush, adapter.FlushCache, adapter.GetWrites, source, source.Values);
}
public static ICachedQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source) {
var adapter = new ConcurrentCachedWriteDictionaryAdapter<TKey, TValue>(source);
    return new CachedQueryableDictionaryAdapter<TKey, TValue>(adapter, adapter.AsBypassCache, adapter.AsNeverFlush, adapter.FlushCache, adapter.GetWrites, source.Values);
}
#endregion
}
}
