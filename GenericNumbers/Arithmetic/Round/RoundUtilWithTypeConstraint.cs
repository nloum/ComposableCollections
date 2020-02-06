using System;

namespace GenericNumbers.Arithmetic.Round
{
    internal static class RoundUtilWithTypeConstraint<T, TOutput>
        where T : IRound<TOutput>
    {
        static RoundUtilWithTypeConstraint()
        {
            Round = (arg1) =>
            {
                TOutput output;
                arg1.Round(out output);
                return output;
            };
        }

        internal static Func<T, TOutput> Round { get; private set; }
    }
}