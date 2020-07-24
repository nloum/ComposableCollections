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
            var result = new DictionaryEx<TKey, TValue>();
            result.AddRange(source);
            return result;
        }
        
        public static IDictionaryEx<TKey, TValue> ToDictionaryEx<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var result = new DictionaryEx<TKey, TValue>();
            result.AddRange(source, key, value);
            return result;
        }
        
        public static IReadOnlyDictionaryEx<TKey, TValue> ToReadOnlyDictionaryEx<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            var result = new DictionaryEx<TKey, TValue>();
            result.AddRange(source);
            return result;
        }
        
        public static IReadOnlyDictionaryEx<TKey, TValue> ToReadOnlyDictionaryEx<TKeyValue, TKey, TValue>(
            this IEnumerable<TKeyValue> source, Func<TKeyValue, TKey> key, Func<TKeyValue, TValue> value)
        {
            var result = new DictionaryEx<TKey, TValue>();
            result.AddRange(source, key, value);
            return result;
        }
    }
}