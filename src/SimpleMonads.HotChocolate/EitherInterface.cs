using HotChocolate.Types;

namespace SimpleMonads.HotChocolate
{
    public class EitherInterface<TBase> : InterfaceType<TBase>
    {
        protected override void Configure(IInterfaceTypeDescriptor<TBase> descriptor)
        {
            descriptor.Name(typeof(TBase).Name);
        }
    }
}