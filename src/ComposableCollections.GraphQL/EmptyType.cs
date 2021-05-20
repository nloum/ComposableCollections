using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class EmptyType<T> : ObjectType<T>
    {
        protected override void Configure(IObjectTypeDescriptor<T> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field("hello").Type<StringType>().Resolve(rc => "world");
        }
    }
}