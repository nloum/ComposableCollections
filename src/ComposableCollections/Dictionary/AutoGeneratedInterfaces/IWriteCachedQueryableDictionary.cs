namespace ComposableCollections.Dictionary.Interfaces {
public interface IWriteCachedQueryableDictionary<TKey, TValue> : IQueryableDictionary<TKey, TValue>, IWriteCachedDictionary<TKey, TValue> {
}
}