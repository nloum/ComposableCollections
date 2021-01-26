using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using DebuggableSourceGenerators;
using DebuggableSourceGenerators.NonLoadedAssembly;

namespace DebuggableSourceGenerators.NonLoadedAssembly
{
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

                var name = mr.GetString(tdef.Name);
                var indexOfAritySeparator = name.LastIndexOf('`');
                var arity = 0;
                if (indexOfAritySeparator != -1)
                {
                    var split = name.Split('`');
                    name = split[0];
                    arity = int.Parse(split[1]);
                }
                
                var identifier = new TypeIdentifier(mr.GetString(tdef.Namespace), name, arity);

                if (identifier.FullName == "<Module>" || identifier.FullName.Contains("__AnonymousType"))
                {
                    continue;
                }
                
                TypeRegistryService.TryAddType(identifier, () =>
                {
                    var result = new NonLoadedAssemblyClass(this);
                    result.Initialize(identifier, mr, tdef);
                    return result;
                });
            }
        }
    }
}