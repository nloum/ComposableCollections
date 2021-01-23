using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace DebuggableSourceGenerators.NonLoadedAssembly
{
     public interface INonLoadedAssemblyService
    {
        void AddAllTypes(string assemblyFilePath);
    }

    public class NonLoadedAssemblyService : INonLoadedAssemblyService
    {
        private ITypeRegistryService TypeRegistryService;

        public NonLoadedAssemblyService(ITypeRegistryService typeRegistryService)
        {
            TypeRegistryService = typeRegistryService;
        }

        public void AddAllTypes(string assemblyFilePath)
        {
            using var fs = new FileStream(assemblyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var peReader = new PEReader(fs);

            MetadataReader mr = peReader.GetMetadataReader();

            foreach (TypeDefinitionHandle tdefh in mr.TypeDefinitions)
            {
                TypeDefinition tdef = mr.GetTypeDefinition(tdefh);

                var identifier = new TypeIdentifier(mr.GetString(tdef.Namespace), mr.GetString(tdef.Name), 0);
                
                TypeRegistryService.TryAddType(identifier, () =>
                {
                    var result = new NonLoadedAssemblyClass();
                    result.Initialize(identifier, mr, tdef);
                    return result;
                });
            }
        }
    }

    public class NonLoadedAssemblyClass : IClass
    {
        public void Initialize(TypeIdentifier identifier, MetadataReader metadataReader, TypeDefinition typeDefinition)
        {
            Identifier = identifier;

            var properties = typeDefinition.GetProperties().ToImmutableList();
            foreach (var property in properties)
            {
                var propertyDefinition = metadataReader.GetPropertyDefinition(property);
                var propertyName = metadataReader.GetString(propertyDefinition.Name);
                
            }
        }

        public TypeIdentifier Identifier { get; private set; }
        public IReadOnlyList<TypeParameter> TypeParameters { get; }
        public IReadOnlyList<Property> Properties { get; }
        public IReadOnlyList<Method> Methods { get; }
        public IReadOnlyList<Indexer> Indexers { get; }
    }
}