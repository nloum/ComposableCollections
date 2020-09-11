namespace ComposableCollections.Dictionary.Interfaces
{
    public interface ICachedWriteQueryableDictionary<TKey, TValue> : ICachedWriteDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue> { }
}