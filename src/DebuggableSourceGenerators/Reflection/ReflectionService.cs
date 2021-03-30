using System;
using System.Collections.Generic;
using System.Reflection;

namespace DebuggableSourceGenerators.Reflection
{
    public class ReflectionService : IReflectionService
    {
        public IType AddType(TypeInfo type)
        {
            
        }
    }

    public class ReflectionClass : IClass
    {
        public ReflectionClass(IReflectionService reflectionService, TypeInfo type)
        {
            var typeParameters = new List<TypeParameter>();
            var properties = new List<Property>();
            foreach (var property in type.DeclaredProperties)
            {
                properties.Add(new Property(property.Name, new Lazy<IType>(() => reflectionService.AddType(property.PropertyType.GetTypeInfo()))));
            }
        }

        public TypeIdentifier Identifier { get; }
        public IReadOnlyList<TypeParameter> TypeParameters { get; }
        public IReadOnlyList<Property> Properties { get; }
        public IReadOnlyList<Method> Methods { get; }
        public IReadOnlyList<Indexer> Indexers { get; }
    }
}