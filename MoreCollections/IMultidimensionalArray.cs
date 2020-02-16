using System;
using System.Collections.Generic;

namespace MoreCollections
{
    public interface IMultidimensionalArray<out T> : IEnumerable<T>
    {
        T this[params int[] indices] { get; }
        int[] Dimensions { get; }

        IReadOnlyList<T> Elements(params int[] indicesRange);
        IReadOnlyList<int[]> Indices(params int[] indicesRange);
    }
}
