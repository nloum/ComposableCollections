using SimpleMonads;

namespace MoreCollections
{
    public class DictionaryItemRemoveAttempt<TValue> : IDictionaryItemRemoveAttempt<TValue>
    {
        public DictionaryItemRemoveAttempt(IMaybe<TValue> removedValue)
        {
            RemovedValue = removedValue;
        }

        public IMaybe<TValue> RemovedValue { get; }

        public override string ToString()
        {
            return RemovedValue.HasValue ? "remove succeeded" : "remove failed";
        }
    }
}