using SimpleMonads;

namespace ComposableCollections.Dictionary.Mutations
{
    public interface IDictionaryItemUpdateAttempt<out TValue>
    {
        bool Updated { get; }
        IMaybe<TValue> ExistingValue { get; }
        IMaybe<TValue> NewValue { get; }
    }
}