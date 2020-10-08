
namespace ComposableCollections.Dictionary.Interfaces {
public interface IDisposableQueryableReadOnlyDictionary<TKey, TValue> : IQueryableReadOnlyDictionary<TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue> {
}
}