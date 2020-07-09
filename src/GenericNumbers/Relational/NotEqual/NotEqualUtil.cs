using System;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericNumbers.Relational.NotEqual
{
    internal static class NotEqualUtil<T, TInput>
    {
        static NotEqualUtil()
        {
            TryInit();

            UsedOperators = true;

            if (NotEqual == null)
            {
                if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IComparable<TInput>).GetTypeInfo()))
                    NotEqual = (arg1, input) => ((IComparable<TInput>)arg1).CompareTo(input) != 0;
                else if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IComparable).GetTypeInfo()))
                    NotEqual = (arg1, input) => ((IComparable)arg1).CompareTo(input) != 0;
                else if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IEquatable<TInput>).GetTypeInfo()))
                    NotEqual = (arg1, input) => !((IEquatable<TInput>)arg1).Equals(input);
                else
                {
                    NotEqual = (arg1, input) =>
                    {
                        if (arg1 is IComparable<TInput>)
                            return ((IComparable<TInput>)arg1).CompareTo(input) != 0;
                        if (arg1 is IComparable)
                            return ((IComparable)arg1).CompareTo(input) != 0;
                        if (arg1 is IEquatable<TInput>)
                            return !((IEquatable<TInput>)arg1).Equals(input);
                        throw new ArgumentException($"An attempt was made to determine whether a {NumbersUtility.ConvertToCSharpTypeName(typeof(T))} was not equal to a {NumbersUtility.ConvertToCSharpTypeName(typeof(TInput))} but {NumbersUtility.ConvertToCSharpTypeName(typeof(T))} is not an IComparable<{NumbersUtility.ConvertToCSharpTypeName(typeof(TInput))}>, an IComparable, or an IEquatable<{NumbersUtility.ConvertToCSharpTypeName(typeof(TInput))}>");
                    };
                }

                UsedOperators = false;
            }
        }

        // ReSharper disable once StaticFieldInGenericType
        private readonly static bool UsedOperators;

        private static void TryInit()
        {
            NotEqual = TryDirectTypeCombination();
            //NotEqual = TryTypeCombination<T, T, T>();
            if (NotEqual != null)
                return;
            NotEqual = TryTypeCombination<T, T>();
            if (NotEqual != null)
                return;
            NotEqual = TryTypeCombination<TInput, T>();
            if (NotEqual != null)
                return;
            NotEqual = TryTypeCombination<TInput, TInput>();
            if (NotEqual != null)
                return;
        }

        public static Func<T, TInput, bool> TryDirectTypeCombination()
        {
            try
            {
                var var1 = Expression.Variable(typeof(T));
                var var2 = Expression.Variable(typeof(TInput));
                Expression expression = Expression.Multiply(var1, var2);
                var expr = Expression.Lambda(expression, var1, var2);
                return (Func<T, TInput, bool>)expr.Compile();
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static Func<T, TInput, bool> TryTypeCombination<T1, T2>()
        {
            try
            {
                if (NotEqualUtil<T1, T2>.UsedOperators)
                {
                    var var1 = Expression.Variable(typeof(T));
                    var var2 = Expression.Variable(typeof(TInput));
                    var expr = Expression.Lambda<Func<T, TInput, bool>>(Expression.Convert(Expression.Call(typeof(NotEqualUtil<T1, T2>), "NotEqualMethod", new Type[0], Expression.Convert(var1, typeof(T1)), Expression.Convert(var2, typeof(T2))), typeof(bool)), var1, var2);
                    return expr.Compile();
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static bool NotEqualMethod(T t, TInput input)
        {
            return NotEqual(t, input);
        }

        internal static Func<T, TInput, bool> NotEqual { get; set; }
    }
}
