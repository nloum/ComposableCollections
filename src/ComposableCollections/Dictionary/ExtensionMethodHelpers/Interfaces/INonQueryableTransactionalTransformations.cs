using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface INonQueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, IDisposableDictionary<TKey1, TValue1>> source,
            TParameter parameter);
        ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableDictionary<TKey1, TValue1>> source,
            TParameter parameter);
    }
}