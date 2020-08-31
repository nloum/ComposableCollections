using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public interface ISetItemAddOrUpdate<out TValue>
    {
        SetItemAddOrUpdateResult Result { get; }
        IMaybe<TValue> ExistingValue { get; }
        TValue NewValue { get; }
    }
}