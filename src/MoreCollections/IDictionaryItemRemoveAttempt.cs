using SimpleMonads;

namespace MoreCollections
{
    public interface IDictionaryItemRemoveAttempt<out TValue>
    {
        IMaybe<TValue> RemovedValue { get; }
    }
}