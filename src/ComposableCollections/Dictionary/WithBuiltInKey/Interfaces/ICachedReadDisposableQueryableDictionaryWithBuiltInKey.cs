using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TValue>, ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TValue>, ICachedReadDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}