using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace ComposableCollections.CodeGenerator
{
    public interface IGenerator<TSettings>
    {
        void Initialize(TSettings settings);
        ImmutableDictionary<string, string> Generate(IEnumerable<SyntaxTree> syntaxTrees, Func<SyntaxTree, SemanticModel> getSemanticModel);
    }
}