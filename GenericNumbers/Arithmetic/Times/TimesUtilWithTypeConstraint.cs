using System;

namespace GenericNumbers.Arithmetic.Times
{
    internal static class TimesUtilWithTypeConstraint<T, TInput, TOutput>
        where T : ITimes<TInput, TOutput>
    {
        static TimesUtilWithTypeConstraint()
        {
            Times = (arg1, input) =>
            {
                TOutput output;
                arg1.Times(input, out output);
                return output;
            };
        }

        internal static Func<T, TInput, TOutput> Times { get; private set; }
    }
}