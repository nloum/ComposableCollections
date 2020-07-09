using System;

namespace GenericNumbers.Relational.NotEqual
{
    internal static class NotEqualUtilWithTypeConstraint<T, TInput>
        where T : IComparable<TInput>
    {
        static NotEqualUtilWithTypeConstraint()
        {
            NotEqual = (arg1, input) => arg1.CompareTo(input) != 0;
        }

        internal static Func<T, TInput, bool> NotEqual { get; private set; }
    }
}