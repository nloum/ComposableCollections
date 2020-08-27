using ComposableCollections.Common;
using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public static class WithDefaultValueTransformations<TKey, TValue>
    {
        public static IComposableDictionaryTransformations<TKey, TValue, TKey, TValue, GetDefaultValueWithOptionalPersistence<TKey, TValue>> ComposableDictionaryTransformations { get; }
        public static IDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, GetDefaultValueWithOptionalPersistence<TKey, TValue>> DictionaryWithBuiltInKeyTransformations { get; }
        public static ITransactionalTransformations<TKey, TValue, TKey, TValue, GetDefaultValueWithOptionalPersistence<TKey, TValue>> TransactionalTransformations { get; }
        public static ITransactionalTransformationsWithBuiltInKey<TKey, TValue, TKey, TValue, GetDefaultValueWithOptionalPersistence<TKey, TValue>> TransactionalTransformationsWithBuiltInKey { get; }

        static WithDefaultValueTransformations()
        {
            var composableDictionaryTransformations = new ComposableDictionaryTransformations<TKey, TValue, GetDefaultValueWithOptionalPersistence<TKey, TValue>>(Transform);
            ComposableDictionaryTransformations = composableDictionaryTransformations;
            DictionaryWithBuiltInKeyTransformations = composableDictionaryTransformations;
            var transactionalTransformations = new TransactionalTransformationsWriteWrite<TKey, TValue, TKey, TValue, GetDefaultValueWithOptionalPersistence<TKey, TValue>>(composableDictionaryTransformations, composableDictionaryTransformations);
            TransactionalTransformations = transactionalTransformations;
            TransactionalTransformationsWithBuiltInKey = transactionalTransformations;
        }

        private static IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source,
            GetDefaultValueWithOptionalPersistence<TKey, TValue> p)
        {
            return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, p);
        }
    }
}