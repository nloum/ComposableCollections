using System;

namespace GenericNumbers.Arithmetic.Abs
{
    internal static class AbsUtilWithTypeConstraint<T, TOutput>
        where T : IAbs<TOutput>
    {
        static AbsUtilWithTypeConstraint()
        {
            Abs = (arg1) =>
            {
                TOutput output;
                arg1.Abs(out output);
                return output;
            };
        }

        internal static Func<T, TOutput> Abs { get; private set; }
    }
}