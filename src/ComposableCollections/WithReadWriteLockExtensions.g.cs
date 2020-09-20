using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.Interfaces;
namespace ComposableCollections {
public static class WithReadWriteLockExtensions {
public static IReadWriteCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadWriteCachedDisposableDictionary<TKey, TValue> adapted) {
return new ReadWriteCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IWriteCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IWriteCachedDisposableDictionary<TKey, TValue> adapted) {
return new WriteCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IReadCachedDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadCachedDisposableDictionary<TKey, TValue> adapted) {
return new ReadCachedDisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IWriteCachedDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IWriteCachedDictionary<TKey, TValue> adapted) {
return new WriteCachedReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IComposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IComposableDictionary<TKey, TValue> source) {
return new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);}
public static IReadWriteCachedDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IReadWriteCachedDictionary<TKey, TValue> adapted) {
return new ReadWriteCachedReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
public static IDisposableDictionary<TKey, TValue> WithReadWriteLock<TKey, TValue>(this IDisposableDictionary<TKey, TValue> adapted) {
return new DisposableReadWriteLockDictionaryDecorator<TKey, TValue>(adapted);}
}
}
