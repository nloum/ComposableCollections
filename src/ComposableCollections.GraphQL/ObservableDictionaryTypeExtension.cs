using ComposableCollections.Dictionary.Interfaces;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class ObservableDictionaryTypeExtension<TComposableDictionary, TKey, TValue> : ObjectTypeExtension<ObservableDictionarySubscription<TComposableDictionary, TKey, TValue>> where TComposableDictionary : IObservableReadOnlyDictionary<TKey, TValue>
    {
        private readonly string _typeName;
        private readonly string _fieldName;

        public ObservableDictionaryTypeExtension(string typeName, string fieldName)
        {
            _typeName = typeName;
            _fieldName = fieldName;
        }

        protected override void Configure(IObjectTypeDescriptor<ObservableDictionarySubscription<TComposableDictionary, TKey, TValue>> descriptor)
        {
            descriptor.Name(_typeName);
            descriptor.BindFieldsExplicitly();
            descriptor.Field(x => x.Changes(default)).Name(_fieldName);
        }
    }
}