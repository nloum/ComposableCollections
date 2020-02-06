using System;

namespace GenericNumbers.Arithmetic.Sqrt
{
    internal static class SqrtUtilWithTypeConstraint<T, TOutput>
        where T : ISqrt<TOutput>
    {
        static SqrtUtilWithTypeConstraint()
        {
            Sqrt = (arg1) =>
            {
                TOutput output;
                arg1.Sqrt(out output);
                return output;
            };
        }

        internal static Func<T, TOutput> Sqrt { get; private set; }
    }
}