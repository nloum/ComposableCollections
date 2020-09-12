using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class InterfaceCombinerSettings
    {
        [XmlAttribute("Namespace")]
        public string Namespace { get; set; }
        [XmlArray("InterfaceNameModifiers")]
        [XmlArrayItem("InterfaceNameModifier", typeof(InterfaceNameModifier))]
        public List<InterfaceNameModifier> InterfaceNameModifiers { get; set; }
        [XmlArray("InterfaceNameBuilders")]
        [XmlArrayItem("InterfaceNameBuilder", typeof(InterfaceNameBuilder))]
        public List<InterfaceNameBuilder> InterfaceNameBuilders { get; set; }
        [XmlArray("InterfaceNameBlacklist")]
        [XmlArrayItem("Regex", typeof(string))]
        public List<string> InterfaceNameBlacklistRegexes { get; set; }
    }

    public class InterfaceNameBuilder
    {
        [XmlAttribute("SearchRegex")]
        public string Search { get; set; }
        [XmlAttribute("ReplaceRegex")]
        public string Replacement { get; set; }
    }

    public class InterfaceNameModifier : IEnumerable<string>
    {
        [XmlArray]
        [XmlArrayItem("Value")]
        public List<string> Values { get; set; } = new List<string>();

        public void Add(string value)
        {
            Values.Add(value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Values.GetEnumerator();
        }
    }

    public class InterfaceCombiner
    {
        private readonly InterfaceCombinerSettings _settings;
        
        public InterfaceCombiner(InterfaceCombinerSettings settings)
        {
            _settings = settings;
        }

        private void TraverseTree(SyntaxNode syntaxNode, Action<SyntaxNode> visit)
        {
            visit(syntaxNode);
            foreach (var child in syntaxNode.ChildNodes())
            {
                TraverseTree(child, visit);
            }
        }
        
        private enum TypeParameterVariance
        {
            Out, In, None
        }

        private class TypeParameter
        {
            public TypeParameter(TypeParameterVariance variance, int index)
            {
                Variance = variance;
                Index = index;
            }

            public TypeParameterVariance Variance { get; }
            public int Index { get; }
        }
        
        public ImmutableDictionary<string, string> Generate(IEnumerable<SyntaxTree> syntaxTrees)
        {
            var interfaceDeclarations = new Dictionary<string, InterfaceDeclarationSyntax>();
            
            foreach (var syntaxTree in syntaxTrees)
            {
                TraverseTree(syntaxTree.GetRoot(), node =>
                {
                    if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax)
                    {
                        interfaceDeclarations.Add(interfaceDeclarationSyntax.Identifier.Text, interfaceDeclarationSyntax);
                    }
                });
            }
            
            var combinations = Utilities.CalcCombinationsOfOneFromEach(_settings.InterfaceNameModifiers);
            var interfaces = new Dictionary<string, ImmutableDictionary<string, TypeParameter>>();

            var results = new Dictionary<string, string>();
            
            foreach (var combination in combinations)
            {
                if (!GetName(combination, out var name)) continue;

                if (interfaceDeclarations.TryGetValue(name, out var value))
                {
                    var genericParams = new Dictionary<string, TypeParameter>();

                    var index = 0;
                    foreach (var param in value.TypeParameterList.Parameters)
                    {
                        var variance = TypeParameterVariance.None;
                        if (param.AttributeLists.Any(SyntaxKind.OutKeyword))
                        {
                            variance = TypeParameterVariance.Out;
                        }

                        if (param.AttributeLists.Any(SyntaxKind.InKeyword))
                        {
                            variance = TypeParameterVariance.In;
                        }

                        genericParams.Add(param.Identifier.Text, new TypeParameter(variance, index));
                        index++;
                    }

                    interfaces.Add(name, genericParams.ToImmutableDictionary());
                }
                else
                {
                    var baseInterfaces = new List<string>();
                    for (var i = 0; i < combination.Count; i++)
                    {
                        if (combination[i] == _settings.InterfaceNameModifiers[i].Values[0])
                        {
                            continue;
                        }

                        var subCombination = combination.ToList();
                        subCombination[i] = _settings.InterfaceNameModifiers[i].Values[0];
                        
                        if (!GetName(subCombination, out var baseName)) continue;
                        
                        baseInterfaces.Add(baseName);
                    }

                    var baseList = "";
                    if (baseInterfaces.Count > 0)
                    {
                        var joinedBaseInterfaces = string.Join(", ",
                            baseInterfaces.Select(baseInterface =>
                            {
                                var baseInterfaceTypeArguments = string.Join(", ", interfaces[baseInterface].OrderBy(kvp =>
                                    kvp.Value.Index).Select(kvp => kvp.Key));
                                if (string.IsNullOrWhiteSpace(baseInterfaceTypeArguments))
                                {
                                    return baseInterface;
                                }
                                else
                                {
                                    return $"{baseInterface}<{baseInterfaceTypeArguments}>";
                                }
                            }));
                        baseList = $" : {joinedBaseInterfaces}";
                    }
                    
                    var baseInterfacesTypeParameters = interfaces.Where(iface => baseInterfaces.Any(baseInterface => baseInterface == iface.Key)).Select(x => x.Value).ToImmutableList();
                    interfaces.Add(name, Aggregate(baseInterfacesTypeParameters));
                    var genericParams = string.Join(", ", interfaces[name].OrderBy(x => x.Value.Index).Select(ConvertToString));
                    
                    if (!string.IsNullOrWhiteSpace(genericParams))
                    {
                        genericParams = $"<{genericParams}>";
                    }
                    
                    results.Add($"{name}.cs", $"namespace {_settings.Namespace} {{\npublic interface {name}{genericParams}{baseList} {{\n}}\n}}");
                }
            }

            return results.ToImmutableDictionary();
        }

        private string ConvertToString(KeyValuePair<string, TypeParameter> kvp)
        {
            if (kvp.Value.Variance == TypeParameterVariance.Out)
            {
                return $"out {kvp.Key}";
            }

            if (kvp.Value.Variance == TypeParameterVariance.In)
            {
                return $"in {kvp.Key}";
            }
            
            return kvp.Key;
        }

        private ImmutableDictionary<string, TypeParameter> Aggregate(ImmutableList<ImmutableDictionary<string, TypeParameter>> baseInterfaceTypeParameters)
        {
            var variances = new Dictionary<string, TypeParameterVariance>();
            var indices = new Dictionary<string, List<double>>();

            var allTypeParameterNames = baseInterfaceTypeParameters.SelectMany(x => x.Keys).ToImmutableHashSet();
            
            foreach (var typeParameterName in allTypeParameterNames)
            {
                TypeParameterVariance? currentValue = null;
                indices.Add(typeParameterName, new List<double>());
                var index = 0;
                foreach (var baseInterface in baseInterfaceTypeParameters)
                {
                    if (!baseInterface.ContainsKey(typeParameterName))
                    {
                        continue;
                    }
                    
                    indices[typeParameterName].Add(index);
                    
                    if (currentValue == null)
                    {
                        currentValue = baseInterface[typeParameterName].Variance;
                    }
                    else
                    {
                        if (currentValue.Value == baseInterface[typeParameterName].Variance)
                        {
                            
                        }
                        else
                        {
                            currentValue = TypeParameterVariance.None;
                        }
                    }

                    index++;
                }

                if (currentValue == null)
                {
                    currentValue = TypeParameterVariance.None;
                }
                
                variances.Add(typeParameterName, currentValue.Value);
            }

            var items = variances.OrderBy(x => indices[x.Key].Average()).ThenBy(x => x.Key)
                .Select((kvp, index) => new KeyValuePair<string, TypeParameter>(kvp.Key, new TypeParameter(kvp.Value, index)));
            
            return items.ToImmutableDictionary();
        }

        private bool GetName(IReadOnlyList<string> combination,
            out string name)
        {
            var generatedInterfaceName = new StringBuilder();
            foreach (var ifaceName in combination)
            {
                generatedInterfaceName.Append(ifaceName);
            }

            name = null;
            foreach (var item in _settings.InterfaceNameBuilders)
            {
                var str = generatedInterfaceName.ToString();
                if (Regex.IsMatch(str, item.Search))
                {
                    name = Regex.Replace(str, item.Search, item.Replacement);
                    break;
                }
            }

            if (name == null)
            {
                throw new InvalidOperationException("None of the name creation regexes matched");
            }
            

            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            var tmpName = name;
            
            if (_settings.InterfaceNameBlacklistRegexes.Any(regex => Regex.IsMatch(tmpName, regex)))
            {
                return false;
            }

            return true;
        }
    }
}