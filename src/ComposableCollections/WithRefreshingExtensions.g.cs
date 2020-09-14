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
         public static class WithRefreshingExtensions
         {
#region WithRefreshing that always refreshes the value
public static ICachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites);
}
public static ICachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDisposableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source, source.Values);
}
public static ICachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source.Values);
}
public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return decorator;
}
public static IComposableReadOnlyDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableReadOnlyDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return decorator;
}
public static IDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableReadOnlyDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new QueryableDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
public static IQueryableReadOnlyDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableReadOnlyDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new QueryableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
#endregion
#region WithRefreshing
public static ICachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites);
}
public static ICachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDisposableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source, source.Values);
}
public static ICachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source.Values);
}
public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return decorator;
}
public static IComposableReadOnlyDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableReadOnlyDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return decorator;
}
public static IDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableReadOnlyDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new QueryableDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
public static IQueryableReadOnlyDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableReadOnlyDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
var decorator = new ReadOnlyDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new QueryableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
#endregion
#region WithRefreshing - optional persistence
public static ICachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites);
}
public static ICachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDisposableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source, source.Values);
}
public static ICachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new CachedQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source.Values);
}
public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return decorator;
}
public static IDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new DisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
var decorator = new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
    return new QueryableDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
#endregion
}
}
