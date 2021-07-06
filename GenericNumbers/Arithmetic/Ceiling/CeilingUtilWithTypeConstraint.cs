using System;

namespace GenericNumbers.Arithmetic.Ceiling
{
    internal static class CeilingUtilWithTypeConstraint<T, TOutput>
        where T : ICeiling<TOutput>
    {
        static CeilingUtilWithTypeConstraint()
        {
            Ceiling = (arg1) =>
            {
                TOutput output;
                arg1.Ceiling(out output);
                return output;
            };
        }

        internal static Func<T, TOutput> Ceiling { get; private set; }
    }
}