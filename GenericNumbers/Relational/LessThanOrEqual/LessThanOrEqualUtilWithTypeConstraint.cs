using System;

namespace GenericNumbers.Relational.LessThanOrEqual
{
    internal static class LessThanOrEqualUtilWithTypeConstraint<T, TInput>
        where T : IComparable<TInput>
    {
        static LessThanOrEqualUtilWithTypeConstraint()
        {
            LessThanOrEqual = (arg1, input) => arg1.CompareTo(input) <= 0;
        }

        internal static Func<T, TInput, bool> LessThanOrEqual { get; private set; }
    }
}