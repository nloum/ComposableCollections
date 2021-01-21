using System;

namespace DebuggableSourceGenerators
{
    public class Parameter
    {
        public Lazy<IType> Type { get; }

        public Parameter(string name, Lazy<IType> type, ParameterMode mode)
        {
            Name = name;
            Type = type;
            Mode = mode;
        }

        public string Name { get; }
        public ParameterMode Mode { get; }

        public override string ToString()
        {
            var modeString = Mode == ParameterMode.Out ? "out " : "";
            return $"{modeString}{Type.Value}{Name}";
        }
    }
}