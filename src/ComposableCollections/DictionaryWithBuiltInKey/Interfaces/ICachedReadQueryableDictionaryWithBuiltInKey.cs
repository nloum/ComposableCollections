namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>, ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}