using System;
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
        
        public void Write(TypeBuilder typeBuilder, Type? baseClass)
        {
            var parameterTypes = new List<Type>();

            foreach (var property in PropertiesToInitialize)
            {
                parameterTypes.Add(property.Type);
            }

            foreach (var method in MethodsToInitialize)
            {
                parameterTypes.Add(method.Field.FieldType);
            }

            if (BaseConstructor != null)
            {
                foreach (var parameter in BaseConstructor.GetParameters())
                {
                    parameterTypes.Add(parameter.ParameterType);
                }
            }

            Builder = typeBuilder.DefineConstructor(MethodAttributes.Public,
                CallingConventions.Standard, parameterTypes.ToArray());

            ILGenerator ctor1IL = Builder.GetILGenerator();
            // For a constructor, argument zero is a reference to the new
            // instance. Push it on the stack before calling the base
            // class  Specify the default constructor of the
            // base class (System.Object) by passing an empty array of
            // types (Type.EmptyTypes) to GetConstructor.
            ctor1IL.Emit(OpCodes.Ldarg_0);
            if (BaseConstructor != null)
            {
                var baseConstructorParameters = BaseConstructor.GetParameters();
                for (var i = 0; i < baseConstructorParameters.Length; i++)
                {
                    var argIndex = PropertiesToInitialize.Count + i + 1;
                    if (argIndex == 1)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_1);
                    }
                    else if (argIndex == 2)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_2);
                    }
                    else if (argIndex == 3)
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_3);
                    }
                    else
                    {
                        ctor1IL.Emit(OpCodes.Ldarg_S, argIndex);
                    }
                }

                ctor1IL.Emit(OpCodes.Call, BaseConstructor);
            }
            else
            {
                ctor1IL.Emit(OpCodes.Call, baseClass.GetConstructor(Type.EmptyTypes));
            }

            for (var i = 0; i < PropertiesToInitialize.Count; i++)
            {
                var property = PropertiesToInitialize[i];
                // Push the instance on the stack before pushing the argument
                // that is to be assigned to the private field m_number.
                ctor1IL.Emit(OpCodes.Ldarg_0);

                var argIndex = i + 1;
                if (argIndex == 1)
                {
                    ctor1IL.Emit(OpCodes.Ldarg_1);
                }
                else if (argIndex == 2)
                {
                    ctor1IL.Emit(OpCodes.Ldarg_2);
                }
                else if (argIndex == 3)
                {
                    ctor1IL.Emit(OpCodes.Ldarg_3);
                }
                else
                {
                    ctor1IL.Emit(OpCodes.Ldarg_S, argIndex);
                }

                ctor1IL.Emit(OpCodes.Stfld, property.Field);
            }

            for (var i = 0; i < MethodsToInitialize.Count; i++)
            {
                var method = MethodsToInitialize[i];
                // Push the instance on the stack before pushing the argument
                // that is to be assigned to the private field m_number.
                ctor1IL.Emit(OpCodes.Ldarg_0);

                var argIndex = PropertiesToInitialize.Count + i + 1;
                if (argIndex == 1)
                {
                    ctor1IL.Emit(OpCodes.Ldarg_1);
                }
                else if (argIndex == 2)
                {
                    ctor1IL.Emit(OpCodes.Ldarg_2);
                }
                else if (argIndex == 3)
                {
                    ctor1IL.Emit(OpCodes.Ldarg_3);
                }
                else
                {
                    ctor1IL.Emit(OpCodes.Ldarg_S, argIndex);
                }

                ctor1IL.Emit(OpCodes.Stfld, method.Field);
            }

            ctor1IL.Emit(OpCodes.Ret);
        }
    }
}