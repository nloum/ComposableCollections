using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public class DictionaryWriteResult<TKey, TValue>
    {
        private readonly IEither<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>,
            IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>> _either;

        public static DictionaryWriteResult<TKey, TValue> CreateAdd(TKey key, bool added, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new DictionaryWriteResult<TKey, TValue>(key, new DictionaryItemAddAttempt<TValue>(added, existingValue, newValue), DictionaryWriteType.Add);
        }

        public static DictionaryWriteResult<TKey, TValue> CreateTryAdd(TKey key, bool added, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new DictionaryWriteResult<TKey, TValue>(key, new DictionaryItemAddAttempt<TValue>(added, existingValue, newValue), DictionaryWriteType.TryAdd);
        }

        public static DictionaryWriteResult<TKey, TValue> CreateUpdate(TKey key, bool updated, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new DictionaryWriteResult<TKey, TValue>(key, new DictionaryItemUpdateAttempt<TValue>(updated, existingValue, newValue), DictionaryWriteType.Update);
        }

        public static DictionaryWriteResult<TKey, TValue> CreateTryUpdate(TKey key, bool updated, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new DictionaryWriteResult<TKey, TValue>(key, new DictionaryItemUpdateAttempt<TValue>(updated, existingValue, newValue), DictionaryWriteType.TryUpdate);
        }

        public static DictionaryWriteResult<TKey, TValue> CreateAddOrUpdate(TKey key, DictionaryItemAddOrUpdateResult result, IMaybe<TValue> existingValue, TValue newValue)
        {
            return new DictionaryWriteResult<TKey, TValue>(key, new DictionaryItemAddOrUpdate<TValue>(result, existingValue, newValue), DictionaryWriteType.TryUpdate);
        }
        
        public static DictionaryWriteResult<TKey, TValue> CreateRemove(TKey key, IMaybe<TValue> removedValue)
        {
            return new DictionaryWriteResult<TKey, TValue>(key, removedValue, DictionaryWriteType.Remove);
        }
        
        public static DictionaryWriteResult<TKey, TValue> CreateTryRemove(TKey key, IMaybe<TValue> removedValue)
        {
            return new DictionaryWriteResult<TKey, TValue>(key, removedValue, DictionaryWriteType.TryRemove);
        }

        protected DictionaryWriteResult(TKey key, IDictionaryItemAddAttempt<TValue> add, DictionaryWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>, IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>>(add);
        }
        
        protected DictionaryWriteResult(TKey key, IDictionaryItemAddOrUpdate<TValue> addOrUpdate, DictionaryWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>, IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>>(addOrUpdate);
        }

        protected DictionaryWriteResult(TKey key, IDictionaryItemUpdateAttempt<TValue> update, DictionaryWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>, IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>>(update);
        }

        protected DictionaryWriteResult(TKey key, IMaybe<TValue> remove, DictionaryWriteType type)
        {
            Key = key;
            Type = type;
            _either = new Either<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>, IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>>(remove);
        }

        public DictionaryWriteType Type { get; }
        
        public TKey Key { get; }
        public IMaybe<IDictionaryItemAddAttempt<TValue>> Add => _either.Item1;

        public IMaybe<IDictionaryItemAddOrUpdate<TValue>> AddOrUpdate => _either.Item2;

        public IMaybe<IDictionaryItemUpdateAttempt<TValue>> Update => _either.Item3;
        public IMaybe<IMaybe<TValue>> Remove => _either.Item4;

        public override string ToString()
        {
            return $"{Key} {base.ToString()}";
        }
    }
}