using System;
using System.Collections.Generic;
using System.Reflection;
using ComposableCollections.Dictionary;
using SimpleMonads;
using UtilityDisposables;

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
        public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TValue, TInnerValue>(this IComposableDictionary<TKey, TInnerValue> source, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack)
        {
            return new AnonymousMapDictionary<TKey, TValue, TInnerValue>(source, convert, convertBack);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary. Also this caches the converted values.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithCachedMapping<TKey, TValue, TInnerValue>(this IComposableDictionary<TKey, TInnerValue> source, Func<TKey, TValue, TInnerValue> convert, Func<TKey, TInnerValue, TValue> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false)
        {
            return new AnonymousCachedMapDictionary<TKey, TValue, TInnerValue>(source, convert, convertBack, cache, proactivelyConvertAllValues);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithMapping<TKey, TValue, TInnerValue>(this IComposableDictionary<TKey, TInnerValue> source, Func<IEnumerable<IKeyValue<TKey, TValue>>, IEnumerable<IKeyValue<TKey, TInnerValue>>> convert, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack)
        {
            return new AnonymousBulkMapDictionary<TKey, TValue, TInnerValue>(source, convert, convertBack);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary. Also this caches the converted values.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithCachedMapping<TKey, TValue, TInnerValue>(this IComposableDictionary<TKey, TInnerValue> source, Func<IEnumerable<IKeyValue<TKey, TValue>>, IEnumerable<IKeyValue<TKey, TInnerValue>>> convert, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false)
        {
            return new AnonymousCachedBulkMapDictionary<TKey, TValue, TInnerValue>(source, convert, convertBack, cache, proactivelyConvertAllValues);
        }
        
        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TValue, TInnerValue>(this IComposableReadOnlyDictionary<TKey, TInnerValue> source, Func<TKey, TInnerValue, TValue> convertBack)
        {
            return new AnonymousReadOnlyMapDictionary<TKey, TValue, TInnerValue>(source, convertBack);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary. Also this caches the converted values.
        /// </summary>
        public static IComposableReadOnlyDictionary<TKey, TValue> WithCachedMapping<TKey, TValue, TInnerValue>(this IComposableReadOnlyDictionary<TKey, TInnerValue> source, Func<TKey, TInnerValue, TValue> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false)
        {
            return new AnonymousCachedReadOnlyMapDictionary<TKey, TValue, TInnerValue>(source, convertBack, cache, proactivelyConvertAllValues);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static IComposableReadOnlyDictionary<TKey, TValue> WithMapping<TKey, TValue, TInnerValue>(this IComposableReadOnlyDictionary<TKey, TInnerValue> source, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack)
        {
            return new AnonymousBulkReadOnlyMapDictionary<TKey, TValue, TInnerValue>(source, convertBack);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary. Also this caches the converted values.
        /// </summary>
        public static IComposableReadOnlyDictionary<TKey, TValue> WithCachedMapping<TKey, TValue, TInnerValue>(this IComposableReadOnlyDictionary<TKey, TInnerValue> source, Func<IEnumerable<IKeyValue<TKey, TInnerValue>>, IEnumerable<IKeyValue<TKey, TValue>>> convertBack, IComposableDictionary<TKey, TValue> cache = null, bool proactivelyConvertAllValues = false)
        {
            return new AnonymousCachedBulkReadOnlyMapDictionary<TKey, TValue, TInnerValue>(source, convertBack, cache, proactivelyConvertAllValues);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static ICacheDictionary<TKey, TValue> WithReadWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushTo, IComposableDictionary<TKey, TValue> cache = null)
        {
            return new ConcurrentCachingDictionary<TKey, TValue>(flushTo, cache);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that keeps tracks of changes and occasionally
        /// flushes them to the specified IComposableDictionary.
        /// </summary>
        public static ICacheDictionary<TKey, TValue> WithWriteCaching<TKey, TValue>(this IComposableDictionary<TKey, TValue> flushTo, IComposableDictionary<TKey, TValue> addedOrUpdated = null, IComposableDictionary<TKey, TValue> removed = null)
        {
            return new ConcurrentCachingDictionaryWithMinimalState<TKey, TValue>(flushTo, addedOrUpdated, removed);
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
        /// Creates a facade on top of the specified IComposableDictionary that lets you optionally update values when
        /// they're accessed, on demand.
        /// </summary>
        public static IComposableDictionary<TKey, TValue> WithRefreshing<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> refreshValue)
        {
            return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, refreshValue);
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

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that has a built-in key, which means you're telling
        /// the object how to get the key from a value. That means any API where you pass in a TValue, you
        /// won't have to tell the API what the key is.
        /// </summary>
        public static IDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableDictionary<TKey, TValue> source, Func<TValue, TKey> key)
        {
            return new AnonymousDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, key);
        }

        /// <summary>
        /// Creates a facade on top of the specified IComposableDictionary that has a built-in key, which means you're telling
        /// the object how to get the key from a value. That means any API where you pass in a TValue, you
        /// won't have to tell the API what the key is.
        /// </summary>
        public static IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> WithBuiltInKey<TKey, TValue>(this IComposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> key)
        {
            return new AnonymousReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(source, key);
        }

        /// <summary>
        /// Creates an adapter on top of the specified ITransactionalCollection that has a built-in key, which means you're telling
        /// the object how to get the key from a value. That means any API where you pass in a TValue, you
        /// won't have to tell the API what the key is.
        /// </summary>
        public static ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(this ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, Func<TValue, TKey> key)
        {
            return source.Select(x =>
                new DisposableReadOnlyDictionaryWithBuiltInKeyDecorator<TKey, TValue>(x.WithBuiltInKey(key), x), x =>
                new DisposableDictionaryWithBuiltInKeyDecorator<TKey, TValue>(x.WithBuiltInKey(key), x));
        }

        /// <summary>
        /// Creates an adapter on top of the specified IReadOnlyTransactionalCollection that has a built-in key, which means you're telling
        /// the object how to get the key from a value. That means any API where you pass in a TValue, you
        /// won't have to tell the API what the key is.
        /// </summary>
        public static IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>> WithBuiltInKey<TKey, TValue>(this IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>> source, Func<TValue, TKey> key)
        {
            return source.Select(x =>
                new DisposableReadOnlyDictionaryWithBuiltInKeyDecorator<TKey, TValue>(x.WithBuiltInKey(key), x));
        }

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
    }
}