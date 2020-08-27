using ComposableCollections.Common;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface INonQueryableReadOnlyTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            TParameter parameter);
    }
}