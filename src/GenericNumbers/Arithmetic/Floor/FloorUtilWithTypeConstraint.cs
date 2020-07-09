using System;

namespace GenericNumbers.Arithmetic.Floor
{
    internal static class FloorUtilWithTypeConstraint<T, TOutput>
        where T : IFloor<TOutput>
    {
        static FloorUtilWithTypeConstraint()
        {
            Floor = (arg1) =>
            {
                TOutput output;
                arg1.Floor(out output);
                return output;
            };
        }

        internal static Func<T, TOutput> Floor { get; private set; }
    }
}