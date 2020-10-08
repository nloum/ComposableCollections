namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadCachedDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>,
        IReadCachedReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        
    }
}