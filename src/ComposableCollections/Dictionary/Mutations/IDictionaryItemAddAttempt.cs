using SimpleMonads;

namespace ComposableCollections.Dictionary.Mutations
{
    public interface IDictionaryItemAddAttempt<out TValue>
    {
        bool Added { get; }
        IMaybe<TValue> ExistingValue { get; }
        IMaybe<TValue> NewValue { get; }
    }
}