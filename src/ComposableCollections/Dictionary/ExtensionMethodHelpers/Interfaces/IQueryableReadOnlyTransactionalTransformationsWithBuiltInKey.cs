using ComposableCollections.Common;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IQueryableReadOnlyTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            TParameter parameter);
    }
}