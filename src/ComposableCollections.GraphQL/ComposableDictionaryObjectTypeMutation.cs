using ComposableCollections.Dictionary.Interfaces;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class ComposableDictionaryObjectTypeMutation<TComposableDictionary, TKey, TValue> : ObjectType<TComposableDictionary> where TComposableDictionary : IComposableDictionary<TKey, TValue>
    {
        private readonly string _typeName;

        public ComposableDictionaryObjectTypeMutation(string typeName)
        {
            _typeName = typeName;
        }

        protected override void Configure(IObjectTypeDescriptor<TComposableDictionary> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Name(_typeName );
            descriptor.Field(x => x.GetOrAdd(default, default(TValue))).Name("getOrAdd");
            descriptor.Field(x => x.TryAdd(default, default(TValue)));
            descriptor.Field(x => x.TryUpdate(default, default));
            descriptor.Field(x => x.AddOrUpdate(default, default));
            descriptor.Field(x => x.TryRemove(default));
        }
    }
}