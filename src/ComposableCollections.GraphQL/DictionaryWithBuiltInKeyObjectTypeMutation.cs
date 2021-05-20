using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class DictionaryWithBuiltInKeyObjectTypeMutation<TDictionaryWithBuiltInKey, TKey, TValue> : ObjectType<TDictionaryWithBuiltInKey> where TDictionaryWithBuiltInKey : IDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly string _typeName;

        public DictionaryWithBuiltInKeyObjectTypeMutation(string typeName)
        {
            _typeName = typeName;
        }

        protected override void Configure(IObjectTypeDescriptor<TDictionaryWithBuiltInKey> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Name(_typeName );
            descriptor.Field(x => x.GetOrAdd(default(TValue))).Name("getOrAdd");
            descriptor.Field(x => x.TryAdd(default(TValue)));
            descriptor.Field(x => x.TryUpdate(default));
            descriptor.Field(x => x.AddOrUpdate(default));
            descriptor.Field(x => x.TryRemove(default));
        }
    }
}