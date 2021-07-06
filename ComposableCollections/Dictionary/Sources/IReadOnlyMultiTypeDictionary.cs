namespace ComposableCollections.Dictionary.Sources
{
    public interface IReadOnlyMultiTypeDictionary<in TValue>
    {
        bool TryGetValue<T>(out T result) where T : TValue;
    }
}