using ComposableCollections.Dictionary.Interfaces;
using HotChocolate.Types;
using Humanizer;

namespace ComposableCollections.GraphQL
{
    public class ObservableDictionaryTypeExtension<TComposableDictionary, TKey, TValue> : ObjectTypeExtension<ObservableDictionarySubscription<TComposableDictionary, TKey, TValue>> where TComposableDictionary : IObservableReadOnlyDictionary<TKey, TValue>
    {
        private readonly ComposableDictionaryObjectTypeParameters _parameters;

        public ObservableDictionaryTypeExtension(ComposableDictionaryObjectTypeParameters parameters)
        {
            _parameters = parameters;
        }

        protected override void Configure(IObjectTypeDescriptor<ObservableDictionarySubscription<TComposableDictionary, TKey, TValue>> descriptor)
        {
            descriptor.Name("Subscription");
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Changes(default)).Name(_parameters.CollectionName.Camelize());
        }
    }
}