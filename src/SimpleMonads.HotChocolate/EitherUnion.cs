using HotChocolate.Language;
using HotChocolate.Types;

namespace SimpleMonads.HotChocolate
{
    public class EitherUnion<TEither> : UnionType<TEither>
    {
        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Name(typeof(TEither).Name);

            var metadata = typeof(TEither).GetEitherMetadata();

            foreach (var type in metadata.Types)
            {
                descriptor.Type(new NamedTypeNode(type.Name));
            }
        }
    }
}