namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedReadWriteQueryableDictionary<TKey, TValue> : ICachedReadQueryableDictionary<TKey, TValue>, ICachedWriteQueryableDictionary<TKey, TValue> { }
}