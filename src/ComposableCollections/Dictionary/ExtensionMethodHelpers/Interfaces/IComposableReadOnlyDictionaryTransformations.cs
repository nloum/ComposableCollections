using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>
    {
        IQueryableReadOnlyDictionary<TKey2, TValue2> Transform(IQueryableReadOnlyDictionary<TKey1, TValue1> source,
            TReadOnlyParameter parameter);
        IComposableReadOnlyDictionary<TKey2, TValue2> Transform(IComposableReadOnlyDictionary<TKey1, TValue1> source, TReadOnlyParameter parameter);
        IDisposableQueryableReadOnlyDictionary<TKey2, TValue2> Transform(IDisposableQueryableReadOnlyDictionary<TKey1, TValue1> source, TReadOnlyParameter parameter);
        IDisposableReadOnlyDictionary<TKey2, TValue2> Transform(IDisposableReadOnlyDictionary<TKey1, TValue1> source, TReadOnlyParameter parameter);
    }
}