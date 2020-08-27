namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface ITransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> :
        INonQueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>,
        IQueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
    }
}