using System;

namespace GenericNumbers.Relational.GreaterThanOrEqual
{
    internal static class GreaterThanOrEqualUtilWithTypeConstraint<T, TInput>
        where T : IComparable<TInput>
    {
        static GreaterThanOrEqualUtilWithTypeConstraint()
        {
            GreaterThanOrEqual = (arg1, input) => arg1.CompareTo(input) >= 0;
        }

        internal static Func<T, TInput, bool> GreaterThanOrEqual { get; private set; }
    }
}