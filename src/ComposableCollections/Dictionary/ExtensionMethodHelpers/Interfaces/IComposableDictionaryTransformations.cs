namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> :
        INonQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>,
        IQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
    }
}