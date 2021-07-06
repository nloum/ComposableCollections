using HotChocolate.Types;

namespace SimpleMonads.HotChocolate
{
    public class EitherInterface<TBase> : InterfaceType<TBase>
    {
        private EitherObjectTypeParameters _parameters;

        public EitherInterface(EitherObjectTypeParameters parameters)
        {
            _parameters = parameters;
        }

        protected override void Configure(IInterfaceTypeDescriptor<TBase> descriptor)
        {
            descriptor.Name(_parameters.InterfaceTypeName);
        }
    }
}