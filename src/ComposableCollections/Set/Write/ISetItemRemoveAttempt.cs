using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public interface ISetItemRemoveAttempt<out TValue>
    {
        bool Successful { get; }
        TValue Value { get; }
    }
}