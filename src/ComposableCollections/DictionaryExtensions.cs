using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ComposableCollections.Common;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.Transactional;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using ComposableCollections.Set;
using ComposableCollections.Utilities;
using UtilityDisposables;

namespace ComposableCollections
{
    public static partial class DictionaryExtensions
    {
        public static IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary<TKey, TValue>(
            this IQueryable<TValue> queryable, Expression<Func<TValue, TKey>> getKey)
        {
            return new QueryableToQueryableReadOnlyDictionaryAdapter<TKey, TValue>(queryable, getKey);
        }

        #region Set to dictionary
        
        public static IDisposableQueryableDictionary<TKey, TKey> ToDictionary<TKey>(this IDisposableQueryableSet<TKey> source) {
            return new DisposableQueryableDictionaryAdapter<TKey, TKey>(new SetToDictionaryAdapter<TKey>(source), source, source);
        }
        
        public static IDisposableQueryableReadOnlyDictionary<TKey, TKey> ToDictionary<TKey>(this IDisposableQueryableReadOnlySet<TKey> source) {
            return new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TKey>(new ReadOnlySetToReadOnlyDictionaryAdapter<TKey>(source), source, source);
        }

        public static IQueryableDictionary<TKey, TKey> ToDictionary<TKey>(this IQueryableSet<TKey> source) {
            return new QueryableDictionaryAdapter<TKey, TKey>(new SetToDictionaryAdapter<TKey>(source), source);
        }
        
        public static IQueryableReadOnlyDictionary<TKey, TKey> ToDictionary<TKey>(this IQueryableReadOnlySet<TKey> source) {
            return new QueryableReadOnlyDictionaryAdapter<TKey, TKey>(new ReadOnlySetToReadOnlyDictionaryAdapter<TKey>(source), source);
        }

        public static IComposableDictionary<TKey, TKey> ToDictionary<TKey>(this Set.IComposableSet<TKey> source) {
            return new SetToDictionaryAdapter<TKey>(source);
        }
        
        public static IComposableReadOnlyDictionary<TKey, TKey> ToDictionary<TKey>(this IReadOnlySet<TKey> source) {
            return new ReadOnlySetToReadOnlyDictionaryAdapter<TKey>(source);
        }

        #endregion
        
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

        public static IReadWriteFactory<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(
            this IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> source, Func<TValue, TKey> getKey)
        {
            return source.Select(readOnly => readOnly.WithBuiltInKey(getKey),
                readWrite => readWrite.WithBuiltInKey(getKey));
        }
        public static IReadWriteFactory<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(
            this IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source, Func<TValue, TKey> getKey) {
            return source.Select(readOnly => readOnly.WithBuiltInKey(getKey),
                readWrite => readWrite.WithBuiltInKey(getKey));
        }
        public static IReadWriteFactory<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(
            this IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>> source, Func<TValue, TKey> getKey) {
            return source.Select(readOnly => readOnly.WithBuiltInKey(getKey),
                readWrite => readWrite.WithBuiltInKey(getKey));
        }
        public static IReadWriteFactory<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(
            this IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, Func<TValue, TKey> getKey) {
            return source.Select(readOnly => readOnly.WithBuiltInKey(getKey),
                readWrite => readWrite.WithBuiltInKey(getKey));
        }
        
        #endregion
        
        #region WithEachMethodAsSeparateTransaction
        
