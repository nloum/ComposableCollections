using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DebuggableSourceGenerators.Write
{
    public class Constructor
    {
        public List<Property> PropertiesToInitialize { get; } = new List<Property>();
        public List<Method> MethodsToInitialize { get; } = new List<Method>();
        public ConstructorBuilder Builder { get; set; }
        public ConstructorInfo BaseConstructor { get; set; }
    }
}