using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public interface IDictionaryItemRemoveAttempt<out TValue>
    {
        IMaybe<TValue> RemovedValue { get; }
    }
}