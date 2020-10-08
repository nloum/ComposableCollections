namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}