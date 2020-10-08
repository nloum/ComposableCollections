namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>, ICachedWriteDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> { }
}