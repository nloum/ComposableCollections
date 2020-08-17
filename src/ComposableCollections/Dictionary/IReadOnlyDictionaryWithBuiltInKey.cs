namespace ComposableCollections.Dictionary
{
    public interface IReadOnlyDictionaryWithBuiltInKey<TKey, out TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
    }
}