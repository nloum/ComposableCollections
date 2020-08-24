namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public static class WithDefaultValueTransformations<TKey, TValue>
    {
        public static ComposableDictionaryTransformationsImpl ComposableDictionaryTransformations { get; }

        static WithDefaultValueTransformations()
        {
            ComposableDictionaryTransformations = new ComposableDictionaryTransformationsImpl();
        }
        
        public class ComposableDictionaryTransformationsImpl : ComposableDictionaryTransformationsBase<TKey, TValue, GetDefaultValue<TKey, TValue>>
        {
            public override IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source,
                GetDefaultValue<TKey, TValue> parameter)
            {
                return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, parameter);
            }
        }
    }
}