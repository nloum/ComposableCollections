using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace CodeIO.LoadedTypes.Write
{
    public class ConstructorWriter
    {
        public List<PropertyWriter> PropertiesToInitialize { get; } = new List<PropertyWriter>();
        public List<MethodWriter> MethodsToInitialize { get; } = new List<MethodWriter>();
        public ConstructorBuilder Builder { get; set; }
        public ConstructorInfo BaseConstructor { get; set; }
    }
}