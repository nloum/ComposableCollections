namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface ICachedReadDisposableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>, ICachedReadDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}