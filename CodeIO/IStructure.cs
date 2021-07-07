using System.Collections.Generic;

namespace CodeIO
{
    public interface IStructure : IValueType
    {
        IReadOnlyList<IField> Fields { get; }
    }
}