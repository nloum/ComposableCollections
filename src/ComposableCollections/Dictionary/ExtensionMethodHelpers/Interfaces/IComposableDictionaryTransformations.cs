using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        IComposableDictionary<TKey2, TValue2> Transform(IComposableDictionary<TKey1, TValue1> source,
            TParameter p);
        IQueryableDictionary<TKey2, TValue2> Transform(IQueryableDictionary<TKey1, TValue1> source,
            TParameter p);
        ICachedDictionary<TKey2, TValue2> Transform(ICachedDictionary<TKey1, TValue1> source,
            TParameter p);
        ICachedDisposableQueryableDictionary<TKey2, TValue2> Transform(ICachedDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p);
        IDisposableQueryableDictionary<TKey2, TValue2> Transform(IDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p);
        ICachedQueryableDictionary<TKey2, TValue2> Transform(ICachedQueryableDictionary<TKey1, TValue1> source, TParameter p);
        ICachedDisposableDictionary<TKey2, TValue2> Transform(ICachedDisposableDictionary<TKey1, TValue1> source, TParameter p);
        IDisposableDictionary<TKey2, TValue2> Transform(IDisposableDictionary<TKey1, TValue1> source, TParameter p);
    }
}