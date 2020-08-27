namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IWithMappingTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyQueryableParameter, TReadWriteQueryableParameter, TReadOnlyNonQueryableParameter, TReadWriteNonQueryableParameter
            , TReadOnlyQueryableWithBuiltInKeyParameter, TReadWriteQueryableWithBuiltInKeyParameter, TReadOnlyNonQueryableWithBuiltInKeyParameter, TReadWriteNonQueryableWithBuiltInKeyParameter>
            : INonQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadWriteNonQueryableParameter>
            , IQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadWriteQueryableParameter>
            , INonQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyNonQueryableParameter>
            , IQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyQueryableParameter>

            , INonQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadWriteNonQueryableWithBuiltInKeyParameter>
            , IQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadWriteQueryableWithBuiltInKeyParameter>
            , INonQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyNonQueryableWithBuiltInKeyParameter>
            , IQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyQueryableWithBuiltInKeyParameter>

            , INonQueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TReadWriteNonQueryableParameter>
            , IQueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TReadWriteQueryableParameter>
            , INonQueryableReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyNonQueryableParameter>
            , IQueryableReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyQueryableParameter>

            , INonQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TReadWriteNonQueryableWithBuiltInKeyParameter>
            , IQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TReadWriteQueryableWithBuiltInKeyParameter>
            , INonQueryableReadOnlyTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TReadOnlyNonQueryableWithBuiltInKeyParameter>
            , IQueryableReadOnlyTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TReadOnlyQueryableWithBuiltInKeyParameter>
    {
        
    }
}