using System;

namespace CodeIO.LoadedTypes.Read
{
    public record ReflectionPrimitive : Primitive, IReflectionType
    {
        public Type Type { get; init; }
    }
}