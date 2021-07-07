using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DebuggableSourceGenerators.Read
{
    public class Utilities
    {
        public static void TraverseTree(SyntaxNode syntaxNode, Action<SyntaxNode> visit)
        {
            visit(syntaxNode);
            foreach (var child in syntaxNode.ChildNodes())
            {
                TraverseTree(child, visit);
            }
        }

        public static string GetNamespace(SyntaxNode syntaxNode)
        {
            while (syntaxNode != null)
            {
                if (syntaxNode is NamespaceDeclarationSyntax namespaceDeclarationSyntax)
                {
                    return namespaceDeclarationSyntax.Name.ToString();
                }

                syntaxNode = syntaxNode.Parent;
            }

            return string.Empty;
        }
    }
}