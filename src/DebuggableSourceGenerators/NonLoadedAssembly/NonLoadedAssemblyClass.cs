using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;

namespace DebuggableSourceGenerators.NonLoadedAssembly
{
    public class NonLoadedAssemblyClass : IClass
    {
        private INonLoadedAssemblyService NonLoadedAssemblyService;

        public NonLoadedAssemblyClass(INonLoadedAssemblyService nonLoadedAssemblyService)
        {
            NonLoadedAssemblyService = nonLoadedAssemblyService;
        }

        public void Initialize(TypeIdentifier identifier, MetadataReader metadataReader, TypeDefinition typeDefinition)
        {
            Identifier = identifier;

            var properties = typeDefinition.GetProperties().ToImmutableList();
            foreach (var property in properties)
            {
                var propertyDefinition = metadataReader.GetPropertyDefinition(property);
                var propertyName = metadataReader.GetString(propertyDefinition.Name);
                using var textWriter = new StringWriter();
                var signature = propertyDefinition.DecodeSignature(
                    new SignatureVisualizer(new MetadataVisualizer(metadataReader, textWriter)), null);
                
                
            }
        }

        public TypeIdentifier Identifier { get; private set; }
        public IReadOnlyList<TypeParameter> TypeParameters { get; }
        public IReadOnlyList<Property> Properties { get; }
        public IReadOnlyList<Method> Methods { get; }
        public IReadOnlyList<Indexer> Indexers { get; }
    }
}