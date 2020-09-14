namespace ComposableCollections.Dictionary.Interfaces {
public interface IWriteCachedDisposableDictionary<TKey, TValue> : IDisposableDictionary<TKey, TValue>, IWriteCachedDictionary<TKey, TValue> {
}
}