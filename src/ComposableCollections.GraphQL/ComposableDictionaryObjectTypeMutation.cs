using ComposableCollections.Dictionary.Interfaces;
using HotChocolate.Types;
using Humanizer;

namespace ComposableCollections.GraphQL
{
    public class ComposableDictionaryObjectTypeMutation<TComposableDictionary, TKey, TValue> : ObjectType<TComposableDictionary> where TComposableDictionary : IComposableDictionary<TKey, TValue>
    {
        private readonly ComposableDictionaryObjectTypeParameters _parameters;

        public ComposableDictionaryObjectTypeMutation(ComposableDictionaryObjectTypeParameters parameters)
        {
            _parameters = parameters;
        }

        protected override void Configure(IObjectTypeDescriptor<TComposableDictionary> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Name( _parameters.CollectionName.Pascalize() + "Mutation" );
            descriptor.Field(x => x.GetOrAdd(default, default(TValue))).Name("getOrAdd" + _parameters.ValueNameSingular.Pascalize());
            descriptor.Field(x => x.TryAdd(default, default(TValue))).Name("tryAdd" + _parameters.ValueNameSingular.Pascalize());
            descriptor.Field(x => x.TryUpdate(default, default)).Name("tryUpdate" + _parameters.ValueNameSingular.Pascalize());
            descriptor.Field(x => x.AddOrUpdate(default, default)).Name("addOrUpdate" + _parameters.ValueNameSingular.Pascalize());
            descriptor.Field(x => x.TryRemove(default)).Name("tryRemove" + _parameters.ValueNameSingular.Pascalize());
        }
    }
}