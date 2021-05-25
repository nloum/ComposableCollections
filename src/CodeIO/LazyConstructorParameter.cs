using System;

namespace CodeIO
{
    public class LazyConstructorParameter : IConstructorParameter
    {
        private readonly Lazy<IType> _type;

        public LazyConstructorParameter(string name, Lazy<IType> type)
        {
            Name = name;
            _type = type;
        }

        public string Name { get; }
        public IType Type => _type.Value;
    }
}