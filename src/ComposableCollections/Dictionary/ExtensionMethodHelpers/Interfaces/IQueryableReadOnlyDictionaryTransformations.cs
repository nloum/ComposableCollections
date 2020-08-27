using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TReadOnlyParameter>
    {
        IQueryableReadOnlyDictionary<TKey2, TValue2> Transform(IQueryableReadOnlyDictionary<TKey1, TValue1> source,
            TReadOnlyParameter parameter);
        IDisposableQueryableReadOnlyDictionary<TKey2, TValue2> Transform(IDisposableQueryableReadOnlyDictionary<TKey1, TValue1> source, TReadOnlyParameter parameter);
    }
}