namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>,
        ICachedReadReadOnlyDictionary<TKey, TValue> { }
}