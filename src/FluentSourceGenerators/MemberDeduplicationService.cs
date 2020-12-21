using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace FluentSourceGenerators
{
    public class MemberDeduplicationService
    {
        public IEnumerable<DeduplicatedMember<ISymbol>> GetDeduplicatedMembers(params INamedTypeSymbol[] interfaces)
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

        public IEnumerable<DeduplicatedMember<ISymbol>> GetDeduplicatedMembers(IEnumerable<INamedTypeSymbol> interfaces)
        {
            var members = interfaces
                .SelectMany(iface => Utilities.GetBaseInterfaces(iface))
                .SelectMany(iface => iface.GetMembers());

            return DeduplicateMembers(members, x => x);
        }

        public IEnumerable<DeduplicatedMember<T>> DeduplicateMembers<T>(IEnumerable<T> members, Func<T, ISymbol> symbol)
        {
            var indexers = new Dictionary<string, List<Tuple<T, IPropertySymbol>>>();
            var properties = new Dictionary<string, List<Tuple<T, IPropertySymbol>>>();
            var methods = new Dictionary<string, List<Tuple<T, IMethodSymbol>>>();

            foreach (var member in members)
            {
                var memberSymbol = symbol(member);
                var key = GetExplicitImplementationProfile(memberSymbol);
                if (memberSymbol is IPropertySymbol propertySymbol)
                {
                    if (propertySymbol.IsIndexer)
                    {
                        if (!indexers.ContainsKey(key))
                        {
                            indexers[key] = new List<Tuple<T, IPropertySymbol>>();
                        }

                        indexers[key].Add(Tuple.Create(member, propertySymbol));
                    }
                    else
                    {
                        if (!properties.ContainsKey(key))
                        {
                            properties[key] = new List<Tuple<T, IPropertySymbol>>();
                        }

                        properties[key].Add(Tuple.Create(member, propertySymbol));
                    }
                }
                else if (memberSymbol is IMethodSymbol methodSymbol)
                {
                    if (!methods.ContainsKey(key))
                    {
                        methods[key] = new List<Tuple<T, IMethodSymbol>>();
                    }

                    methods[key].Add(Tuple.Create(member, methodSymbol));
                }
            }

            var results = new List<DeduplicatedMember<T>>();

            foreach (var kvp in indexers)
            {
                var indexerGroup = kvp.Value;
                Tuple<T, IPropertySymbol> readWriteSymbol = null;
                foreach (var indexer in indexerGroup)
                {
                    if (!indexer.Item2.IsReadOnly)
                    {
                        readWriteSymbol = indexer;
                    }
                }

                if (readWriteSymbol != null)
                {
                    results.Add(new DeduplicatedMember<T>(kvp.Key, readWriteSymbol.Item1, false, indexerGroup.Select(duplicate => duplicate.Item1)));
                }
                else
                {
                    results.Add(new DeduplicatedMember<T>(kvp.Key, kvp.Value.First().Item1, false, indexerGroup.Select(duplicate => duplicate.Item1)));
                }
            }

            foreach (var kvp in properties)
            {
                var propertyGroup = kvp.Value;
                if (propertyGroup.Count == 1)
                {
                    results.Add(new DeduplicatedMember<T>(kvp.Key, propertyGroup.First().Item1, false, propertyGroup.Select(tuple => tuple.Item1)));
                }
                else
                {
                    var propertiesGroupedByType = propertyGroup.GroupBy(property => property.Item2.Type)
                        .Select(group => new {@group.Key, Values = @group.ToImmutableList()}).ToImmutableList();
                    if (propertiesGroupedByType.Count == 1)
                    {
                        results.Add(new DeduplicatedMember<T>(kvp.Key, propertiesGroupedByType[0].Values[0].Item1, false,
                            propertyGroup.Select(tuple => tuple.Item1)));
                    }
                    else
                    {
                        results.AddRange(propertiesGroupedByType.Select(property =>
                            new DeduplicatedMember<T>(kvp.Key, property.Values.First().Item1, true, propertyGroup.Select(tuple => tuple.Item1))));
                    }
                }
            }

            foreach (var kvp in methods)
            {
                var methodGroup = kvp.Value;
                if (methodGroup.Count == 1)
                {
                    results.Add(new DeduplicatedMember<T>(kvp.Key, methodGroup.First().Item1, false, methodGroup.Select(tuple => tuple.Item1)));
                }
                else
                {
                    var propertiesGroupedByType = methodGroup.GroupBy(property => property.Item2.ReturnType)
                        .Select(group => new {@group.Key, Values = @group.ToImmutableList()}).ToImmutableList();
                    if (propertiesGroupedByType.Count == 1)
                    {
                        results.Add(new DeduplicatedMember<T>(kvp.Key, propertiesGroupedByType[0].Values[0].Item1, false, methodGroup.Select(tuple => tuple.Item1)));
                    }
                    else
                    {
                        results.AddRange(methodGroup
                            .GroupBy(methodSymbol => this.GetExplicitImplementationProfile(methodSymbol.Item2))
                            .Select(method => new DeduplicatedMember<T>(kvp.Key, method.First().Item1, true, methodGroup.Select(tuple => tuple.Item1))));
                    }
                }
            }

            return results;
        }
    }
}