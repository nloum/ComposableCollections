using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DebuggableSourceGenerators.Write
{
    public class Method
    {
        public string Name { get; set; }
        public MethodBuilder Builder { get; set; }
        public MethodInfo MethodToOverride { get; set; }
        public Type ReturnType { get; set; }
        public List<Type> Parameters { get; } = new List<Type>();
        internal FieldBuilder Field { get; set; }
        public MethodImplementationMode ImplementationMode { get; set; }
    }
}