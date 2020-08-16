using System;
using System.Collections.Generic;

namespace ComposableCollections.Dictionary
{
    public static class DictionaryExtensions
    {
        public static IDictionaryEx<TKey, TValue> ToDictionaryEx<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var results = new DictionaryEx<TKey, TValue>();
            results.AddRange(source);
            return results;
        }
        
        public static IDictionaryEx<TKey, TValue> ToDictionaryEx<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var results = new DictionaryEx<TKey, TValue>();
            results.AddRange(source, key, value);
            return results;
        }
        
        public static IReadOnlyDictionaryEx<TKey, TValue> ToReadOnlyDictionaryEx<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var results = new DictionaryEx<TKey, TValue>();
            results.AddRange(source);
            return results;
        }
        
        public static IReadOnlyDictionaryEx<TKey, TValue> ToReadOnlyDictionaryEx<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var results = new DictionaryEx<TKey, TValue>();
            results.AddRange(source, key, value);
            return results;
        }

        public static ICachingDictionary<TKey, TValue> WithCaching<TKey, TValue>(this IDictionaryEx<TKey, TValue> flushTo, IDictionaryEx<TKey, TValue> cache = null)
        {
            return new ConcurrentCachingDictionary<TKey, TValue>(flushTo, cache ?? new ConcurrentDictionaryEx<TKey, TValue>());
        }

        public static IDictionaryEx<TKey, TValue> WithDefaultValue<TKey, TValue>(this IDictionaryEx<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
        }

        public static IDictionaryEx<TKey, TValue> WithRefreshing<TKey, TValue>(this IDictionaryEx<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue)
        {
            return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
        }

        public static IDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IDictionaryEx<TKey, TValue> source, Func<TValue, TKey> key)
        {
            return new AnonymousDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, key);
        }
    }
}