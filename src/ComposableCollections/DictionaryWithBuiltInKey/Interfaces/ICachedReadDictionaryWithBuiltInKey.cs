namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>,
        ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}