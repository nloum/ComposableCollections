using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}