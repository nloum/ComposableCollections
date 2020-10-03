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
public static class WithWriteCachingExtensions {
public static IWriteCachedQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadConcurrentWriteCachedDisposableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadConcurrentWriteCachedQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IReadWriteCachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IWriteCachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushCacheTo, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(flushCacheTo, addedOrUpdated, removed);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ReadConcurrentWriteCachedDisposableQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedDisposableQueryableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedDisposableDictionaryAdapter<TKey, TValue>(adapted, addedOrUpdated, removed);}
}
}
