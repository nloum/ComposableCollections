using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public interface IDictionaryItemAddOrUpdate<out TValue>
    {
        DictionaryItemAddOrUpdateResult Result { get; }
        TValue? ExistingValue { get; }
        TValue NewValue { get; }
    }
}