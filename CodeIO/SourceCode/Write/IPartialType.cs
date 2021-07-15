using System.Collections.Immutable;

namespace CodeIO.SourceCode.Write
{
    public interface IPartialType : ISourceCodeGenerator
    {
        public TypeIdentifier Identifier { get; }
        public ImmutableList<PropertySourceCodeWriter> Properties { get; }
        IPartialType RemoveDuplicates();
    }
}