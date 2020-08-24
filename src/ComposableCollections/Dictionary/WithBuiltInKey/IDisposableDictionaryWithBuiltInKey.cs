namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public interface IDisposableDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>, IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IDisposableDictionary<TKey, TValue> AsDisposableDictionary();
    }
}