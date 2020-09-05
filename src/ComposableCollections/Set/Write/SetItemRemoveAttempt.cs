using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetItemRemoveAttempt<TValue> : ISetItemRemoveAttempt<TValue>
    {
        public SetItemRemoveAttempt(bool successful, TValue removedValue)
        {
            Successful = successful;
            Value = removedValue;
        }

        public bool Successful { get; }
        public TValue Value { get; }

        public override string ToString()
        {
            return Successful ? $"successfully removed {Value}" : $"failed to remove {Value}";
        }
    }
}