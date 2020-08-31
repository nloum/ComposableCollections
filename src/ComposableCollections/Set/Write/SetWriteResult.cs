using ComposableCollections.Common;
using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetWriteResult<TKey, TValue>
    {
        private readonly IEither<ISetItemAddAttempt<TValue>, IMaybe<TValue>> _either;

        public static SetWriteResult<TKey, TValue> CreateAdd(TKey key, bool added, TValue newValue)
        {
            return new SetWriteResult<TKey, TValue>(key, new SetItemAddAttempt<TValue>(added, newValue), CollectionWriteType.Add);
        }

        public static SetWriteResult<TKey, TValue> CreateTryAdd(TKey key, bool added, TValue newValue)
        {
            return new SetWriteResult<TKey, TValue>(key, new SetItemAddAttempt<TValue>(added, newValue), CollectionWriteType.TryAdd);
        }

        public static SetWriteResult<TKey, TValue> CreateRemove(TKey key, IMaybe<TValue> removedValue)
        {
            return new SetWriteResult<TKey, TValue>(key, removedValue, CollectionWriteType.Remove);
        }
        
        public static SetWriteResult<TKey, TValue> CreateTryRemove(TKey key, IMaybe<TValue> removedValue)
        {
            return new SetWriteResult<TKey, TValue>(key, removedValue, CollectionWriteType.TryRemove);
        }

        protected SetWriteResult(TKey key, ISetItemAddAttempt<TValue> add, CollectionWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<ISetItemAddAttempt<TValue>, IMaybe<TValue>>(add);
        }
        
        protected SetWriteResult(TKey key, IMaybe<TValue> remove, CollectionWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<ISetItemAddAttempt<TValue>, IMaybe<TValue>>(remove);
        }

        public CollectionWriteType Type { get; }
        
        public TKey Key { get; }
        public IMaybe<ISetItemAddAttempt<TValue>> Add => _either.Item1;

        public IMaybe<IMaybe<TValue>> Remove => _either.Item2;

        public override string ToString()
        {
            return $"{Key} {base.ToString()}";
        }
    }
}