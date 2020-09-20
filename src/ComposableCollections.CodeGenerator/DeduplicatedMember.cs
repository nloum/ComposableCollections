using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace ComposableCollections.CodeGenerator
{
    public class DeduplicatedMember
    {
        private readonly string _explicitImplementationProfile;

        public DeduplicatedMember(string explicitImplementationProfile, ISymbol symbol, bool implementExplicitly, IEnumerable<ISymbol> duplicates)
        {
            _explicitImplementationProfile = explicitImplementationProfile;
            Symbol = symbol;
            ImplementExplicitly = implementExplicitly;
            Duplicates = duplicates.ToImmutableList();
        }

        public ImmutableList<ISymbol> Duplicates { get; }
        public ISymbol Symbol { get; }
        public bool ImplementExplicitly { get; }

        public override string ToString()
        {
            return _explicitImplementationProfile;
        }
    }
}