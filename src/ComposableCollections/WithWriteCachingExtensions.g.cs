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
public static class WithWriteCachingExtensions {
public static ReadWriteCachedQueryable<TKey, TValue> WithWriteCaching<TKey, TValue>(this ReadWriteCachedQueryable<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new eadConcurrentWriteCachedQueryable<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadWriteCachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadCachedWriteCachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadCachedWriteCachedDisposableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadCachedConcurrentWriteCachedDisposableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IWriteCachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushCacheTo, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(flushCacheTo, addedOrUpdated, removed);}
public static IReadCachedWriteCachedQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadCachedWriteCachedQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadCachedConcurrentWriteCachedQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadConcurrentWriteCachedQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static ReadWriteCachedDisposable<TKey, TValue> WithWriteCaching<TKey, TValue>(this ReadWriteCachedDisposable<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new eadConcurrentWriteCachedDisposable<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedDisposableQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadCachedWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadCachedConcurrentWriteCachedDisposableQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadConcurrentWriteCachedDisposableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadConcurrentWriteCachedDisposableQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadCachedWriteCachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadCachedWriteCachedDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadCachedConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static ReadWriteCachedDisposableQueryable<TKey, TValue> WithWriteCaching<TKey, TValue>(this ReadWriteCachedDisposableQueryable<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new eadConcurrentWriteCachedDisposableQueryable<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static ReadWriteCached<TKey, TValue> WithWriteCaching<TKey, TValue>(this ReadWriteCached<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new eadConcurrentWriteCached<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedDisposableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
}
}
