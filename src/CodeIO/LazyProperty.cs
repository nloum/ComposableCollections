using System;

namespace CodeIO
{
    public class LazyProperty : IProperty
    {
        private Lazy<IType> _type;

        public LazyProperty(Lazy<IType> type, string name, Visibility visibility)
        {
            _type = type;
            Name = name;
            Visibility = visibility;
        }

        public string Name { get; }
        public IType Type => _type.Value;
        public Visibility Visibility { get; }
    }
}