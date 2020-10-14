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
public static class WithWriteCachingExtensions {
public static IWriteCachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushCacheTo, IComposableDictionary<TKey, TValue> addedOrUpdated, IComposableDictionary<TKey, TValue> removed) {
return new ConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(flushCacheTo, addedOrUpdated, removed);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source) {
return new DisposableConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(source);}
public static IReadWriteCachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadCachedDictionary<TKey, TValue> source) {
return new ReadCachedConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(source);}
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> source) {
return new ReadCachedDisposableConcurrentWriteCachedDictionaryAdapter<TKey, TValue>(source);}
}
}
