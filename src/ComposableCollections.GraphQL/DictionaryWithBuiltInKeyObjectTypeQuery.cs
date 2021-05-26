using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using HotChocolate.Types;
using Humanizer;

namespace ComposableCollections.GraphQL
{
    public class DictionaryWithBuiltInKeyObjectTypeQuery<TComposableDictionary, TKey, TValue> : ObjectType<TComposableDictionary> where TComposableDictionary : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly ComposableDictionaryObjectTypeParameters _parameters;
        private readonly bool _allowEnumeration;

        public DictionaryWithBuiltInKeyObjectTypeQuery(ComposableDictionaryObjectTypeParameters parameters) : this(parameters, true)
        {
        }

        protected DictionaryWithBuiltInKeyObjectTypeQuery(ComposableDictionaryObjectTypeParameters parameters, bool allowEnumeration)
        {
            _parameters = parameters;
            _allowEnumeration = allowEnumeration;
        }

        protected override void Configure(IObjectTypeDescriptor<TComposableDictionary> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Name( _parameters.CollectionName.Pascalize() + "Query" );
            descriptor.Field(x => x.TryGetValue(default)).Name(_parameters.ValueNameSingular.Camelize());
            descriptor.Field(x => x.ContainsKey(default)).Name("contains" + _parameters.KeyNameSingular.Pascalize());
            descriptor.Field(x => x.Count);
            descriptor.Field(x => x.Keys).Name(_parameters.KeyNamePlural.Camelize());
            
            if (_allowEnumeration)
            {
                descriptor.Field(x => x.Values).Name(_parameters.ValueNamePlural.Camelize());
            }
        }
    }
}