using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>, ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}