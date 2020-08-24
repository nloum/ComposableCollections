namespace ComposableCollections.Dictionary
{
    public interface IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        IComposableReadOnlyDictionary<TKey, TValue> AsComposableReadOnlyDictionary();
        TKey GetKey(TValue value);
    }
}