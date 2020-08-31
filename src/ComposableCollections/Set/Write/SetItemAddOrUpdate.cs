using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetItemAddOrUpdate<TValue> : ISetItemAddOrUpdate<TValue>
    {
        public SetItemAddOrUpdate(SetItemAddOrUpdateResult result, IMaybe<TValue> existingValue, TValue newValue)
        {
            Result = result;
            ExistingValue = existingValue;
            NewValue = newValue;
        }

        public SetItemAddOrUpdateResult Result { get; }
        public IMaybe<TValue> ExistingValue { get; }
        public TValue NewValue { get; }

        public override string ToString()
        {
            var result = Result == SetItemAddOrUpdateResult.Add ? "added the value" : "updated the value";
            return result;
        }
    }
}