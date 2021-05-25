using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using SimpleMonads;

namespace CodeIO.LoadedTypes.Write
{
    public class MethodWriter
    {
        public string Name { get; set; }
        public MethodBuilder Builder { get; set; }
        public MethodInfo MethodToOverride { get; set; }
        public Type ReturnType { get; set; }
        public List<Type> Parameters { get; } = new List<Type>();
        internal FieldBuilder Field { get; set; }
        public MethodImpl Implementation { get; set; }
        
        public class MethodImpl : Either<MethodImpl.Static, MethodImpl.Delegate,
            MethodImpl.Constant>
        {
            public MethodImpl(Static item1) : base(item1)
            {
            }

            public MethodImpl(Delegate item2) : base(item2)
            {
            }

            public MethodImpl(Constant item3) : base(item3)
            {
            }
            
            public record Constant { }
            public record Delegate { }
            public class Static
            {
                public Dictionary<string, Parameter> ParameterSettings { get; } =
                    new Dictionary<string, Parameter>();
                public List<Type> ImplementationSources { get; } = new List<Type>();
                public MethodInfo StaticMethod { get; set; }

                public class Parameter : Either<Parameter.PullFromConstructor, Parameter.IncludeInMethod, Parameter.Constant>
                {
                    public Parameter(PullFromConstructor item1) : base(item1)
                    {
                    }

                    public Parameter(IncludeInMethod item2) : base(item2)
                    {
                    }

                    public Parameter(Constant item3) : base(item3)
                    {
                    }
                
                    public record Constant(object Value) { }
                    public record IncludeInMethod() { }
                    public record PullFromConstructor() { }
                }
            }
        }
    }
}