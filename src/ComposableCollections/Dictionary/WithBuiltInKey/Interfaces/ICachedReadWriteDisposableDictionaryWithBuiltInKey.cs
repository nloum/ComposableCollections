using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TValue>, ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> {}
}