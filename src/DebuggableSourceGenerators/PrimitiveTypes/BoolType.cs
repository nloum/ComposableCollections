namespace DebuggableSourceGenerators.PrimitiveTypes
{
    public class BoolType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("bool", 0);
    }

    public class ByteType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("byte", 0);
    }
    
    public class CharType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("char", 0);
    }
    
    public class DoubleType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("double", 0);
    }
    
    public class Integer16Type : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("short", 0);
    }
    
    
    public class Integer32Type : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("int", 0);
    }
    
    
    public class Integer64Type : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("long", 0);
    }
    
    public class NativeIntegerType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("int*", 0);
    }
    
    public class ObjectType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("object", 0);
    }
    
    public class SignedByteType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("sbyte", 0);
    }
    
    public class FloatType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("float", 0);
    }
    
    public class StringType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("string", 0);
    }
    
    public class TypedReferenceType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("typedref", 0);
    }
    
    public class UnsignedInteger32Type : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("uint", 0);
    }
    
    public class UnsignedInteger16Type : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("ushort", 0);
    }
    
    public class UnsignedInteger64Type : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("ulong", 0);
    }
    
    public class NativeUnsignedIntegerType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("native_uint", 0);
    }
    
    public class VoidType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("void", 0);
    }
}