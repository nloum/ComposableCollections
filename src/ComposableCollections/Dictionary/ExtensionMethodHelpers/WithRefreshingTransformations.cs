using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithRefreshingTransformations<TKey, TValue>
    {
        public static IComposableDictionaryTransformations<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>> ComposableDictionaryTransformations { get; }
        public static IDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>> DictionaryWithBuiltInKeyTransformations { get; }
        public static ITransactionalTransformations<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>> TransactionalTransformations { get; }
        public static ITransactionalTransformationsWithBuiltInKey<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>> TransactionalTransformationsWithBuiltInKey { get; }

        static WithRefreshingTransformations()
        {
            var composableDictionaryTransformations = new ComposableDictionaryTransformations<TKey, TValue, RefreshValue<TKey, TValue>>(Transform);
            ComposableDictionaryTransformations = composableDictionaryTransformations;
            DictionaryWithBuiltInKeyTransformations = composableDictionaryTransformations;
            var transactionalTransformations = new TransactionalTransformationsWriteWrite<TKey, TValue, TKey, TValue, RefreshValue<TKey, TValue>>(composableDictionaryTransformations, composableDictionaryTransformations);
            TransactionalTransformations = transactionalTransformations;
            TransactionalTransformationsWithBuiltInKey = transactionalTransformations;
        }

        private static IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source,
            RefreshValue<TKey, TValue> p)
        {
            return new DictionaryGetOrRefreshDecorator<TKey, TValue>(source, p);
        }
    }
}