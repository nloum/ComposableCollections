using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;
using SimpleMonads;

namespace CodeIO.LoadedTypes.Write
{
    public class ClassWriter
    {
        public System.Type? BaseClass { get; set; }
        public List<Type> Interfaces { get; } = new List<Type>();
        public string Name { get; set; }
        public List<ConstructorWriter> Constructors { get; } = new List<ConstructorWriter>();
        public List<PropertyWriter> Properties { get; } = new List<PropertyWriter>();
        public List<MethodWriter> Methods { get; } = new List<MethodWriter>();
        public List<Type> StaticMethodImplementationSources { get; } = new List<Type>();
        public bool IncludeConstructorForAllProperties { get; set; }
    }
}