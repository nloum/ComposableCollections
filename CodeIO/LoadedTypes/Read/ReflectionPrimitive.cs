using System;

namespace CodeIO.LoadedTypes.Read
{
    public record ReflectionPrimitive : Primitive, IReflectionType
    {
        public ReflectionPrimitive(Type type)
        {
            Type = type;
            Identifier = type.GetTypeIdentifier();
            PrimitiveType = type.Name switch
            {
                "Int32" => PrimitiveType.Int,
                "UInt32" => PrimitiveType.UInt,
                "Int16" => PrimitiveType.Short,
                "UInt16" => PrimitiveType.UShort,
                "Int64" => PrimitiveType.Long,
                "UInt64" => PrimitiveType.ULong,
                "Double" => PrimitiveType.Double,
                "Float" => PrimitiveType.Float,
                "Decimal" => PrimitiveType.Decimal,
                "Boolean" => PrimitiveType.Bool,
                "Byte" => PrimitiveType.Byte,
                "SByte" => PrimitiveType.SByte,
                "Char" => PrimitiveType.Char,
                "IntPtr" => PrimitiveType.NInt,
                "UIntPtr" => PrimitiveType.NUint,
            };
        }

        public Type Type { get; }
    }
}