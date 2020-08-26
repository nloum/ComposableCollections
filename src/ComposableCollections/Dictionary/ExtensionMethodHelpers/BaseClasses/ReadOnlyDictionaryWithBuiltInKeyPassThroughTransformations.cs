using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class ReadOnlyDictionaryWithBuiltInKeyPassThroughTransformations<TKey, TValue, TParameter> : IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, TParameter>
    {
        public IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> Transform(
            IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> source, TParameter parameter)
        {
            return source;
        }

        public IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> Transform(IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> source,
            TParameter parameter)
        {
            return source;
        }

        public IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> Transform(IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> source,
            TParameter parameter)
        {
            return source;
        }

        public IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> Transform(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> source, TParameter parameter)
        {
            return source;
        }
    }
}