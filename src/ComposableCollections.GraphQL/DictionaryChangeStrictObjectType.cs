using HotChocolate.Types;
using LiveLinq.Dictionary;

namespace ComposableCollections.GraphQL
{
    public class DictionaryChangeStrictObjectType<TKey, TValue> : ObjectType<IDictionaryChangeStrict<TKey, TValue>> {
        private readonly string _typeName;

        public DictionaryChangeStrictObjectType(string typeName)
        {
            _typeName = typeName;
        }

        protected override void Configure( IObjectTypeDescriptor<IDictionaryChangeStrict<TKey, TValue>> descriptor ) {
            descriptor.Name( _typeName );
            descriptor.BindFieldsExplicitly();
            descriptor.Field( x => x.KeyValuePairs ).Type<NonNullType<ListType<NonNullType<KeyValuePairType<TKey, TValue>>>>>();
        }
    }
}