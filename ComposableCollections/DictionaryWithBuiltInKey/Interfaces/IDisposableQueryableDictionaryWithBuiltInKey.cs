using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        IDisposableDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>,
        IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IDisposableQueryableDictionary<TKey, TValue> AsDisposableQueryableDictionary();
    }
}