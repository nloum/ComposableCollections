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
         public static class WithMappingExtensions
         {
#region WithMapping - different key types
public static ICachedDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDictionary<TKey1, TValue1> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, convertToValue2, convertToValue1, convertToKey2, convertToKey1);
var cachedMapSource = new ConcurrentCachedWriteDictionaryAdapter<TKey2, TValue2>(mappedSource);
    return new CachedDictionaryAdapter<TKey2, TValue2>(mappedSource, cachedMapSource.AsBypassCache, cachedMapSource.AsNeverFlush, () => {  cachedMapSource.FlushCache(); source.FlushCache(); }, cachedMapSource.GetWrites);
}
public static ICachedDisposableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDisposableDictionary<TKey1, TValue1> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, convertToValue2, convertToValue1, convertToKey2, convertToKey1);
var cachedMapSource = new ConcurrentCachedWriteDictionaryAdapter<TKey2, TValue2>(mappedSource);
    return new CachedDisposableDictionaryAdapter<TKey2, TValue2>(mappedSource, cachedMapSource.AsBypassCache, cachedMapSource.AsNeverFlush, () => {  cachedMapSource.FlushCache(); source.FlushCache(); }, cachedMapSource.GetWrites, source);
}
public static ICachedDisposableQueryableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDisposableQueryableDictionary<TKey1, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
var convertToValue2Compiled = convertToValue2.Compile();
var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, (key, value) => new KeyValue<TKey2, TValue2>(convertToKey2(key), convertToValue2Compiled(value)), convertToValue1, convertToKey2, convertToKey1);
var cachedMapSource = new ConcurrentCachedWriteDictionaryAdapter<TKey2, TValue2>(mappedSource);
    return new CachedDisposableQueryableDictionaryAdapter<TKey2, TValue2>(mappedSource, cachedMapSource.AsBypassCache, cachedMapSource.AsNeverFlush, () => {  cachedMapSource.FlushCache(); source.FlushCache(); }, cachedMapSource.GetWrites, source, source.Values.Select(convertToValue2));
}
public static ICachedQueryableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedQueryableDictionary<TKey1, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
var convertToValue2Compiled = convertToValue2.Compile();
var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, (key, value) => new KeyValue<TKey2, TValue2>(convertToKey2(key), convertToValue2Compiled(value)), convertToValue1, convertToKey2, convertToKey1);
var cachedMapSource = new ConcurrentCachedWriteDictionaryAdapter<TKey2, TValue2>(mappedSource);
    return new CachedQueryableDictionaryAdapter<TKey2, TValue2>(mappedSource, cachedMapSource.AsBypassCache, cachedMapSource.AsNeverFlush, () => {  cachedMapSource.FlushCache(); source.FlushCache(); }, cachedMapSource.GetWrites, source.Values.Select(convertToValue2));
}
public static IComposableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IComposableDictionary<TKey1, TValue1> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, convertToValue2, convertToValue1, convertToKey2, convertToKey1);
    return mappedSource;
}
public static IComposableReadOnlyDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IComposableReadOnlyDictionary<TKey1, TValue1> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TKey1> convertToKey1) {
var mappedSource = new MappingReadOnlyDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, convertToValue2, convertToKey2, convertToKey1);
    return mappedSource;
}
public static IDisposableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableDictionary<TKey1, TValue1> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, convertToValue2, convertToValue1, convertToKey2, convertToKey1);
    return new DisposableDictionaryAdapter<TKey2, TValue2>(mappedSource, source);
}
public static IDisposableQueryableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableQueryableDictionary<TKey1, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
var convertToValue2Compiled = convertToValue2.Compile();
var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, (key, value) => new KeyValue<TKey2, TValue2>(convertToKey2(key), convertToValue2Compiled(value)), convertToValue1, convertToKey2, convertToKey1);
    return new DisposableQueryableDictionaryAdapter<TKey2, TValue2>(mappedSource, source, source.Values.Select(convertToValue2));
}
public static IDisposableQueryableReadOnlyDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableQueryableReadOnlyDictionary<TKey1, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TKey1> convertToKey1) {
var convertToValue2Compiled = convertToValue2.Compile();
var mappedSource = new MappingReadOnlyDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, (key, value) => new KeyValue<TKey2, TValue2>(convertToKey2(key), convertToValue2Compiled(value)), convertToKey2, convertToKey1);
    return new DisposableQueryableReadOnlyDictionaryAdapter<TKey2, TValue2>(mappedSource, source, source.Values.Select(convertToValue2));
}
public static IDisposableReadOnlyDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableReadOnlyDictionary<TKey1, TValue1> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TKey1> convertToKey1) {
var mappedSource = new MappingReadOnlyDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, convertToValue2, convertToKey2, convertToKey1);
    return new DisposableReadOnlyDictionaryAdapter<TKey2, TValue2>(mappedSource, source);
}
public static IQueryableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IQueryableDictionary<TKey1, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
var convertToValue2Compiled = convertToValue2.Compile();
var mappedSource = new MappingDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, (key, value) => new KeyValue<TKey2, TValue2>(convertToKey2(key), convertToValue2Compiled(value)), convertToValue1, convertToKey2, convertToKey1);
    return new QueryableDictionaryAdapter<TKey2, TValue2>(mappedSource, source.Values.Select(convertToValue2));
}
public static IQueryableReadOnlyDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IQueryableReadOnlyDictionary<TKey1, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TKey1> convertToKey1) {
var convertToValue2Compiled = convertToValue2.Compile();
var mappedSource = new MappingReadOnlyDictionaryAdapter<TKey1, TValue1, TKey2, TValue2>(source, (key, value) => new KeyValue<TKey2, TValue2>(convertToKey2(key), convertToValue2Compiled(value)), convertToKey2, convertToKey1);
    return new QueryableReadOnlyDictionaryAdapter<TKey2, TValue2>(mappedSource, source.Values.Select(convertToValue2));
}
#endregion

