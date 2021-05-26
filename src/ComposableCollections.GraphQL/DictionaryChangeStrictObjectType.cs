using HotChocolate.Language;
using HotChocolate.Types;
using Humanizer;
using LiveLinq.Dictionary;

namespace ComposableCollections.GraphQL
{
    public class DictionaryChangeStrictObjectType<TKey, TValue> : ObjectType<IDictionaryChangeStrict<TKey, TValue>> {
        private readonly ComposableDictionaryObjectTypeParameters _parameters;

        public DictionaryChangeStrictObjectType(ComposableDictionaryObjectTypeParameters parameters)
        {
            _parameters = parameters;
        }

        protected override void Configure( IObjectTypeDescriptor<IDictionaryChangeStrict<TKey, TValue>> descriptor ) {
            descriptor.Name( _parameters.CollectionName.Pascalize() + "Subscription" );
            descriptor.BindFieldsExplicitly();
            descriptor.Field( x => x.KeyValuePairs )
                //.Type<NonNullType<ListType<NonNullType<KeyValuePairType<TKey, TValue>>>>>()
                .Type(new NonNullType(new ListType(new NonNullType(new KeyValuePairType<TKey, TValue>(_parameters)))))
                .Name(_parameters.ValueNamePlural.Camelize());
        }
    }
}