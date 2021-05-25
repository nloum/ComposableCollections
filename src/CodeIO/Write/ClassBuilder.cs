using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;
using SimpleMonads;

namespace CodeIO.Write
{
    public class ClassBuilder
    {
        public System.Type? BaseClass { get; set; }
        public List<Type> Interfaces { get; } = new List<Type>();
        public string Name { get; set; }
        public List<Constructor> Constructors { get; } = new List<Constructor>();
        public List<Property> Properties { get; } = new List<Property>();
        public List<Method> Methods { get; } = new List<Method>();
        public List<Type> StaticMethodImplementationSources { get; } = new List<Type>();
        public bool IncludeConstructorForAllProperties { get; set; }

        public Type Build()
        {
            BaseClass ??= typeof(object);

            var assemblyName = new AssemblyName("DynamicAssemblyExample");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()),
                AssemblyBuilderAccess.Run);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            var typeBuilder = moduleBuilder.DefineType(Name, TypeAttributes.Public, BaseClass, Interfaces.ToArray());

            var baseTypes = Interfaces.ToList();
            if (BaseClass != null)
            {
                baseTypes.Insert(0, BaseClass);
            }

            var propertiesThatNeedToBeOverriden = new HashSet<PropertyInfo>();

            foreach (var baseType in baseTypes)
            {
                var propertiesThatNeedToBeOverridenForThisBaseType = baseType.GetProperties().Where(property =>
                    (property.GetMethod?.IsAbstract == true || property.SetMethod?.IsAbstract == true ||
                     baseType.IsInterface));
                foreach (var property in propertiesThatNeedToBeOverridenForThisBaseType)
                {
                    propertiesThatNeedToBeOverriden.Add(property);
                }
            }

            foreach (var property in Properties)
            {
                foreach (var baseType in baseTypes)
                {
                    property.PropertyToOverride = baseType.GetProperty(property.Name);

                    if (property.PropertyToOverride?.PropertyType != property.Type)
                    {
                        property.PropertyToOverride = null;
                    }

                    if (property.PropertyToOverride != null)
                    {
                        if (propertiesThatNeedToBeOverriden.Contains(property.PropertyToOverride))
                        {
                            propertiesThatNeedToBeOverriden.Remove(property.PropertyToOverride);
                        }

                        break;
                    }
                }
            }

            foreach (var propertyThatNeedsToBeOverriden in propertiesThatNeedToBeOverriden)
            {
                var property = new Property()
                {
                    Name = propertyThatNeedsToBeOverriden.Name,
                    Type = propertyThatNeedsToBeOverriden.PropertyType,
                    PropertyToOverride = propertyThatNeedsToBeOverriden,
                };

                Properties.Add(property);
            }

            foreach (var property in Properties)
            {
                property.Field = typeBuilder.DefineField("_" + property.Name.Camelize(), property.Type,
                    FieldAttributes.Private);
            }

            foreach (var method in Methods)
            {
                var methodInfo = method.MethodToOverride ?? method.Implementation.Item1?.StaticMethod;
                method.Implementation.ForEach(_ =>
                {
                    throw new InvalidOperationException(
                        $"There is supposed to be a constructor to setup the {method.Name} method but the implementation mode {method.Implementation} is invalid for this");
                }, delegateFromConstructorParameter =>
                {
                    method.Field = typeBuilder.DefineField("_" + method.Name.Camelize(), GetDelegateType(methodInfo),
                        FieldAttributes.Private);
                }, unchangingReturnValueFromConstructorParameter =>
                {
                    method.Field = typeBuilder.DefineField("_" + method.Name.Camelize(), methodInfo.ReturnType,
                        FieldAttributes.Private);
                });
            }

            if (IncludeConstructorForAllProperties)
            {
                var constructor = new Constructor();
                constructor.PropertiesToInitialize.AddRange(Properties);
                Constructors.Add(constructor);
            }

