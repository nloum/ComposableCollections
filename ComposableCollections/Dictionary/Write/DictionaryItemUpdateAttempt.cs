using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public class DictionaryItemUpdateAttempt<TValue> : IDictionaryItemUpdateAttempt<TValue>
    {
        public DictionaryItemUpdateAttempt(bool updated, TValue? existingValue, TValue? newValue)
        {
            Updated = updated;
            ExistingValue = existingValue;
            NewValue = newValue;
        }

        public bool Updated { get; }
        public TValue? ExistingValue { get; }
        public TValue? NewValue { get; }

        public override string ToString()
        {
            var status = Updated ? "update succeeded" : "update failed";
            return status;
        }
    }
}