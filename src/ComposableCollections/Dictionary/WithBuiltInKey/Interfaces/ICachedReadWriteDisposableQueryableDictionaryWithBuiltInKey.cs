using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>, ICachedWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> { }
}