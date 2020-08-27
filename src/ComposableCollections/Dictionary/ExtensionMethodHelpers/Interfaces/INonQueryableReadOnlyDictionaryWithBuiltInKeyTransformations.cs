using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface INonQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>
    {
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TReadOnlyParameter parameter);
        IReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TReadOnlyParameter parameter);
    }
}