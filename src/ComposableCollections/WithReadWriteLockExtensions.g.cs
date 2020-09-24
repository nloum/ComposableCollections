using ComposableCollections.Common;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Adapters;
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
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted) {
return new ReadWriteCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted) {
return new WriteCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IComposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IComposableDictionary<TKey, TValue> source) {
return new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted) {
return new ReadCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted) {
return new DisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IReadWriteCachedDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted) {
return new ReadWriteCachedReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IWriteCachedDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted) {
return new WriteCachedReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
}
}
