namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TValue>, ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TValue>, ICachedReadDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}