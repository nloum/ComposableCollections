using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithWriteCachingTransformations<TKey, TValue>
    {
        public static CachedDictionaryTransformationsImpl CachedDictionaryTransformations;
        public static CachedTransactionalDictionaryTransformationsImpl TransactionalTransformations;

        static WithWriteCachingTransformations()
        {
            CachedDictionaryTransformations = new CachedDictionaryTransformationsImpl();
            TransactionalTransformations = new CachedTransactionalDictionaryTransformationsImpl(CachedDictionaryTransformations);
        }
        
        public class CachedDictionaryTransformationsImpl : CachedDictionaryTransformationsBase<TKey, TValue, object>
        {
            public override ICachedDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, object p)
            {
                return new ConcurrentCachedWriteDictionaryAdapter<TKey, TValue>(source);
            }
        }

        public class
            CachedTransactionalDictionaryTransformationsImpl : CachedTransactionalDictionaryTransformationsBase<TKey,
                TValue, object>
        {
            public CachedTransactionalDictionaryTransformationsImpl(ICachedDictionaryTransformations<TKey, TValue, TKey, TValue, object> cachedDictionaryTransformations) : base(cachedDictionaryTransformations)
            {
            }
        }
    }
}