using System;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;

namespace CodeIO.LoadedTypes.Write
{
    public class PropertyWriter
    {
        public string Name { get; init; }
        public Type Type { get; init; }
        internal FieldBuilder Field { get; set; }
        public PropertyBuilder Builder { get; set; }
        public PropertyInfo PropertyToOverride { get; set; }
        
        public void DefineField(TypeBuilder typeBuilder)
        {
            Field = typeBuilder.DefineField("_" + Name.Camelize(), Type,
                FieldAttributes.Private);
        }
        
        public void Write(ClassWriter classWriter, TypeBuilder typeBuilder)
        {
            // Define a property named Number that gets and sets the private
            // field.
            //
            // The last argument of DefineProperty is null, because the
            // property has no parameters. (If you don't specify null, you must
            // specify an array of Type objects. For a parameterless property,
            // use the built-in array with no elements: Type.EmptyTypes)
            Builder = typeBuilder.DefineProperty(
                Name,
                PropertyAttributes.HasDefault,
                Type,
                null);

            MethodInfo propertyGetterOverride = null;
            MethodInfo propertySetterOverride = null;

            foreach (var baseType in classWriter.BaseTypes)
            {
                var propertyToOverride = baseType.GetProperty(Name);
                if (propertyToOverride == null)
                {
                    continue;
                }

                if (propertyToOverride.PropertyType == Type)
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
                "get_" + Name,
                propertyGetterOverride == null ? getSetAttr : getSetAttr | MethodAttributes.Virtual,
                Type,
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
            propertyGetterIl.Emit(OpCodes.Ldfld, Field);
            propertyGetterIl.Emit(OpCodes.Ret);

            // Define the "set" accessor method for Number, which has no return
            // type and takes one argument of type int (Int32).
            MethodBuilder propertySetter = typeBuilder.DefineMethod(
                "set_" + Name,
                propertySetterOverride == null ? getSetAttr : getSetAttr | MethodAttributes.Virtual,
                null,
                new Type[] {Type});
            if (propertySetterOverride != null)
            {
                typeBuilder.DefineMethodOverride(propertySetter, propertySetterOverride);
            }

            ILGenerator propertySetterIl = propertySetter.GetILGenerator();
            // Load the instance and then the numeric argument, then store the
            // argument in the field.
            propertySetterIl.Emit(OpCodes.Ldarg_0);
            propertySetterIl.Emit(OpCodes.Ldarg_1);
            propertySetterIl.Emit(OpCodes.Stfld, Field);
            propertySetterIl.Emit(OpCodes.Ret);

            // Last, map the "get" and "set" accessor methods to the
            // PropertyBuilder. The property is now complete.
            Builder.SetGetMethod(propertyGetter);
            Builder.SetSetMethod(propertySetter);
        }
    }
}