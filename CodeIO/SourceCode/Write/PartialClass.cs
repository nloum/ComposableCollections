using System.Collections.Immutable;
using System.IO;
using CodeIO.LoadedTypes.Read;
using CodeIO.LoadedTypes.Write;

namespace CodeIO.SourceCode.Write
{
    public class PartialClass : IPartialType
    {
        public TypeIdentifier Identifier { get; init; }
        public ImmutableList<PropertySourceCodeWriter> Properties { get; init; }

        public void Generate(SourceCodeWriter sourceCodeWriter)
        {
            sourceCodeWriter.WriteLine($"namespace {Identifier.Namespace} ");
            using(sourceCodeWriter.Braces()) {
                sourceCodeWriter.WriteLine($"public partial class {Identifier.Name} ");
                using(sourceCodeWriter.Braces()) {
                    foreach (var property in Properties)
                    {
                        property.WriteImplementationInClass(sourceCodeWriter);
                    }
                }
            }
        }

        public IPartialType RemoveDuplicates()
        {
            return new PartialClass()
            {
                Identifier = Identifier,
                Properties = Properties.RemoveDuplicates()
            };
        }
    }
}