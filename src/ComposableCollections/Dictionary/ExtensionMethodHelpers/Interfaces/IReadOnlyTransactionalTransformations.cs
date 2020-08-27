namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> :
        INonQueryableReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>,
        IQueryableReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
    }
}