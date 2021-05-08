using System;

namespace DebuggableSourceGenerators.Read
{
    public class Property
    {
        public bool IsStatic { get; init; }
        public string Name { get; init; }
        public Lazy<Type> Type { get; init; }
        public Visibility GetterVisibility { get; init; }
        public Visibility SetterVisibility { get; init; }
        public bool IsVirtual { get; init; }
    }
}