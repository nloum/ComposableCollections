using System;

namespace DebuggableSourceGenerators
{
    public class Field
    {
        public bool IsStatic { get; init; }
        public string Name { get; init; }
        public Lazy<Type> Type { get; init; }
        public Visibility Visibility { get; init; }

        // TODO - are the getter / setter public, private, or protected?
    }
}