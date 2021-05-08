using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DebuggableSourceGenerators.Reflection
{
    public interface IReflectionService
    {
        void AddAllTypes(Assembly assembly);
        Lazy<IType> Convert<T>();
        Lazy<IType> Convert(Type type);
    }

    public class ReflectionService : IReflectionService
    {
        private ITypeRegistryService TypeRegistryService;

        public ReflectionService(ITypeRegistryService typeRegistryService)
        {
            TypeRegistryService = typeRegistryService;
        }

        public Lazy<IType> Convert<T>()
        {
            return Convert(typeof(T));
        }

        public void AddAllTypes(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                Convert(type);
            }
        }
        
        private TypeIdentifier GetTypeIdentifier(Type type)
        {
            return new TypeIdentifier(type.Namespace, type.Name, type.GenericTypeArguments.Length);
        }
        
        public Lazy<IType> Convert(Type type)
        {
            if (type.IsConstructedGenericType)
            {
                return new Lazy<IType>(() =>
                {
                    var unboundType = type.GetGenericTypeDefinition();
                    return new ReflectionBoundGenericClass(GetTypeIdentifier(type), new Lazy<IStructuredType>(() => (IStructuredType) Convert(unboundType).Value),
                        type.GenericTypeArguments.Select(Convert).ToImmutableList());
                });
            }
            else
            {
                return new Lazy<IType>(() =>
                {
                    var typeIdentifier = GetTypeIdentifier(type);
                    return TypeRegistryService.TryAddType(typeIdentifier, () =>
                    {
                        var clazz = new ReflectionClass();
                        var unboundType = type.GetGenericTypeDefinition();
                        clazz.Initialize(GetTypeIdentifier(unboundType), unboundType);
                        return clazz;
                    });
                });
            }
        }
    }

    public class ReflectionBoundGenericClass : IBoundGenericStructuredType
    {
        private Lazy<IStructuredType> _unboundForm;
        private IReadOnlyList<Lazy<IType>> _typeParameterValues;

        public ReflectionBoundGenericClass(TypeIdentifier identifier, Lazy<IStructuredType> unboundForm, IReadOnlyList<Lazy<IType>> typeParameterValues)
        {
            Identifier = identifier;
            _unboundForm = unboundForm;
            _typeParameterValues = typeParameterValues;
        }

        public TypeIdentifier Identifier { get; }
        public IStructuredType UnboundForm => _unboundForm.Value;
        public IReadOnlyList<IType> TypeParameterValues => _typeParameterValues.Select(x => x.Value).ToImmutableList();
    }
    
    public class ReflectionClass : IClass
    {
        public void Initialize(TypeIdentifier identifier, Type type)
        {
            Identifier = identifier;
            
            
        }

        public TypeIdentifier Identifier { get; private set; }
        public IReadOnlyList<TypeParameter> TypeParameters { get; }
        public IReadOnlyList<Property> Properties { get; }
        public IReadOnlyList<Method> Methods { get; }
        public IReadOnlyList<Indexer> Indexers { get; }
    }
}