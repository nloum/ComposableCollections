using HotChocolate.Language;
using HotChocolate.Types;

namespace SimpleMonads.HotChocolate
{
    public class EitherUnion<TEither> : UnionType<TEither>
    {
        private readonly EitherObjectTypeParameters _parameters;

        public EitherUnion(EitherObjectTypeParameters parameters)
        {
            _parameters = parameters;
        }

        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Name(_parameters.UnionTypeName);

            var metadata = typeof(TEither).GetEitherMetadata();

            foreach (var type in metadata.Types)
            {
                descriptor.Type(new NamedTypeNode(type.Name));
            }
        }
    }
}