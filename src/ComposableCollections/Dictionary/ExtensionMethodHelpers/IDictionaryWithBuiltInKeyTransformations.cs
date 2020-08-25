using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public interface IDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        ICachedDictionaryWithBuiltInKey<TKey2, TValue2> Transform(ICachedDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter parameter);
        ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter);
        IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter);
        IQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter);
        ICachedQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(ICachedQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter);
        ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(ICachedDisposableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter);
        IDisposableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter);
        IDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter);
    }
}