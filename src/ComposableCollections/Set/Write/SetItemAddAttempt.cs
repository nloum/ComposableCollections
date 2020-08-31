using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetItemAddAttempt<TValue> : ISetItemAddAttempt<TValue>
    {
        public SetItemAddAttempt(bool added, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            Added = added;
            ExistingValue = existingValue;
            NewValue = newValue;
        }

        public bool Added { get; }
        public IMaybe<TValue> ExistingValue { get; }
        public IMaybe<TValue> NewValue { get; }

        public override string ToString()
        {
            return Added ? "add succeeded" : "add failed";
        }
    }
}