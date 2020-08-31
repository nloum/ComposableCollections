using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public interface ISetItemAddAttempt<out TValue>
    {
        bool Added { get; }
        TValue NewValue { get; }
    }
}