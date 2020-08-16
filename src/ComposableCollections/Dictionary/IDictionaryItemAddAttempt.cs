using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public interface IDictionaryItemAddAttempt<out TValue>
    {
        bool Added { get; }
        IMaybe<TValue> ExistingValue { get; }
        IMaybe<TValue> NewValue { get; }
    }
}