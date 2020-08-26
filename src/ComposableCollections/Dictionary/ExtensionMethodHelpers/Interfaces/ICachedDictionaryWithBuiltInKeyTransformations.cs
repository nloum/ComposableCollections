using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces
{
    public interface ICachedDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        ICachedDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter p);
        ICachedQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter p);
        ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter p);
        ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter p);
    }
}