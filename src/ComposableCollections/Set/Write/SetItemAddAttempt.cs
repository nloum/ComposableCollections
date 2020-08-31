using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetItemAddAttempt<TValue> : ISetItemAddAttempt<TValue>
    {
        public SetItemAddAttempt(bool added, TValue newValue)
        {
            Added = added;
            NewValue = newValue;
        }

        public bool Added { get; }
        public TValue NewValue { get; }

        public override string ToString()
        {
            return Added ? "add succeeded" : "add failed";
        }
    }
}