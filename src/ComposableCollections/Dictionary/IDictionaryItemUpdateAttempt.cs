using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public interface IDictionaryItemUpdateAttempt<out TValue>
    {
        bool Updated { get; }
        IMaybe<TValue> ExistingValue { get; }
        IMaybe<TValue> NewValue { get; }
    }
}