using ComposableCollections.Common;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Anonymous;
using ComposableCollections.Dictionary.Base;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Exceptions;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Transactional;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using ComposableCollections.Dictionary.Write;
using ComposableCollections.List;
using ComposableCollections.Set;
using ComposableCollections.Set.Base;
using ComposableCollections.Set.Exceptions;
using ComposableCollections.Set.Write;
using ComposableCollections.Utilities;
using GenericNumbers;
using GenericNumbers.Relational;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;
using UtilityDisposables;
namespace ComposableCollections {
public static class WithMappingExtensions {
public static IQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IQueryableReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new QueryableReadOnly<TKey, TSourceValue, TValue>(adapted);}
public static IQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new QueryableReadOnly<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedQueryableReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new ReadCachedQueryableReadOnly<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCachedQueryableReadOnly<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedWriteDisposableQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedWriteDisposableQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedReadWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadWriteQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadWriteQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadWriteQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadWriteQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedReadDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDictionary<TSourceKey, TSourceValue> adapted) {
return new WriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static ICachedReadWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadWriteDisposableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadWriteDisposableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadWriteDisposableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadWriteDisposableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new DisposableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new DisposableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDictionary<TKey, TSourceValue> adapted) {
return new WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadDisposableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadDisposableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedReadWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadWriteDisposableQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadWriteDisposableQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedReadDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadDisposableQueryableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadDisposableQueryableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableDictionary<TKey, TSourceValue> adapted) {
return new ReadCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new ReadCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new DisposableReadOnly<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new DisposableReadOnly<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedReadDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDisposableQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadDisposableQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDisposableQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadDisposableQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDictionary<TKey, TSourceValue> adapted) {
return new ReadWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new ReadWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new DisposableQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new DisposableQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> adapted) {
return new Composable<TKey, TSourceValue, TValue>(adapted);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new Composable<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedWriteDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedWriteDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedWriteWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedWriteDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedWriteDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedWriteWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new WithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static IDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new WithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted) {
return new WriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new DisposableQueryableReadOnly<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new DisposableQueryableReadOnly<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IDisposableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new DisposableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new DisposableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new ReadCachedReadOnly<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCachedReadOnly<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IComposableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertToKey2, convertToKey1);}
public static ICachedReadDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadDisposableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadDisposableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableReadOnlyDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2);}
public static IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new QueryableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new QueryableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new ReadCachedDisposableQueryableReadOnly<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCachedDisposableQueryableReadOnly<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionary<TKey, TSourceValue> adapted) {
return new DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedWriteDisposableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedWriteDisposableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new DisposableQueryableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new DisposableQueryableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadQueryableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadQueryableReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2, convertTo1);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2, convertTo1);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted) {
return new DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertToKey2, convertToKey1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted) {
return new WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new ReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadOnlyDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadOnlyWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new QueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static IQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new QueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedWriteQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedWriteQueryableWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IComposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IComposableDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDictionary<TKey, TSourceValue> adapted) {
return new ReadCached<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCached<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static ICachedReadWriteDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadWriteDictionaryWithBuiltInKey<TKey, TSourceValue> adapted) {
return new CachedReadWriteWithBuiltInKey<TKey, TSourceValue, TValue>(adapted);}
public static ICachedReadWriteDictionaryWithBuiltInKey<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this ICachedReadWriteDictionaryWithBuiltInKey<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new CachedReadWriteWithBuiltInKey<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableDictionary<TSourceKey, TSourceValue> adapted) {
return new DisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TSourceValue> adapted) {
return new ReadWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new ReadWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
}
}
