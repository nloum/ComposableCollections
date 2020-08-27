using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface INonQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>
    {
        IComposableReadOnlyDictionary<TKey2, TValue2> Transform(IComposableReadOnlyDictionary<TKey1, TValue1> source, TReadOnlyParameter parameter);
        IDisposableReadOnlyDictionary<TKey2, TValue2> Transform(IDisposableReadOnlyDictionary<TKey1, TValue1> source, TReadOnlyParameter parameter);
    }
}