using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using CodeIO.LoadedTypes.Read;
using Humanizer;
using SimpleMonads;

namespace CodeIO.LoadedTypes.Write
{
    public static class Extensions
    {
        public static ClassWriter ImplementClass(this ReflectionNonGenericInterface iface)
        {
            var result = new ClassWriter()
            {
                Interfaces = ImmutableList<Type>.Empty.Add(iface.Type)
            };
            return result;
        }
        
        public static ClassWriter WithDefaultImplementationsForMissingProperties(this ClassWriter classWriter)
        {
            var properties = classWriter.Properties;
            foreach (var propertyThatNeedsToBeOverriden in classWriter.PropertiesNotYetOverriden)
            {
                var property = new PropertyWriter()
                {
                    Name = propertyThatNeedsToBeOverriden.Name,
                    Type = propertyThatNeedsToBeOverriden.PropertyType,
                    PropertyToOverride = propertyThatNeedsToBeOverriden,
                };

                properties = properties.Add(property);
            }

            return classWriter with {Properties = properties};
        }

        public static ClassWriter WithConstructorThatInitializesAllProperties(this ClassWriter classWriter)
        {
            var constructor = new ConstructorWriter();
            constructor.PropertiesToInitialize.AddRange(classWriter.Properties);
            return classWriter with {Constructors = classWriter.Constructors.Add(constructor)};
        }

        public static ClassWriter WithStaticImplementationsForMissingMethods(this ClassWriter classWriter, params Type[] staticMethodImplementationSources)
        {
            var methods = classWriter.Methods;
            
            foreach (var methodThatNeedsToBeOverriden in classWriter.MethodsNotYetOverriden)
            {
                var method = new MethodWriter()
                {
                    MethodToOverride = methodThatNeedsToBeOverriden,
                };

                foreach (var staticMethodImplementationSource in staticMethodImplementationSources)
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

                        if (!classWriter.BaseTypes.Any(baseType =>
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
                            method.Implementation = new MethodWriter.MethodImpl(new MethodWriter.MethodImpl.Static()
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

                methods = methods.Add(method);
            }

            return classWriter with {Methods = methods};
        }
        
        public static Type Write(this ClassWriter classWriter)
        {
            var baseClass = classWriter.BaseClass ?? typeof(object);

            var assemblyName = new AssemblyName("DynamicAssemblyExample");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()),
                AssemblyBuilderAccess.Run);

            // For a single-module assembly, the module name is usually
            // the assembly name plus an extension.
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            var typeBuilder = moduleBuilder.DefineType(classWriter.Name, TypeAttributes.Public, baseClass, classWriter.Interfaces.ToArray());

            foreach (var property in classWriter.Properties)
            {
                property.DefineField(typeBuilder);
            }

            foreach (var method in classWriter.Methods)
            {
                method.DefineField(typeBuilder);
            }

            foreach (var constructor in classWriter.Constructors)
            {
                constructor.Write(typeBuilder, baseClass);
            }

            foreach (var property in classWriter.Properties)
            {
                property.Write(classWriter, typeBuilder);
            }

            foreach (var method in classWriter.Methods)
            {
                method.Write(typeBuilder);
            }

            // Finish the type.
            Type t = typeBuilder.CreateType();

            return t;
        }
    }
}