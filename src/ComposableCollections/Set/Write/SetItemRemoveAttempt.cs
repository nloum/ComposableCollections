using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetItemRemoveAttempt<TValue> : ISetItemRemoveAttempt<TValue>
    {
        public SetItemRemoveAttempt(IMaybe<TValue> removedValue)
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