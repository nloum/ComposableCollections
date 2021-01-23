namespace DebuggableSourceGenerators
{
    public class TypeParameter : IType
    {
        public TypeParameter(TypeIdentifier identifier, VarianceMode varianceMode)
        {
            Identifier = identifier;
            VarianceMode = varianceMode;
        }

        public TypeIdentifier Identifier { get; }

        public VarianceMode VarianceMode { get; }

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