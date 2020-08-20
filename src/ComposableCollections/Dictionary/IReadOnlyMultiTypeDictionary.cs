namespace ComposableCollections.Dictionary
{
    public interface IReadOnlyMultiTypeDictionary<in TValue>
    {
        bool TryGetValue<T>(out T result) where T : TValue;
    }
}