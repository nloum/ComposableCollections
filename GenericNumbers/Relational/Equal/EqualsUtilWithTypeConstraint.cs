using System;

namespace GenericNumbers.Relational.Equal
{
    internal static class EqualUtilWithTypeConstraint<T, TInput>
        where T : IComparable<TInput>
    {
        static EqualUtilWithTypeConstraint()
        {
            Equal = (arg1, input) => arg1.CompareTo(input) == 0;
        }

        internal static Func<T, TInput, bool> Equal { get; private set; }
    }
}