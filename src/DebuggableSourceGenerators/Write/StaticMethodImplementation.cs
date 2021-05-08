using System.Collections.Generic;
using System.Reflection;

namespace DebuggableSourceGenerators.Write
{
    public class StaticMethodImplementation
    {
        public Dictionary<string, StaticMethodParameterImplementation> ParameterSettings { get; } =
            new Dictionary<string, StaticMethodParameterImplementation>();
        public MethodInfo StaticMethod { get; set; }
    }
}