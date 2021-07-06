using System;

namespace GenericNumbers.Relational.LessThan
{
    internal static class LessThanUtilWithTypeConstraint<T, TInput>
        where T : IComparable<TInput>
    {
        static LessThanUtilWithTypeConstraint()
        {
            LessThan = (arg1, input) => arg1.CompareTo(input) < 0;
        }

        internal static Func<T, TInput, bool> LessThan { get; private set; }
    }
}