using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TValue>, ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> { }
}