namespace ComposableCollections.Dictionary
{
    public interface IDisposableDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>, IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
    }
}