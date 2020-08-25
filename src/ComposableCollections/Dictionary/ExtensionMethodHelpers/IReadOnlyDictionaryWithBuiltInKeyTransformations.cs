using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public interface IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>
    {
        IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TReadOnlyParameter parameter);
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TReadOnlyParameter parameter);
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TReadOnlyParameter parameter);
        IReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TReadOnlyParameter parameter);
    }
}