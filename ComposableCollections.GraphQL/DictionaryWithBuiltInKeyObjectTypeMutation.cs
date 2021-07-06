using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using HotChocolate.Types;
using Humanizer;

namespace ComposableCollections.GraphQL
{
    public class DictionaryWithBuiltInKeyObjectTypeMutation<TDictionaryWithBuiltInKey, TKey, TValue> : ObjectType<TDictionaryWithBuiltInKey> where TDictionaryWithBuiltInKey : IDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly ComposableDictionaryObjectTypeParameters _parameters;

        public DictionaryWithBuiltInKeyObjectTypeMutation(ComposableDictionaryObjectTypeParameters parameters)
        {
            _parameters = parameters;
        }

        protected override void Configure(IObjectTypeDescriptor<TDictionaryWithBuiltInKey> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Name( _parameters.CollectionName.Pascalize() + "Mutation" );
            descriptor.Field(x => x.GetOrAdd(default(TValue))).Name("getOrAdd" + _parameters.ValueNameSingular.Pascalize() );
            descriptor.Field(x => x.TryAdd(default(TValue))).Name("tryAdd" + _parameters.ValueNameSingular.Pascalize());
            descriptor.Field(x => x.TryUpdate(default)).Name("tryUpdate" + _parameters.ValueNameSingular.Pascalize());
            descriptor.Field(x => x.AddOrUpdate(default)).Name("addOrUpdate" + _parameters.ValueNameSingular.Pascalize());
            descriptor.Field(x => x.TryRemove(default)).Name("tryRemove" + _parameters.ValueNameSingular.Pascalize());
        }
    }
}