#region WithMapping - one key type
public static ICachedDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDictionary<TKey, TValue1> source, Func<TKey, TValue1, TValue2> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>((key, value) => new KeyValue<TKey, TValue2>(key, convertToValue2(key, value)), x => x, (key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value)), x => x);
}
public static ICachedDisposableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDisposableDictionary<TKey, TValue1> source, Func<TKey, TValue1, TValue2> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>((key, value) => new KeyValue<TKey, TValue2>(key, convertToValue2(key, value)), x => x, (key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value)), x => x);
}
public static ICachedDisposableQueryableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDisposableQueryableDictionary<TKey, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>(convertToValue2, x => x, (key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value)), x => x);
}
public static ICachedQueryableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedQueryableDictionary<TKey, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>(convertToValue2, x => x, (key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value)), x => x);
}
public static IComposableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IComposableDictionary<TKey, TValue1> source, Func<TKey, TValue1, TValue2> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>((key, value) => new KeyValue<TKey, TValue2>(key, convertToValue2(key, value)), x => x, (key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value)), x => x);
}
public static IComposableReadOnlyDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IComposableReadOnlyDictionary<TKey, TValue1> source, Func<TKey, TValue1, TValue2> convertToValue2) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>((key, value) => new KeyValue<TKey, TValue2>(key, convertToValue2(key, value)), x => x, x => x);
}
public static IDisposableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableDictionary<TKey, TValue1> source, Func<TKey, TValue1, TValue2> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>((key, value) => new KeyValue<TKey, TValue2>(key, convertToValue2(key, value)), x => x, (key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value)), x => x);
}
public static IDisposableQueryableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableQueryableDictionary<TKey, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>(convertToValue2, x => x, (key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value)), x => x);
}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableQueryableReadOnlyDictionary<TKey, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>(convertToValue2, x => x, x => x);
}
public static IDisposableReadOnlyDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableReadOnlyDictionary<TKey, TValue1> source, Func<TKey, TValue1, TValue2> convertToValue2) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>((key, value) => new KeyValue<TKey, TValue2>(key, convertToValue2(key, value)), x => x, x => x);
}
public static IQueryableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IQueryableDictionary<TKey, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>(convertToValue2, x => x, (key, value) => new KeyValue<TKey, TValue1>(key, convertToValue1(key, value)), x => x);
}
public static IQueryableReadOnlyDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IQueryableReadOnlyDictionary<TKey, TValue1> source, Expression<Func<TValue1, TValue2>> convertToValue2) {
return source.WithMapping<TKey, TValue1, TKey, TValue2>(convertToValue2, x => x, x => x);
}
#endregion

