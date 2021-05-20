using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public interface IDictionaryItemAddAttempt<out TValue>
    {
        bool Added { get; }
        TValue? ExistingValue { get; }
        TValue? NewValue { get; }
    }
}