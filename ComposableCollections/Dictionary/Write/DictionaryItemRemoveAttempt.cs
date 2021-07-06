using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public class DictionaryItemRemoveAttempt<TValue> : IDictionaryItemRemoveAttempt<TValue>
    {
        public DictionaryItemRemoveAttempt(TValue? removedValue)
        {
            RemovedValue = removedValue;
        }

        public TValue? RemovedValue { get; }

        public override string ToString()
        {
            return RemovedValue != null ? "remove succeeded" : "remove failed";
        }
    }
}