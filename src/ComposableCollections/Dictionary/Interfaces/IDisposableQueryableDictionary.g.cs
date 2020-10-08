
namespace ComposableCollections.Dictionary.Interfaces {
public interface IDisposableQueryableDictionary<TKey, TValue> : IQueryableDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue> {
}
}