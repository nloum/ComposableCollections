using System;

namespace DebuggableSourceGenerators.Read
{
    public class Field
    {
        public bool IsStatic { get; init; }
        public string Name { get; init; }
        public Lazy<TypeBase> Type { get; init; }
        public Visibility Visibility { get; init; }
    }
}