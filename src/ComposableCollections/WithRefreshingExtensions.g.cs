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
public static class WithRefreshingExtensions {
public static IDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new DisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new DisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new DisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadWriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadWriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadWriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new WriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new WriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new WriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new WriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new WriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new WriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new DisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new DisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new DisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new WriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new WriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new WriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new QueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new QueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new QueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadWriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadWriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadWriteCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadWriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadWriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadWriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDisposableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDisposableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDisposableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedDisposableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);}
public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue) {
return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);}
public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);}
public static IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedWriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new WriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new WriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new WriteCachedDisposableQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadWriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadWriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadWriteCachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadWriteCachedGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedQueryableDictionary<TKey, TValue> adapted, RefreshValueWithOptionalPersistence<TKey, TValue> refreshValue) {
return new ReadCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedQueryableDictionary<TKey, TValue> adapted, RefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
public static IReadCachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IReadCachedQueryableDictionary<TKey, TValue> adapted, AlwaysRefreshValue<TKey, TValue> refreshValue) {
return new ReadCachedQueryableGetOrRefreshDictionaryDecorator<TKey, TValue>(adapted, refreshValue);}
}
}
