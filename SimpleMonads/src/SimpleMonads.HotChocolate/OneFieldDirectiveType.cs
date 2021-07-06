using HotChocolate.Types;

namespace SimpleMonads.HotChocolate
{
    public class OneFieldDirectiveType : DirectiveType<OneFieldDirective>
    {
        protected override void Configure(IDirectiveTypeDescriptor<OneFieldDirective> descriptor)
        {
            descriptor.Name("oneField");
            descriptor.Location(DirectiveLocation.InputObject);
        }
    }
}