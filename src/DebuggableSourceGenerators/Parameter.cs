using System;

namespace DebuggableSourceGenerators
{
    public record Parameter
    {
        public Lazy<Type> Type { get; init; }
        public string Name { get; init; }
        public ParameterMode Mode { get; init; }

        public override string ToString()
        {
            var modeString = Mode == ParameterMode.Out ? "out " : "";
            return $"{modeString}{Type.Value}{Name}";
        }
    }
}