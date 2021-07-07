using System;

namespace CodeIO
{
    public class LazyParameter : IParameter
    {
        private readonly Lazy<IType> _type;

        public LazyParameter(string name, Lazy<IType> type, bool isOut, bool hasDefaultValue, object? defaultValue)
        {
            Name = name;
            _type = type;
            IsOut = isOut;
            HasDefaultValue = hasDefaultValue;
            DefaultValue = defaultValue;
        }

        public string Name { get; }
        public IType Type => _type.Value;
        public bool IsOut { get; }
        public bool HasDefaultValue { get; }
        public object? DefaultValue { get; }
    }
}