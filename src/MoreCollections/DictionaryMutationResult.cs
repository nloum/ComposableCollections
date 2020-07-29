using SimpleMonads;

namespace MoreCollections
{
    public class DictionaryMutationResult<TKey, TValue>
    {
        private readonly IEither<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>,
            IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>> _either;

        public static DictionaryMutationResult<TKey, TValue> CreateAdd(TKey key, bool added, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new DictionaryMutationResult<TKey, TValue>(key, new DictionaryItemAddAttempt<TValue>(added, existingValue, newValue), DictionaryMutationType.Add);
        }

        public static DictionaryMutationResult<TKey, TValue> CreateTryAdd(TKey key, bool added, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new DictionaryMutationResult<TKey, TValue>(key, new DictionaryItemAddAttempt<TValue>(added, existingValue, newValue), DictionaryMutationType.TryAdd);
        }

        public static DictionaryMutationResult<TKey, TValue> CreateUpdate(TKey key, bool updated, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new DictionaryMutationResult<TKey, TValue>(key, new DictionaryItemUpdateAttempt<TValue>(updated, existingValue, newValue), DictionaryMutationType.Update);
        }

        public static DictionaryMutationResult<TKey, TValue> CreateTryUpdate(TKey key, bool updated, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            return new DictionaryMutationResult<TKey, TValue>(key, new DictionaryItemUpdateAttempt<TValue>(updated, existingValue, newValue), DictionaryMutationType.TryUpdate);
        }

        public static DictionaryMutationResult<TKey, TValue> CreateAddOrUpdate(TKey key, DictionaryItemAddOrUpdateResult result, IMaybe<TValue> existingValue, TValue newValue)
        {
            return new DictionaryMutationResult<TKey, TValue>(key, new DictionaryItemAddOrUpdate<TValue>(result, existingValue, newValue), DictionaryMutationType.TryUpdate);
        }
        
        public static DictionaryMutationResult<TKey, TValue> CreateRemove(TKey key, IMaybe<TValue> removedValue)
        {
            return new DictionaryMutationResult<TKey, TValue>(key, removedValue, DictionaryMutationType.Remove);
        }
        
        public static DictionaryMutationResult<TKey, TValue> CreateTryRemove(TKey key, IMaybe<TValue> removedValue)
        {
            return new DictionaryMutationResult<TKey, TValue>(key, removedValue, DictionaryMutationType.TryRemove);
        }

        protected DictionaryMutationResult(TKey key, IDictionaryItemAddAttempt<TValue> add, DictionaryMutationType type)
        {
            Key = key;
            Type = type;
            _either = new Either<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>, IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>>(add);
        }
        
        protected DictionaryMutationResult(TKey key, IDictionaryItemAddOrUpdate<TValue> addOrUpdate, DictionaryMutationType type)
        {
            Key = key;
            Type = type;
            _either = new Either<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>, IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>>(addOrUpdate);
        }

        protected DictionaryMutationResult(TKey key, IDictionaryItemUpdateAttempt<TValue> update, DictionaryMutationType type)
        {
            Key = key;
            Type = type;
            _either = new Either<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>, IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>>(update);
        }

        protected DictionaryMutationResult(TKey key, IMaybe<TValue> remove, DictionaryMutationType type)
        {
            Key = key;
            Type = type;
            _either = new Either<IDictionaryItemAddAttempt<TValue>, IDictionaryItemAddOrUpdate<TValue>, IDictionaryItemUpdateAttempt<TValue>, IMaybe<TValue>>(remove);
        }

        public DictionaryMutationType Type { get; }
        
        public TKey Key { get; }
        public IMaybe<IDictionaryItemAddAttempt<TValue>> Add => _either.Item1;

        public IMaybe<IDictionaryItemAddOrUpdate<TValue>> AddOrUpdate => _either.Item2;

        public IMaybe<IDictionaryItemUpdateAttempt<TValue>> Update => _either.Item3;
        public IMaybe<IMaybe<TValue>> Remove => _either.Item4;
    }
}