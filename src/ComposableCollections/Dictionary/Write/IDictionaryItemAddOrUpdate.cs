using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public interface IDictionaryItemAddOrUpdate<out TValue>
    {
        DictionaryItemAddOrUpdateResult Result { get; }
        IMaybe<TValue> ExistingValue { get; }
        TValue NewValue { get; }
    }
}