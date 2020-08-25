using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithRefreshingTransformations<TKey, TValue>
    {
        public static ComposableDictionaryTransformations<TKey, TValue, RefreshValue<TKey, TValue>> ComposableDictionaryTransformations { get; }
        public static TransactionalTransformationsWriteWrite<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>> TransactionalTransformations { get; }

        static WithRefreshingTransformations()
        {
            ComposableDictionaryTransformations = new ComposableDictionaryTransformations<TKey, TValue, RefreshValue<TKey, TValue>>(Transform);
            TransactionalTransformations = new TransactionalTransformationsWriteWrite<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>>(ComposableDictionaryTransformations, ComposableDictionaryTransformations);
        }

        private static IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source,
            RefreshValue<TKey, TValue> p)
        {
            return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, p);
        }
    }
}