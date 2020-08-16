using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public interface IDictionaryItemRemoveAttempt<out TValue>
    {
        IMaybe<TValue> RemovedValue { get; }
    }
}