using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public class DictionaryItemAddOrUpdate<TValue> : IDictionaryItemAddOrUpdate<TValue>
    {
        public DictionaryItemAddOrUpdate(DictionaryItemAddOrUpdateResult result, TValue? existingValue, TValue newValue)
        {
            Result = result;
            ExistingValue = existingValue;
            NewValue = newValue;
        }

        public DictionaryItemAddOrUpdateResult Result { get; }
        public TValue? ExistingValue { get; }
        public TValue NewValue { get; }

        public override string ToString()
        {
            var result = Result == DictionaryItemAddOrUpdateResult.Add ? "added the value" : "updated the value";
            return result;
        }
    }
}