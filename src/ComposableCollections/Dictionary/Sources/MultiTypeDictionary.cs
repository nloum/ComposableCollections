using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Sources
{
    public class MultiTypeDictionary<TValue> : IMultiTypeDictionary<TValue>
    {
        private readonly IComposableDictionary<Type, TValue> _values;

        public MultiTypeDictionary(IComposableDictionary<Type, TValue> values = null)
        {
            _values = values ?? new ComposableDictionary<Type, TValue>();
        }
        
        public virtual bool TryAdd<T>(Func<T> value, out T result, out T previousValue) where T : TValue
        {
            var key = typeof(T);
            _values.Write(new[] { DictionaryWrite<Type, TValue>.CreateTryAdd(key, () => value()) }, out var results);
            var firstResult = results.First();
            result = (T) firstResult.Add.Value.NewValue.ValueOrDefault;
            previousValue = (T) firstResult.Add.Value.ExistingValue.ValueOrDefault;
            return firstResult.Add.Value.NewValue.HasValue;
        }

        public virtual bool TryUpdate<T>(Func<TValue, TValue> value, out TValue previousValue, out TValue newValue) where T : TValue
        {
            var key = typeof(T);
            _values.Write(new[] {DictionaryWrite<Type, TValue>.CreateTryUpdate(key, value)}, out var results);
            var firstResult = results.First();
            newValue = firstResult.Update.Value.NewValue.ValueOrDefault;
            previousValue = firstResult.Update.Value.ExistingValue.ValueOrDefault;
            return firstResult.Update.Value.NewValue.HasValue;
        }

        public DictionaryItemAddOrUpdateResult AddOrUpdate<T>(Func<T> valueIfAdding,
            Func<T, T> valueIfUpdating, out T previousValue, out T newValue) where T : TValue
        {
            var key = typeof(T);
            _values.Write(new[] {DictionaryWrite<Type, TValue>.CreateAddOrUpdate(key, () => valueIfAdding(), x => valueIfUpdating((T)x))}, out var results);
            var firstResult = results.First();
            newValue = (T) firstResult.AddOrUpdate.Value.NewValue;
            previousValue = (T) firstResult.AddOrUpdate.Value.ExistingValue.ValueOrDefault;
            return firstResult.AddOrUpdate.Value.Result;
        }

        public virtual bool TryRemove<T>(out T removedItem) where T : TValue
        {
            var key = typeof(T);
            _values.Write(new[] {DictionaryWrite<Type, TValue>.CreateTryRemove(key)}, out var results);
            var firstResult = results.First();
            removedItem = (T) firstResult.Remove.Value.ValueOrDefault;
            return firstResult.Remove.Value.HasValue;
        }

        public bool TryGetValue<T>(out T result) where T : TValue
        {
            var key = typeof(T);

            var returnValue = _values.TryGetValue(key, out TValue tmpResult);

            result = (T) tmpResult;
            
            return returnValue;
        }
    }
}