            foreach (var constructor in Constructors)
            {
                var parameterTypes = new List<Type>();

                foreach (var property in constructor.PropertiesToInitialize)
                {
                    parameterTypes.Add(property.Type);
                }

                foreach (var method in constructor.MethodsToInitialize)
                {
                    parameterTypes.Add(method.Field.FieldType);
                }

                if (constructor.BaseConstructor != null)
                {
                    foreach (var parameter in constructor.BaseConstructor.GetParameters())
                    {
                        parameterTypes.Add(parameter.ParameterType);
                    }
                }

                constructor.Builder = typeBuilder.DefineConstructor(MethodAttributes.Public,
                    CallingConventions.Standard, parameterTypes.ToArray());

                ILGenerator ctor1IL = constructor.Builder.GetILGenerator();
                // For a constructor, argument zero is a reference to the new
                // instance. Push it on the stack before calling the base
                // class constructor. Specify the default constructor of the
                // base class (System.Object) by passing an empty array of
                // types (Type.EmptyTypes) to GetConstructor.
                ctor1IL.Emit(OpCodes.Ldarg_0);
                if (constructor.BaseConstructor != null)
                {
                    var baseConstructorParameters = constructor.BaseConstructor.GetParameters();
                    for (var i = 0; i < baseConstructorParameters.Length; i++)
                    {
                        var argIndex = constructor.PropertiesToInitialize.Count + i + 1;
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

                    ctor1IL.Emit(OpCodes.Call, constructor.BaseConstructor);
                }
                else
                {
                    ctor1IL.Emit(OpCodes.Call, BaseClass.GetConstructor(Type.EmptyTypes));
                }

                for (var i = 0; i < constructor.PropertiesToInitialize.Count; i++)
                {
                    var property = constructor.PropertiesToInitialize[i];
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

                for (var i = 0; i < constructor.MethodsToInitialize.Count; i++)
                {
                    var method = constructor.MethodsToInitialize[i];
                    // Push the instance on the stack before pushing the argument
                    // that is to be assigned to the private field m_number.
                    ctor1IL.Emit(OpCodes.Ldarg_0);

                    var argIndex = constructor.PropertiesToInitialize.Count + i + 1;
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

            foreach (var property in Properties)
            {
                // Define a property named Number that gets and sets the private
                // field.
                //
                // The last argument of DefineProperty is null, because the
                // property has no parameters. (If you don't specify null, you must
                // specify an array of Type objects. For a parameterless property,
                // use the built-in array with no elements: Type.EmptyTypes)
                property.Builder = typeBuilder.DefineProperty(
                    property.Name,
                    PropertyAttributes.HasDefault,
                    property.Type,
                    null);

                MethodInfo propertyGetterOverride = null;
                MethodInfo propertySetterOverride = null;

                foreach (var baseType in baseTypes)
                {
                    var propertyToOverride = baseType.GetProperty(property.Name);
                    if (propertyToOverride == null)
                    {
                        continue;
                    }

                    if (propertyToOverride.PropertyType == property.Type)
                    {
                        if (propertyToOverride.GetMethod != null)
                        {
                            propertyGetterOverride = propertyToOverride.GetMethod;
                        }

                        if (propertyToOverride.SetMethod != null)
                        {
                            propertySetterOverride = propertyToOverride.SetMethod;
                        }

                        break;
                    }
                }

                // The property "set" and property "get" methods require a special
                // set of attributes.
                MethodAttributes getSetAttr = MethodAttributes.Public |
                                              MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                // Define the "get" accessor method for Number. The method returns
                // an integer and has no arguments. (Note that null could be
                // used instead of Types.EmptyTypes)
                MethodBuilder propertyGetter = typeBuilder.DefineMethod(
                    "get_" + property.Name,
                    propertyGetterOverride == null ? getSetAttr : getSetAttr | MethodAttributes.Virtual,
                    property.Type,
                    Type.EmptyTypes);
                if (propertyGetterOverride != null)
                {
                    typeBuilder.DefineMethodOverride(propertyGetter, propertyGetterOverride);
                }

                ILGenerator propertyGetterIl = propertyGetter.GetILGenerator();
                // For an instance property, argument zero is the instance. Load the
                // instance, then load the private field and return, leaving the
                // field value on the stack.
                propertyGetterIl.Emit(OpCodes.Ldarg_0);
                propertyGetterIl.Emit(OpCodes.Ldfld, property.Field);
                propertyGetterIl.Emit(OpCodes.Ret);

                // Define the "set" accessor method for Number, which has no return
                // type and takes one argument of type int (Int32).
                MethodBuilder propertySetter = typeBuilder.DefineMethod(
                    "set_" + property.Name,
                    propertySetterOverride == null ? getSetAttr : getSetAttr | MethodAttributes.Virtual,
                    null,
                    new Type[] {property.Type});
                if (propertySetterOverride != null)
                {
                    typeBuilder.DefineMethodOverride(propertySetter, propertySetterOverride);
                }

                ILGenerator propertySetterIl = propertySetter.GetILGenerator();
                // Load the instance and then the numeric argument, then store the
                // argument in the field.
                propertySetterIl.Emit(OpCodes.Ldarg_0);
                propertySetterIl.Emit(OpCodes.Ldarg_1);
                propertySetterIl.Emit(OpCodes.Stfld, property.Field);
                propertySetterIl.Emit(OpCodes.Ret);

                // Last, map the "get" and "set" accessor methods to the
                // PropertyBuilder. The property is now complete.
                property.Builder.SetGetMethod(propertyGetter);
                property.Builder.SetSetMethod(propertySetter);
            }

            var methodsThatNeedToBeOverriden = new HashSet<MethodInfo>();

            foreach (var baseType in baseTypes)
            {
                var methodsThatNeedToBeOverridenForThisBaseType = baseType.GetMethods().Where(method =>
                    (method.IsAbstract || baseType.IsInterface) &&
                    !(method.Name.StartsWith("get_") || method.Name.StartsWith("set_")));
                foreach (var method in methodsThatNeedToBeOverridenForThisBaseType)
                {
                    methodsThatNeedToBeOverriden.Add(method);
                }
            }

            foreach (var method in Methods)
            {
                method.Name ??= method.Implementation.Item1?.StaticMethod?.Name;

                if (method.Implementation.Item1 != null)
                {
                    foreach (var baseType in baseTypes)
                    {
                        method.MethodToOverride = baseType.GetMethod(method.Name,
                            method.Implementation.Item1.StaticMethod.GetParameters().Select(x => x.ParameterType).Skip(1).ToArray());

                        if (method.MethodToOverride != null)
                        {
                            if (methodsThatNeedToBeOverriden.Contains(method.MethodToOverride))
                            {
                                methodsThatNeedToBeOverriden.Remove(method.MethodToOverride);
                            }

                            break;
                        }
                    }
                }
            }

            foreach (var methodThatNeedsToBeOverriden in methodsThatNeedToBeOverriden)
            {
                var method = new Method()
                {
                    MethodToOverride = methodThatNeedsToBeOverriden,
                };

                foreach (var staticMethodImplementationSource in StaticMethodImplementationSources)
                {
                    foreach (var possibleImplementation in staticMethodImplementationSource.GetMethods()
                        .Where(x => x.IsStatic))
                    {
                        if (possibleImplementation.Name != methodThatNeedsToBeOverriden.Name)
                        {
                            continue;
                        }

                        if (possibleImplementation.GetParameters().Length - 1 !=
                            methodThatNeedsToBeOverriden.GetParameters().Length)
                        {
                            continue;
                        }

                        if (!baseTypes.Any(baseType =>
                            possibleImplementation.GetParameters()[0].ParameterType.IsAssignableFrom(baseType)))
                        {
                            continue;
                        }

                        var parameters = possibleImplementation.GetParameters().Skip(1)
                            .Zip(methodThatNeedsToBeOverriden.GetParameters());
                        var match = true;
                        foreach (var paramPair in parameters)
                        {
                            if (paramPair.First.ParameterType != paramPair.Second.ParameterType)
                            {
                                match = false;
                            }
                        }

                        if (match)
                        {
                            method.Implementation = new Method.MethodImpl(new Method.MethodImpl.Static()
                            {
                                StaticMethod = possibleImplementation
                            });
                            break;
                        }
                    }

                    if (method.Implementation?.Item1 != null)
                    {
                        break;
                    }
                }

                Methods.Add(method);
            }

            foreach (var method in Methods)
            {
                method.Implementation.ForEach(staticMethodImplementation =>
                {
                    method.Name ??= staticMethodImplementation.StaticMethod.Name;

                    method.Builder =
                        typeBuilder.DefineMethod(
                            method.Name,
                            MethodAttributes.Public
                            | MethodAttributes.Virtual
                            //| MethodAttributes.Final
                            | MethodAttributes.HideBySig
                            | MethodAttributes.NewSlot,
                            staticMethodImplementation.StaticMethod.ReturnType,
                            staticMethodImplementation.StaticMethod.GetParameters().Skip(1).Select(p => p.ParameterType).ToArray());
                    var methodIl = method.Builder.GetILGenerator();
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

                    if (method.MethodToOverride != null)
                    {
                        typeBuilder.DefineMethodOverride(method.Builder, method.MethodToOverride);
                    }
                }, delegateFromConstructorParameter =>
                {
                    throw new NotImplementedException();
                }, unchangingReturnValueFromConstructorParameter =>
                {
                    throw new NotImplementedException();
                });
            }

            // Finish the type.
            Type t = typeBuilder.CreateType();

            return t;
        }

        private Type GetDelegateType(MethodInfo methodInfo)
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
    }
}