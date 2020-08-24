using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.ExtensionMethodHelpers;
using SimpleMonads;
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
        
        #region WithMapping
        
        #endregion
        
        #region WithMutationCaching
        
        public static ICachedQueryableDictionary<TKey, TValue> WithChangeCaching<TKey, TValue>(this IQueryableDictionary<TKey, TValue> source, object p)
        {
            return WithChangeCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDisposableQueryableDictionary<TKey, TValue> WithChangeCaching<TKey, TValue>(this IDisposableQueryableDictionary<TKey, TValue> source, object p)
        {
            return WithChangeCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDisposableDictionary<TKey, TValue> WithChangeCaching<TKey, TValue>(this IDisposableDictionary<TKey, TValue> source, object p)
        {
            return WithChangeCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDictionaryWithBuiltInKey<TKey, TValue> WithChangeCaching<TKey, TValue>(this IDictionaryWithBuiltInKey<TKey, TValue> source, object p)
        {
            return WithChangeCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, p);
        }

        public static ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> WithChangeCaching<TKey, TValue>(this IQueryableDictionaryWithBuiltInKey<TKey, TValue> source, object p)
        {
            return WithChangeCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> WithChangeCaching<TKey, TValue>(this IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            object p)
        {
            return WithChangeCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> WithChangeCaching<TKey, TValue>(this IDisposableDictionaryWithBuiltInKey<TKey, TValue> source, object p)
        {
            return WithChangeCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, p);
        }

        public static ICachedDictionary<TKey, TValue> WithChangeCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, object p)
        {
            return WithChangeCachingTransformations<TKey, TValue>.CachedDictionaryTransformations.Transform(source, p);
        }
        
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithChangeCaching<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, object parameter)
        {
            return WithChangeCachingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> WithChangeCaching<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source, object parameter)
        {
            return WithChangeCachingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithChangeCaching<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> source, object parameter)
        {
            return WithChangeCachingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }

        public static ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> WithChangeCaching<TKey, TValue>(this ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source, object parameter)
        {
            return WithChangeCachingTransformations<TKey, TValue>.TransactionalTransformations.Transform(source, parameter);
        }
        
        #endregion
        
        #region WithCaching
        
        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static ICachedDictionary<TKey, TValue> WithReadWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushTo, IComposableDictionary<TKey, TValue> cache = null)
        {
            return new ConcurrentCachedDictionary<TKey, TValue>(flushTo, cache);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static ICachedDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushTo, IComposableDictionary<TKey, TValue> addedOrUpdated = null, IComposableDictionary<TKey, TValue> removed = null)
        {
            return new ConcurrentMinimalCachedStateDictionaryDecorator<TKey, TValue>(flushTo, addedOrUpdated, removed);
        }
        
        #endregion
        
        #region WithDefaultValue
        
        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that lets you optionally create values when
        /// they're accessed, on demand.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, getDefaultValue);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that lets you optionally create values when
        /// they're accessed, on demand.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithDefaultValue<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, Func<TKey, TValue> getDefaultValue, bool persist = true)
        {
            return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source,
                (TKey key, out IMaybe<TValue> value, out bool b) =>
                {
                    value = getDefaultValue(key).ToMaybe();
                    b = persist;
                });
        }

        /// <summary>
        /// Creates a facade on top of the specified ITransactionalDictionary that lets you optionally create values when
        /// they're accessed, on demand.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithDefaultValue<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, Func<TKey, TValue> getDefaultValue, bool persist = true)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(() =>
            {
                var writer = source.BeginWrite();
                return new DisposableDictionaryDecorator<TKey, TValue>(writer.WithDefaultValue(getDefaultValue), writer);
            }, () =>
            {
                var writer = source.BeginWrite();
                return new DisposableDictionaryDecorator<TKey, TValue>(writer.WithDefaultValue(getDefaultValue), writer);
            });
        }

        #endregion
        
        #region WithRefreshing
        
        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that lets you optionally update values when
        /// they're accessed, on demand.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue)
        {
            return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
        }
        
        /// <summary>
        /// Creates a facade on top of the specified ITransactionalDictionary that lets you optionally update values when
        /// they're accessed, on demand.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithRefreshing<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, RefreshValue<TKey, TValue> refreshValue)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(() =>
            {
                var writer = source.BeginWrite();
                return new DisposableDictionaryDecorator<TKey, TValue>(writer.WithRefreshing(refreshValue), writer);
            }, () =>
            {
                var writer = source.BeginWrite();
                return new DisposableDictionaryDecorator<TKey, TValue>(writer.WithRefreshing(refreshValue), writer);
            });
        }

        #endregion

        #region WithBuiltInKey
        
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
        /// Converts the source to a transactional dictionary that keeps all mutations pending until the transaction is completed.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithWritesCombinedAtEndOfTransaction<TKey, TValue>(this IComposableDictionary<TKey, TValue> source)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(() =>
            {
                return new DisposableReadOnlyDictionaryDecorator<TKey, TValue>(source, EmptyDisposable.Default);
            }, () =>
            {
                var cache = source.WithWriteCaching();
                return new DisposableDictionaryDecorator<TKey, TValue>(cache, new AnonymousDisposable(() => cache.FlushCache()));
            });
        }
        
        /// <summary>
        /// Converts the source to a transactional dictionary that keeps all mutations pending until the transaction is completed.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithWritesCombinedAtEndOfTransaction<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>>(source.BeginRead, () =>
            {
                var disposableDictionary = source.BeginWrite();
                var cache = disposableDictionary.WithWriteCaching();
                return new DisposableDictionaryDecorator<TKey, TValue>(cache, new AnonymousDisposable(() =>
                {
                    cache.FlushCache();
                    disposableDictionary.Dispose();
                }));
            });
        }

        #endregion
        
        #region WithReadWriteLock
        
        /// <summary>
        /// Converts the dictionary into an object that lets you access the dictionary in a transactional API,
        /// that ensures that when the dictionary is being modified, nobody else is modifying it or even reading from it
        /// but reads can happen simultaneously.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithReadWriteLock<TKey, TValue>(
            this IComposableDictionary<TKey, TValue> wrapped)
        {
            return new AtomicDictionaryAdapter<TKey, TValue>(wrapped);
        }

        /// <summary>
        /// Adds locks around the read/write transactions
        /// that ensures that when the dictionary is being modified, nobody else is modifying it or even reading from it
        /// but reads can happen simultaneously.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> WithReadWriteLock<TKey, TValue>(
            this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> wrapped)
        {
            return new AtomicTransactionalDecorator<TKey, TValue>(wrapped);
        }

        /// <summary>
        /// Converts the dictionary into an object that lets you access the dictionary in a transactional API,
        /// that ensures that when the dictionary is being modified, nobody else is modifying it or even reading from it
        /// but reads can happen simultaneously.
        /// </summary>
        public static IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>> WithReadWriteLock<TKey, TValue>(
            this IComposableReadOnlyDictionary<TKey, TValue> wrapped)
        {
            return new AtomicReadOnlyDictionaryAdapter<TKey, TValue>(wrapped);
        }
        
        #endregion
    }
}