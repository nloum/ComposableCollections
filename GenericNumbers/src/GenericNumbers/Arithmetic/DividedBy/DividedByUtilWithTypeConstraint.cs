using System;

namespace GenericNumbers.Arithmetic.DividedBy
{
    internal static class DividedByUtilWithTypeConstraint<T, TInput, TOutput>
        where T : IDividedBy<TInput, TOutput>
    {
        static DividedByUtilWithTypeConstraint()
        {
            DividedBy = (arg1, input) =>
            {
                TOutput output;
                arg1.DividedBy(input, out output);
                return output;
            };
        }

        internal static Func<T, TInput, TOutput> DividedBy { get; private set; }
    }
}