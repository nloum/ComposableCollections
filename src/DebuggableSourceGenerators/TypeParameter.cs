namespace DebuggableSourceGenerators
{
    public class TypeParameter : IType
    {
        public TypeParameter(string name, VarianceMode varianceMode)
        {
            Name = name;
            VarianceMode = varianceMode;
        }

        public string Name { get; }

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

            return $"{Name}";
        }
    }
}