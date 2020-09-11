using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}