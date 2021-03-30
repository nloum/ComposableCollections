using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Text;
using DebuggableSourceGenerators.PrimitiveTypes;
using Microsoft.VisualBasic.CompilerServices;
using ByteType = DebuggableSourceGenerators.PrimitiveTypes.ByteType;
using CharType = DebuggableSourceGenerators.PrimitiveTypes.CharType;
using DoubleType = DebuggableSourceGenerators.PrimitiveTypes.DoubleType;

namespace DebuggableSourceGenerators.NonLoadedAssembly
{
    // /// <summary>
    // /// Test implementation of ISignatureTypeProvider<TType, TGenericContext> that uses strings in ilasm syntax as TType.
    // /// A real provider in any sort of perf constraints would not want to allocate strings freely like this, but it keeps test code simple.
    // /// </summary>
    // internal sealed class SignatureVisualizer : ISignatureTypeProvider<IType, object>
    // {
    //     private readonly MetadataVisualizer _visualizer;
    //
    //     public SignatureVisualizer(MetadataVisualizer visualizer)
    //     {
    //         _visualizer = visualizer;
    //     }
    //
    //     public IType GetPrimitiveType(PrimitiveTypeCode typeCode)
    //     {
    //         switch (typeCode)
    //         {
    //             case PrimitiveTypeCode.Boolean: return new BoolType();
    //             case PrimitiveTypeCode.Byte: return new ByteType();
    //             case PrimitiveTypeCode.Char: return new CharType();
    //             case PrimitiveTypeCode.Double: return new DoubleType();
    //             case PrimitiveTypeCode.Int16: return new Integer16Type();
    //             case PrimitiveTypeCode.Int32: return new Integer32Type();
    //             case PrimitiveTypeCode.Int64: return new Integer64Type();
    //             case PrimitiveTypeCode.IntPtr: return new NativeIntegerType();
    //             case PrimitiveTypeCode.Object: return new ObjectType();
    //             case PrimitiveTypeCode.SByte: return new SignedByteType();
    //             case PrimitiveTypeCode.Single: return new FloatType();
    //             case PrimitiveTypeCode.String: return new StringType();
    //             case PrimitiveTypeCode.TypedReference: return new TypedReferenceType();
    //             case PrimitiveTypeCode.UInt16: return new UnsignedInteger16Type();
    //             case PrimitiveTypeCode.UInt32: return new UnsignedInteger32Type();
    //             case PrimitiveTypeCode.UInt64: return new UnsignedInteger64Type();
    //             case PrimitiveTypeCode.UIntPtr: return new NativeUnsignedIntegerType();
    //             case PrimitiveTypeCode.Void: return new VoidType();
    //             default: throw new ArgumentException(nameof(typeCode));
    //         }
    //     }
    //
    //     private string RowId(EntityHandle handle) => _visualizer.RowId(handle);
    //
    //     public string GetTypeFromDefinition(MetadataReader reader, TypeDefinitionHandle handle, byte rawTypeKind = 0) =>
    //         $"typedef{RowId(handle)}";
    //
    //     public string GetTypeFromReference(MetadataReader reader, TypeReferenceHandle handle, byte rawTypeKind = 0) =>
    //         $"typeref{RowId(handle)}";
    //
    //     public string GetTypeFromSpecification(MetadataReader reader, object genericContext, TypeSpecificationHandle handle, byte rawTypeKind = 0) =>
    //         $"typespec{RowId(handle)}";
    //
    //     public string GetSZArrayType(string elementType) =>
    //         elementType + "[]";
    //
    //     public string GetPointerType(string elementType)
    //         => elementType + "*";
    //
    //     public string GetByReferenceType(string elementType)
    //         => elementType + "&";
    //
    //     public string GetGenericMethodParameter(object genericContext, int index)
    //         => "!!" + index;
    //
    //     public string GetGenericTypeParameter(object genericContext, int index)
    //         => "!" + index;
    //
    //     public string GetPinnedType(string elementType)
    //         => elementType + " pinned";
    //
    //     public string GetGenericInstantiation(string genericType, ImmutableArray<string> typeArguments)
    //         => genericType + "<" + string.Join(",", typeArguments) + ">";
    //
    //     public string GetModifiedType(string modifierType, string unmodifiedType, bool isRequired) =>
    //         unmodifiedType + (isRequired ? " modreq(" : " modopt(") + modifierType + ")";
    //
    //     public string GetArrayType(string elementType, ArrayShape shape)
    //     {
    //         var builder = new StringBuilder();
    //
    //         builder.Append(elementType);
    //         builder.Append('[');
    //
    //         for (int i = 0; i < shape.Rank; i++)
    //         {
    //             int lowerBound = 0;
    //
    //             if (i < shape.LowerBounds.Length)
    //             {
    //                 lowerBound = shape.LowerBounds[i];
    //                 builder.Append(lowerBound);
    //             }
    //
    //             builder.Append("...");
    //
    //             if (i < shape.Sizes.Length)
    //             {
    //                 builder.Append(lowerBound + shape.Sizes[i] - 1);
    //             }
    //
    //             if (i < shape.Rank - 1)
    //             {
    //                 builder.Append(',');
    //             }
    //         }
    //
    //         builder.Append(']');
    //         return builder.ToString();
    //     }
    //
    //     public string GetFunctionPointerType(MethodSignature<string> signature)
    //         => $"methodptr({signature})";
    // }
}