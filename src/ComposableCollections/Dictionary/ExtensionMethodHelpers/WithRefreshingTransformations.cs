namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithRefreshingTransformations<TKey, TValue>
    {
        public static ComposableDictionaryTransformationsImpl ComposableDictionaryTransformations { get; }

        static WithRefreshingTransformations()
        {
            ComposableDictionaryTransformations = new ComposableDictionaryTransformationsImpl();
        }
        
        public class ComposableDictionaryTransformationsImpl : ComposableDictionaryTransformationsBase<TKey, TValue, RefreshValue<TKey, TValue>>
        {
            public override IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source,
                RefreshValue<TKey, TValue> parameter)
            {
                return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, parameter);
            }
        }
    }
}