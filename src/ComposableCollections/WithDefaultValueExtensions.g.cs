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
public static class WithDefaultValueExtensions {
public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);}
public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);}
public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new ReadCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new WriteCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new WriteCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new WriteCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new DisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new DisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new DisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new DisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new DisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new DisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new QueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new QueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new QueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new ReadCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new WriteCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new WriteCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new WriteCachedDisposableQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new WriteCachedGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new WriteCachedGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new WriteCachedGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadWriteCachedDisposableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new WriteCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new WriteCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new WriteCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedQueryableDictionary<TKey, TValue> adapted, GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue) {
return new ReadCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedQueryableDictionary<TKey, TValue> adapted, GetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
public static IReadCachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IReadCachedQueryableDictionary<TKey, TValue> adapted, AlwaysGetDefaultValue<TKey, TValue> getDefaultValue) {
return new ReadCachedQueryableGetOrDefaultDictionaryDecorator<TKey, TValue>(adapted, getDefaultValue);}
}
}
