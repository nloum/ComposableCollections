using System;

namespace GenericNumbers.Arithmetic.SpecialNumbers
{
    internal static class SpecialNumbersUtilWithTypeConstraint<T>
        where T : ISpecialNumbers
    {
        static SpecialNumbersUtilWithTypeConstraint()
        {
            IsNegativeInfinite = (arg1) =>
            {
                bool output;
                arg1.IsNegativeInfinite(out output);
                return output;
            };
            IsPositiveInfinite = (arg1) =>
            {
                bool output;
                arg1.IsPositiveInfinite(out output);
                return output;
            };
            IsNumber = (arg1) =>
            {
                bool output;
                arg1.IsNumber(out output);
                return output;
            };
        }

        internal static Func<T, bool> IsNegativeInfinite { get; private set; }
        internal static Func<T, bool> IsPositiveInfinite { get; private set; }
        internal static Func<T, bool> IsNumber { get; private set; }
    }
}