namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public interface IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> :
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IDisposableQueryableReadOnlyDictionary<TKey, TValue> AsDisposableQueryableReadOnlyDictionary();
    }
}