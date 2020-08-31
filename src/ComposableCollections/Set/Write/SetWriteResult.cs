using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Set.Write
{
    public class SetWriteResult<TValue>
    {
        public SetWriteResult(SetWriteType type, bool successful, TValue value)
        {
            Type = type;
            Successful = successful;
            Value = value;
        }

        public SetWriteType Type { get; }
        public bool Successful { get; }
        public TValue Value { get; }

        public override string ToString()
        {
            var adverbly = Successful ? "successfully" : "unsuccessfully";
            var typePastTense = (Type == SetWriteType.Add || Type == SetWriteType.TryAdd) ? "added" : "removed";
            return $"{Value} was {adverbly} {typePastTense}";
        }
    }
}