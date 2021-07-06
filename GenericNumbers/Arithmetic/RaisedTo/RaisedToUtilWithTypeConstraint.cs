using System;

namespace GenericNumbers.Arithmetic.RaisedTo
{
    internal static class RaisedToUtilWithTypeConstraint<T, TInput, TOutput>
        where T : IRaisedTo<TInput, TOutput>
    {
        static RaisedToUtilWithTypeConstraint()
        {
            RaisedTo = (arg1, input) =>
            {
                TOutput output;
                arg1.RaisedTo(input, out output);
                return output;
            };
        }

        internal static Func<T, TInput, TOutput> RaisedTo { get; private set; }
    }
}