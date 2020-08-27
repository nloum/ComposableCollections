using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>
    {
        IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TReadOnlyParameter parameter);
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TReadOnlyParameter parameter);
    }
}