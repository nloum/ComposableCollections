namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface ITransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter> :
        IQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>,
        INonQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
    }
}