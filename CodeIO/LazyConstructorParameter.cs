using System;

namespace CodeIO
{
    public class LazyConstructorParameter : IConstructorParameter
    {
        private readonly Lazy<IType> _type;

        public LazyConstructorParameter(string name, Lazy<IType> type, bool hasDefaultValue, object? defaultValue)
        {
            Name = name;
            _type = type;
            HasDefaultValue = hasDefaultValue;
            DefaultValue = defaultValue;
        }

        public string Name { get; }
        public IType Type => _type.Value;
        public bool HasDefaultValue { get; }
        public object? DefaultValue { get; }
    }
}