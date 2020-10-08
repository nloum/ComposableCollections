namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface ICachedReadWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> : ICachedReadQueryableDictionaryWithBuiltInKey<TKey, TValue>, ICachedWriteQueryableDictionaryWithBuiltInKey<TKey, TValue> { }
}