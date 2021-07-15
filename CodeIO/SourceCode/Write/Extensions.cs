using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;

namespace CodeIO.SourceCode.Write
{
    public static class Extensions
    {
        public static bool IsConvertibleTo(this IType type, IType possibleBaseType)
        {
            if (type.Identifier.Equals(possibleBaseType.Identifier))
            {
                return true;
            }
        
            if (type is IClass clazz)
            {
                if (possibleBaseType is IInterface possibleBaseInterface)
                {
                    foreach (var baseIface in clazz.Interfaces)
                    {
                        if (baseIface.IsConvertibleTo(possibleBaseInterface))
                        {
                            return true;
                        }
                    }
                }
                else if (possibleBaseType is IClass possibleBaseClass && clazz.BaseClass != null)
                {
                    return clazz.BaseClass.IsConvertibleTo(possibleBaseClass);
                }
            }
            else if (type is IInterface iface)
            {
                if (possibleBaseType is IInterface possibleBaseInterface)
                {
                    foreach (var baseIface in iface.Interfaces)
                    {
                        if (baseIface.IsConvertibleTo(possibleBaseInterface))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    
        public static IEnumerable<IPartialType> Merge(this IEnumerable<IPartialType> partialTypes)
        {
            return partialTypes.GroupBy(x => x.Identifier)
                .Select(group =>
                {
                    var first = group.First();
                    if (first is PartialClass partialClass)
                    {
                        return (IPartialType)new PartialClass()
                        {
                            Identifier = first.Identifier,
                            Properties = group.SelectMany(x => x.Properties).ToImmutableList(),
                        };
                    }

                    if (first is PartialInterface partialInterface)
                    {
                        return new PartialInterface()
                        {
                            Identifier = first.Identifier,
                            Properties = group.SelectMany(x => x.Properties).ToImmutableList(),
                        };
                    }

                    throw new InvalidOperationException();
                });
        }

        public static ImmutableList<PropertySourceCodeWriter> RemoveDuplicates(
            this ImmutableList<PropertySourceCodeWriter> implementations)
        {
            return implementations.GroupBy(x => x.Name).Select(x => x.OrderByDescending(x =>
                {
                    if (x.Implementation is MethodToPropertyDelegateImplementation impl)
                    {
                        return new MethodSpecificity(impl.DelegatesTo);
                    }
                    
                    return null;
                }).First())
                .ToImmutableList();
        } 

        public static ImmutableList<PropertySourceCodeWriter> MakeDuplicatesExplicit(
            this ImmutableList<PropertySourceCodeWriter> implementations)
        {
            return implementations.GroupBy(x => x.Name).Select(x => x.First())
                .ToImmutableList();
        } 
    }
}