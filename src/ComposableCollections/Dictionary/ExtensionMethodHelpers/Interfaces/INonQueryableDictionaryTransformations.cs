using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface INonQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        IComposableDictionary<TKey2, TValue2> Transform(IComposableDictionary<TKey1, TValue1> source,
            TParameter parameter);
        ICachedDictionary<TKey2, TValue2> Transform(ICachedDictionary<TKey1, TValue1> source,
            TParameter parameter);
        ICachedDisposableDictionary<TKey2, TValue2> Transform(ICachedDisposableDictionary<TKey1, TValue1> source, TParameter parameter);
        IDisposableDictionary<TKey2, TValue2> Transform(IDisposableDictionary<TKey1, TValue1> source, TParameter parameter);
    }
}