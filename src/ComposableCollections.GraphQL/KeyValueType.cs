using ComposableCollections.Dictionary;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class KeyValueType<TKey, TValue> : ObjectType<IKeyValue<TKey, TValue>>
    {
        protected override void Configure(IObjectTypeDescriptor<IKeyValue<TKey, TValue>> descriptor)
        {
            descriptor.Name(typeof(TKey).Name + typeof(TValue).Name + "KeyValue");
        }
    }
}