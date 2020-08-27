namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter> :
        INonQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>,
        IQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>
    {
    }
}