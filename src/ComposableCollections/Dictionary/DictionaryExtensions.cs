using System;
using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public static class DictionaryExtensions
    {
        public static IComposableDictionary<TKey, TValue> CopyToComposableDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source);
            return results;
        }
        
        public static IComposableDictionary<TKey, TValue> CopyToComposableDictionary<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source, key, value);
            return results;
        }
        
        public static IReadOnlyDictionaryEx<TKey, TValue> CopyToComposableReadOnlyDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source);
            return results;
        }
        
        public static IReadOnlyDictionaryEx<TKey, TValue> CopyToComposableReadOnlyDictionary<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var results = new ComposableDictionary<TKey, TValue>();
            results.AddRange(source, key, value);
            return results;
        }

        public static ICachingComposableDictionary<TKey, TValue> WithCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushTo, IComposableDictionary<TKey, TValue> cache = null)
        {
            return new ConcurrentCachingComposableDictionary<TKey, TValue>(flushTo, cache ?? new ConcurrentComposableDictionary<TKey, TValue>());
        }

        public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            return new ComposableDictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
        }

        public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue)
        {
            return new ComposableDictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
        }

        public static IDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, Func<TValue, TKey> key)
        {
            return new AnonymousDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, key);
        }
    }
}