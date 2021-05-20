using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public interface IDictionaryItemUpdateAttempt<out TValue>
    {
        bool Updated { get; }
        TValue? ExistingValue { get; }
        TValue? NewValue { get; }
    }
}