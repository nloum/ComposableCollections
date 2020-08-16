using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public interface IDictionaryItemAddOrUpdate<out TValue>
    {
        DictionaryItemAddOrUpdateResult Result { get; }
        IMaybe<TValue> ExistingValue { get; }
        TValue NewValue { get; }
    }
}