using ComposableCollections.Common;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface INonQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            TParameter parameter);
        ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            TParameter parameter);
    }
}