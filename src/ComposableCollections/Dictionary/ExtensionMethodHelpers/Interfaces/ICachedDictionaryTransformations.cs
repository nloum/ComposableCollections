using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface ICachedDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        ICachedDictionary<TKey2, TValue2> Transform(IComposableDictionary<TKey1, TValue1> source,
            TParameter p);
        ICachedQueryableDictionary<TKey2, TValue2> Transform(IQueryableDictionary<TKey1, TValue1> source,
            TParameter p);
        ICachedDisposableQueryableDictionary<TKey2, TValue2> Transform(IDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p);
        ICachedDisposableDictionary<TKey2, TValue2> Transform(IDisposableDictionary<TKey1, TValue1> source, TParameter p);
    }
}