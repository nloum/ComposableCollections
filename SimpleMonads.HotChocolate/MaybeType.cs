using HotChocolate.Types;

namespace SimpleMonads.HotChocolate
{
    
    
    public class MaybeType<T> : ObjectType<IMaybe<T>>
    {
        protected override void Configure(IObjectTypeDescriptor<IMaybe<T>> descriptor)
        {
            descriptor.Name("Maybe" + typeof(T).Name);
            descriptor.Field(x => x.Value).Ignore();
            descriptor.Field(x => x.ValueOrDefault).Name("value");
        }
    }
}