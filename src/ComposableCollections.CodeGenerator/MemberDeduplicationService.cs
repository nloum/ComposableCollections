using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class MemberDeduplicationService
    {
        public IEnumerable<DeduplicatedMember> GetDeduplicatedMembers(params INamedTypeSymbol[] interfaces)
        {
            return GetDeduplicatedMembers(interfaces.AsEnumerable());
        }

        public string GetExplicitImplementationProfile(ISymbol symbol)
        {
            if (symbol is IPropertySymbol propertySymbol)
            {
                if (propertySymbol.IsIndexer)
                {
                    return "this[" + string.Join(", ", propertySymbol.Parameters.Select(parameter => parameter.Type)) + "]";                    
                }
                else
                {
                    return propertySymbol.Name;
                }
            }
            else if (symbol is IMethodSymbol methodSymbol)
            {
                var parameters = string.Join(", ", methodSymbol.Parameters);
                return $"arity={methodSymbol.Arity} {methodSymbol.Name}({parameters});";
            }

            return symbol.Name;
        }
        
        public string GetImplementationProfile(ISymbol symbol)
        {
            if (symbol is IPropertySymbol propertySymbol)
            {
                if (propertySymbol.IsIndexer)
                {
                    return propertySymbol.Type + "this[" + string.Join(", ", propertySymbol.Parameters.Select(parameter => parameter.Type)) + "]";                    
                }
                else
                {
                    return propertySymbol.Type + " " + propertySymbol.Name;
                }
            }
            else if (symbol is IMethodSymbol methodSymbol)
            {
                var parameters = string.Join(", ", methodSymbol.Parameters);
                return $"{methodSymbol.ReturnType} arity={methodSymbol.Arity} {methodSymbol.Name}({parameters});";
            }

            return symbol.Name;
        }

        public IEnumerable<DeduplicatedMember> GetDeduplicatedMembers(IEnumerable<INamedTypeSymbol> interfaces)
        {
            var indexers = new Dictionary<string, List<IPropertySymbol>>();
            var properties = new Dictionary<string, List<IPropertySymbol>>();
            var methods = new Dictionary<string, List<IMethodSymbol>>();

            foreach (var iface in interfaces.SelectMany(iface => Utilities.GetBaseInterfaces(iface)))
            {
                foreach (var member in iface.GetMembers())
                {
                    var key = GetExplicitImplementationProfile(member);
                    if (member is IPropertySymbol propertySymbol)
                    {
                        if (propertySymbol.IsIndexer)
                        {
                            if (!indexers.ContainsKey(key))
                            {
                                indexers[key] = new List<IPropertySymbol>();
                            }
                            indexers[key].Add(propertySymbol);
                        }
                        else
                        {
                            if (!properties.ContainsKey(key))
                            {
                                properties[key] = new List<IPropertySymbol>();
                            }
                            properties[key].Add(propertySymbol);
                        }
                    }
                    else if (member is IMethodSymbol methodSymbol)
                    {
                        if (!methods.ContainsKey(key))
                        {
                            methods[key] = new List<IMethodSymbol>();
                        }
                        methods[key].Add(methodSymbol);
                    }
                }
            }

            var results = new List<DeduplicatedMember>();
            
            foreach (var kvp in indexers)
            {
                var indexerGroup = kvp.Value;
                IPropertySymbol readWriteSymbol = null;
                foreach (var indexer in indexerGroup)
                {
                    if (!indexer.IsReadOnly)
                    {
                        readWriteSymbol = indexer;
                    }
                }

                if (readWriteSymbol != null)
                {
                    results.Add(new DeduplicatedMember(kvp.Key, readWriteSymbol, false, indexerGroup));
                }
                else
                {
                    results.Add(new DeduplicatedMember(kvp.Key, kvp.Value.First(), false, indexerGroup));
                }
            }

            foreach (var kvp in properties)
            {
                var propertyGroup = kvp.Value;
                if (propertyGroup.Count == 1)
                {
                    results.Add(new DeduplicatedMember(kvp.Key, propertyGroup.First(), false, propertyGroup));
                }
                else
                {
                    var propertiesGroupedByType = propertyGroup.GroupBy(property => property.Type).Select(group => new { group.Key, Values = group.ToImmutableList() }).ToImmutableList();
                    if (propertiesGroupedByType.Count == 1)
                    {
                        results.Add(new DeduplicatedMember(kvp.Key, propertiesGroupedByType[0].Values[0], false, propertyGroup));
                    }
                    else
                    {
                        results.AddRange(propertiesGroupedByType.Select(property => new DeduplicatedMember(kvp.Key, property.Values.First(), true, propertyGroup)));
                    }
                }
            }

            foreach (var kvp in methods)
            {
                var methodGroup = kvp.Value;
                if (methodGroup.Count == 1)
                {
                    results.Add(new DeduplicatedMember(kvp.Key, methodGroup.First(), false, methodGroup));
                }
                else
                {
                    var propertiesGroupedByType = methodGroup.GroupBy(property => property.ReturnType).Select(group => new { group.Key, Values = group.ToImmutableList() }).ToImmutableList();
                    if (propertiesGroupedByType.Count == 1)
                    {
                        results.Add(new DeduplicatedMember(kvp.Key, propertiesGroupedByType[0].Values[0], false, methodGroup));
                    }
                    else
                    {
                        results.AddRange(methodGroup.Select(method => new DeduplicatedMember(kvp.Key, method, true, methodGroup)));
                    }
                }
            }

            return results;
        }
    }
}