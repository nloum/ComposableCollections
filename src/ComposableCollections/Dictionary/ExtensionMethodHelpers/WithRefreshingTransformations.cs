namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithRefreshingTransformations<TKey, TValue>
    {
        public static ComposableDictionaryTransformationsImpl ComposableDictionaryTransformations { get; }
        public static TransactionalTransformationsImpl TransactionalTransformations { get; }

        static WithRefreshingTransformations()
        {
            ComposableDictionaryTransformations = new ComposableDictionaryTransformationsImpl();
            TransactionalTransformations = new TransactionalTransformationsImpl(ComposableDictionaryTransformations, ComposableDictionaryTransformations);
        }

        public class TransactionalTransformationsImpl : TransactionalTransformationsWriteWrite<TKey, TValue, TKey, TValue,
            RefreshValue<TKey, TValue>>
        {
            public TransactionalTransformationsImpl(IComposableDictionaryTransformations<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>> readWriteTransformations, IDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>> readWriteTransformationsWithBuiltInKey) : base(readWriteTransformations, readWriteTransformationsWithBuiltInKey)
            {
            }
        }
        
        public class ComposableDictionaryTransformationsImpl : ComposableDictionaryTransformationsBase<TKey, TValue, RefreshValue<TKey, TValue>>
        {
            public override IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, RefreshValue<TKey, TValue> p)
            {
                return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, p);
            }
        }
    }
}