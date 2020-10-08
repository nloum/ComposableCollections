namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadWriteDictionaryWithBuiltInKey<TKey, TValue> : ICachedWriteDictionaryWithBuiltInKey<TKey, TValue>,
        ICachedReadDictionaryWithBuiltInKey<TKey, TValue>
    {
    }
}