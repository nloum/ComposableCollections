namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> { }
}