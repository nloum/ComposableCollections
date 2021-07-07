using System;
using System.Collections.Generic;
using System.Reflection;
using ComposableCollections;
using ComposableCollections.Dictionary.Interfaces;

namespace CodeIO.LoadedTypes.Read
{
    public class ReflectionUnboundGenericInterface : ReflectionComplexTypeBase, IUnboundGenericInterface
    {
        public IReadOnlyList<IGenericParameter> Parameters { get; }
        
        public ReflectionUnboundGenericInterface(Type type, IComposableReadOnlyDictionary<Type, Lazy<IType>> typeFormat) : base(type, typeFormat)
        {
            Parameters = type.GetGenericArguments()
                .Select(p =>
                {
                    var attr = p.GenericParameterAttributes;
                    return new GenericParameter()
                    {
                        Identifier = p.GetTypeIdentifier(),
                        MustExtend = p.GetGenericParameterConstraints().Select(constraint => typeFormat[constraint])
                            .Select(x => x.Value),
                        IsContravariant = p.GenericParameterAttributes.HasFlag(GenericParameterAttributes.Contravariant),
                        IsCovariant = p.GenericParameterAttributes.HasFlag(GenericParameterAttributes.Covariant),
                        MustBeReferenceType = p.GenericParameterAttributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint),
                        MustHaveDefaultConstructor = p.GenericParameterAttributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint),
                        MustBeNonNullable = p.GenericParameterAttributes.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint),
                    };
                });

        }
    }
}