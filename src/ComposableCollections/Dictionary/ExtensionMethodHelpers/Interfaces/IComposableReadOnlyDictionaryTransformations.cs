namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter> :
        INonQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>,
        IQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>
    {
    }
}