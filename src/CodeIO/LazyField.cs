using System;

namespace CodeIO
{
    public class LazyField : IField
    {
        private readonly Lazy<IType> _type;

        public LazyField(Lazy<IType> type, string name, Visibility visibility, bool isStatic)
        {
            _type = type;
            Name = name;
            Visibility = visibility;
            IsStatic = isStatic;
        }

        public string Name { get; }
        public IType Type => _type.Value;
        public Visibility Visibility { get; }
        public bool IsStatic { get; }
    }
}