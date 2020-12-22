using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentSourceGenerators
{
    public class CombinationInterfacesGenerator : GeneratorBase<CombinationInterfacesGeneratorSettings>
    {
        private CombinationInterfacesGeneratorSettings _settings;

        public override void Initialize(CombinationInterfacesGeneratorSettings settings)
        {
            _settings = settings;
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
        
        public override ImmutableDictionary<string, string> Generate(CodeIndexerService codeIndexerService)
        {
            var combinations = Utilities.CalcCombinationsOfOneFromEach(_settings.InterfaceNameModifiers.Select(x => x.Values));
            var interfaces = new Dictionary<string, ImmutableDictionary<string, TypeParameter>>();

            var results = new Dictionary<string, string>();
            
            foreach (var combination in combinations)
            {
                if (!GetName(combination, out var name)) continue;

                if (interfaces.ContainsKey(name))
                {
                    continue;
                }

                var withSimpleTypeNames = PreExistingInterfacesWithSimpleTypeNames();
                var matchingPreExisting = withSimpleTypeNames.FirstOrDefault(preExistingInterface => preExistingInterface.simpleTypeName == name);
                if (matchingPreExisting.simpleTypeName != null)
                {
                    var genericParams = new Dictionary<string, TypeParameter>();
                    
                    if (codeIndexerService.TryGetInterfaceDeclaration(name, out var value))
                    {
                        var index = 0;
                        foreach (var param in value?.TypeParameterList?.Parameters ?? Enumerable.Empty<TypeParameterSyntax>())
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
                    }
                    else if (matchingPreExisting.withTypeParameters.Contains("<"))
                    {
                        var matchingPreExistingTypeParameters = matchingPreExisting
                            .withTypeParameters
                            .Substring(matchingPreExisting.withTypeParameters.IndexOf('<')).Trim('<', '>').Split(',')
                            .Select(typeParameter => typeParameter.Trim());

                        var index = 0;
                        foreach (var typeParameter in matchingPreExistingTypeParameters)
                        {
                            if (typeParameter.Contains(" "))
                            {
                                var words = typeParameter.Split(' ');
                                if (words[0] == "in")
                                {
                                    genericParams.Add(words[1], new TypeParameter(TypeParameterVariance.In, index));
                                }
                                else if (words[0] == "out")
                                {
                                    genericParams.Add(words[1], new TypeParameter(TypeParameterVariance.Out, index));
                                }
                                else
                                {
                                    throw new ArgumentException($"Unknown type parameter variance {words[0]}");
                                }
                            }
                            else
                            {
                                genericParams.Add(typeParameter, new TypeParameter(TypeParameterVariance.None, index));
                            }

                            index++;
                        }
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

                    var usings = new StringBuilder();
                    
                    var baseList = "";
                    if (baseInterfaces.Count > 0)
                    {
                        var preExistingInterfacesWithSimpleTypeNames = PreExistingInterfacesWithSimpleTypeNames();

                        foreach (var baseInterface in baseInterfaces)
                        {
                            var matchingBaseInterface = preExistingInterfacesWithSimpleTypeNames.FirstOrDefault(pre =>
                                pre.simpleTypeName == baseInterface);
                            if (matchingBaseInterface.simpleTypeName == null)
                            {
                                continue;
                            }
                            if (matchingBaseInterface.withTypeParameters.Contains("."))
                            {
                                var theNamespace = matchingBaseInterface.withTypeParameters.Substring(0, matchingBaseInterface.withTypeParameters.LastIndexOf('.'));
                                usings.AppendLine($"using {theNamespace};");
                            }
                        }
                        
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
                    
                    results.Add($"{name}.g.cs", $"{usings}\nnamespace {_settings.Namespace} {{\npublic interface {name}{genericParams}{baseList} {{\n}}\n}}");
                }
            }

            return results.ToImmutableDictionary();
        }

        private ImmutableList<(string simpleTypeName, string withTypeParameters)> PreExistingInterfacesWithSimpleTypeNames()
        {
            return _settings.PreExistingInterfaces.Select(withTypeParameters =>
            {
                var simpleTypeName = withTypeParameters;

                var typeParameterIndex = withTypeParameters.IndexOf("[");
                if (typeParameterIndex >= 0)
                {
                    simpleTypeName = withTypeParameters.Substring(0, typeParameterIndex);
                }

                if (simpleTypeName.Contains("."))
                {
                    simpleTypeName = simpleTypeName.Substring(simpleTypeName.LastIndexOf('.')+1);
                }

                withTypeParameters = withTypeParameters.Replace('[', '<').Replace(']', '>');

                return (simpleTypeName, withTypeParameters);
            }).ToImmutableList();
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

            name = generatedInterfaceName.ToString();
            foreach (var item in _settings.InterfaceNameBuilders)
            {
                if (Regex.IsMatch(name, item.Search))
                {
                    name = Regex.Replace(name, item.Search, item.Replace);
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