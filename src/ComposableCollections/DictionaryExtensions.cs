using System;
using System.Collections.Generic;
using ComposableCollections.Common;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Transactional;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using UtilityDisposables;

namespace ComposableCollections
{
    public static class DictionaryExtensions
    {
        #region To and from IKeyValue
        
        /// <summary>
        /// Converts from a ComposableCollections IKeyValue object to a normal .NET KeyValuePair object.
        /// </summary>
        public static KeyValuePair<TKey, TValue> ToKeyValuePair<TKey, TValue>(this IKeyValue<TKey, TValue> source)
        {
            return new KeyValuePair<TKey, TValue>(source.Key, source.Value);
        }

        /// <summary>
        /// Converts from a normal .NET KeyValuePair object to a ComposableCollections IKeyValue object.
        /// </summary>
        public static IKeyValue<TKey, TValue> ToKeyValue<TKey, TValue>(this KeyValuePair<TKey, TValue> source)
        {
            return new KeyValue<TKey, TValue>(source.Key, source.Value);
        }
        
        #endregion
        
        #region Creating a new Composable Dictionary
        
        /// <summary>
        /// Copies all the items into a new composable dictionary.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> ToComposableDictionary<TKey, TValue>(
            this IEnumerable<IKeyValue<TKey, TValue>> source)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source);
            return results;
        }

        /// <summary>
        /// Copies all the items into a new composable dictionary.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> ToComposableDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source);
            return results;
        }
        
        /// <summary>
        /// Copies all the items into a new composable dictionary.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> ToComposableDictionary<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source, key, value);
            return results;
        }
        
        /// <summary>
        /// Copies all the items into a new composable read-only dictionary.
        /// </summary>
        public static IComposableReadOnlyDictionary<TKey, TValue> ToComposableReadOnlyDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source);
            return results;
        }
        
        /// <summary>
        /// Copies all the items into a new composable read-only dictionary.
        /// </summary>
        public static IComposableReadOnlyDictionary<TKey, TValue> ToComposableReadOnlyDictionary<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source, key, value);
            return results;
        }

        #endregion
        
        #region WithWriteCaching
        
        public static ICachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, null);
        }

        public static ICachedQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, null);
        }

        public static ICachedDisposableQueryableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, null);
        }

        public static ICachedDisposableDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, null);
        }

        public static ICachedDictionaryWithBuiltInKey<TKey, TValue> WithWriteCaching<TKey, TValue>(this IDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.CachedDictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> WithWriteCaching<TKey, TValue>(this IQueryableDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.CachedDictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithWriteCaching<TKey, TValue>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            object p)
        {
            return WithWriteCachingTransformations<TKey, TValue>.CachedDictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> WithWriteCaching<TKey, TValue>(this IDisposableDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.CachedDictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithWriteCaching<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> WithWriteCaching<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithWriteCaching<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithWriteCaching<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source)
        {
            return WithWriteCachingTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, null);
        }
        
        #endregion
        
        #region WithMapping - two types of keys
        
        public static IComposableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IComposableDictionary<TKey1, TValue1> source, Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static IQueryableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IQueryableDictionary<TKey1, TValue1> source, Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ICachedDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDictionary<TKey1, TValue1> source, Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ICachedDisposableQueryableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDisposableQueryableDictionary<TKey1, TValue1> source, Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static IDisposableQueryableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableQueryableDictionary<TKey1, TValue1> source, Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ICachedQueryableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedQueryableDictionary<TKey1, TValue1> source, Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ICachedDisposableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDisposableDictionary<TKey1, TValue1> source, Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static IDisposableDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableDictionary<TKey1, TValue1> source, Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ICachedDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static IQueryableDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static ICachedQueryableDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this ICachedDisposableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static IDisposableDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static IDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static IQueryableReadOnlyDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IQueryableReadOnlyDictionary<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableReadOnlyDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static IComposableReadOnlyDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IComposableReadOnlyDictionary<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableReadOnlyDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static IDisposableQueryableReadOnlyDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableQueryableReadOnlyDictionary<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableReadOnlyDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static IDisposableReadOnlyDictionary<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableReadOnlyDictionary<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ComposableReadOnlyDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this 
            IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ReadOnlyDictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static IQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ReadOnlyDictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ReadOnlyDictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static IReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> WithMapping<TKey1, TValue1, TKey2, TValue2>(this IReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.ReadOnlyDictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(
            this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, IDisposableDictionary<TKey1, TValue1>> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.TransactionalTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(
            this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableDictionary<TKey1, TValue1>> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.TransactionalTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, IDisposableQueryableDictionary<TKey1, TValue1>> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.TransactionalTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableQueryableDictionary<TKey1, TValue1>> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.TransactionalTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1));
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(
            this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.TransactionalTransformationsWithBuiltInKey.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(
            this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.TransactionalTransformationsWithBuiltInKey.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.TransactionalTransformationsWithBuiltInKey.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> WithMapping<TKey1, TValue1, TKey2, TValue2>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            Func<TKey1, TKey2> convertKeyTo2, Func<TValue1, TValue2> convertValueTo2, Func<TKey2, TKey1> convertKeyTo1, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey2> getKey)
        {
            return WithMappingTransformations<TKey1, TValue1, TKey2, TValue2>.TransactionalTransformationsWithBuiltInKey.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2, convertKeyTo2, convertKeyTo1), getKey));
        }
        
        #endregion
        
        #region WithMapping - one type of key
        
        public static IComposableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IComposableDictionary<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static IQueryableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IQueryableDictionary<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ICachedDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDictionary<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ICachedDisposableQueryableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDisposableQueryableDictionary<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static IDisposableQueryableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableQueryableDictionary<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ICachedQueryableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedQueryableDictionary<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ICachedDisposableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDisposableDictionary<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static IDisposableDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableDictionary<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ICachedDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDictionaryWithBuiltInKey<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static IQueryableDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IQueryableDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static IDisposableDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static IDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.DictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static IQueryableReadOnlyDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IQueryableReadOnlyDictionary<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableReadOnlyDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static IComposableReadOnlyDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IComposableReadOnlyDictionary<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableReadOnlyDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static IDisposableQueryableReadOnlyDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableQueryableReadOnlyDictionary<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableReadOnlyDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static IDisposableReadOnlyDictionary<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableReadOnlyDictionary<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ComposableReadOnlyDictionaryTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this 
            IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue1> source, Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ReadOnlyDictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ReadOnlyDictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ReadOnlyDictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static IReadOnlyDictionaryWithBuiltInKey<TKey, TValue2> WithMapping<TKey, TValue1, TValue2>(this IReadOnlyDictionaryWithBuiltInKey<TKey, TValue1> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.ReadOnlyDictionaryWithBuiltInKeyTransformations.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue2>, IDisposableDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(
            this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue1>, IDisposableDictionary<TKey, TValue1>> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.TransactionalTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue2>, ICachedDisposableDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(
            this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue1>, ICachedDisposableDictionary<TKey, TValue1>> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.TransactionalTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue2>, IDisposableQueryableDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue1>, IDisposableQueryableDictionary<TKey, TValue1>> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.TransactionalTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue2>, ICachedDisposableQueryableDictionary<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue1>, ICachedDisposableQueryableDictionary<TKey, TValue1>> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.TransactionalTransformations.Transform(source, Tuple.Create(convertValueTo1, convertValueTo2));
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue2>, IDisposableDictionaryWithBuiltInKey<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(
            this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue1>, IDisposableDictionaryWithBuiltInKey<TKey, TValue1>> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.TransactionalTransformationsWithBuiltInKey.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue2>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(
            this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue1>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue1>> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.TransactionalTransformationsWithBuiltInKey.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue2>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue1>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue1>> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.TransactionalTransformationsWithBuiltInKey.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue2>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue2>> WithMapping<TKey, TValue1, TValue2>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue1>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue1>> source,
            Func<TValue1, TValue2> convertValueTo2, Func<TValue2, TValue1> convertValueTo1, Func<TValue2, TKey> getKey)
        {
            return WithMappingTransformations<TKey, TValue1, TValue2>.TransactionalTransformationsWithBuiltInKey.Transform(source, Tuple.Create(Tuple.Create(convertValueTo1, convertValueTo2), getKey));
        }
        
        #endregion
        
        #region WithDefaultValue
        
        public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return WithDefaultValueTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static IQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return WithDefaultValueTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return WithDefaultValueTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return WithDefaultValueTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static IDisposableQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return WithDefaultValueTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedQueryableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return WithDefaultValueTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return WithDefaultValueTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static IDisposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return WithDefaultValueTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDictionaryWithBuiltInKey<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDictionaryWithBuiltInKey<TKey, TValue> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static IQueryableDictionaryWithBuiltInKey<TKey, TValue> WithDefaultValue<TKey, TValue>(this IQueryableDictionaryWithBuiltInKey<TKey, TValue> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> WithDefaultValue<TKey, TValue>(this ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> source,
            GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static IDisposableDictionaryWithBuiltInKey<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDisposableDictionaryWithBuiltInKey<TKey, TValue> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static IDictionaryWithBuiltInKey<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDictionaryWithBuiltInKey<TKey, TValue> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source, GetDefaultValue<TKey, TValue> parameter)
        {
            return WithDefaultValueTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, parameter);
        }
        
        #endregion
        
        #region WithRefreshing
        
        public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
        {
            return WithRefreshingTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static IQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
        {
            return WithRefreshingTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
        {
            return WithRefreshingTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
        {
            return WithRefreshingTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static IDisposableQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
        {
            return WithRefreshingTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedQueryableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
        {
            return WithRefreshingTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
        {
            return WithRefreshingTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static IDisposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
        {
            return WithRefreshingTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDictionaryWithBuiltInKey<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDictionaryWithBuiltInKey<TKey, TValue> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static IQueryableDictionaryWithBuiltInKey<TKey, TValue> WithRefreshing<TKey, TValue>(this IQueryableDictionaryWithBuiltInKey<TKey, TValue> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> WithRefreshing<TKey, TValue>(this ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> source,
            RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static IDisposableDictionaryWithBuiltInKey<TKey, TValue> WithRefreshing<TKey, TValue>(this IDisposableDictionaryWithBuiltInKey<TKey, TValue> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static IDictionaryWithBuiltInKey<TKey, TValue> WithRefreshing<TKey, TValue>(this IDictionaryWithBuiltInKey<TKey, TValue> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source, RefreshValue<TKey, TValue> parameter)
        {
            return WithRefreshingTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, parameter);
        }
        
        #endregion

        #region WithBuiltInKey
        
        public static ICachedDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this ICachedDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new CachedDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static IDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new DictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new DisposableQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new QueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new CachedDisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new DisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static IQueryableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new QueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new CachedQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new CachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDisposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new DisposableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static IDisposableDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new DisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }
        public static IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) {
            return new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, getKey);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> source, Func<TValue, TKey> getKey)
        {
            return source.Select(readOnly => readOnly.WithBuiltInKey(getKey),
                readWrite => readWrite.WithBuiltInKey(getKey));
        }
        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source, Func<TValue, TKey> getKey) {
            return source.Select(readOnly => readOnly.WithBuiltInKey(getKey),
                readWrite => readWrite.WithBuiltInKey(getKey));
        }
        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(
            this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> source, Func<TValue, TKey> getKey) {
            return source.Select(readOnly => readOnly.WithBuiltInKey(getKey),
                readWrite => readWrite.WithBuiltInKey(getKey));
        }
        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(
            this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, Func<TValue, TKey> getKey) {
            return source.Select(readOnly => readOnly.WithBuiltInKey(getKey),
                readWrite => readWrite.WithBuiltInKey(getKey));
        }
        
        #endregion
        
        #region WithReadWriteLock
        
        public static IComposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IComposableDictionary<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, null);
        }

        public static IQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, null);
        }

        public static ICachedDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDictionary<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, null);
        }

        public static ICachedDisposableQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDisposableQueryableDictionary<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, null);
        }

        public static IDisposableQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, null);
        }

        public static ICachedQueryableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedQueryableDictionary<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, null);
        }

        public static ICachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDisposableDictionary<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, null);
        }

        public static IDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.ComposableDictionaryTransformations.Transform(source, null);
        }

        public static ICachedDictionaryWithBuiltInKey<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static IQueryableDictionaryWithBuiltInKey<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IQueryableDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> WithReadWriteLock<TKey, TValue>(this ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static IDisposableDictionaryWithBuiltInKey<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static IDictionaryWithBuiltInKey<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDictionaryWithBuiltInKey<TKey, TValue> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.DictionaryWithBuiltInKeyTransformations.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithReadWriteLock<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> WithReadWriteLock<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> WithReadWriteLock<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> WithReadWriteLock<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithReadWriteLock<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithReadWriteLock<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithReadWriteLock<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, null);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithReadWriteLock<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source)
        {
            return WithReadWriteLockTransformations<TKey, TValue>.TransactionalTransformationsWithBuiltInKey.Transform(source, null);
        }
        
        #endregion
        
        #region WithEachMethodAsSeparateTransaction
        
        /// <summary>
        /// Converts a transactional read-only dictionary into a non-transactional one by making each method call
        /// a separate transaction.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithEachMethodAsSeparateTransaction<TKey, TValue>(
            this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source)
        {
            return new DetransactionalDictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// Converts a transactional read-only dictionary into a non-transactional one by making each method call
        /// a separate transaction.
        /// </summary>
        public static IComposableReadOnlyDictionary<TKey, TValue> WithEachMethodAsSeparateTransaction<TKey, TValue>(
            this IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>> source)
        {
            return new ReadOnlyDetransactionalDictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// Converts a transactional read-only queryable dictionary into a non-transactional one by making each method call
        /// a separate transaction.
        /// </summary>
        public static IQueryableDictionary<TKey, TValue> WithEachMethodAsSeparateTransaction<TKey, TValue>(
            this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source)
        {
            return new DetransactionalQueryableDictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// Converts a transactional read-only queryable dictionary into a non-transactional one by making each method call
        /// a separate transaction.
        /// </summary>
        public static IQueryableReadOnlyDictionary<TKey, TValue> WithEachMethodAsSeparateTransaction<TKey, TValue>(
            this IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>> source)
        {
            return new ReadOnlyDetransactionalQueryableDictionary<TKey, TValue>(source);
        }
        
        #endregion
        
        #region Select
        
        /// <summary>
        /// Converts the read-only and read/write objects
        /// </summary>
        public static ITransactionalCollection<TReadOnly2, TReadWrite2>
            Select<TReadOnly1, TReadWrite1, TReadOnly2, TReadWrite2>(
                this ITransactionalCollection<TReadOnly1, TReadWrite1> source, Func<TReadOnly1, TReadOnly2> readOnly,
                Func<TReadWrite1, TReadWrite2> readWrite) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable where TReadWrite1 : IDisposable where TReadWrite2 : IDisposable
        {
            return new AnonymousTransactionalCollection<TReadOnly2, TReadWrite2>(() => readOnly(source.BeginRead()), () => readWrite(source.BeginWrite()));
        }
        
        /// <summary>
        /// Converts the read-only object
        /// </summary>
        public static IReadOnlyTransactionalCollection<TReadOnly2>
            Select<TReadOnly1, TReadOnly2>(
                this IReadOnlyTransactionalCollection<TReadOnly1> source, Func<TReadOnly1, TReadOnly2> readOnly) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable
        {
            return new AnonymousReadOnlyTransactionalCollection<TReadOnly2>(() => readOnly(source.BeginRead()));
        }

        #endregion
        
        #region SelectMany
        
        /// <summary>
        /// Converts the read-only and read/write objects
        /// </summary>
        public static ITransactionalCollection<TReadOnly2, TReadWrite2>
            SelectMany<TReadOnly1, TReadWrite1, TReadOnly2, TReadWrite2>(
                this ITransactionalCollection<TReadOnly1, TReadWrite1> source, Func<TReadOnly1, TReadOnly2> readOnly,
                Func<TReadWrite1, TReadWrite2> readWrite) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable where TReadWrite1 : IDisposable where TReadWrite2 : IDisposable
        {
            return new AnonymousTransactionalCollection<TReadOnly2, TReadWrite2>(() => readOnly(source.BeginRead()), () => readWrite(source.BeginWrite()));
        }
        
        /// <summary>
        /// Flattens a nested transaction. Note that the readOnly parameter must ensure that the TReadOnly1 gets disposed
        /// when its return value is disposed. The same is true of readWrite.
        /// </summary>
        public static ITransactionalCollection<TReadOnly2, TReadWrite2>
            SelectMany<TReadOnly1, TReadWrite1, TReadOnly2, TReadWrite2>(
                this ITransactionalCollection<TReadOnly1, TReadWrite1> source, Func<TReadOnly1, IDisposableReadOnlyTransactionalCollection<TReadOnly2>> readOnly, Func<TReadWrite1, IDisposableTransactionalCollection<TReadOnly2, TReadWrite2>> readWrite) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable where TReadWrite1 : IDisposable where TReadWrite2 : IDisposable
        {
            return new AnonymousTransactionalCollection<TReadOnly2, TReadWrite2>(() =>
            {
                var it = readOnly(source.BeginRead());
                return it.BeginRead();
            }, () =>
            {
                var it = readWrite(source.BeginWrite());
                return it.BeginWrite();
            });
        }

        /// <summary>
        /// Flattens a nested transaction. Note that the readOnly parameter must ensure that the TReadOnly1 gets disposed
        /// when its return value is disposed.
        /// </summary>
        public static IReadOnlyTransactionalCollection<TReadOnly2>
            SelectMany<TReadOnly1, TReadOnly2>(
                this IReadOnlyTransactionalCollection<TReadOnly1> source, Func<TReadOnly1, IDisposableReadOnlyTransactionalCollection<TReadOnly2>> readOnly) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable
        {
            return new AnonymousReadOnlyTransactionalCollection<TReadOnly2>(() =>
            {
                var it = readOnly(source.BeginRead());
                return it.BeginRead();
            });
        }
        
        #endregion
        
        #region WithWritesCombinedAtEndOfTransaction
        
        /// <summary>
        /// Converts the source to a transactional dictionary that keeps all writes pending until the transaction is completed.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithWritesCombinedAtEndOfTransaction<TKey, TValue>(this IComposableDictionary<TKey, TValue> source)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(() =>
            {
                return new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(source, EmptyDisposable.Default);
            }, () =>
            {
                var cache = source.WithWriteCaching();
                return new DisposableDictionaryAdapter<TKey, TValue>(cache, new AnonymousDisposable(() => cache.FlushCache()));
            });
        }

        /// <summary>
        /// Converts the source to a transactional dictionary that keeps all writes pending until the transaction is completed.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithWritesCombinedAtEndOfTransaction<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(source.BeginRead, () =>
            {
                var disposableDictionary = source.BeginWrite();
                var cache = disposableDictionary.WithWriteCaching();
                return new DisposableDictionaryAdapter<TKey, TValue>(cache, new AnonymousDisposable(() =>
                {
                    cache.FlushCache();
                    disposableDictionary.Dispose();
                }));
            });
        }

        #endregion
    }
}