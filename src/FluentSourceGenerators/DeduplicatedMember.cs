using System.Collections.Generic;
using System.Collections.Immutable;

namespace FluentSourceGenerators
{
    public class DeduplicatedMember<T>
    {
        private readonly string _explicitImplementationProfile;

        public DeduplicatedMember(string explicitImplementationProfile, T value, bool implementExplicitly, IEnumerable<T> duplicates)
        {
            _explicitImplementationProfile = explicitImplementationProfile;
            Value = value;
            ImplementExplicitly = implementExplicitly;
            Duplicates = duplicates.ToImmutableList();
        }

        public ImmutableList<T> Duplicates { get; }
        public T Value { get; }
        public bool ImplementExplicitly { get; }

        public override string ToString()
        {
            return _explicitImplementationProfile;
        }
    }
}