using System.Collections.Generic;

namespace CodeIO
{
    public interface IEnum : IValueType
    {
        Primitive BaseType { get; }
        IReadOnlyList<EnumValue> Values { get; }
    }
}