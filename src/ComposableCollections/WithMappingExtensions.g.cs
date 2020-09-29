﻿using ComposableCollections.Common;
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
public static IComposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IComposableDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new MappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDictionary<TKey, TSourceValue> adapted) {
return new ReadWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new ReadWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadWriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableDictionary<TSourceKey, TSourceValue> adapted) {
return new DisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IComposableReadOnlyDictionary<TSourceKey, TSourceValue> innerValues, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new MappingKeysAndValuesReadOnlyDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(innerValues, convertTo2, convertToKey2, convertToKey1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted) {
return new WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new WriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new DisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new DisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDictionary<TKey, TSourceValue> adapted) {
return new WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IWriteCachedDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new WriteCachedMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TSourceValue> adapted) {
return new ReadWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new ReadWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadWriteCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new ReadCachedReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCachedReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionary<TKey, TSourceValue> adapted) {
return new DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new DisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> innerValues) {
return new MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2, convertTo1);}
public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new MappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2, convertTo1);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDictionary<TSourceKey, TSourceValue> adapted) {
return new WriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted);}
public static IWriteCachedDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableDictionary<TKey, TSourceValue> adapted) {
return new ReadCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, TValue> convertTo2, Func<TKey, TValue, TSourceValue> convertTo1) {
return new ReadCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TKey, TSourceValue>> convertTo1) {
return new ReadCachedDisposableMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2, convertTo1);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted) {
return new DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted);}
public static IDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IDisposableReadOnlyDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new DisposableReadOnlyMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertToKey2, convertToKey1);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted) {
return new WriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithMapping<TSourceKey, TSourceValue, TKey, TValue>(this IWriteCachedDisposableDictionary<TSourceKey, TSourceValue> adapted, Func<TSourceKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2, Func<TKey, TValue, IKeyValue<TSourceKey, TSourceValue>> convertTo1, Func<TSourceKey, TKey> convertToKey2, Func<TKey, TSourceKey> convertToKey1) {
return new WriteCachedDisposableMappingKeysAndValuesDictionaryAdapter<TSourceKey, TSourceValue, TKey, TValue>(adapted, convertTo2, convertTo1, convertToKey2, convertToKey1);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> adapted) {
return new ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IReadCachedDisposableReadOnlyDictionary<TKey, TSourceValue> adapted, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new ReadCachedDisposableReadOnlyMappingValuesDictionaryAdapter<TKey, TSourceValue, TValue>(adapted, convertTo2);}
public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TSourceValue, TValue>(this IComposableReadOnlyDictionary<TKey, TSourceValue> innerValues, Func<TKey, TSourceValue, IKeyValue<TKey, TValue>> convertTo2) {
return new MappingValuesReadOnlyDictionaryAdapter<TKey, TSourceValue, TValue>(innerValues, convertTo2);}
}
}
