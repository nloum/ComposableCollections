using ComposableCollections.Common;
using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetWriteResult<TKey, TValue>
    {
        private readonly IEither<ISetItemAddAttempt<TValue>, ISetItemAddOrUpdate<TValue>,
            ISetItemUpdateAttempt<TValue>, IMaybe<TValue>> _either;

        public static SetWriteResult<TKey, TValue> CreateAdd(TKey key, bool added, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new SetWriteResult<TKey, TValue>(key, new SetItemAddAttempt<TValue>(added, existingValue, newValue), CollectionWriteType.Add);
        }

        public static SetWriteResult<TKey, TValue> CreateTryAdd(TKey key, bool added, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new SetWriteResult<TKey, TValue>(key, new SetItemAddAttempt<TValue>(added, existingValue, newValue), CollectionWriteType.TryAdd);
        }

        public static SetWriteResult<TKey, TValue> CreateUpdate(TKey key, bool updated, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new SetWriteResult<TKey, TValue>(key, new SetItemUpdateAttempt<TValue>(updated, existingValue, newValue), CollectionWriteType.Update);
        }

        public static SetWriteResult<TKey, TValue> CreateTryUpdate(TKey key, bool updated, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new SetWriteResult<TKey, TValue>(key, new SetItemUpdateAttempt<TValue>(updated, existingValue, newValue), CollectionWriteType.TryUpdate);
        }

        public static SetWriteResult<TKey, TValue> CreateAddOrUpdate(TKey key, SetItemAddOrUpdateResult result, IMaybe<TValue> existingValue, TValue newValue)
        {
            return new SetWriteResult<TKey, TValue>(key, new SetItemAddOrUpdate<TValue>(result, existingValue, newValue), CollectionWriteType.TryUpdate);
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
            _either = new Either<ISetItemAddAttempt<TValue>, ISetItemAddOrUpdate<TValue>, ISetItemUpdateAttempt<TValue>, IMaybe<TValue>>(add);
        }
        
        protected SetWriteResult(TKey key, ISetItemAddOrUpdate<TValue> addOrUpdate, CollectionWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<ISetItemAddAttempt<TValue>, ISetItemAddOrUpdate<TValue>, ISetItemUpdateAttempt<TValue>, IMaybe<TValue>>(addOrUpdate);
        }

        protected SetWriteResult(TKey key, ISetItemUpdateAttempt<TValue> update, CollectionWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<ISetItemAddAttempt<TValue>, ISetItemAddOrUpdate<TValue>, ISetItemUpdateAttempt<TValue>, IMaybe<TValue>>(update);
        }

        protected SetWriteResult(TKey key, IMaybe<TValue> remove, CollectionWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<ISetItemAddAttempt<TValue>, ISetItemAddOrUpdate<TValue>, ISetItemUpdateAttempt<TValue>, IMaybe<TValue>>(remove);
        }

        public CollectionWriteType Type { get; }
        
        public TKey Key { get; }
        public IMaybe<ISetItemAddAttempt<TValue>> Add => _either.Item1;

        public IMaybe<ISetItemAddOrUpdate<TValue>> AddOrUpdate => _either.Item2;

        public IMaybe<ISetItemUpdateAttempt<TValue>> Update => _either.Item3;
        public IMaybe<IMaybe<TValue>> Remove => _either.Item4;

        public override string ToString()
        {
            return $"{Key} {base.ToString()}";
        }
    }
}