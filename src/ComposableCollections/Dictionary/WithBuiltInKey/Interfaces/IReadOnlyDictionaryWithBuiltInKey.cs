using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.WithBuiltInKey.Interfaces
{
    public interface IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        IComposableReadOnlyDictionary<TKey, TValue> AsComposableReadOnlyDictionary();
        TKey GetKey(TValue value);
    }
}