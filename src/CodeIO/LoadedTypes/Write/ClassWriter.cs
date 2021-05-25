using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using Humanizer;

namespace CodeIO.LoadedTypes.Write
{
    public record ClassWriter
    {
        public System.Type? BaseClass { get; init; }
        public ImmutableList<Type> Interfaces { get; init; } = ImmutableList<Type>.Empty;
        public string Name { get; init; } = "DynamicClass" + RandomString(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + RandomString(6, "abcdefghijklmnopqrstuvwxyz");
        public ImmutableList<ConstructorWriter> Constructors { get; init; } = ImmutableList<ConstructorWriter>.Empty;
        public ImmutableList<PropertyWriter> Properties { get; init; } = ImmutableList<PropertyWriter>.Empty;
        public ImmutableList<MethodWriter> Methods { get; init; } = ImmutableList<MethodWriter>.Empty;

        public IEnumerable<Type> BaseTypes
        {
            get
            {
                var baseTypes = Interfaces.ToList();
                if (BaseClass != null)
                {
                    baseTypes.Insert(0, BaseClass);
                }
                
                return baseTypes;
            }
        }
        
        public IEnumerable<PropertyInfo> PropertiesThatNeedToBeOverriden
        {
            get
            {
                var propertiesThatNeedToBeOverriden = new HashSet<PropertyInfo>();

                foreach (var baseType in BaseTypes)
                {
                    var propertiesThatNeedToBeOverridenForThisBaseType = baseType.GetProperties().Where(property =>
                        (property.GetMethod?.IsAbstract == true || property.SetMethod?.IsAbstract == true ||
                         baseType.IsInterface));
                    foreach (var property in propertiesThatNeedToBeOverridenForThisBaseType)
                    {
                        propertiesThatNeedToBeOverriden.Add(property);
                    }
                }

                return propertiesThatNeedToBeOverriden;
            }
        }

        public IEnumerable<PropertyInfo> PropertiesNotYetOverriden
        {
            get
            {
                var propertiesThatNeedToBeOverriden = PropertiesThatNeedToBeOverriden.ToHashSet();
                
                foreach (var property in Properties)
                {
                    foreach (var baseType in BaseTypes)
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

                return propertiesThatNeedToBeOverriden;
            }
        }

        public IEnumerable<MethodInfo> MethodsThatNeedToBeOverriden
        {
            get
            {
                var methodsThatNeedToBeOverriden = new HashSet<MethodInfo>();

                foreach (var baseType in BaseTypes)
                {
                    var methodsThatNeedToBeOverridenForThisBaseType = baseType.GetMethods().Where(method =>
                        (method.IsAbstract || baseType.IsInterface) &&
                        !(method.Name.StartsWith("get_") || method.Name.StartsWith("set_")));
                    foreach (var method in methodsThatNeedToBeOverridenForThisBaseType)
                    {
                        methodsThatNeedToBeOverriden.Add(method);
                    }
                }

                return methodsThatNeedToBeOverriden;
            }
        }

        public IEnumerable<MethodInfo> MethodsNotYetOverriden
        {
            get
            {
                var methodsThatNeedToBeOverriden = MethodsThatNeedToBeOverriden.ToHashSet();
                
                foreach (var method in Methods)
                {
                    method.Name ??= method.Implementation.Item1?.StaticMethod?.Name;

                    if (method.Implementation.Item1 != null)
                    {
                        foreach (var baseType in BaseTypes)
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

                return methodsThatNeedToBeOverriden;
            }
        }
        
        private static Random _random = new Random();
        private static string RandomString(int length, string valid)
        {
            StringBuilder res = new StringBuilder();
            while (0 < length--)
            {
                res.Append(valid[_random.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}