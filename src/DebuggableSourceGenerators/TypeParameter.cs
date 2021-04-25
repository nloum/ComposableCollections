namespace DebuggableSourceGenerators
{
    public record TypeParameter : Type
    {
        public TypeIdentifier Identifier { get; init; }

        public VarianceMode VarianceMode { get; init; }

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