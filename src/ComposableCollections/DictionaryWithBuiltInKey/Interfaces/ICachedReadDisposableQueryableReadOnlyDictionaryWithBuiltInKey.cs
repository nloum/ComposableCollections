namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}