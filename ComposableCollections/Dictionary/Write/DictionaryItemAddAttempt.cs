using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public class DictionaryItemAddAttempt<TValue> : IDictionaryItemAddAttempt<TValue>
    {
        public DictionaryItemAddAttempt(bool added, TValue? existingValue, TValue? newValue)
        {
            Added = added;
            ExistingValue = existingValue;
            NewValue = newValue;
        }

        public bool Added { get; }
        public TValue? ExistingValue { get; }
        public TValue? NewValue { get; }

        public override string ToString()
        {
            return Added ? "add succeeded" : "add failed";
        }
    }
}