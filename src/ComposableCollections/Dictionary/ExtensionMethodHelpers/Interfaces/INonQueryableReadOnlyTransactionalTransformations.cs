using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface INonQueryableReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>> source,
            TParameter parameter);
    }
}