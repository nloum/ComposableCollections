using Algorithms.Common;
using Algorithms.Sorting;
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
using DataStructures.Common;
using DataStructures.Graphs;
using DataStructures.Hashing;
using DataStructures.Heaps;
using DataStructures.Lists;
using DataStructures.Trees;
using GenericNumbers;
using GenericNumbers.Relational;
using SimpleMonads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using UtilityDisposables;
namespace ComposableCollections {
public static class WithBuiltInKeyExtensions {
public static IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new DisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadWriteCachedDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadWriteCachedDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadCachedDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadCachedDisposableQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadCachedDisposableQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadCachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadCachedDisposableQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadCachedDisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IDisposableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new DisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadCachedQueryableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadCachedQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadCachedQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadWriteCachedDisposableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadWriteCachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new QueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IQueryableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new QueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadCachedQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadCachedQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadCachedQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadCachedDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadCachedDisposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadCachedDisposableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadCachedReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadCachedReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadCachedReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDisposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new DisposableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadCachedDisposableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadCachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IReadCachedDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IReadCachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new ReadCachedDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new DisposableQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new DictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
public static IWriteCachedDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
return new WriteCachedDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);}
}
}
