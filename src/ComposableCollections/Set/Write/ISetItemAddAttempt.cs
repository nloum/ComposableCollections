using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public interface ISetItemAddAttempt<out TValue>
    {
        bool Added { get; }
        IMaybe<TValue> ExistingValue { get; }
        IMaybe<TValue> NewValue { get; }
    }
}