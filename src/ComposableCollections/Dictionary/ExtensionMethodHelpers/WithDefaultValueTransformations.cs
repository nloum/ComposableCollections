using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public static class WithDefaultValueTransformations<TKey, TValue>
    {
        public static ComposableDictionaryTransformations<TKey, TValue, GetDefaultValue<TKey, TValue>> ComposableDictionaryTransformations { get; }
        public static TransactionalTransformationsWriteWrite<TKey, TValue, TKey, TValue, GetDefaultValue<TKey, TValue>> TransactionalTransformations { get; }

        static WithDefaultValueTransformations()
        {
            ComposableDictionaryTransformations = new ComposableDictionaryTransformations<TKey, TValue, GetDefaultValue<TKey, TValue>>(Transform);
            TransactionalTransformations = new TransactionalTransformationsWriteWrite<TKey, TValue, TKey, TValue, GetDefaultValue<TKey, TValue>>(ComposableDictionaryTransformations, ComposableDictionaryTransformations);
        }

        private static IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, GetDefaultValue<TKey, TValue> p)
        {
            return new DictionaryGetOrDefaultDecorator<TKey, TValue>(source, p);
        }
    }
}