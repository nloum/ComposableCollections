using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        IQueryableDictionary<TKey2, TValue2> Transform(IQueryableDictionary<TKey1, TValue1> source,
            TParameter p);
        ICachedDisposableQueryableDictionary<TKey2, TValue2> Transform(ICachedDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p);
        IDisposableQueryableDictionary<TKey2, TValue2> Transform(IDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p);
        ICachedQueryableDictionary<TKey2, TValue2> Transform(ICachedQueryableDictionary<TKey1, TValue1> source, TParameter p);
    }
}