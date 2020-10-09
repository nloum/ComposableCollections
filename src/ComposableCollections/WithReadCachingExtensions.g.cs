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
public static class WithReadCachingExtensions {
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithReadCaching<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> innerValues) {
return new ReadWriteCachedDisposableDictionaryAdapter<TKey, TValue>(innerValues);}
public static IReadCachedDisposableReadOnlyDictionary<TKey, TValue> WithReadCaching<TKey, TValue>(this IDisposableReadOnlyDictionary<TKey, TValue> innerValues) {
return new ReadCachedDisposableReadOnlyDictionaryAdapter<TKey, TValue>(innerValues);}
public static IReadCachedDictionary<TKey, TValue> WithReadCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> innerValues) {
return new ReadCachedDictionaryAdapter<TKey, TValue>(innerValues);}
public static IReadCachedReadOnlyDictionary<TKey, TValue> WithReadCaching<TKey, TValue>(this IComposableReadOnlyDictionary<TKey, TValue> innerValues) {
return new ReadCachedReadOnlyDictionaryAdapter<TKey, TValue>(innerValues);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithReadCaching<TKey, TValue>(this IDisposableDictionary<TKey, TValue> innerValues) {
return new ReadCachedDisposableDictionaryAdapter<TKey, TValue>(innerValues);}
}
}
