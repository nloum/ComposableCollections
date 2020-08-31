using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public interface ISetItemUpdateAttempt<out TValue>
    {
        bool Updated { get; }
        IMaybe<TValue> ExistingValue { get; }
        IMaybe<TValue> NewValue { get; }
    }
}