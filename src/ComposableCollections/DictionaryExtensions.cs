using System;
using System.Collections.Generic;
using System.Reflection;
using ComposableCollections.Dictionary;

namespace ComposableCollections
{
    public static class DictionaryExtensions
    {
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

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TValue, TInnerValue>(this IComposableDictionary<TKey, TInnerValue> source, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack) where TValue : class
        {
            return new AnonymousMapDictionary<TKey, TValue, TInnerValue>(source, convert, convertBack);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary. Also this caches the converted values.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithCachedMapping<TKey, TValue, TInnerValue>(this IComposableDictionary<TKey, TInnerValue> source, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false) where TValue : class
        {
            return new AnonymousCachedMapDictionary<TKey, TValue, TInnerValue>(source, convert, convertBack, cache, proactivelyConvertAllValues);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TValue, TInnerValue>(this IComposableDictionary<TKey, TInnerValue> source, Func<IEnumerable<IKeyValue<TKey, TValue>>, IEnumerable<IKeyValue<TKey, TInnerValue>>> convert, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack) where TValue : class
        {
            return new AnonymousBulkMapDictionary<TKey, TValue, TInnerValue>(source, convert, convertBack);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary. Also this caches the converted values.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithCachedMapping<TKey, TValue, TInnerValue>(this IComposableDictionary<TKey, TInnerValue> source, Func<IEnumerable<IKeyValue<TKey, TValue>>, IEnumerable<IKeyValue<TKey, TInnerValue>>> convert, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false) where TValue : class
        {
            return new AnonymousCachedBulkMapDictionary<TKey, TValue, TInnerValue>(source, convert, convertBack, cache, proactivelyConvertAllValues);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static ICacheDictionary<TKey, TValue> WithCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushTo, IComposableDictionary<TKey, TValue> cache = null)
        {
            return new ConcurrentCachingDictionary<TKey, TValue>(flushTo, cache);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that lets you optionally create values when
        /// they're accessed, on demand.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that lets you optionally update values when
        /// they're accessed, on demand.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue)
        {
            return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that has a built-in key, which means you're telling
        /// the object how to get the key from a value. That means any API where you pass in a TValue, you
        /// won't have to tell the API what the key is.
        /// </summary>
        public static IDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, Func<TValue, TKey> key)
        {
            return new AnonymousDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, key);
        }
    }
}