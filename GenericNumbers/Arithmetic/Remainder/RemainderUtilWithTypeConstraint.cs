using System;

namespace GenericNumbers.Arithmetic.Remainder
{
    internal static class RemainderUtilWithTypeConstraint<T, TInput, TOutput>
        where T : IRemainder<TInput, TOutput>
    {
        static RemainderUtilWithTypeConstraint()
        {
            Remainder = (arg1, input) =>
            {
                TOutput output;
                arg1.Remainder(input, out output);
                return output;
            };
        }

        internal static Func<T, TInput, TOutput> Remainder { get; private set; }
    }
}