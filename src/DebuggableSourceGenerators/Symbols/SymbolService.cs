using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FindSymbols;

namespace DebuggableSourceGenerators
{
    public class SymbolService : ISymbolService
    {
        ITypeRegistryService TypeRegistryService;
        private HashSet<IAssemblySymbol> _assemblies = new();

        public SymbolService(ITypeRegistryService typeRegistryService)
        {
            TypeRegistryService = typeRegistryService;
        }

        public void LoadTypesFromAssemblies()
        {
            foreach (var assembly in _assemblies)
            {
                foreach (var typeName in assembly.TypeNames)
                {
                    var symbol = assembly.GetTypeByMetadataName(typeName);
                }
            }
        }
        
        public string Convert(INamespaceSymbol namespaceSymbol)
        {
            var sb = new StringBuilder();
            while (namespaceSymbol != null)
            {
                if (sb.Length != 0)
                {
                    sb.Append(".");
                }

                sb.Append(namespaceSymbol.Name);
                namespaceSymbol = namespaceSymbol.ContainingNamespace;
            }

            return sb.ToString();
        }

        private void TryAddAssembly(IAssemblySymbol assembly)
        {
            if (!_assemblies.Contains(assembly))
            {
                _assemblies.Add(assembly);
            }
        }

        public Lazy<IType> GetType(ITypeSymbol symbol)
        {
            TryAddAssembly(symbol.ContainingAssembly);
            
            if (symbol is INamedTypeSymbol namedTypeSymbol)
            {
                return GetType(namedTypeSymbol);
            }
            else if (symbol is ITypeParameterSymbol typeParameterSymbol)
            {
                return new Lazy<IType>(new TypeParameter(GetTypeIdentifier(typeParameterSymbol), Convert(typeParameterSymbol.Variance)));
            }

            throw new NotImplementedException();
        }

        private VarianceMode Convert(VarianceKind varianceKind)
        {
            if (varianceKind == VarianceKind.In)
            {
                return VarianceMode.In;
            }

            if (varianceKind == VarianceKind.Out)
            {
                return VarianceMode.Out;
            }

            return VarianceMode.None;
        }

        public TypeIdentifier GetTypeIdentifier(ITypeSymbol namedTypeSymbol)
        {
            return new TypeIdentifier(Convert(namedTypeSymbol.ContainingNamespace), namedTypeSymbol.Name,
                0);
        }

        public TypeIdentifier GetTypeIdentifier(INamedTypeSymbol namedTypeSymbol)
        {
            return new TypeIdentifier(Convert(namedTypeSymbol.ContainingNamespace), namedTypeSymbol.Name,
                namedTypeSymbol.Arity);
        }
        
        public Lazy<IType> GetType(INamedTypeSymbol namedTypeSymbol)
        {
            TryAddAssembly(namedTypeSymbol.ContainingAssembly);
            
            if (namedTypeSymbol.IsGenericType && !namedTypeSymbol.IsDefinition)
            {
                if (namedTypeSymbol.TypeKind == TypeKind.Interface)
                {
                    return new Lazy<IType>(() =>
                    {
                        return new SymbolBoundGenericInterface(GetTypeIdentifier(namedTypeSymbol),
                            (IStructuredType) GetType(namedTypeSymbol.OriginalDefinition),
                            namedTypeSymbol.TypeArguments.Select(typeArg =>
                                GetType(typeArg)).Select(l => l.Value).ToList());
                    });
                }
                if (namedTypeSymbol.TypeKind == TypeKind.Class)
                {
                    return new Lazy<IType>(() =>
                    {
                        return new SymbolBoundGenericClass(GetTypeIdentifier(namedTypeSymbol),
                            (IStructuredType) GetType(namedTypeSymbol.OriginalDefinition),
                            namedTypeSymbol.TypeArguments.Select(typeArg =>
                                GetType(typeArg)).Select(l => l.Value).ToList());
                    });
                } 
            }

            return new Lazy<IType>(() =>
            {
                return TypeRegistryService.TryAddType(GetTypeIdentifier(namedTypeSymbol),
                    () =>
                    {
                        if (namedTypeSymbol.TypeKind == TypeKind.Interface)
                        {
                            var result = new SymbolInterface(TypeRegistryService, this);
                            result.Initialize(GetTypeIdentifier(namedTypeSymbol), namedTypeSymbol);
                            return result;
                        }

                        if (namedTypeSymbol.TypeKind == TypeKind.Class)
                        {
                            var result = new SymbolClass(TypeRegistryService, this);
                            result.Initialize(GetTypeIdentifier(namedTypeSymbol), namedTypeSymbol);
                            return result;
                        }

                        if (namedTypeSymbol.TypeKind == TypeKind.Enum)
                        {
                            var result = new SymbolEnum(GetTypeIdentifier(namedTypeSymbol), namedTypeSymbol.MemberNames);
                            return result;
                        }

                        return new SymbolPrimitiveType(GetTypeIdentifier(namedTypeSymbol));
                    });
            });
        }

        private IEnumerable<Parameter> Convert(IEnumerable<IParameterSymbol> parameterSymbols)
        {
            foreach (var parameterSymbol in parameterSymbols)
            {
                yield return new Parameter(parameterSymbol.Name, GetType(parameterSymbol.Type), ParameterMode.In);
            }
        }
        
        public void ConvertMembers(INamedTypeSymbol symbol, out IReadOnlyList<Property> properties, out IReadOnlyList<Indexer> indexers,
            out IReadOnlyList<Method> methods)
        {
            var tmpProperties = new List<Property>();
            var tmpIndexers = new List<Indexer>();
            var tmpMethods = new List<Method>();
            
            foreach (var member in symbol.GetMembers())
            {
                if (member is IPropertySymbol propertySymbol)
                {
                    if (propertySymbol.IsIndexer)
                    {
                        tmpIndexers.Add(new Indexer(GetType(propertySymbol.Type), Convert(propertySymbol.Parameters).ToImmutableList()));
                    }
                    else
                    {
                        tmpProperties.Add(new Property(propertySymbol.Name, GetType(propertySymbol.Type)));
                    }
                }
                else if (member is IMethodSymbol methodSymbol)
                {
                    tmpMethods.Add(new Method(GetType(methodSymbol.ReturnType), methodSymbol.Name, Convert(methodSymbol.Parameters).ToImmutableList()));
                }
            }
            
            properties = tmpProperties;
            indexers = tmpIndexers;
            methods = tmpMethods;
        }
    }
}