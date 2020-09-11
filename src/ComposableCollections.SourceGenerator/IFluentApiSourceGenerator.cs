using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;

namespace ComposableCollections.SourceGenerator
{
    public interface IFluentApiSourceGenerator
    {
        void AddSyntaxNode(SyntaxNode syntaxNode);
        void AddSource(Action<string, SourceText> addSource);
    }
}
