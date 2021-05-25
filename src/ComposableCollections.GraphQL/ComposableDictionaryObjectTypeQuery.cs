using ComposableCollections.Dictionary.Interfaces;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class ComposableDictionaryObjectTypeQuery<TComposableDictionary, TKey, TValue> : ObjectType<TComposableDictionary> where TComposableDictionary : IComposableReadOnlyDictionary<TKey, TValue>
    {
        private readonly string _typeName;
        private readonly bool _allowEnumeration;

        public ComposableDictionaryObjectTypeQuery(string typeName) : this(typeName, true)
        {
        }

        protected ComposableDictionaryObjectTypeQuery(string typeName, bool allowEnumeration)
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