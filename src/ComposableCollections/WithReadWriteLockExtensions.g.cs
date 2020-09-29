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
public static class WithReadWriteLockExtensions {
public static IReadCachedQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadCachedQueryableDictionary<TKey, TValue> adapted) {
return new ReadCachedQueryableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IDisposableQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> adapted) {
return new DisposableQueryableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted) {
return new ReadWriteCachedDisposableQueryableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted) {
return new ReadWriteCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IReadWriteCachedQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadWriteCachedQueryableDictionary<TKey, TValue> adapted) {
return new ReadWriteCachedQueryableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IWriteCachedDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted) {
return new WriteCachedReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted) {
return new WriteCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IWriteCachedDisposableQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IWriteCachedDisposableQueryableDictionary<TKey, TValue> adapted) {
return new WriteCachedDisposableQueryableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IComposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IComposableDictionary<TKey, TValue> source) {
return new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);}
public static IReadCachedDisposableQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TValue> adapted) {
return new ReadCachedDisposableQueryableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted) {
return new DisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted) {
return new ReadCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IWriteCachedQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IWriteCachedQueryableDictionary<TKey, TValue> adapted) {
return new WriteCachedQueryableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IReadWriteCachedDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted) {
return new ReadWriteCachedReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
}
}
