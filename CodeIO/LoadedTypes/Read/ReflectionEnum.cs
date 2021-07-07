using System;
using System.Collections.Generic;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionEnum : IEnum, IReflectionType
    {
        public ReflectionEnum(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> composableReadOnlyDictionary)
        {
            Identifier = type.GetTypeIdentifier();
            Visibility = type.GetTypeVisibility();
            BaseType = null;
            var fields = type.GetFields();
            Type = type;
            BaseType = new ReflectionPrimitive(fields[0].FieldType);
            Values = fields.Skip(1).Select(x => new EnumValue
            {
                Name = x.Name,
                Value = Convert.ChangeType(x.GetValue(null), Enum.GetUnderlyingType(type)),
            });
        }

        public Type Type { get; }
        public TypeIdentifier Identifier { get; }
        public Visibility Visibility { get; }
        public Primitive BaseType { get; }
        public IReadOnlyList<EnumValue> Values { get; }
    }
}