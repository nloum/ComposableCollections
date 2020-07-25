using SimpleMonads;

namespace MoreCollections
{
    public class DictionaryItemAddAttempt<TValue> : IDictionaryItemAddAttempt<TValue>
    {
        public DictionaryItemAddAttempt(bool added, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            Added = added;
            ExistingValue = existingValue;
            NewValue = newValue;
        }

        public bool Added { get; }
        public IMaybe<TValue> ExistingValue { get; }
        public IMaybe<TValue> NewValue { get; }
    }
}