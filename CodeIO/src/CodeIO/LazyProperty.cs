using System;

namespace CodeIO
{
    public class LazyProperty : IProperty
    {
        private Lazy<IType> _type;

        public LazyProperty(Lazy<IType> type, string name, Visibility? getterVisibility, Visibility? setterVisibility, bool isStatic)
        {
            _type = type;
            Name = name;
            GetterVisibility = getterVisibility;
            SetterVisibility = setterVisibility;
            IsStatic = isStatic;
        }

        public string Name { get; }
        public IType Type => _type.Value;
        public Visibility? GetterVisibility { get; }
        public Visibility? SetterVisibility { get; }
        public bool IsStatic { get; }
    }
}