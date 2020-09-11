using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadWriteDictionaryWithBuiltInKey<TKey, TValue> : ICachedWriteDictionaryWithBuiltInKey<TKey, TValue>,
        ICachedReadDictionaryWithBuiltInKey<TKey, TValue>
    {
    }
}