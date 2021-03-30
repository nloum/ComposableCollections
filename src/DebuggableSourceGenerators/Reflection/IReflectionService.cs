using System;
using System.Reflection;

namespace DebuggableSourceGenerators.Reflection
{
    public interface IReflectionService
    {
        IType AddType(TypeInfo type);
    }
}