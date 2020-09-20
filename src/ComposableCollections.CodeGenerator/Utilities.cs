using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComposableCollections.CodeGenerator
{
    public class Utilities
    {
        public static SyntaxNode FindParentOfKind(SyntaxNode child, SyntaxKind syntaxKind)
        {
            if (child.IsKind(syntaxKind))
            {
                return child;
            }

            if (child.Parent != null)
            {
                return FindParentOfKind(child.Parent, syntaxKind);
            }

            return null;
        }

        public static ImmutableList<T> GetDescendantsOfType<T>(SyntaxNode node) where T : SyntaxNode
        {
            var result = new List<T>();
            TraverseTree(node, descendant =>
            {
                if (descendant is T t)
                {
                    result.Add(t);
                }
            });
            return result.ToImmutableList();
        }

        public static ImmutableList<SyntaxNode> GetDescendants(SyntaxNode node)
        {
            var result = new List<SyntaxNode>();
            TraverseTree(node, descendant =>
            {
                result.Add(descendant);
            });
            return result.ToImmutableList();
        }

        public static void TraverseTree(SyntaxNode syntaxNode, Action<SyntaxNode> visit)
        {
            visit(syntaxNode);
            foreach (var child in syntaxNode.ChildNodes())
            {
                TraverseTree(child, visit);
            }
        }

        public static string ConvertToParameter(IParameterSymbol parameter)
        {
            var modifier = string.Join(" ", parameter.CustomModifiers);
            if (parameter.RefKind == RefKind.In)
                modifier += " in";
            if (parameter.RefKind == RefKind.Out)
                modifier += " out";
            if (parameter.RefKind == RefKind.Ref)
                modifier += " ref";
            return $"{modifier} {parameter.Type} {parameter.Name}";
        }
        
        public static string ConvertToArgument(IParameterSymbol parameter)
        {
            var modifier = string.Join(" ", parameter.CustomModifiers);
            if (parameter.RefKind == RefKind.In)
                modifier += " in";
            if (parameter.RefKind == RefKind.Out)
                modifier += " out";
            if (parameter.RefKind == RefKind.Ref)
                modifier += " ref";
            return $"{modifier} {parameter.Name}";
        }
        
        public static IEnumerable<string> GetUsings(ITypeSymbol typeSymbol)
        {
            var results = new List<string>();
            GetUsings(typeSymbol, results);
            return results;
        }

        private static void GetUsings(ITypeSymbol typeSymbol, List<string> results)
        {
            if (typeSymbol.ContainingNamespace != null)
            {
                var result = string.Join(".", typeSymbol.ContainingNamespace.ConstituentNamespaces);
                if (result != "<global namespace>")
                {
                    results.Add("using " + string.Join(".", typeSymbol.ContainingNamespace.ConstituentNamespaces) + ";");
                }
            }

            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                foreach (var typeArgument in namedTypeSymbol.TypeArguments)
                {
                    GetUsings(typeArgument, results);
                }
            }
        }
        
        public static ImmutableDictionary<string, ImmutableList<ISymbol>> GetMembersGroupedByDeclaringType(InterfaceDeclarationSyntax syntax, SemanticModel semanticModel)
        {
            var symbol = semanticModel.GetDeclaredSymbol(syntax);
            return GetMembersGroupedByDeclaringType(symbol);
        }

        public static ImmutableDictionary<string, ImmutableList<ISymbol>> GetMembersGroupedByDeclaringType(INamedTypeSymbol symbol)
        {
            var results = new Dictionary<string, ImmutableList<ISymbol>>();
            GetMembersGroupedByDeclaringType(symbol, results);
            return results.ToImmutableDictionary();
        }
        
        private static void GetMembersGroupedByDeclaringType(INamedTypeSymbol symbol, Dictionary<string, ImmutableList<ISymbol>> results)
        {
            var typeArguments = symbol.TypeArguments.Length == 0 ? "" : $"<{string.Join(", ", symbol.TypeArguments)}>";
            var key = $"{symbol.Name}{typeArguments}";
            if (results.ContainsKey(key))
            {
                return;
            }

            results.Add(key, symbol.GetMembers().ToImmutableList());
            
            foreach (var baseInterface in symbol.Interfaces)
            {
                GetMembersGroupedByDeclaringType(baseInterface, results);
            }
        }
        
        public static bool IsBaseClass(INamedTypeSymbol superClass, INamedTypeSymbol baseClass)
        {
            var baseClasses = new List<INamedTypeSymbol>();
            GetBaseClasses(superClass, baseClasses);
            return baseClasses.Any(x => x.ToString() == baseClass.ToString());
        }

        public static string GetWithoutTypeArguments(string type)
        {
            var idx = type.IndexOf('<');
            if (idx >= 0)
            {
                return type.Substring(0, idx);
            }

            return type;
        }
        
        public static IReadOnlyList<INamedTypeSymbol> GetBaseClasses(INamedTypeSymbol superClass)
        {
            var baseClasses = new List<INamedTypeSymbol>();
            GetBaseClasses(superClass, baseClasses);
            return baseClasses;
        }
        
        private static void GetBaseClasses(INamedTypeSymbol superClass, List<INamedTypeSymbol> result)
        {
            result.Add(superClass);
            if (superClass?.BaseType?.TypeKind == TypeKind.Class)
            {
                GetBaseClasses(superClass.BaseType, result);
            }
        }
        
        public static bool IsBaseInterface(INamedTypeSymbol superInterface, INamedTypeSymbol baseInterface)
        {
            var baseInterfaces = new List<INamedTypeSymbol>();
            GetBaseInterfaces(superInterface, baseInterfaces);
            return baseInterfaces.Any(x => x.ToString() == baseInterface.ToString());
        }

        public static IReadOnlyList<INamedTypeSymbol> GetBaseInterfaces(INamedTypeSymbol superInterface)
        {
            var baseInterfaces = new List<INamedTypeSymbol>();
            GetBaseInterfaces(superInterface, baseInterfaces);
            return baseInterfaces;
        }
        
        private static void GetBaseInterfaces(INamedTypeSymbol superInterface, List<INamedTypeSymbol> result)
        {
            result.Add(superInterface);
            foreach (var baseType in superInterface.Interfaces)
            {
                GetBaseInterfaces(baseType, result);
            }
        }
        
        public static string GenerateVariableName(string typeName, bool isInterface)
        {
            var result = isInterface ? typeName.Substring(1) : typeName;
            if (result.Contains("<"))
            {
                result = result.Substring(0, result.IndexOf('<'));
            }

            return result.Camelize();
        }
        
        /// <summary>
        /// If you pass in new[]{0, 1} and new[]{0, 1} you'll get new[]{0, 0}, new[]{0, 1}, new[]{1, 0}, new[]{1, 1} out.
        /// </summary>
        /// <typeparam name="TItem">The type of item in the enumerables.</typeparam>
        /// <param name="enumerables">The enumerables that will be used to calculate combinations</param>
        /// <returns>A lazily-computed enumerable where each return item is a combination</returns>
        public static IEnumerable<ImmutableList<TItem>> CalcCombinationsOfOneFromEach<TItem>(
            IEnumerable<IEnumerable<TItem>> enumerables ) {
            var enumerators = enumerables.Select( enumerable => Tuple.Create( enumerable, enumerable.GetEnumerator() ) ).ToList();

            foreach ( var enumerator in enumerators ) {
                if ( !enumerator.Item2.MoveNext() ) {
                    throw new ArgumentException( "An empty enumerable was specified" );
                }
            }

            yield return enumerators.Select( t => t.Item2.Current ).ToImmutableList();

            while (true) {
                var i = 0;
                while(true) {
                    if ( i == enumerators.Count ) {
                        yield break;
                    }

                    if ( !enumerators[i].Item2.MoveNext() ) {
                        enumerators[i] = Tuple.Create( enumerators[i].Item1,
                            enumerators[i].Item1.GetEnumerator() );
                        enumerators[i].Item2.MoveNext();
                    } else {
                        break;
                    }

                    i++;
                }

                yield return enumerators.Select( t => t.Item2.Current ).ToImmutableList();
            }
        }

        public static string ConvertToTypeParameters(ImmutableArray<ITypeParameterSymbol> methodDeclarationTypeParameters)
        {
            if (methodDeclarationTypeParameters.Length == 0)
            {
                return "";
            }

            return
                $"<{string.Join(", ", methodDeclarationTypeParameters.Select(typeParameter => typeParameter.Name))}>";
        }
    }
}