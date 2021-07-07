using System;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public record TypeParameter : TypeBase
    {
        public override bool IsGenericParameter => true;
        public VarianceMode VarianceMode { get; init; }
        public bool MustHaveEmptyConstructor { get; init; }
        public ImmutableList<Lazy<TypeBase>> MustBeAssignedTo { get; init; }

        public override string ToString()
        {
            // string varianceModeString = "";
            // if (VarianceMode == VarianceMode.In)
            // {
            //     varianceModeString = "in ";
            // }
            // else if (VarianceMode == VarianceMode.Out)
            // {
            //     varianceModeString = "out ";
            // }

            return $"{Identifier}";
        }
    }
}