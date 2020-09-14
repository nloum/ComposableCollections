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
         public static class WithDefaultValueExtensions
         {
#region WithDefaultValue that always returns a value
public static ICachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites);
}
public static ICachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDisposableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source, source.Values);
}
public static ICachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source.Values);
}
public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return decorator;
}
public static IComposableReadOnlyDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableReadOnlyDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return decorator;
}
public static IDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableReadOnlyDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new QueryableDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
public static IQueryableReadOnlyDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableReadOnlyDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new QueryableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
#endregion
#region WithDefaultValue
public static ICachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites);
}
public static ICachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDisposableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source, source.Values);
}
public static ICachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source.Values);
}
public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return decorator;
}
public static IComposableReadOnlyDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableReadOnlyDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return decorator;
}
public static IDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableReadOnlyDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new QueryableDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
public static IQueryableReadOnlyDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableReadOnlyDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
var decorator = new ReadOnlyDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new QueryableReadOnlyDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
#endregion
#region WithDefaultValue - optional persistence
public static ICachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites);
}
public static ICachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDisposableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source, source.Values);
}
public static ICachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new CachedQueryableDictionaryAdapter<TKey, TValue>(decorator, source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source.Values);
}
public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return decorator;
}
public static IDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableDictionaryAdapter<TKey, TValue>(decorator, source);
}
public static IDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new DisposableQueryableDictionaryAdapter<TKey, TValue>(decorator, source, source.Values);
}
public static IQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
var decorator = new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
    return new QueryableDictionaryAdapter<TKey, TValue>(decorator, source.Values);
}
#endregion
}
}
