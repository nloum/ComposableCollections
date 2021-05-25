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
    public class MethodWriter
    {
        public string Name { get; set; }
        public MethodBuilder Builder { get; set; }
        public MethodInfo MethodToOverride { get; set; }
        public Type ReturnType { get; set; }
        public List<Type> Parameters { get; } = new List<Type>();
        internal FieldBuilder Field { get; set; }
        public MethodImpl Implementation { get; set; }

        public void DefineField(TypeBuilder typeBuilder)
        {
            var methodInfo = MethodToOverride ?? Implementation.Item1?.StaticMethod;
            Implementation.ForEach(_ =>
            {
                throw new InvalidOperationException(
                    $"There is supposed to be a constructor to setup the {Name} method but the implementation mode {Implementation} is invalid for this");
            }, delegateFromConstructorParameter =>
            {
                Field = typeBuilder.DefineField("_" + Name.Camelize(), GetDelegateType(methodInfo),
                    FieldAttributes.Private);
            }, unchangingReturnValueFromConstructorParameter =>
            {
                Field = typeBuilder.DefineField("_" + Name.Camelize(), methodInfo.ReturnType,
                    FieldAttributes.Private);
            });
        }
        
        private static Type GetDelegateType(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            var parameterTypes = parameters.Select(x => x.ParameterType).ToArray();
            if (methodInfo.ReturnType == null || methodInfo.ReturnType == typeof(void))
            {
                if (parameters.Length == 0)
                {
                    return typeof(Action);
                }

                var actionType = Type.GetType($"System.Action`{parameters.Length}")
                    .MakeGenericType(parameterTypes);
                return actionType;
            }
            else
            {
                if (parameters.Length == 0)
                {
                    return typeof(Func<>).MakeGenericType(methodInfo.ReturnType);
                }

                var funcType = Type.GetType($"System.Func`{parameters.Length + 1}")
                    .MakeGenericType(parameterTypes.Concat(new[] {methodInfo.ReturnType}).ToArray());
                return funcType;
            }
        }

        public void Write(TypeBuilder typeBuilder)
        {
            Implementation.ForEach(staticMethodImplementation =>
            {
                Name ??= staticMethodImplementation.StaticMethod.Name;

                Builder =
                    typeBuilder.DefineMethod(
                        Name,
                        MethodAttributes.Public
                        | MethodAttributes.Virtual
                        //| MethodAttributes.Final
                        | MethodAttributes.HideBySig
                        | MethodAttributes.NewSlot,
                        staticMethodImplementation.StaticMethod.ReturnType,
                        staticMethodImplementation.StaticMethod.GetParameters().Skip(1).Select(p => p.ParameterType)
                            .ToArray());
                var methodIl = Builder.GetILGenerator();
                //methodIl.Emit(OpCodes.Nop);
                methodIl.Emit(OpCodes.Ldarg_0);

                var extensionMethodParameters =
                    staticMethodImplementation.StaticMethod.GetParameters().Skip(1).ToImmutableList();
                for (var i = 0; i < extensionMethodParameters.Count; i++)
                {
                    if (i == 0)
                    {
                        methodIl.Emit(OpCodes.Ldarg_1);
                    }
                    else
                    {
                        methodIl.Emit(OpCodes.Ldarg_S, i + 1);
                    }
                }

                if (staticMethodImplementation.StaticMethod.ReturnType != null &&
                    staticMethodImplementation.StaticMethod.ReturnType != typeof(void))
                {
                    var localVariable = methodIl.DeclareLocal(staticMethodImplementation.StaticMethod.ReturnType);
                    methodIl.EmitCall(OpCodes.Call, staticMethodImplementation.StaticMethod,
                        staticMethodImplementation.StaticMethod.GetParameters().Select(p => p.ParameterType).ToArray());
                    methodIl.Emit(OpCodes.Stloc_0, localVariable);
                    methodIl.Emit(OpCodes.Ldloc_0);
                }
                else
                {
                    methodIl.EmitCall(OpCodes.Call, staticMethodImplementation.StaticMethod,
                        staticMethodImplementation.StaticMethod.GetParameters().Select(p => p.ParameterType).ToArray());
                }

                methodIl.Emit(OpCodes.Ret);

                if (MethodToOverride != null)
                {
                    typeBuilder.DefineMethodOverride(Builder, MethodToOverride);
                }
            }, delegateFromConstructorParameter => { throw new NotImplementedException(); },
            unchangingReturnValueFromConstructorParameter => { throw new NotImplementedException(); });
        }
        
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