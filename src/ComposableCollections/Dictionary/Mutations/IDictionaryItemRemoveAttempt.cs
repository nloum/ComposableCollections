using SimpleMonads;

namespace ComposableCollections.Dictionary.Mutations
{
    public interface IDictionaryItemRemoveAttempt<out TValue>
    {
        IMaybe<TValue> RemovedValue { get; }
    }
}