        /// <summary>
        /// Converts a transactional read-only dictionary into a non-transactional one by making each method call
        /// a separate transaction.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithEachMethodAsSeparateTransaction<TKey, TValue>(
            this IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source)
        {
            return new DetransactionalDictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// Converts a transactional read-only dictionary into a non-transactional one by making each method call
        /// a separate transaction.
        /// </summary>
        public static IComposableReadOnlyDictionary<TKey, TValue> WithEachMethodAsSeparateTransaction<TKey, TValue>(
            this IReadOnlyFactory<IDisposableReadOnlyDictionary<TKey, TValue>> source)
        {
            return new ReadOnlyDetransactionalDictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// Converts a transactional read-only queryable dictionary into a non-transactional one by making each method call
        /// a separate transaction.
        /// </summary>
        public static IQueryableDictionary<TKey, TValue> WithEachMethodAsSeparateTransaction<TKey, TValue>(
            this IReadWriteFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source)
        {
            return new DetransactionalQueryableDictionary<TKey, TValue>(source);
        }

        /// <summary>
        /// Converts a transactional read-only queryable dictionary into a non-transactional one by making each method call
        /// a separate transaction.
        /// </summary>
        public static IQueryableReadOnlyDictionary<TKey, TValue> WithEachMethodAsSeparateTransaction<TKey, TValue>(
            this IReadOnlyFactory<IDisposableQueryableReadOnlyDictionary<TKey, TValue>> source)
        {
            return new ReadOnlyDetransactionalQueryableDictionary<TKey, TValue>(source);
        }
        
        #endregion
        
        #region Select
        
        /// <summary>
        /// Converts the read-only and read/write objects
        /// </summary>
        public static IReadWriteFactory<TReadOnly2, TReadWrite2>
            Select<TReadOnly1, TReadWrite1, TReadOnly2, TReadWrite2>(
                this IReadWriteFactory<TReadOnly1, TReadWrite1> source, Func<TReadOnly1, TReadOnly2> readOnly,
                Func<TReadWrite1, TReadWrite2> readWrite) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable where TReadWrite1 : IDisposable where TReadWrite2 : IDisposable
        {
            return new AnonymousReadWriteFactory<TReadOnly2, TReadWrite2>(() => readOnly(source.CreateReader()), () => readWrite(source.CreateWriter()));
        }
        
        /// <summary>
        /// Converts the read-only object
        /// </summary>
        public static IReadOnlyFactory<TReadOnly2>
            Select<TReadOnly1, TReadOnly2>(
                this IReadOnlyFactory<TReadOnly1> source, Func<TReadOnly1, TReadOnly2> readOnly) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable
        {
            return new AnonymousReadOnlyFactory<TReadOnly2>(() => readOnly(source.CreateReader()));
        }

        #endregion
        
        #region SelectMany
        
        /// <summary>
        /// Converts the read-only and read/write objects
        /// </summary>
        public static IReadWriteFactory<TReadOnly2, TReadWrite2>
            SelectMany<TReadOnly1, TReadWrite1, TReadOnly2, TReadWrite2>(
                this IReadWriteFactory<TReadOnly1, TReadWrite1> source, Func<TReadOnly1, TReadOnly2> readOnly,
                Func<TReadWrite1, TReadWrite2> readWrite) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable where TReadWrite1 : IDisposable where TReadWrite2 : IDisposable
        {
            return new AnonymousReadWriteFactory<TReadOnly2, TReadWrite2>(() => readOnly(source.CreateReader()), () => readWrite(source.CreateWriter()));
        }
        
        /// <summary>
        /// Flattens a nested transaction. Note that the readOnly parameter must ensure that the TReadOnly1 gets disposed
        /// when its return value is disposed. The same is true of readWrite.
        /// </summary>
        public static IReadWriteFactory<TReadOnly2, TReadWrite2>
            SelectMany<TReadOnly1, TReadWrite1, TReadOnly2, TReadWrite2>(
                this IReadWriteFactory<TReadOnly1, TReadWrite1> source, Func<TReadOnly1, IDisposableReadOnlyFactory<TReadOnly2>> readOnly, Func<TReadWrite1, IDisposableReadWriteFactory<TReadOnly2, TReadWrite2>> readWrite) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable where TReadWrite1 : IDisposable where TReadWrite2 : IDisposable
        {
            return new AnonymousReadWriteFactory<TReadOnly2, TReadWrite2>(() =>
            {
                var it = readOnly(source.CreateReader());
                return it.CreateReader();
            }, () =>
            {
                var it = readWrite(source.CreateWriter());
                return it.CreateWriter();
            });
        }

        /// <summary>
        /// Flattens a nested transaction. Note that the readOnly parameter must ensure that the TReadOnly1 gets disposed
        /// when its return value is disposed.
        /// </summary>
        public static IReadOnlyFactory<TReadOnly2>
            SelectMany<TReadOnly1, TReadOnly2>(
                this IReadOnlyFactory<TReadOnly1> source, Func<TReadOnly1, IDisposableReadOnlyFactory<TReadOnly2>> readOnly) where TReadOnly1 : IDisposable where TReadOnly2 : IDisposable
        {
            return new AnonymousReadOnlyFactory<TReadOnly2>(() =>
            {
                var it = readOnly(source.CreateReader());
                return it.CreateReader();
            });
        }
        
        #endregion
        
        #region WithWritesCombinedAtEndOfTransaction
        
        /// <summary>
        /// Converts the source to a transactional dictionary that keeps all writes pending until the transaction is completed.
        /// </summary>
        public static IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithWritesCombinedAtEndOfTransaction<TKey, TValue>(this IComposableDictionary<TKey, TValue> source)
        {
            return new AnonymousReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(() =>
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
        public static IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithWritesCombinedAtEndOfTransaction<TKey, TValue>(this IReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source)
        {
            return new AnonymousReadWriteFactory<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(source.CreateReader, () =>
            {
                var disposableDictionary = source.CreateWriter();
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