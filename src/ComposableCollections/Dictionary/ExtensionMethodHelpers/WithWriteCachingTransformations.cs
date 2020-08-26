using ComposableCollections.Common;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithWriteCachingTransformations<TKey, TValue>
    {
        public static ICachedDictionaryTransformations<TKey, TValue, TKey, TValue, object> CachedDictionaryTransformations { get; }
        public static ICachedDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, object> CachedDictionaryWithBuiltInKeyTransformations { get; }
        public static ICachedTransactionalDictionaryTransformations<TKey, TValue, TKey, TValue, object> TransactionalTransformations { get; }
        public static ICachedTransactionalDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, object> TransactionalTransformationsWithBuiltInKey { get; }

        static WithWriteCachingTransformations()
        {
            var cachedDictionaryTransformations = new CachedDictionaryTransformations<TKey, TValue, object>(Transform);
            CachedDictionaryTransformations = cachedDictionaryTransformations;
            CachedDictionaryWithBuiltInKeyTransformations = cachedDictionaryTransformations;
            var transactionalTransformations = new CachedTransactionalDictionaryTransformations<TKey, TValue, object>(cachedDictionaryTransformations, cachedDictionaryTransformations);
            TransactionalTransformations = transactionalTransformations;
            TransactionalTransformationsWithBuiltInKey = transactionalTransformations;
        }
        
        private static ICachedDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, object p)
        {
            return new ConcurrentCachedWriteDictionaryAdapter<TKey, TValue>(source);
        }
    }
}