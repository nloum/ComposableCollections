using System.Collections.Immutable;
using System.IO;
using CodeIO.LoadedTypes.Read;

namespace CodeIO.SourceCode.Write
{
    public class PartialInterface : IPartialType
    {
        public TypeIdentifier Identifier { get; init; }
        public ImmutableList<PropertySourceCodeWriter> Properties { get; init; }
        
        public void Generate(SourceCodeWriter sourceCodeWriter)
        {
            sourceCodeWriter.WriteLine($"namespace {Identifier.Namespace} ");
            using(sourceCodeWriter.Braces()) {
                sourceCodeWriter.WriteLine($"public partial interface {Identifier.Name} ");
                using(sourceCodeWriter.Braces()) {
                    foreach (var property in Properties)
                    {
                        property.WriteDeclarationInInterface(sourceCodeWriter);
                    }
                }
            }
        }
        
        public IPartialType RemoveDuplicates()
        {
            return new PartialInterface()
            {
                Identifier = Identifier,
                Properties = Properties.RemoveDuplicates()
            };
        }
    }
}