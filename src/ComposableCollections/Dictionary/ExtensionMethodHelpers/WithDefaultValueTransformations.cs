namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public static class WithDefaultValueTransformations<TKey, TValue>
    {
        public static ComposableDictionaryTransformationsImpl ComposableDictionaryTransformations { get; }
        public static TransactionalTransformationsImpl TransactionalTransformations { get; }

        static WithDefaultValueTransformations()
        {
            ComposableDictionaryTransformations = new ComposableDictionaryTransformationsImpl();
            TransactionalTransformations = new TransactionalTransformationsImpl(ComposableDictionaryTransformations, ComposableDictionaryTransformations);
        }

        public class TransactionalTransformationsImpl : TransactionalTransformationsWriteWrite<TKey, TValue, TKey, TValue,
            GetDefaultValue<TKey, TValue>>
        {
            public TransactionalTransformationsImpl(IComposableDictionaryTransformations<TKey, TValue, TKey, TValue, GetDefaultValue<TKey, TValue>> readWriteTransformations, IDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, GetDefaultValue<TKey, TValue>> readWriteTransformationsWithBuiltInKey) : base(readWriteTransformations, readWriteTransformationsWithBuiltInKey)
            {
            }
        }
        
        public class ComposableDictionaryTransformationsImpl : ComposableDictionaryTransformationsBase<TKey, TValue, GetDefaultValue<TKey, TValue>>
        {
            public override IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
            {
                return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, p);
            }
        }
    }
}