using System.Collections.Generic;

namespace GenericNumbers
{
    public interface INumberRangeIntersectionCount<out T>
    {
        IReadOnlyList<INumberRange<T>> RangesThatIncludeThis { get; }
        INumberRange<T> Range { get; }
    }
}
