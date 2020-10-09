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
using ComposableCollections.Dictionary.Write;
using ComposableCollections.DictionaryWithBuiltInKey.Adapters;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
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
public static IReadCachedQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2) {
return new ReadCachedQueryableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadCachedDisposableQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedDisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertToKey2, convertToKey1);}
public static IReadCachedWriteCachedQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedWriteCachedQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedWriteCachedQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IComposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IComposableDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IComposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IComposableDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableQueryableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableQueryableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertToKey2, convertToKey1);}
public static IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedWriteCachedDisposableQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedWriteCachedDisposableQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadCachedQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new WriteCachedDisposableQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedDisposableQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2) {
return new DisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new DisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceValue, TValue> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertToKey2, convertToKey1);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertToKey2, convertToKey1);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2) {
return new ReadCachedReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCachedReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadWriteCachedQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadWriteCachedQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2) {
return new ReadCachedDisposableQueryableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedWriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedWriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadCachedWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadCachedWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2) {
return new DisposableQueryableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IQueryableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new QueryableMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertToKey2, convertToKey1);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedDisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertToKey2, convertToKey1);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedDisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertToKey2, convertToKey1);}
public static IReadCachedWriteCachedQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedWriteCachedQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadCachedWriteCachedQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadWriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadWriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IQueryableReadOnlyDictionary<TKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2) {
return new QueryableMappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IQueryableDictionary<TKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new QueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2, convertTo1);}
public static IDisposableQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadWriteCachedDisposableQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IComposableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceValue, TValue> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertToKey2, convertToKey1);}
public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IComposableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertToKey2, convertToKey1);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2) {
return new ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadWriteCachedQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedWriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedWriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedQueryableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedQueryableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedQueryableMappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertToKey2, convertToKey1);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadWriteCachedDisposableQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> innerValues, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2, convertTo1);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2, convertTo1);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new WriteCachedQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TSourceKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedDisposableQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadCachedWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadCachedWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new ReadCachedWriteCachedDisposableQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertToKey2, convertToKey1);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertToKey2, convertToKey1);}
public static IQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IQueryableDictionary<TSourceKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new QueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadWriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadWriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableReadOnlyDictionary<TKey, TSourceValue> innerValues, Func<TSourceValue, TValue> convertTo2) {
return new MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2);}
public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableReadOnlyDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2);}
public static IDisposableQueryableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableQueryableDictionary<TKey, TSourceValue> adapted, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1) {
return new DisposableQueryableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedQueryableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedQueryableDictionary<TSourceKey, TSourceValue> innerValues, Expression<Func<TSourceValue, TValue>> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedQueryableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IReadCachedDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceValue, TValue> convertTo2, Func<TValue, TSourceValue> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new ReadCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1);}
}
}
