using ComposableCollections.Dictionary.Decorators;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class WithReadWriteLockTransformations<TKey, TValue>
    {
        public static IComposableDictionaryTransformations<TKey, TValue, TKey, TValue, object> ComposableDictionaryTransformations { get; }
        public static IDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, object> DictionaryWithBuiltInKeyTransformations { get; }
        public static ITransactionalTransformations<TKey, TValue, TKey, TValue, object> TransactionalTransformations { get; }
        public static ITransactionalTransformationsWithBuiltInKey<TKey, TValue, TKey, TValue, object> TransactionalTransformationsWithBuiltInKey { get; }

        static WithReadWriteLockTransformations()
        {
            var composableDictionaryTransformations = new ComposableDictionaryTransformations<TKey, TValue, object>(Transform);
            ComposableDictionaryTransformations = composableDictionaryTransformations;
            DictionaryWithBuiltInKeyTransformations = composableDictionaryTransformations;
            TransactionalTransformations = new TransactionalTransformations<TKey, TValue, TKey, TValue, object>(new ComposableReadOnlyDictionaryPassThroughTransformations<TKey, TValue, object>(), composableDictionaryTransformations);
            TransactionalTransformationsWithBuiltInKey = new TransactionalTransformationsWithBuiltInKey<TKey, TValue, TKey, TValue, object>(new ReadOnlyDictionaryWithBuiltInKeyPassThroughTransformations<TKey, TValue, object>(), DictionaryWithBuiltInKeyTransformations);
        }
        
        private static IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, object p)
        {
            return new ReadWriteLockDictionaryDecorator<TKey, TValue>(source);
        }
    }
}