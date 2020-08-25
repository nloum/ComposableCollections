using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithWriteCachingTransformations<TKey, TValue>
    {
        public static CachedDictionaryTransformations<TKey, TValue, object> CachedDictionaryTransformations { get; }
        public static CachedTransactionalDictionaryTransformations<TKey, TValue, object> TransactionalTransformations { get; }

        static WithWriteCachingTransformations()
        {
            CachedDictionaryTransformations = new CachedDictionaryTransformations<TKey, TValue, object>(Transform);
            TransactionalTransformations = new CachedTransactionalDictionaryTransformations<TKey, TValue, object>(CachedDictionaryTransformations);
        }
        
        private static ICachedDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, object p)
        {
            return new ConcurrentCachedWriteDictionaryAdapter<TKey, TValue>(source);
        }
    }
}