using System;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators
{
    public record TypeParameter : Type
    {
        public override bool IsGenericParameter => true;
        public VarianceMode VarianceMode { get; init; }
        public bool MustHaveEmptyConstructor { get; init; }
        public ImmutableList<Lazy<Type>> MustBeAssignedTo { get; init; }

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