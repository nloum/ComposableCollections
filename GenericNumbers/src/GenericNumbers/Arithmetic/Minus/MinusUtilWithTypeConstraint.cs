using System;

namespace GenericNumbers.Arithmetic.Minus
{
    internal static class MinusUtilWithTypeConstraint<T, TInput, TOutput>
        where T : IMinus<TInput, TOutput>
    {
        static MinusUtilWithTypeConstraint()
        {
            Minus = (arg1, input) =>
            {
                TOutput output;
                arg1.Minus(input, out output);
                return output;
            };
        }

        internal static Func<T, TInput, TOutput> Minus { get; private set; }
    }
}