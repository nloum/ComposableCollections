using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DebuggableSourceGenerators
{
    public class CodeIndexingService : ITypeRegistryService
    {
        Func<SyntaxTree, SemanticModel> GetSemanticModel;

        Dictionary<TypeIdentifier, IType> _types = new Dictionary<TypeIdentifier, IType>();

        ISyntaxService SyntaxService;
        ISymbolService SymbolService;

        private int GetHashCode(string namespaceName, string typeName, int arity)
        {
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                typeName = $"{namespaceName}.{typeName}";
            }
            
            return HashCode.Combine(typeName, arity);
        }
        
        public CodeIndexingService()
        {
            SymbolService = new SymbolService(this);
            SyntaxService = new SyntaxService(this, SymbolService, syntaxTree => GetSemanticModel(syntaxTree));
        }

        public Lazy<IType> GetType(string typeName, int arity = 0)
        {
            var typeIdentifier = new TypeIdentifier(typeName, arity);
            return new Lazy<IType>(() => _types[typeIdentifier]);
        }

        public IType TryAddType(string typeName, int arity, Func<IType> type)
        {
            var typeIdentifier = new TypeIdentifier(typeName, arity);
            if (!_types.ContainsKey(typeIdentifier))
            {
                _types.Add(typeIdentifier, type());
            }

            return _types[typeIdentifier];
        }

        public Lazy<IType> GetType(string namespaceName, string typeName, int arity = 0)
        {
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                typeName = $"{namespaceName}.{typeName}";
            }

            return GetType(typeName, arity);
        }

        public IType TryAddType(string namespaceName, string typeName, int arity, Func<IType> type)
        {
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                typeName = $"{namespaceName}.{typeName}";
            }

            return TryAddType(typeName, arity, type);
        }

        public void Add(Compilation compilation)
        {
            GetSemanticModel = syntaxTree => compilation.GetSemanticModel(syntaxTree);
            
            var interfaceDeclarationSyntaxes = new Dictionary<TypeIdentifier, List<InterfaceDeclarationSyntax>>();
            var classDeclarationSyntaxes = new Dictionary<TypeIdentifier, List<ClassDeclarationSyntax>>();
            
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                Utilities.TraverseTree(syntaxTree.GetRoot(), node =>
                {
                    if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax)
                    {
                        var typeIdentifier = new TypeIdentifier(Utilities.GetNamespace(interfaceDeclarationSyntax),
                            interfaceDeclarationSyntax.Identifier.Text,
                            interfaceDeclarationSyntax.TypeParameterList == null
                                ? 0
                                : interfaceDeclarationSyntax.TypeParameterList.Parameters.Count);
                        
                        if (!interfaceDeclarationSyntaxes.TryGetValue(typeIdentifier, out var items))
                        {
                            items = new List<InterfaceDeclarationSyntax>();
                            interfaceDeclarationSyntaxes[typeIdentifier] = items;
                        }
                        items.Add(interfaceDeclarationSyntax);
                    }
                    else if (node is ClassDeclarationSyntax classDeclarationSyntax)
                    {
                        var typeIdentifier = new TypeIdentifier(
                            Utilities.GetNamespace(classDeclarationSyntax),
                            classDeclarationSyntax.Identifier.Text,
                            classDeclarationSyntax.TypeParameterList == null
                                ? 0
                                : classDeclarationSyntax.TypeParameterList.Parameters.Count);
                        if (!classDeclarationSyntaxes.TryGetValue(typeIdentifier, out var items))
                        {
                            items = new List<ClassDeclarationSyntax>();
                            classDeclarationSyntaxes[typeIdentifier] = items;
                        }
                        items.Add(classDeclarationSyntax);
                    }
                });
            }

            foreach (var list in interfaceDeclarationSyntaxes)
            {
                var iface = new SyntaxInterface(this, SyntaxService);
                iface.Initialize(list.Value.ToArray());
                _types[list.Key] = iface;
            }
            
            foreach (var list in classDeclarationSyntaxes)
            {
                var clazz = new SyntaxClass(this, SyntaxService);
                clazz.Initialize(list.Value.ToArray());
                _types[list.Key] = clazz;
            }
        }
    }
}