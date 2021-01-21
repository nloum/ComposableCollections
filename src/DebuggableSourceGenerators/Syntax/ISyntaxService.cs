using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DebuggableSourceGenerators
{
    public interface ISyntaxService
    {
        IEnumerable<Parameter> Convert(SeparatedSyntaxList<ParameterSyntax> parameterListSyntax);
        IEnumerable<TypeParameter> Convert(TypeParameterListSyntax typeParameterListSyntax);
        Lazy<IType> Convert(TypeSyntax typeSyntax);
        void Convert(SyntaxList<MemberDeclarationSyntax> memberDeclarationSyntaxes,
            out IReadOnlyList<Property> properties, out IReadOnlyList<Indexer> indexers,
            out IReadOnlyList<Method> methods);
    }
}