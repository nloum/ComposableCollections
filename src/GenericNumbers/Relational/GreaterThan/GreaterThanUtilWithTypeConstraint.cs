using System;

namespace GenericNumbers.Relational.GreaterThan
{
    internal static class GreaterThanUtilWithTypeConstraint<T, TInput>
        where T : IComparable<TInput>
    {
        static GreaterThanUtilWithTypeConstraint()
        {
            GreaterThan = (arg1, input) => arg1.CompareTo(input) > 0;
        }

        internal static Func<T, TInput, bool> GreaterThan { get; private set; }
    }
}