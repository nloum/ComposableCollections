using System;

namespace CodeIO.LoadedTypes.Read
{
    public interface IReflectionType : IType
    {
        Type Type { get; }
    }
}