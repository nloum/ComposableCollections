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
         public static class WithReadWriteLockExtensions
         {
#region WithReadWriteLock
public static ICachedDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDictionary<TKey, TValue> source) {
var decorator = new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);
    return new CachedDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites);
}
public static ICachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source) {
var decorator = new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);
    return new CachedDisposableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source) {
var decorator = new ReadWriteLockQueryableDictionaryDecorator<TKey, TValue>(source);
    return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source, ((IQueryableReadOnlyDictionary<TKey, TValue>)decorator).Values);
}
public static ICachedQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source) {
var decorator = new ReadWriteLockQueryableDictionaryDecorator<TKey, TValue>(source);
    return new CachedQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, ((IQueryableReadOnlyDictionary<TKey, TValue>)decorator).Values);
}
public static IComposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IComposableDictionary<TKey, TValue> source) {
var decorator = new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);
    return decorator;
}
public static IDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source) {
var decorator = new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);
    return new DisposableDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IDisposableQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source) {
var decorator = new ReadWriteLockQueryableDictionaryDecorator<TKey, TValue>(source);
    return new DisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source, ((IQueryableReadOnlyDictionary<TKey, TValue>)decorator).Values);
}
public static IQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source) {
var decorator = new ReadWriteLockQueryableDictionaryDecorator<TKey, TValue>(source);
    return new QueryableDictionaryAdapter<TKey, TValue>(decorator, ((IQueryableReadOnlyDictionary<TKey, TValue>)decorator).Values);
}
#endregion
}
}
