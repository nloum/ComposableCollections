namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TValue>, ICachedWriteDisposableDictionaryWithBuiltInKey<TKey, TValue> {}
}