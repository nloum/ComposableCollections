using System;

namespace GenericNumbers.ConvertTo
{
    internal static class ConvertToUtilWithTypeConstraint<T, TOutput>
        where T : IConvertTo<TOutput>
    {
        static ConvertToUtilWithTypeConstraint()
        {
            ConvertTo = (arg1) =>
            {
                TOutput output;
                arg1.ConvertTo(out output);
                return output;
            };
        }

        internal static Func<T, TOutput> ConvertTo { get; private set; }
    }
}