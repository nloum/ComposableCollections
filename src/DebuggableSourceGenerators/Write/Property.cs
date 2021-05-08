using System;
using System.Reflection;
using System.Reflection.Emit;

namespace DebuggableSourceGenerators.Write
{
    public class Property
    {
        public string Name { get; init; }
        public Type Type { get; init; }
        internal FieldBuilder Field { get; set; }
        public PropertyBuilder Builder { get; set; }
        public PropertyInfo PropertyToOverride { get; set; }
    }
}