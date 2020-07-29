using System;
using System.Collections.Generic;
using System.Reflection;

namespace MoreCollections
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
    }
}