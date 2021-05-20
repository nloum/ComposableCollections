using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class DictionaryWithBuiltInKeyObjectTypeQuery<TComposableDictionary, TKey, TValue> : ObjectType<TComposableDictionary> where TComposableDictionary : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly string _typeName;
        private readonly bool _allowEnumeration;

        public DictionaryWithBuiltInKeyObjectTypeQuery(string typeName) : this(typeName, true)
        {
        }

        protected DictionaryWithBuiltInKeyObjectTypeQuery(string typeName, bool allowEnumeration)
        {
            _typeName = typeName;
            _allowEnumeration = allowEnumeration;
        }

        protected override void Configure(IObjectTypeDescriptor<TComposableDictionary> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Name(_typeName );
            descriptor.Field(x => x.TryGetValue(default)).Name("get");
            descriptor.Field(x => x.ContainsKey(default));
            descriptor.Field(x => x.Count);
            descriptor.Field(x => x.Keys);
            
            if (_allowEnumeration)
            {
                descriptor.Field(x => x.Values);
            }
        }
    }
}