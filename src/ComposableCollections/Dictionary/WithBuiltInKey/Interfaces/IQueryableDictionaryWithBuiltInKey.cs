namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public interface IQueryableDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>,
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IQueryableDictionary<TKey, TValue> AsQueryableDictionary();
    }
}