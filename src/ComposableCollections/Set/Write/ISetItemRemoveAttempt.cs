using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public interface ISetItemRemoveAttempt<out TValue>
    {
        IMaybe<TValue> RemovedValue { get; }
    }
}