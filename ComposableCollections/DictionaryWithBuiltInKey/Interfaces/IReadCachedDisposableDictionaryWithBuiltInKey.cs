namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadCachedDisposableDictionaryWithBuiltInKey<TKey, TValue> : IReadCachedDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>, IReadCachedDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}