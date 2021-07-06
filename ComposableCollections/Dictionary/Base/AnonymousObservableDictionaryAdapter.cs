using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;

namespace ComposableCollections.Dictionary.Base
{
    public class AnonymousObservableDictionaryAdapter<TKey, TValue> : ObservableDictionaryAdapterBase<TKey, TValue>
    {
        private Action<IKeyValue<TKey, TValue>> _onRemove;
        private Action<IKeyValue<TKey, TValue>> _onAdd;

        public AnonymousObservableDictionaryAdapter(Action<IKeyValue<TKey, TValue>> onRemove, Action<IKeyValue<TKey, TValue>> onAdd, IComposableDictionary<TKey, TValue>? state = null) : base(state ?? new ComposableDictionary<TKey, TValue>())
        {
            _onRemove = onRemove;
            _onAdd = onAdd;
        }

        protected override void OnRemove(IKeyValue<TKey, TValue> removedItem)
        {
            _onRemove(removedItem);
        }

        protected override void OnAdd(IKeyValue<TKey, TValue> addedItem)
        {
            _onAdd(addedItem);
        }
    }
}