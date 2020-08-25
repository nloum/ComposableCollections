using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public interface IReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>> source,
            TParameter parameter);
        IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>> source,
            TParameter parameter);
        IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            TParameter parameter);
        IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>> source,
            TParameter parameter);
    }
}