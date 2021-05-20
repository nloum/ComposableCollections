using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public interface IDictionaryItemRemoveAttempt<out TValue>
    {
        TValue? RemovedValue { get; }
    }
}