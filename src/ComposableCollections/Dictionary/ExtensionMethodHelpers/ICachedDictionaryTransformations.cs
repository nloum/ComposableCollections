using ComposableCollections.Dictionary.WithBuiltInKey;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public interface ICachedDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        ICachedDictionary<TKey2, TValue2> Transform(IComposableDictionary<TKey1, TValue1> source,
            TParameter p);
        ICachedQueryableDictionary<TKey2, TValue2> Transform(IQueryableDictionary<TKey1, TValue1> source,
            TParameter p);
        ICachedDisposableQueryableDictionary<TKey2, TValue2> Transform(IDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p);
        ICachedDisposableDictionary<TKey2, TValue2> Transform(IDisposableDictionary<TKey1, TValue1> source, TParameter p);
        ICachedDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter p);
        ICachedQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter p);
        ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter p);
        ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter p);
    }
}