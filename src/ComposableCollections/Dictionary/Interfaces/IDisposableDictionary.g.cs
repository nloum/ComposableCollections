
namespace ComposableCollections.Dictionary.Interfaces {
public interface IDisposableDictionary<TKey, TValue> : IComposableDictionary<TKey, TValue>, IDisposableReadOnlyDictionary<TKey, TValue> {
}
}