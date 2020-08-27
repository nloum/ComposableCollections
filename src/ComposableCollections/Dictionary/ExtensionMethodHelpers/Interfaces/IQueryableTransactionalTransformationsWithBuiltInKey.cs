using ComposableCollections.Common;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            TParameter parameter);
        ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            TParameter parameter);
    }
}