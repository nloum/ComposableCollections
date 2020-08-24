namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithChangeCachingTransformations<TKey, TValue>
    {
        public static CachedDictionaryTransformationsImpl CachedDictionaryTransformations;
        public static CachedTransactionalDictionaryTransformationsImpl TransactionalTransformations;

        static WithChangeCachingTransformations()
        {
            CachedDictionaryTransformations = new CachedDictionaryTransformationsImpl();
            TransactionalTransformations = new CachedTransactionalDictionaryTransformationsImpl(CachedDictionaryTransformations);
        }
        
        public class CachedDictionaryTransformationsImpl : CachedDictionaryTransformationsBase<TKey, TValue, object>
        {
            public override ICachedDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, object p)
            {
                return new ConcurrentMinimalCachedStateDictionaryDecorator<TKey, TValue>(source);
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