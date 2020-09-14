using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class DuplicateMembersService
    {
        private ImmutableDictionary<Tuple<string, ImmutableArray<IParameterSymbol>>, ImmutableList<Tuple<string, ImmutableArray<IParameterSymbol>>>> _duplicateMembers;

        private ImmutableDictionary<string, ImmutableHashSet<IPropertySymbol>> _duplicateIndexers;
        private ImmutableDictionary<string, ImmutableHashSet<IPropertySymbol>> _duplicateProperties;
        private ImmutableDictionary<string, ImmutableHashSet<IMethodSymbol>> _duplicateMethods;
        
        public DuplicateMembersService(InterfaceDeclarationSyntax interfaceDeclaration, SemanticModel semanticModel)
        {
            var membersPerInterfaceToImplement = Utilities.GetMembers(interfaceDeclaration, semanticModel);

            _duplicateIndexers = membersPerInterfaceToImplement.SelectMany(memberToImplement =>
            {
                return memberToImplement.Value.OfType<IPropertySymbol>().Where(x => x.IsIndexer);
            }).GroupBy(x => GetIndexerKey(x))
                .ToImmutableDictionary(x => x.Key, x => x.ToImmutableHashSet());

            _duplicateProperties = membersPerInterfaceToImplement.SelectMany(memberToImplement =>
                {
                    return memberToImplement.Value.OfType<IPropertySymbol>().Where(x => !x.IsIndexer);
                }).GroupBy(x => GetPropertyKey(x))
                .ToImmutableDictionary(x => x.Key, x => x.ToImmutableHashSet());

            _duplicateMethods = membersPerInterfaceToImplement.SelectMany(memberToImplement =>
                {
                    return memberToImplement.Value.OfType<IMethodSymbol>();
                }).GroupBy(x => GetMethodKey(x))
                .ToImmutableDictionary(x => x.Key, x => x.ToImmutableHashSet());
        }

        public bool IsDuplicate(IPropertySymbol propertySymbol)
        {
            if (propertySymbol.IsIndexer)
            {
                return _duplicateIndexers.ContainsKey(GetIndexerKey(propertySymbol));
            }

            return _duplicateProperties.ContainsKey(GetPropertyKey(propertySymbol));
        }
        
        public bool IsDuplicate(IMethodSymbol methodSymbol)
        {
            return _duplicateMethods.ContainsKey(GetMethodKey(methodSymbol));
        }

        private string GetIndexerKey(IPropertySymbol propertySymbol)
        {
            return string.Join(", ", propertySymbol.Parameters);
        }

        private string GetPropertyKey(IPropertySymbol propertySymbol)
        {
            return propertySymbol.Name;
        }

        private string GetMethodKey(IMethodSymbol methodSymbol)
        {
            return
                $"{methodSymbol.Name}({string.Join(", ", methodSymbol.Parameters.Select(parameter => parameter.Type.ToString()))})";
        }

        //
        // public bool IsDuplicate(IMethodSymbol method)
        // {
        //     
        // }
    }
}