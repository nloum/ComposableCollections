using System;
using System.Collections.Generic;
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

        public static IComposableDictionary<TKey, TValue> ToComposableDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source);
            return results;
        }
        
        public static IComposableDictionary<TKey, TValue> ToComposableDictionary<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source, key, value);
            return results;
        }
        
        public static IComposableReadOnlyDictionary<TKey, TValue> ToComposableReadOnlyDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source);
            return results;
        }
        
        public static IComposableReadOnlyDictionary<TKey, TValue> ToComposableReadOnlyDictionary<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source, key, value);
            return results;
        }

        public static ICacheDictionary<TKey, TValue> WithCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushTo, IComposableDictionary<TKey, TValue> cache = null)
        {
            return new ConcurrentCachingDictionary<TKey, TValue>(flushTo, cache ?? new ConcurrentDictionary<TKey, TValue>());
        }

        public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
        }

        public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue)
        {
            return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
        }

        public static IDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, Func<TValue, TKey> key)
        {
            return new AnonymousDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, key);
        }
    }
}