#region WithMapping - transactional different key types
public static IReadWriteFactory<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadWriteFactory<IDisposableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableDictionary<TKey1, TValue1>> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
return new AnonymousReadWriteFactory<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>>(
() => source.CreateReader().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToKey1),
() => source.CreateWriter().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToValue1, convertToKey1));
}
public static IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableQueryableDictionary<TKey1, TValue1>> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
return new AnonymousReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>>(
() => source.CreateReader().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToKey1),
() => source.CreateWriter().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToValue1, convertToKey1));
}
public static IReadWriteFactory<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadWriteFactory<IDisposableReadOnlyDictionary<TKey1, TValue1>, IDisposableDictionary<TKey1, TValue1>> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
return new AnonymousReadWriteFactory<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>>(
() => source.CreateReader().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToKey1),
() => source.CreateWriter().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToValue1, convertToKey1));
}
public static IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, IDisposableQueryableDictionary<TKey1, TValue1>> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
return new AnonymousReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>>(
() => source.CreateReader().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToKey1),
() => source.CreateWriter().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToValue1, convertToKey1));
}
#endregion
#region WithMapping - transactional same key type
public static IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue2>, ICachedDisposableDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(this IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue1>, ICachedDisposableDictionary<TKey, TValue1>> source, Func<TKey, TValue1, TValue2> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return new AnonymousReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue2>, ICachedDisposableDictionary<TKey, TValue2>>(
() => source.CreateReader().WithMapping<TKey, TValue1, TValue2>(convertToValue2),
() => source.CreateWriter().WithMapping<TKey, TValue1, TValue2>(convertToValue2, convertToValue1));
}
public static IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue2>, ICachedDisposableQueryableDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(this IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue1>, ICachedDisposableQueryableDictionary<TKey, TValue1>> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return new AnonymousReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue2>, ICachedDisposableQueryableDictionary<TKey, TValue2>>(
() => source.CreateReader().WithMapping<TKey, TValue1, TValue2>(convertToValue2),
() => source.CreateWriter().WithMapping<TKey, TValue1, TValue2>(convertToValue2, convertToValue1));
}
public static IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue2>, IDisposableDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(this IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue1>, IDisposableDictionary<TKey, TValue1>> source, Func<TKey, TValue1, TValue2> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return new AnonymousReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue2>, IDisposableDictionary<TKey, TValue2>>(
() => source.CreateReader().WithMapping<TKey, TValue1, TValue2>(convertToValue2),
() => source.CreateWriter().WithMapping<TKey, TValue1, TValue2>(convertToValue2, convertToValue1));
}
public static IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue2>, IDisposableQueryableDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(this IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue1>, IDisposableQueryableDictionary<TKey, TValue1>> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return new AnonymousReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue2>, IDisposableQueryableDictionary<TKey, TValue2>>(
() => source.CreateReader().WithMapping<TKey, TValue1, TValue2>(convertToValue2),
() => source.CreateWriter().WithMapping<TKey, TValue1, TValue2>(convertToValue2, convertToValue1));
}
#endregion
#region WithMapping - read-only transactional different key types
public static IReadOnlyFactory<IDisposableReadOnlyDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadOnlyFactory<IDisposableReadOnlyDictionary<TKey1, TValue1>> source, Func<TKey1, TValue1, IKeyValue<TKey2, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
return new AnonymousReadOnlyFactory<IDisposableReadOnlyDictionary<TKey2, TValue2>>(
() => source.CreateReader().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToKey1));
}
public static IReadOnlyFactory<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadOnlyFactory<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey1, TKey2> convertToKey2, Func<TKey2, TValue2, IKeyValue<TKey1, TValue1>> convertToValue1, Func<TKey2, TKey1> convertToKey1) {
return new AnonymousReadOnlyFactory<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>>(
() => source.CreateReader().WithMapping<TKey1, TValue1, TKey2, TValue2>(convertToValue2, convertToKey2, convertToKey1));
}
#endregion
#region WithMapping - read-only transactional same key type
public static IReadOnlyFactory<IDisposableReadOnlyDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(this IReadOnlyFactory<IDisposableReadOnlyDictionary<TKey, TValue1>> source, Func<TKey, TValue1, TValue2> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return new AnonymousReadOnlyFactory<IDisposableReadOnlyDictionary<TKey, TValue2>>(
() => source.CreateReader().WithMapping<TKey, TValue1, TValue2>(convertToValue2));
}
public static IReadOnlyFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(this IReadOnlyFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue1>> source, Expression<Func<TValue1, TValue2>> convertToValue2, Func<TKey, TValue2, TValue1> convertToValue1) {
return new AnonymousReadOnlyFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue2>>(
() => source.CreateReader().WithMapping<TKey, TValue1, TValue2>(convertToValue2));
}
#endregion
}
}
