using System;

namespace GenericNumbers.Arithmetic.Plus
{
    internal static class PlusUtilWithTypeConstraint<T, TInput, TOutput>
        where T : IPlus<TInput, TOutput>
    {
        static PlusUtilWithTypeConstraint()
        {
            Plus = (arg1, input) =>
            {
                TOutput output;
                arg1.Plus(input, out output);
                return output;
            };
        }

        internal static Func<T, TInput, TOutput> Plus { get; private set; }
    }
}